﻿using System.Data;
using System.Linq;
using System.Reflection;

namespace SimpleAdmin.Core;

/// <summary>
///  SqlSugar设置启动
/// </summary>
public static class SqlsugarSetup
{
    /// <summary>
    /// 注入Sqlsugar
    /// </summary>
    /// <param name="services"></param>
    public static void AddSqlSugar(this IServiceCollection services)
    {

        //services.AddSingleton<ISqlSugarClient>(DbContext.Db); // 单例注册,不用工作单元不需要注入
        //services.AddUnitOfWork<SqlSugarUnitOfWork>(); // 事务与工作单元注册

        //遍历配置
        DbContext.DbConfigs.ForEach(it =>
        {
            //是否需要初始化数据库
            if (it.IsInitDb)
            {
                InitDb(it);//初始化数据库表结构
            }
            //是否需要更新种子数据
            if (it.IsSeedData)
            {
                InitSeedData(it);//初始化种子数据
            }
        });
    }

    /// <summary>
    /// 初始化数据库表结构
    /// </summary>
    /// <param name="config">数据库配置</param>
    private static void InitDb(SqlSugarConfig config)
    {
        DbContext.Db.DbMaintenance.CreateDatabase();//创建数据库
        // 获取所有实体表-初始化表结构
        var entityTypes = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false));
        if (!entityTypes.Any()) return;//没有就退出
        var db = DbContext.Db.GetConnectionScope(config.ConfigId);//获取数据库对象
        foreach (var entityType in entityTypes)
        {
            var tenantAtt = entityType.GetCustomAttribute<TenantAttribute>();//获取Sqlsugar多租户特性

            if (tenantAtt != null && tenantAtt.configId.ToString() != config.ConfigId) continue;//如果特性存在并且租户ID是当前数据库ID
            var splitTable = entityType.GetCustomAttribute<SplitTableAttribute>();//获取自动分表特性
            if (splitTable == null)//如果特性是空
                db.CodeFirst.InitTables(entityType);//普通创建
            else
                db.CodeFirst.SplitTables().InitTables(entityType);//自动分表创建
        }
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="config"></param>
    private static void InitSeedData(SqlSugarConfig config)
    {
        var db = DbContext.Db.GetConnectionScope(config.ConfigId);//获取数据库对象
        // 获取所有种子配置-初始化数据
        var seedDataTypes = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
            && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(ISqlSugarEntitySeedData<>))));
        if (!seedDataTypes.Any()) return;
        foreach (var seedType in seedDataTypes)//遍历种子类
        {
            //使用与指定参数匹配程度最高的构造函数来创建指定类型的实例。
            var instance = Activator.CreateInstance(seedType);
            //获取SeedData方法
            var hasDataMethod = seedType.GetMethod("SeedData");
            //判断是否有种子数据
            var seedData = ((IEnumerable)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
            if (seedData == null) continue;//没有种子数据就下一个
            var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();//获取实体类型
            var tenantAtt = entityType.GetCustomAttribute<TenantAttribute>();//获取sqlsugar租户特性
            if (tenantAtt != null && tenantAtt.configId.ToString() != config.ConfigId) continue;//如果不是当前租户的就下一个
            var seedDataTable = seedData.ToList().ToDataTable();//获取种子数据
            seedDataTable.TableName = db.EntityMaintenance.GetEntityInfo(entityType).DbTableName;//获取表名
            if (config.IsUnderLine) // 驼峰转下划线
            {
                foreach (DataColumn col in seedDataTable.Columns)
                {
                    col.ColumnName = UtilMethods.ToUnderLine(col.ColumnName);
                }
            }
            var ignoreAdd = hasDataMethod.GetCustomAttribute<IgnoreSeedDataAddAttribute>();//读取忽略插入特性
            var ignoreUpdate = hasDataMethod.GetCustomAttribute<IgnoreSeedDataUpdateAttribute>();//读取忽略更新特性
            if (seedDataTable.Columns.Contains(SqlsugarConst.DB_PrimaryKey))//判断种子数据是否有主键
            {
                //根据判断主键插入或更新
                var storage = db.Storageable(seedDataTable).WhereColumns(SqlsugarConst.DB_PrimaryKey).ToStorage();
                if (ignoreAdd == null) storage.AsInsertable.ExecuteCommand();//执行插入
                if (ignoreUpdate == null) storage.AsUpdateable.ExecuteCommand();//只有没有忽略更新的特性才执行更新
            }
            else // 没有主键或者不是预定义的主键(有重复的可能)
            {

                //全量插入
                var storage = db.Storageable(seedDataTable).ToStorage();
                if (ignoreAdd == null) storage.AsInsertable.ExecuteCommand();
            }
        }
    }
}

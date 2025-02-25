﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin.System;

/// <summary>
/// 代码生成基础服务
/// </summary>
public interface IGenbasicService : ITransient
{
    /// <summary>
    /// 添加代码生成器基础
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<GenBasic> Add(GenBasicAddInput input);

    /// <summary>
    /// 删除代码生成配置
    /// </summary>
    /// <param name="input">ID列表</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);

    /// <summary>
    /// 编辑代码生成基础配置
    /// </summary>
    /// <param name="input"></param>
    /// <returns>代码生产基础</returns>
    Task<GenBasic> Edit(GenBasicEditInput input);

    /// <summary>
    /// 执行代码生成:本地
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ExecGenPro(BaseIdInput input);

    /// <summary>
    /// 执行代码生成:压缩包
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<FileStreamResult> ExecGenZip(BaseIdInput input);

    /// <summary>
    /// 获取项目所有程序集
    /// </summary>
    /// <returns>程序集列表</returns>
    List<string> GetAssemblies();

    /// <summary>
    /// 获取表内所有字段信息
    /// </summary>
    /// <param name="input">表信息</param>
    /// <returns>字段信息列表</returns>
    List<GenBasicColumnOutput> GetTableColumns(GenBasicColumnInput input);

    /// <summary>
    /// 获取所有表信息
    /// </summary>
    /// <returns>所有表信息</returns>
    List<GenBasicTableOutput> GetTables();

    /// <summary>
    /// 获取代码生成基础分页
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns代码生成基础分页列表</returns>
    Task<SqlSugarPagedList<GenBasic>> Page(BasePageInput input);

    /// <summary>
    /// 代码生成预览
    /// </summary>
    /// <param name="input">代码生成基础Id</param>
    /// <returns>预览结果</returns>
    Task<GenBasePreviewOutput> PreviewGen(BaseIdInput input);
}

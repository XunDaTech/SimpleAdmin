﻿

global using Microsoft.Extensions.Logging;

namespace SimpleAdmin.Core;

/// <summary>
/// 日志写入文件的组件
/// </summary>
public sealed class LoggingFileComponent : IServiceComponent
{
    /// <summary>
    /// 是否写入LoggingMonitor日志
    /// </summary>
    private readonly bool WriteMonitor = App.GetConfig<bool>("Logging:WriteMonitor");
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {

        //获取默认日志等级
        var defaultLevel = App.GetConfig<LogLevel?>("Logging:LogLevel:Default");
        //获取最大日志等级，默认Error
        var maxLevel = App.GetConfig<LogLevel?>("Logging:LogLevel:Max") ?? LogLevel.Error;
        //获取程序根目录
        var rootPath = App.HostEnvironment.ContentRootPath;
        if (defaultLevel != null)//如果默认日志等级不是空
        {
            //遍历日志等级
            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                //如果日志等级是默认等级和最大等级之间
                if (level >= defaultLevel && level != LogLevel.None && level <= maxLevel)
                {
                    //每天创建一个日志文件
                    services.AddLogging(builder =>
                   {
                       var fileName = "logs/" + level.ToString() + "/{0:yyyy}-{0:MM}-{0:dd}.log";
                       builder.AddFile(fileName, options =>
                       {
                           SetLogOptions(options, level);//日志格式化
                       });
                   });
                }
            }
        }
        else
        {
            //添加日志文件
            services.AddFileLogging("logs/{0:yyyy}-{0:MM}-{0:dd}.log", options =>
            {
                SetLogOptions(options, null);//日志格式化
            });
        }


    }

    /// <summary>
    /// 日志格式化
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logLevel"></param>
    private void SetLogOptions(FileLoggerOptions options, LogLevel? logLevel)
    {
        //每天创建一个日志文件
        var rootPath = App.HostEnvironment.ContentRootPath;
        if (logLevel != null)//如果日志等级不为空
        {
            //过滤日志等级
            options.WriteFilter = (logMsg) =>
            {
                //如果配置不写入mongitor日志和日志名称为System.Logging.LoggingMonitor
                if (!WriteMonitor && logMsg.LogName == "System.Logging.LoggingMonitor")
                    return false;
                return logMsg.LogLevel == logLevel;
            };
        }
        //定义日志文件名
        options.FileNameRule = fileName =>
        {
            return rootPath + "\\" + string.Format(fileName, DateTime.UtcNow);
        };
        options.FileSizeLimitBytes = 500000 * 1024;//日志最大500M
                                                   //日志内容格式化
        options.MessageFormat = (logMsg) =>
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("【日志级别】：" + logMsg.LogLevel);
                stringBuilder.AppendLine("【日志类名】：" + logMsg.LogName);
                stringBuilder.AppendLine("【日志时间】：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                stringBuilder.AppendLine("【日志内容】：" + logMsg.Message);
                if (logMsg.Exception != null)
                {
                    stringBuilder.AppendLine("【异常信息】：" + logMsg.Exception);
                }
                return stringBuilder.ToString();
            };
    }
}

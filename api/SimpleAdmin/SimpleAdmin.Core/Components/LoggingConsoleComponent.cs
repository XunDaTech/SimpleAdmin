﻿using Microsoft.Extensions.Logging;
using System.IO;

namespace SimpleAdmin.Core;

/// <summary>
/// 日志写入文件的组件
/// </summary>
public sealed class LoggingConsoleComponent : IServiceComponent
{
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {
        services.AddConsoleFormatter(options =>
         {


             options.MessageFormat = (logMsg) =>
             {
                 //如果不是LoggingMonitor日志才格式化
                 if (logMsg.LogName != "System.Logging.LoggingMonitor")
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
                 }
                 else
                 {
                     return logMsg.Message;
                 }
             };
             options.WriteHandler = (logMsg, scopeProvider, writer, fmtMsg, opt) =>
             {
                 ConsoleColor consoleColor = ConsoleColor.White;
                 switch (logMsg.LogLevel)
                 {
                     case LogLevel.Information:
                         consoleColor = ConsoleColor.DarkGreen;
                         break;
                     case LogLevel.Warning:
                         consoleColor = ConsoleColor.DarkYellow;
                         break;
                     case LogLevel.Error:
                         consoleColor = ConsoleColor.DarkRed;
                         break;
                 }
                 writer.WriteWithColor(fmtMsg, ConsoleColor.Black, consoleColor);
             };
         });


    }
}



public static class TextWriterExtensions
{
    const string DefaultForegroundColor = "\x1B[39m\x1B[22m";
    const string DefaultBackgroundColor = "\x1B[49m";

    public static void WriteWithColor(
        this TextWriter textWriter,
        string message,
        ConsoleColor? background,
        ConsoleColor? foreground)
    {
        // Order:
        //   1. background color
        //   2. foreground color
        //   3. message
        //   4. reset foreground color
        //   5. reset background color

        var backgroundColor = background.HasValue ? GetBackgroundColorEscapeCode(background.Value) : null;
        var foregroundColor = foreground.HasValue ? GetForegroundColorEscapeCode(foreground.Value) : null;

        if (backgroundColor != null)
        {
            textWriter.Write(backgroundColor);
        }
        if (foregroundColor != null)
        {
            textWriter.Write(foregroundColor);
        }

        textWriter.WriteLine(message);

        if (foregroundColor != null)
        {
            textWriter.Write(DefaultForegroundColor);
        }
        if (backgroundColor != null)
        {
            textWriter.Write(DefaultBackgroundColor);
        }
    }

    static string GetForegroundColorEscapeCode(ConsoleColor color) =>
        color switch
        {
            ConsoleColor.Black => "\x1B[30m",
            ConsoleColor.DarkRed => "\x1B[31m",
            ConsoleColor.DarkGreen => "\x1B[32m",
            ConsoleColor.DarkYellow => "\x1B[33m",
            ConsoleColor.DarkBlue => "\x1B[34m",
            ConsoleColor.DarkMagenta => "\x1B[35m",
            ConsoleColor.DarkCyan => "\x1B[36m",
            ConsoleColor.Gray => "\x1B[37m",
            ConsoleColor.Red => "\x1B[1m\x1B[31m",
            ConsoleColor.Green => "\x1B[1m\x1B[32m",
            ConsoleColor.Yellow => "\x1B[1m\x1B[33m",
            ConsoleColor.Blue => "\x1B[1m\x1B[34m",
            ConsoleColor.Magenta => "\x1B[1m\x1B[35m",
            ConsoleColor.Cyan => "\x1B[1m\x1B[36m",
            ConsoleColor.White => "\x1B[1m\x1B[37m",

            _ => DefaultForegroundColor
        };

    static string GetBackgroundColorEscapeCode(ConsoleColor color) =>
        color switch
        {
            ConsoleColor.Black => "\x1B[40m",
            ConsoleColor.DarkRed => "\x1B[41m",
            ConsoleColor.DarkGreen => "\x1B[42m",
            ConsoleColor.DarkYellow => "\x1B[43m",
            ConsoleColor.DarkBlue => "\x1B[44m",
            ConsoleColor.DarkMagenta => "\x1B[45m",
            ConsoleColor.DarkCyan => "\x1B[46m",
            ConsoleColor.Gray => "\x1B[47m",

            _ => DefaultBackgroundColor
        };
}

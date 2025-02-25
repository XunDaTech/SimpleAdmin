﻿using Furion.ConfigurableOptions;

namespace SimpleAdmin.Core;

/// <summary>
/// 系统配置选项
/// </summary>
public class WebSettingsOptions : IConfigurableOptions
{

    /// <summary>
    /// 是否演示环境
    /// </summary>
    public bool EnvPoc { get; set; } = false;

    /// <summary>
    /// 是否清除Redis缓存
    /// </summary>
    public bool ClearRedis { get; set; } = false;

    /// <summary>
    /// 是否使用MQTT
    /// </summary>
    public bool UseMqtt { get; set; } = false;
}

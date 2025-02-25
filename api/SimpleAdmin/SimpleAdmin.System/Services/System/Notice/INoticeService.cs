﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin.System;


/// <summary>
/// 通知服务
/// </summary>
public interface INoticeService : ITransient
{

    /// <summary>
    /// 通知用户下线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="tokenInfos">token列表</param>
    /// <param name="message">消息内容</param>
    /// <returns></returns>
    Task LoginOut(string userId, List<TokenInfo> tokenInfos, string message);
}

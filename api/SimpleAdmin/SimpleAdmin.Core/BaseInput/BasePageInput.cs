﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin.Core;

/// <summary>
/// 全局分页查询输入参数
/// </summary>
public class BasePageInput : IValidatableObject
{
    /// <summary>
    /// 当前页码
    /// </summary>
    [DataValidation(ValidationTypes.Numeric)]
    public virtual int Current { get; set; } = 1;

    /// <summary>
    /// 每页条数
    /// </summary>
    [Range(1, 100, ErrorMessage = "页码容量超过最大限制")]
    [DataValidation(ValidationTypes.Numeric)]
    public virtual int Size { get; set; } = 10;

    /// <summary>
    /// 排序字段
    /// </summary>
    public virtual string SortField { get; set; }

    /// <summary>
    /// 排序方式，升序：ascend；降序：descend"
    /// </summary>
    public virtual string SortOrder { get; set; } = "desc";


    /// <summary>
    /// 关键字
    /// </summary>
    public virtual string SearchKey { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        //配合小诺排序参数
        if (SortOrder == "descend")
        {
            SortOrder = "desc";
        }
        else if (SortOrder == "ascend")
        {
            SortOrder = "asc";
        }
        yield break;
    }
}

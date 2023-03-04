﻿using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;

namespace SimpleAdmin.Core
{
    /// <summary>
    /// 导入基础输入
    /// </summary>
    [ExcelImporter(IsLabelingError = true)]
    public class BaseImportTemplateInput
    {

        /// <summary>
        /// Id
        /// </summary>
        [ImporterHeader(IsIgnore = true)]
        public long Id { get; set; } = YitIdHelper.NextId();

        /// <summary>
        /// 是否有错误
        /// </summary>
        [ImporterHeader(IsIgnore = true)]
        public bool HasError { get; set; } = false;

        /// <summary>
        /// 错误详情
        /// </summary>
        [ImporterHeader(IsIgnore = true)]
        public IDictionary<string, string> ErrorInfo { get; set; } = new Dictionary<string, string>();
    }
}

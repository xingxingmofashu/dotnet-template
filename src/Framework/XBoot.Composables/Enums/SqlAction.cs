
using System.ComponentModel;

namespace XBoot.Composables.Enums;

public enum SqlAction
{
    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    Delete = 1,

    /// <summary>
    /// 修改
    /// </summary>
    [Description("修改")]
    Modify = 2,

    /// <summary>
    /// 添加
    /// </summary>
    [Description("添加")]
    Add = 3,
}


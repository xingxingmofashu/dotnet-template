using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBoot.Composables.Enums;
/// <summary>
/// 系统数据返回状态
/// </summary>
public enum ResponseStatusCode
{
    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    Error = 0,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success = 1,

    /// <summary>
    /// 错误请求
    /// </summary>
    [Description("错误请求")]
    BadRequest = 400,

    /// <summary>
    /// 接口未授权
    /// </summary>
    [Description("接口未授权")]
    ApiUnauthorized = 401,

    /// <summary>
    /// 找不到资源
    /// </summary>
    [Description("找不到资源")]
    NotFound = 404,

    /// <summary>
    /// 程序错误
    /// </summary>
    [Description("程序错误")]
    InternalServerError = 500,

    /// <summary>
    /// Aop权限错误
    /// </summary>
    [Description("Aop权限错误")]
    NonAuthoritativeInformation = 5001,

    /// <summary>
    /// Require必填验证
    /// </summary>
    [Description("Require必填验证")]
    RequiredError = 1001
}

using System.ComponentModel;
using System.Text.Json.Serialization;
using XBoot.Composables.Enums;

namespace XBoot.Composables;

/// <summary>
/// 实体包装类
/// </summary>
[Serializable]
public class XBootResponse
{
    #region 构造方法

    public XBootResponse() : this("", ResponseStatusCode.Error)
    {
    }

    public XBootResponse(string message, ResponseStatusCode code)
    {
        Msg = message;
        Code = code;
    }

    #endregion 构造方法

    /// <summary>
    /// 返回代码
    /// </summary>
    [Description("返回代码")]
    public ResponseStatusCode Code { get; set; }

    /// <summary>
    /// 返回消息
    /// </summary>
    [Description("返回消息")]
    public string Msg { get; set; }

    [JsonPropertyName("success")]
    public bool IsSuccess => Code == ResponseStatusCode.Success;

    public ResponseStatusCode Status => IsSuccess ? 0 : Code;

    public static XBootResponse Success()
    {
        return new XBootResponse() { Code = ResponseStatusCode.Success };
    }

    public static XBootResponse Error(string message)
    {
        return new XBootResponse() { Code = ResponseStatusCode.Error, Msg = message };
    }

    public static XBootResponse<T> Error<T>(string message)
    {
        return new XBootResponse<T>() { Code = ResponseStatusCode.Error, Msg = message };
    }

    public static XBootResponse<T> Success<T>(T data)
    {
        return new XBootResponse<T>() { Code = ResponseStatusCode.Success, Data = data };
    }

    public static XBootResponse NotFound(string entityName)
    {
        return new XBootResponse<string>() { Code = ResponseStatusCode.NotFound, Msg = entityName, Data = entityName };
    }

    public static XBootResponse Invalid(string message, Dictionary<int, List<string>> content)
    {
        var newContent = content
            .Where(_ => _.Value.Count() != 0)
            .Select(_ => new { Key = _.Key, Message = string.Join(',', _.Value) })
            .ToArray();

        return new XBootResponse<Array>(message, ResponseStatusCode.Error, newContent);
    }

    public static XBootResponse<T> NotFound<T>(string entityName)
    {
        return new XBootResponse<T>() { Code = ResponseStatusCode.NotFound, Msg = entityName };
    }
}

/// <summary>
/// 实体包装类
/// </summary>
[Serializable]
public class XBootResponse<T> : XBootResponse
{
    #region 构造方法

    public XBootResponse() : this("", ResponseStatusCode.Error, default(T))
    {
    }

    public XBootResponse(string message, ResponseStatusCode code, T? content)
    {
        Msg = message;
        Code = code;
        Data = content;
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootResponse<T> Success(string message, T? content)
    {
        return new XBootResponse<T>()
        {
            Msg = message,
            Code = ResponseStatusCode.Success,
            Data = content
        };
    }

    /// <summary>
    /// 错误
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootResponse<T> Error(string message, T? content = default)
    {
        return new XBootResponse<T>()
        {
            Msg = message,
            Code = ResponseStatusCode.Error,
            Data = content
        };
    }

    /// <summary>
    /// 批量导入组件返回的认证结果
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public new XBootResponse<Array> Invalid(string message, Dictionary<int, List<string>> content)
    {
        var newContent = content
            .Where(_ => _.Value.Count() != 0)
            .Select(_ => new { Key = _.Key, Message = string.Join(',', _.Value) })
            .ToArray();

        return new XBootResponse<Array>(message, ResponseStatusCode.Error, newContent);
    }

    /// <summary>
    /// 自定义业务返回
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootResponse<T> CustomResult(string message, T? content, ResponseStatusCode eResultCode)
    {
        return new XBootResponse<T>()
        {
            Msg = message,
            Code = eResultCode,
            Data = content
        };
    }

    #endregion 构造方法

    /// <summary>
    /// 实体内容
    /// </summary>
    [Description("Result结果集")]
    public T? Data { get; set; }
}

/// <summary>
/// 返回带分页的Model
/// </summary>
/// <typeparam name="T"></typeparam>
public class XBootPageResponse<T> where T : new()
{
    #region 构造方法

    public XBootPageResponse() : this("", ResponseStatusCode.Success, null)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <param name="content"></param>
    public XBootPageResponse(string message, ResponseStatusCode code, List<T>? content, int pageIndex = 1, int pageSize = 20)
    {
        Msg = message;
        Code = code;
        Data = content;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootPageResponse<T> Success(string message, List<T> content, int totalCount, int pageIndex = 1, int pageSize = 20)
    {
        return new XBootPageResponse<T>()
        {
            Msg = message,
            Code = ResponseStatusCode.Success,
            Data = content,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootPageResponse<T> SuccessExtend(string message, List<T> content, int totalCount, dynamic? dataExtend, int pageIndex = 1, int pageSize = 20)
    {
        return new XBootPageResponse<T>()
        {
            Msg = message,
            Code = ResponseStatusCode.Success,
            Data = content,
            TotalCount = totalCount,
            DataExtend = dataExtend,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// 错误
    /// </summary>
    /// <param name="message"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public XBootPageResponse<T> Error(string message, List<T>? content, int pageIndex = 1, int pageSize = 20)
    {
        return new XBootPageResponse<T>()
        {
            Msg = message,
            Code = ResponseStatusCode.Error,
            Data = content,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    #endregion 构造方法

    /// <summary>
    /// 返回状态
    /// </summary>
    [Description("返回状态")]
    public ResponseStatusCode Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    [Description("返回信息")]
    public string Msg { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    [Description("当前页码")]
    public int PageIndex { get; set; }

    /// <summary>
    /// 当前页大小
    /// </summary>
    [Description("当前页大小")]
    public int PageSize { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    [Description("总记录数")]
    public int TotalCount { get; set; }

    private int _pageCount;

    /// <summary>
    /// 总页数（系统自动计算）
    /// </summary>
    public int PageCount
    {
        get
        {
            if (_pageCount == 0 && TotalCount > 0)
            {
                PageSize = PageSize <= 0 ? 1 : PageSize;//防止 除以0
                _pageCount = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                {
                    _pageCount += 1;
                }
            }
            return _pageCount;
        }
        set { _pageCount = value; }
    }

    /// <summary>
    ///  返回的数据对象
    /// </summary>
    [Description("返回的数据对象")]
    public List<T>? Data { get; set; }

    /// <summary>
    /// 返回的数据对象(用于补充返回的数据)
    /// </summary>
    [Description("补充返回的数据对象")]
    public dynamic? DataExtend { get; set; }
}


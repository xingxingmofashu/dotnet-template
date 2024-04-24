namespace XBoot.Core.IServices;

public interface IEntityBase
{
    /// <summary>
    /// Id
    /// </summary>
    Guid Id { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreatedTime { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    string? ModifyBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? ModifyTime { get; set; }

    /// <summary>
    /// 删除标识
    /// </summary>
    bool? IsDeleted { get; set; }
}

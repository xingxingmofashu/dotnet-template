using SqlSugar;
using XBoot.Core.IServices;

namespace XBoot.Core.Model;

public class EntityBase : IEntityBase
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键", ColumnName = "id")]
    public Guid Id { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnDescription = "创建人", ColumnName = "created_by", IsOnlyIgnoreUpdate = true)]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间", ColumnName = "created_time", IsOnlyIgnoreUpdate = true)]
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnDescription = "修改人", ColumnName = "modify_by")]
    public string? ModifyBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnDescription = "修改时间", ColumnName = "modify_time")]
    public DateTime? ModifyTime { get; set; }

    /// <summary>
    /// Desc:逻辑删除标识
    /// </summary>
    [SugarColumn(ColumnName = "is_deleted")]
    public bool? IsDeleted { get; set; }
}

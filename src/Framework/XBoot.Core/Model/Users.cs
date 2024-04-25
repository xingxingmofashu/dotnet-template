using SqlSugar;

namespace XBoot.Core.Model;

[SugarTable("xb_users")]
public class Users : EntityBase
{
    [SugarColumn(ColumnName ="account")]
    public string? Account { get; set; }

    [SugarColumn(ColumnName = "password")]
    public string? Password { get; set; }
}
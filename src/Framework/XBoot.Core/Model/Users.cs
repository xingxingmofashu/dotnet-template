using SqlSugar;

namespace XBoot.Core.Model;

[SugarTable("xb_users")]
public class Users : EntityBase
{
    [SugarColumn(ColumnName ="account")]
    public string? Account { get; set; }
    [SugarColumn(ColumnName = "first_name")]
    public string? FirstName { get; set; }
    [SugarColumn(ColumnName = "last_name")]
    public string? LastName { get; set; }
    [SugarColumn(ColumnName = "phone")]
    public string? Phone { get; set; }
    [SugarColumn(ColumnName = "email")]
    public string? Email { get; set; }

    [SugarColumn(ColumnName = "password")]
    public string? Password { get; set; }
}
using SqlSugar;

namespace XBoot.Core.Model;

[SugarTable("xb_user")]
public class User : EntityBase
{
    public string Account { get; set; }
}
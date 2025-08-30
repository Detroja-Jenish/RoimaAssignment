using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("JobOpeningWiseSkills")]
[PrimaryKey("ID")]
public class JobOpeningWiseSkill
{
    public int ID { get; set; }
    public int JobOpeningID { get; set; }
    public int SkillID { get; set; }
    public string Importance { get; set; }

    public JobOpening JobOpening { get; set; }
    public Skill Skill { get; set; }
}

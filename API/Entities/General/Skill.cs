using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("Skills")]
[PrimaryKey("SkillID")]
public class Skill
{
    public int SkillID { get; set; }
    public string SkillTitle { get; set; }
    public string Description { get; set; }

    public ICollection<CandidateWiseSkill> CandidateWiseSkills { get; set; }
    public ICollection<JobOpeningWiseSkill> JobOpeningWiseSkills { get; set; }
}

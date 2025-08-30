using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("CandidateWiseSkills")]
[PrimaryKey("ID")]
public class CandidateWiseSkill
{
    public int ID { get; set; }
    public int CandidateID { get; set; }
    public int SkillID { get; set; }

    public Candidate Candidate { get; set; }
    public Skill Skill { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using API.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("JobOpenings")]
[PrimaryKey("JobOpeningID")]
public class JobOpening
{
    public int JobOpeningID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RequiredMinExperience { get; set; }
    public DateOnly LastDateOFRegistration { get; set; }
    public JobOpeningStatus Status { get; set; }  // Can be mapped to enum
    public int NoOfOpenings { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public Employee CreatedByEmployee { get; set; }
    public ICollection<InterviewRound> InterviewRounds { get; set; }
    public ICollection<JobOpeningWiseSkill> JobOpeningWiseSkills { get; set; }
    public ICollection<Candidate> Candidates { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("InterviewRounds")]
[PrimaryKey("InterviewRoundID")]
public class InterviewRound
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InterviewRoundID { get; set; }
    public string InterviewMode { get; set; }
    public string InterviewDescription { get; set; }
    public int JobOpeningID { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPanel { get; set; }
    public JobOpening JobOpening { get; set; }
    public ICollection<CandidateWiseInterview> CandidateWiseInterviews { get; set; }
}

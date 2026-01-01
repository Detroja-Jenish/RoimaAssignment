using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("InterviewWiseAuthorityFeedbacks")]
[PrimaryKey("ID")]
public class InterviewWiseAuthorityFeedback
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public int InterviewerID { get; set; }
    public int InterviewID { get; set; }
    public string? Feedback { get; set; }
    public int Marks { get; set; }

    public Employee Employee { get; set; }
    public CandidateWiseInterview CandidateWiseInterview { get; set; }
}
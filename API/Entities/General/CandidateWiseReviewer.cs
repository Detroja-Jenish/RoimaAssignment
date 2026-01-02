using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("CandidateWiseReviewers")]
[PrimaryKey("ID")]
public class CandidateWiseReviewer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public int CandidateID { get; set; }
    public int ReviewerID { get; set; }

    public string? Feedback { get; set; }
    public bool? IsRecommended { get; set; }
    public DateTime? ReviewedAt { get; set; }

    public Candidate Candidate { get; set; } = null!;
    public Employee Reviewer { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("CandidateWiseInterviews")]
[PrimaryKey("InterviewID")]
public class CandidateWiseInterview
{
    public int InterviewID { get; set; }
    public DateTime InterviewDate { get; set; }
    public string InterviewDescription { get; set; }
    public TimeSpan InterviewTime { get; set; }
    public string MeetingLink { get; set; }
    public int InterviewRoundID { get; set; }
    public int CandidateID { get; set; }

    public Candidate Candidate { get; set; }
    public InterviewRound InterviewRound { get; set; }
    public ICollection<InterviewWiseAuthorityFeedback> Feedbacks { get; set; }

}
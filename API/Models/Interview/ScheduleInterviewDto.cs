namespace API.Models;

public class ScheduleInterviewDto
{
    public int CandidateId { get; set; }

    public int InterviewRoundID { get; set; }

    public DateTime ScheduledAt { get; set; }

    public List<int> InterviewerIds { get; set; }
}

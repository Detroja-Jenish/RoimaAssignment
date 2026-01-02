namespace API.Models;

public class SubmitFeedbackDto
{
    public int InterviewID { get; set; }
    public int InterviewerID { get; set; }
    public string Feedback { get; set; } = null!;
    public int Marks { get; set; }
}

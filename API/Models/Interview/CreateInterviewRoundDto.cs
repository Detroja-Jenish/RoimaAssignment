namespace API.Models;

public class CreateInterviewRoundDto
{
    public string InterviewMode { get; set; }
    public string InterviewDescription { get; set; }
    public int JobOpeningID { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPanel { get; set; }
}

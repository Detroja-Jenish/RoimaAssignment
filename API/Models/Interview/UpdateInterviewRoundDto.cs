namespace API.Models;

public class UpdateInterviewRoundDto
{
    public string InterviewMode { get; set; }
    public string InterviewDescription { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPanel { get; set; }
}

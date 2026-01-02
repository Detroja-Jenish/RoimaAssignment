namespace API.Models;

public class SubmitReviewDto
{
    public int ReviewerId { get; set; } // Provided for testing; ideally taken from Auth context
    public string Feedback { get; set; }
    public bool IsRecommended { get; set; }
}
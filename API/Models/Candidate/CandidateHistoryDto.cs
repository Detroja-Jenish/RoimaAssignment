namespace API.Models;

public class CandidateHistoryDto
{
    public bool HasAppliedBefore { get; set; }
    public List<PastApplication>? Applications { get; set; }
}

public class PastApplication
{
    public int CandidateId { get; set; }
    public string JobTitle { get; set; } = null!;
    public DateTime AppliedDate { get; set; }
    public string Status { get; set; } = null!;
}
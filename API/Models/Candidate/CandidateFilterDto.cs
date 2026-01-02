namespace API.Models;

public class CandidateFilterDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? JobOpeningId { get; set; }
    public string? Status { get; set; } // Applied, Interviewing, etc.
    public decimal? MinExperience { get; set; }
    public decimal? MaxExperience { get; set; }
    public List<int>? SkillIds { get; set; } // Filter by technology
}
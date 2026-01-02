namespace API.Models;

public class AssignPanelDto
{
    public int CandidateId { get; set; }
    public List<int> ReviewerIds { get; set; }
}
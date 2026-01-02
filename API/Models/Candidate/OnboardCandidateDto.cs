namespace API.Models;

public class OnboardCandidateDto
{
    public DateOnly JoiningDate { get; set; }
    public int RoleId { get; set; } // The role they are joining as
}
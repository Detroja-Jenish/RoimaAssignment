using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("Candidates")]
[PrimaryKey("CandidateID")]

public class Candidate
{
    public int CandidateID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public decimal Experience { get; set; }
    public string CurrentCompany { get; set; }
    public int CurrentCTC { get; set; }
    public int ExpectedCTC { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int AppliedJobOpeningId { get; set; }
    public string CVPath { get; set; }

    public JobOpening AppliedJobOpening { get; set; }
    public ICollection<CandidateWiseInterview> CandidateWiseInterviews { get; set; }
    public ICollection<CandidateWiseSkill> CandidateWiseSkills { get; set; }
}
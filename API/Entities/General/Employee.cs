using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("Employees")]
[PrimaryKey("EmployeeID")]
public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeID { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string FirstName { get; set; } = null!;
    [Column(TypeName = "varchar(20)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; } = null!;
    [Column(TypeName = "varchar(10)")]
    public string Phone { get; set; } = null!;
    [Column(TypeName = "varchar(20)")]
    public string Password { get; set; } = null!;
    public DateOnly JoiningDate { get; set; }
    public int RoleID { get; set; }
    public Role Role { get; set; }
    public ICollection<InterviewWiseAuthorityFeedback> Feedbacks { get; set; }
    public ICollection<JobOpening> JobOpenings { get; set; }
    public ICollection<CandidateWiseReviewer> Reviews { get; set; }
    public ICollection<CandidateWiseReviewer> CandidateWiseReviewers { get; set; }


}
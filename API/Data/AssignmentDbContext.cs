
using API.Entities.General;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class AssignmentDbContext : DbContext
{
    private string connectionString;

    public DbSet<Role> Roles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<JobOpening> JobOpenings { get; set; }
    public DbSet<InterviewRound> InterviewRounds { get; set; }
    public DbSet<CandidateWiseInterview> CandidateWiseInterviews { get; set; }
    public DbSet<InterviewWiseAuthorityFeedback> InterviewWiseAuthorityFeedbacks { get; set; }
    public DbSet<CandidateWiseSkill> CandidateWiseSkills { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobOpeningWiseSkill> JobOpeningWiseSkills { get; set; }
    public DbSet<CandidateWiseReviewer> CandidateWiseReviewers { get; set; }


    public AssignmentDbContext(IConfiguration _config)
    {
        connectionString = _config.GetConnectionString("MYSQL_CONNECTION_STRING");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Role -> Employee
        modelBuilder.Entity<Role>()
                    .HasMany<Employee>(role => role.Employees)
                    .WithOne(emp => emp.Role)
                    .HasForeignKey(emp => emp.RoleID)
                    .HasPrincipalKey(role => role.RoleID);

        //Skill -> EmploJobOpeningWiseSkillyee
        modelBuilder.Entity<Skill>()
                    .HasMany<JobOpeningWiseSkill>(skill => skill.JobOpeningWiseSkills)
                    .WithOne(jobOpeningWiseSkill => jobOpeningWiseSkill.Skill)
                    .HasForeignKey(jobOpeningWiseSkill => jobOpeningWiseSkill.SkillID)
                    .HasPrincipalKey(skill => skill.SkillID);

        //JobOpening -> JobOpeningWiseSkill
        modelBuilder.Entity<JobOpening>()
                    .HasMany<JobOpeningWiseSkill>(jobOpening => jobOpening.JobOpeningWiseSkills)
                    .WithOne(jobOpeningWiseSkill => jobOpeningWiseSkill.JobOpening)
                    .HasForeignKey(jobOpeningWiseSkill => jobOpeningWiseSkill.JobOpeningID)
                    .HasPrincipalKey(jobOpening => jobOpening.JobOpeningID);

        //Skill -> CandidateWiseSkill
        modelBuilder.Entity<Skill>()
                    .HasMany<CandidateWiseSkill>(skill => skill.CandidateWiseSkills)
                    .WithOne(candidateWiseSkill => candidateWiseSkill.Skill)
                    .HasForeignKey(candidateWiseSkill => candidateWiseSkill.SkillID)
                    .HasPrincipalKey(skill => skill.SkillID);

        //Candidate -> CandidateWiseSkill
        modelBuilder.Entity<Candidate>()
                    .HasMany<CandidateWiseSkill>(candidate => candidate.CandidateWiseSkills)
                    .WithOne(candidateWiseSkill => candidateWiseSkill.Candidate)
                    .HasForeignKey(candidateWiseSkill => candidateWiseSkill.CandidateID)
                    .HasPrincipalKey(candidate => candidate.CandidateID);

        //JobOpening -> InterviewRound
        modelBuilder.Entity<JobOpening>()
                    .HasMany<InterviewRound>(jobOpening => jobOpening.InterviewRounds)
                    .WithOne(interviewRound => interviewRound.JobOpening)
                    .HasForeignKey(interviewRound => interviewRound.JobOpeningID)
                    .HasPrincipalKey(jobOpening => jobOpening.JobOpeningID);

        //Employee -> InterviewWiseAuthorityFeedback
        modelBuilder.Entity<Employee>()
                    .HasMany<InterviewWiseAuthorityFeedback>(emp => emp.Feedbacks)
                    .WithOne(feedback => feedback.Employee)
                    .HasForeignKey(feedback => feedback.InterviewerID)
                    .HasPrincipalKey(emp => emp.EmployeeID);

        //Employee -> JobOpening
        modelBuilder.Entity<Employee>()
                    .HasMany<JobOpening>(emp => emp.JobOpenings)
                    .WithOne(jobOpening => jobOpening.CreatedByEmployee)
                    .HasForeignKey(jobOpening => jobOpening.CreatedBy)
                    .HasPrincipalKey(emp => emp.EmployeeID);

        //InterviewRound -> CandidateWiseInterview
        modelBuilder.Entity<InterviewRound>()
                    .HasMany<CandidateWiseInterview>(interviewRound => interviewRound.CandidateWiseInterviews)
                    .WithOne(candidateWiseInterview => candidateWiseInterview.InterviewRound)
                    .HasForeignKey(candidateWiseInterview => candidateWiseInterview.InterviewRoundID)
                    .HasPrincipalKey(interviewRound => interviewRound.InterviewRoundID);

        //CandidateWiseInterview -> InterviewWiseAuthorityFeedback
        modelBuilder.Entity<CandidateWiseInterview>()
                    .HasMany<InterviewWiseAuthorityFeedback>(candidateWiseInterview => candidateWiseInterview.Feedbacks)
                    .WithOne(feedback => feedback.CandidateWiseInterview)
                    .HasForeignKey(feedback => feedback.InterviewID)
                    .HasPrincipalKey(candidateWiseInterview => candidateWiseInterview.InterviewID);

        modelBuilder.Entity<JobOpening>()
                    .Property(m => m.Status)
                    .HasConversion<string>();
        modelBuilder.Entity<CandidateWiseReviewer>()
.HasOne(cwr => cwr.Candidate)
.WithMany(c => c.CandidateWiseReviewers)
.HasForeignKey(cwr => cwr.CandidateID);
        modelBuilder.Entity<CandidateWiseReviewer>()
        .HasOne(cwr => cwr.Reviewer)
        .WithMany(e => e.CandidateWiseReviewers)
        .HasForeignKey(cwr => cwr.ReviewerID);

    }
}

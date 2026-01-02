using API.Data;
using API.Entities.General;
using API.Enums;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Praticse.Helpers;
namespace API.Repositories;

public class CandidateRepository : ICandidateRepository
{
    private readonly AssignmentDbContext _context;

    public CandidateRepository(AssignmentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Candidate>> GetAllAsync()
    {
        return await _context.Candidates
            .Include(c => c.AppliedJobOpening)
            .ToListAsync();
    }

    public async Task<Candidate?> GetByIdAsync(int id)
    {
        return await _context.Candidates
            .Include(c => c.AppliedJobOpening)
            .FirstOrDefaultAsync(c => c.CandidateID == id);
    }

    public async Task<IEnumerable<Candidate>> GetByJobOpeningAsync(int jobOpeningId)
    {
        return await _context.Candidates
            .Where(c => c.AppliedJobOpeningId == jobOpeningId)
            .Include(c => c.AppliedJobOpening)
            .ToListAsync();
    }

    public async Task<Candidate> AddAsync(InsertUpdateCandidateModel model)
    {
        var candidate = new Candidate
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Experience = model.Experience,
            CurrentCompany = model.CurrentCompany,
            CurrentCTC = model.CurrentCTC,
            ExpectedCTC = model.ExpectedCTC,
            AppliedJobOpeningId = model.AppliedJobOpeningId,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };

        if (model.CV != null)
        {
            candidate.CVPath = await FileUploadHelper.SaveFile(model.CV, "cvs");
        }

        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();
        return candidate;
    }

    public async Task UpdateAsync(InsertUpdateCandidateModel model)
    {
        var candidate = await _context.Candidates.FindAsync(model.CandidateID);

        if (candidate == null)
            throw new KeyNotFoundException("Candidate not found.");

        candidate.FirstName = model.FirstName;
        candidate.LastName = model.LastName;
        candidate.Email = model.Email;
        candidate.Phone = model.Phone;
        candidate.Experience = model.Experience;
        candidate.CurrentCompany = model.CurrentCompany;
        candidate.CurrentCTC = model.CurrentCTC;
        candidate.ExpectedCTC = model.ExpectedCTC;
        candidate.AppliedJobOpeningId = model.AppliedJobOpeningId;
        candidate.ModifiedAt = DateTime.UtcNow;

        if (model.CV != null)
        {
            candidate.CVPath = await FileUploadHelper.SaveFile(model.CV, "cvs");
        }

        _context.Candidates.Update(candidate);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> UpdateStatusAsync(int candidateId, CandidateStatus status, string? comment)
    {
        var candidate = await _context.Candidates.FindAsync(candidateId);
        if (candidate == null) return false;

        candidate.Status = status;
        candidate.StatusComment = comment;
        candidate.ModifiedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Employee?> OnboardCandidateAsync(int candidateId, DateOnly joiningDate, int roleId)
    {
        var candidate = await _context.Candidates.FindAsync(candidateId);
        if (candidate == null) return null;

        // Create New Employee Entity
        var newEmployee = new Employee
        {
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            Email = candidate.Email,
            Phone = candidate.Phone,
            JoiningDate = joiningDate,
            RoleID = roleId,
            Password = "TemporaryPassword123!"
        };

        _context.Employees.Add(newEmployee);

        // Update Candidate Status to Joined
        candidate.Status = CandidateStatus.Joined;
        candidate.ModifiedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return newEmployee;
    }

    public async Task AssignScreeningPanelAsync(int candidateId, List<int> reviewerIds)
    {
        var panel = reviewerIds.Select(id => new CandidateWiseReviewer
        {
            CandidateID = candidateId,
            ReviewerID = id
        }).ToList();

        _context.CandidateWiseReviewers.AddRange(panel);

        var candidate = await _context.Candidates.FindAsync(candidateId);
        if (candidate != null)
        {
            candidate.Status = CandidateStatus.Screening;
            candidate.ModifiedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> SubmitScreeningFeedbackAsync(int candidateId, int reviewerId, SubmitReviewDto dto)
    {
        var assignment = await _context.CandidateWiseReviewers
            .FirstOrDefaultAsync(x => x.CandidateID == candidateId && x.ReviewerID == reviewerId);

        if (assignment == null) return false;


        assignment.Feedback = dto.Feedback;
        assignment.IsRecommended = dto.IsRecommended;
        assignment.ReviewedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Candidate>> GetAssignedScreeningsAsync(int reviewerId)
    {

        return await _context.CandidateWiseReviewers
            .Where(r => r.ReviewerID == reviewerId && r.ReviewedAt == null)
            .Include(r => r.Candidate)
            .Select(r => r.Candidate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Candidate>> GetApplicationHistoryAsync(string email, string phone)
    {

        return await _context.Candidates
            .Where(c => c.Email == email || c.Phone == phone)
            .Include(c => c.AppliedJobOpening)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
    public async Task<IEnumerable<Candidate>> GetFilteredCandidatesAsync(CandidateFilterDto filter)
    {
        var query = _context.Candidates
            .Include(c => c.AppliedJobOpening)
            .Include(c => c.CandidateWiseSkills)
            .AsQueryable();

        // 1. Filter by Name (First or Last)
        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(c => (c.FirstName + " " + c.LastName).Contains(filter.Name));

        // 2. Filter by Status
        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(c => c.Status.ToString() == filter.Status);

        // 3. Filter by Job Opening
        if (filter.JobOpeningId.HasValue)
            query = query.Where(c => c.AppliedJobOpeningId == filter.JobOpeningId);

        // 4. Filter by Experience Range
        if (filter.MinExperience.HasValue)
            query = query.Where(c => c.Experience >= filter.MinExperience);

        if (filter.MaxExperience.HasValue)
            query = query.Where(c => c.Experience <= filter.MaxExperience);

        // 5. Filter by specific Skills (Requirement #9: Technology-wise profiles)
        if (filter.SkillIds != null && filter.SkillIds.Any())
        {
            query = query.Where(c => c.CandidateWiseSkills
                .Any(s => filter.SkillIds.Contains(s.SkillID)));
        }

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

}

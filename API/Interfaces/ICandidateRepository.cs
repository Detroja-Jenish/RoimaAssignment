using API.Entities.General;
using API.Enums;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task<Candidate> AddAsync(InsertUpdateCandidateModel model);
        Task UpdateAsync(InsertUpdateCandidateModel model);
        Task<IEnumerable<Candidate>> GetByJobOpeningAsync(int jobOpeningId);
        Task<bool> UpdateStatusAsync(int candidateId, CandidateStatus status, string? comment);
        Task<Employee?> OnboardCandidateAsync(int candidateId, DateOnly joiningDate, int roleId);
        Task AssignScreeningPanelAsync(int candidateId, List<int> reviewerIds);

        Task<bool> SubmitScreeningFeedbackAsync(int candidateId, int reviewerId, SubmitReviewDto dto);

        Task<IEnumerable<Candidate>> GetAssignedScreeningsAsync(int reviewerId);
        Task<IEnumerable<Candidate>> GetApplicationHistoryAsync(string email, string phone);
        Task<IEnumerable<Candidate>> GetFilteredCandidatesAsync(CandidateFilterDto filter);
    }
}

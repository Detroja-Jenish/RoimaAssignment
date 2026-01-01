using API.Entities.General;
using API.Models;

namespace API.Interfaces;

public interface IInterviewRoundRepository
{
    Task<InterviewRound> CreateAsync(CreateInterviewRoundDto dto);
    Task<InterviewRound> UpdateAsync(int interviewRoundId, UpdateInterviewRoundDto dto);
    Task<InterviewRound> GetByIdAsync(int interviewRoundId);
    Task<IEnumerable<InterviewRound>> GetByJobOpeningAsync(int jobOpeningId);
    Task<bool> DeleteAsync(int interviewRoundId);
}


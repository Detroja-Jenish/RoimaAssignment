using API.Entities.General;
using API.Models;
namespace API.Interfaces;

public interface IInterviewRepository
{
    Task<CandidateWiseInterview> CreateInterviewAsync(CandidateWiseInterview interview);

    Task AssignInterviewersAsync(
        int candidateWiseInterviewId,
        List<int> interviewerIds
    );
    Task<bool> SubmitFeedbackAsync(SubmitFeedbackDto dto);

}

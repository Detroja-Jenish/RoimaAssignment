using API.Entities.General;
namespace API.Interfaces;

public interface IInterviewRepository
{
    Task<CandidateWiseInterview> CreateInterviewAsync(CandidateWiseInterview interview);

    Task AssignInterviewersAsync(
        int candidateWiseInterviewId,
        List<int> interviewerIds
    );

}

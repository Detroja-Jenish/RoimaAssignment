using API.Data;
using API.Entities.General;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Repositories;

public class InterviewRepository : IInterviewRepository
{
    private readonly AssignmentDbContext _context;

    public InterviewRepository(AssignmentDbContext context)
    {
        _context = context;
    }

    public async Task<CandidateWiseInterview> CreateInterviewAsync(
        CandidateWiseInterview interview)
    {
        _context.CandidateWiseInterviews.Add(interview);
        await _context.SaveChangesAsync();
        return interview;
    }

    public async Task AssignInterviewersAsync(
        int candidateWiseInterviewId,
        List<int> interviewerIds)
    {
        var records = interviewerIds.Select(interviewerId =>
        new InterviewWiseAuthorityFeedback
        {
            InterviewID = candidateWiseInterviewId,
            InterviewerID = interviewerId,


            Feedback = null,
            Marks = 0
        }
    ).ToList();

        _context.InterviewWiseAuthorityFeedbacks.AddRange(records);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> SubmitFeedbackAsync(SubmitFeedbackDto dto)
    {
        var feedbackRecord = await _context.InterviewWiseAuthorityFeedbacks
            .FirstOrDefaultAsync(f => f.InterviewID == dto.InterviewID
                                   && f.InterviewerID == dto.InterviewerID);

        if (feedbackRecord == null) return false;

        feedbackRecord.Feedback = dto.Feedback;
        feedbackRecord.Marks = dto.Marks;

        await _context.SaveChangesAsync();
        return true;
    }


}

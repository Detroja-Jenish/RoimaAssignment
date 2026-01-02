using API.Data;
using API.Entities.General;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/interviews")]
public class InterviewController : ControllerBase
{
    private readonly IInterviewRepository _interviewRepository;
    private readonly AssignmentDbContext _context;

    public InterviewController(
        IInterviewRepository interviewRepository,
        AssignmentDbContext context)
    {
        _interviewRepository = interviewRepository;
        _context = context;
    }

    //  Schedule Interview + Assign Interviewers
    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleInterview(
        ScheduleInterviewDto dto)
    {
        var interview = new CandidateWiseInterview
        {
            CandidateID = dto.CandidateId,
            InterviewRoundID = dto.InterviewRoundID,
            InterviewTime = dto.ScheduledAt
        };

        await _interviewRepository.CreateInterviewAsync(interview);

        await _interviewRepository.AssignInterviewersAsync(
            interview.InterviewID,
            dto.InterviewerIds
        );

        return Ok(new
        {
            InterviewId = interview.InterviewID,
            Message = "Interview scheduled and interviewers assigned"
        });
    }
    [HttpPut("submit-feedback")]
    public async Task<IActionResult> SubmitFeedback(SubmitFeedbackDto dto)
    {
        var result = await _interviewRepository.SubmitFeedbackAsync(dto);

        if (!result)
        {
            return NotFound(new { Message = "Interview assignment not found for this interviewer." });
        }

        return Ok(new { Message = "Feedback submitted successfully." });
    }


}

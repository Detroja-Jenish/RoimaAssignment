using API.Data;
using API.Entities.General;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Repositories;

public class InterviewRoundRepository : IInterviewRoundRepository
{
    private readonly AssignmentDbContext _context;

    public InterviewRoundRepository(AssignmentDbContext context)
    {
        _context = context;
    }

    public async Task<InterviewRound> CreateAsync(CreateInterviewRoundDto dto)
    {
        var interviewRound = new InterviewRound
        {
            InterviewMode = dto.InterviewMode,
            InterviewDescription = dto.InterviewDescription,
            JobOpeningID = dto.JobOpeningID,
            IsDefault = dto.IsDefault,
            IsPanel = dto.IsPanel
        };

        _context.InterviewRounds.Add(interviewRound);
        await _context.SaveChangesAsync();

        return interviewRound;
    }

    public async Task<InterviewRound> UpdateAsync(
        int interviewRoundId,
        UpdateInterviewRoundDto dto)
    {
        var interviewRound = await _context.InterviewRounds
            .FirstOrDefaultAsync(x => x.InterviewRoundID == interviewRoundId);

        if (interviewRound == null)
            return null;

        interviewRound.InterviewMode = dto.InterviewMode;
        interviewRound.InterviewDescription = dto.InterviewDescription;
        interviewRound.IsDefault = dto.IsDefault;
        interviewRound.IsPanel = dto.IsPanel;

        await _context.SaveChangesAsync();
        return interviewRound;
    }

    public async Task<InterviewRound> GetByIdAsync(int interviewRoundId)
    {
        return await _context.InterviewRounds
            .FirstOrDefaultAsync(x => x.InterviewRoundID == interviewRoundId);
    }

    public async Task<IEnumerable<InterviewRound>> GetByJobOpeningAsync(int jobOpeningId)
    {
        return await _context.InterviewRounds
            .Where(x => x.JobOpeningID == jobOpeningId)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int interviewRoundId)
    {
        var interviewRound = await _context.InterviewRounds
            .FirstOrDefaultAsync(x => x.InterviewRoundID == interviewRoundId);

        if (interviewRound == null)
            return false;

        _context.InterviewRounds.Remove(interviewRound);
        await _context.SaveChangesAsync();
        return true;
    }
}

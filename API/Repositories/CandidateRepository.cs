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
    public async Task<IEnumerable<Candidate>> GetByJobOpeningAsync(int jobOpeningId)
    {
        return await _context.Candidates
        .Where(c => c.AppliedJobOpeningId == jobOpeningId)
        .Include(c => c.AppliedJobOpening)
        .ToListAsync();
    }


    
}

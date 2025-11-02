using API.Data;
using API.Entities.General;
using API.Models;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praticse.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly AssignmentDbContext _context;

        public CandidatesController(AssignmentDbContext context)
        {
            _context = context;
        }

        // // GET: api/candidates
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        // {
        //     var candidates = await _context.Candidates
        //         .Include(c => c.AppliedJobOpening)
        //         .ToListAsync();

        //     return Ok(candidates);
        // }

        // // GET: api/candidates/{id}
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Candidate>> GetCandidate(int id)
        // {
        //     var candidate = await _context.Candidates
        //         .Include(c => c.AppliedJobOpening)
        //         .FirstOrDefaultAsync(c => c.CandidateID == id);

        //     if (candidate == null)
        //         return NotFound();

        //     return Ok(candidate);
        // }

        // POST: api/candidates (Insert)
        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromForm] InsertUpdateCandidateModel model)
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

            return Ok(candidate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCandidate([FromForm] InsertUpdateCandidateModel model)
        {
            var candidate = await _context.Candidates.FindAsync(model.CandidateID);

            if (candidate == null)
                return NotFound();

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

            return NoContent();
        }

    }
}

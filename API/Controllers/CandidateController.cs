using API.Data;
using API.Entities.General;
using API.Models;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praticse.Helpers;
using API.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly AssignmentDbContext _context;
        private readonly ICandidateRepository _candidateRepository;

        public CandidatesController(AssignmentDbContext context, ICandidateRepository candidateRepository)
        {
            _context = context;
            _candidateRepository = candidateRepository;
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
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] CandidateUpdateStatusDto dto)
        {
            var result = await _candidateRepository.UpdateStatusAsync(id, dto.Status, dto.Comment);
            if (!result) return NotFound();

            return Ok(new { Message = $"Candidate status updated to {dto.Status}" });
        }
        [HttpPost("{id}/onboard")]
        public async Task<IActionResult> Onboard(int id, [FromBody] OnboardCandidateDto dto)
        {
            var employee = await _candidateRepository.OnboardCandidateAsync(id, dto.JoiningDate, dto.RoleId);

            if (employee == null)
            {
                return NotFound(new
                {
                    Message = "Candidate not found."
                });
            }

            return Ok(new
            {
                Message = "Candidate successfully moved to Employee records.",
                EmployeeId = employee.EmployeeID,
                Name = $"{employee.FirstName} {employee.LastName}"
            });
        }

        // POST: api/candidates/assign-screening-panel
        [HttpPost("assign-screening-panel")]
        public async Task<IActionResult> AssignPanel([FromBody] AssignPanelDto dto)
        {
            await _candidateRepository.AssignScreeningPanelAsync(dto.CandidateId, dto.ReviewerIds);
            return Ok(new { Message = "Screening panel assigned and candidate status updated." });
        }

        // GET: api/candidates/my-screenings/{reviewerId}
        [HttpGet("my-screenings/{reviewerId}")]
        public async Task<IActionResult> GetMyScreenings(int reviewerId)
        {
            var candidates = await _candidateRepository.GetAssignedScreeningsAsync(reviewerId);
            return Ok(candidates);
        }

        // POST: api/candidates/{id}/submit-screening
        [HttpPost("{id}/submit-screening")]
        public async Task<IActionResult> SubmitScreening(int id, [FromBody] SubmitReviewDto dto)
        {
            // In a real app, you'd get the ReviewerId from the JWT Token (User.Identity)
            var result = await _candidateRepository.SubmitScreeningFeedbackAsync(id, dto.ReviewerId, dto);

            if (!result) return NotFound(new { Message = "Assignment not found for this reviewer." });

            return Ok(new { Message = "Feedback recorded successfully." });
        }
        [HttpGet("check-history")]
        public async Task<IActionResult> CheckHistory([FromQuery] string email, [FromQuery] string phone)
        {
            var history = await _candidateRepository.GetApplicationHistoryAsync(email, phone);

            if (!history.Any())
            {
                return Ok(new { HasAppliedBefore = false });
            }

            var response = new CandidateHistoryDto
            {
                HasAppliedBefore = true,
                Applications = history.Select(h => new PastApplication
                {
                    CandidateId = h.CandidateID,
                    JobTitle = h.AppliedJobOpening?.Title ?? "Unknown Position",
                    AppliedDate = h.CreatedAt,
                    Status = h.Status.ToString() // Using the Enum we created earlier
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] CandidateFilterDto filter)
        {
            var candidates = await _candidateRepository.GetFilteredCandidatesAsync(filter);

            // Mapping to a simpler display DTO if necessary
            return Ok(candidates);
        }

    }

}

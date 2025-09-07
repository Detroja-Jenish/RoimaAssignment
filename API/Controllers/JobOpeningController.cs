using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using API.Entities.General;
using API.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobOpeningController : Controller
    {
        private readonly AssignmentDbContext _context;
        public JobOpeningController(AssignmentDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<JsonResult> Create(JobOpeningSaveModel model)
        {
            JobOpening jobOpening = new JobOpening{
                Title= model.Title,
                Description= model.Description,
                RequiredMinExperience= model.RequiredMinExperience,
                LastDateOFRegistration= model.LastDateOFRegistration,
                Status= JobOpeningStatus.Start,
                NoOfOpenings = model.NoOfOpenings,
                CreatedBy= model.CreatedBy,
                CreatedAt= DateTime.Now
            };
            await _context.JobOpenings.AddAsync(jobOpening);
            await _context.SaveChangesAsync();
            return Json(new { jobOpening = jobOpening });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllJobOpening(){
            List<JobOpening> jobOpenings = await _context.JobOpenings.ToListAsync();
            return Json(jobOpenings);
        }
    }
}
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
        public async Task<JsonResult> Create(InsertUpdateJobOpeningModel model)
        {

            JobOpening jobOpening = new JobOpening
            {
                Title = model.Title,
                Description = model.Description,
                RequiredMinExperience = model.RequiredMinExperience,
                LastDateOFRegistration = model.LastDateOFRegistration,
                Status = JobOpeningStatus.Start,
                NoOfOpenings = model.NoOfOpenings,
                CreatedBy = model.CreatedBy,
                CreatedAt = DateTime.Now,
            };
            await _context.JobOpenings.AddAsync(jobOpening);
            await _context.SaveChangesAsync();

            List<JobOpeningWiseSkill> jobOpeningWiseSkills = new List<JobOpeningWiseSkill>();
            foreach (int skillID in model.ImportedSkillIDs)
            {
                JobOpeningWiseSkill jobOpeningWiseSkill = new JobOpeningWiseSkill
                {
                    JobOpeningID = jobOpening.JobOpeningID,
                    Importance = "Required",
                    SkillID = skillID
                };
                await _context.JobOpeningWiseSkills.AddAsync(jobOpeningWiseSkill);
                jobOpeningWiseSkills.Add(jobOpeningWiseSkill);
            }
            foreach (int skillID in model.PreferedSkillIDs)
            {
                JobOpeningWiseSkill jobOpeningWiseSkill = new JobOpeningWiseSkill
                {
                    JobOpeningID = jobOpening.JobOpeningID,
                    Importance = "Prefered",
                    SkillID = skillID
                };
                await _context.JobOpeningWiseSkills.AddAsync(jobOpeningWiseSkill);
                jobOpeningWiseSkills.Add(jobOpeningWiseSkill);
            }

            await _context.SaveChangesAsync();
            var jobWithSkills = await _context.JobOpenings
        .Include(j => j.JobOpeningWiseSkills)
        .ThenInclude(js => js.Skill)
        .FirstOrDefaultAsync(j => j.JobOpeningID == jobOpening.JobOpeningID);

            if (jobWithSkills == null) return Json(new { error = "Failed to retrieve saved job" });

            JobOpeningResponseModel response = new JobOpeningResponseModel
            {
                JobOpeningId = jobWithSkills.JobOpeningID,
                Title = jobWithSkills.Title,
                Description = jobWithSkills.Description,
                RequiredMinExperience = jobWithSkills.RequiredMinExperience,
                LastDateOFRegistration = jobWithSkills.LastDateOFRegistration,
                Status = jobWithSkills.Status,
                NoOfOpenings = jobWithSkills.NoOfOpenings,
                CreatedBy = jobWithSkills.CreatedBy,
                Skills = jobWithSkills.JobOpeningWiseSkills.Select(
                    js => new JobOpeningWiseSkillResponseModel
                    {
                        SkillID = js.SkillID,
                        SkillTitle = js.Skill?.SkillTitle ?? "N/A", // Safe access
                        Description = js.Skill?.Description ?? "",
                        Importance = js.Importance
                    }
                ).ToList()
            };

            return Json(new { jobOpening = response });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllJobOpening()
        {
            var jobs = await _context.JobOpenings
                                    .Include(job => job.JobOpeningWiseSkills)
                                    .ThenInclude(js => js.Skill)
                                    .Select(job => new JobOpeningResponseModel
                                    {
                                        JobOpeningId = job.JobOpeningID,
                                        Title = job.Title,
                                        Description = job.Description,
                                        Skills = job.JobOpeningWiseSkills.Select(js => new JobOpeningWiseSkillResponseModel
                                        {
                                            SkillID = js.SkillID,
                                            SkillTitle = js.Skill.SkillTitle,
                                            Description = js.Skill.Description,
                                            Importance = js.Importance
                                        }).ToList()
                                    })
                                    .ToListAsync();

            return Json(jobs);
        }
    }
}
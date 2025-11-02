namespace API.Controllers
{
    using API.Data;
    using API.Entities.General;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly AssignmentDbContext _context;
        public SkillController(AssignmentDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Skill> skills = await _context.Skills.ToListAsync();
            return Ok(skills);
        }
    }
}
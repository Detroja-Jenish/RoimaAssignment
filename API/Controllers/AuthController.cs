using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data;
using API.Entities.General;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AssignmentDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(AssignmentDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        [HttpPost]
        public JsonResult Login(LoginRequestModel model)
        {
            LoginResponseModel? result = _context.Employees
                    .Where(
                        e => e.Email == model.Email && e.Password == model.Password
                    )
                    .Select(e => new LoginResponseModel
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        RoleTitle = e.Role.RoleTitle,
                        AuthToken = JwtHelper.SignAuthToken(e.FirstName, e.LastName, e.Email, e.Role.RoleTitle, _config)
                    }).FirstOrDefault();

            return Json(new
            {
                result = result
            });
        }
    }
}
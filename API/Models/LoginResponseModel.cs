namespace API.Models
{
    public class LoginResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleTitle { get; set; }
        public string AuthToken { get; set; }
    }
}
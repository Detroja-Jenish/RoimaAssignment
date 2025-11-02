namespace API.Models
{
    public class InsertUpdateCandidateModel
    {
        public int? CandidateID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Experience { get; set; }
        public string CurrentCompany { get; set; }
        public int CurrentCTC { get; set; }
        public int ExpectedCTC { get; set; }
        public int AppliedJobOpeningId { get; set; }
        public IFormFile CV { get; set; }

    }
}
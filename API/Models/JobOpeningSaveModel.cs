using API.Enums;
namespace API.Models
{
    public class JobOpeningSaveModel
    {
        public int? JobOpeningID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredMinExperience { get; set; }
        public DateOnly LastDateOFRegistration { get; set; }
        public int NoOfOpenings { get; set; }
        public int CreatedBy { get; set; }

    }
}
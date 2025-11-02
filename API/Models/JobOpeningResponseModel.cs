using API.Entities.General;
using API.Enums;

namespace API.Models
{
    public class JobOpeningResponseModel
    {
        public int JobOpeningId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredMinExperience { get; set; }
        public DateOnly LastDateOFRegistration { get; set; }
        public JobOpeningStatus Status { get; set; }
        public int NoOfOpenings { get; set; }
        public int CreatedBy { get; set; }
        public List<JobOpeningWiseSkillResponseModel> Skills { get; set; }
    }
}
using API.Enums;

namespace API.Models
{
    public class CandidateUpdateStatusDto
    {
        public CandidateStatus Status { get; set; }
        public string? Comment { get; set; }
    }
}
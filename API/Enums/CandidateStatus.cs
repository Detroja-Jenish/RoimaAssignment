namespace API.Enums;

public enum CandidateStatus
{
    Applied,        // Initial stage
    Screening,      // CV being reviewed by Reviewer
    Shortlisted,    // Passed screening, ready for interview
    Interviewing,   // At least one interview scheduled
    Offered,        // Selected and offer sent
    Joined,         // Moved to Employee records
    Rejected,       // Failed at any stage
    OnHold          // Paused with a reason
}
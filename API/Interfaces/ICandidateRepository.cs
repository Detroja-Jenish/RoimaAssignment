using API.Entities.General;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task<Candidate> AddAsync(InsertUpdateCandidateModel model);
        Task UpdateAsync(InsertUpdateCandidateModel model);
        Task<IEnumerable<Candidate>> GetByJobOpeningAsync(int jobOpeningId);
    }
}

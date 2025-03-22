
// Domain/Repositories/IMccRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMccRepository
    {
        Task<MccEntity> GetMccByIdAsync(string mccId);
        Task<List<MccEntity>> GetListMccAsync(string grapperId);
        Task<bool> CreateNewMccAsync(string grapperId, MccEntity mccEntity);
        Task<bool> UpdateMccAsync(string mccId, MccEntity mccEntity);
        Task<bool> DeleteMccAsync(string mccId);
    }
}
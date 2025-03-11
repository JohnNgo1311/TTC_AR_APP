
// Domain/Repositories/IRackRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRackRepository
    {
        Task<RackEntity> GetRackByIdAsync(string rackId);
        Task<List<RackEntity>> GetListRackAsync(string grapperId);
        Task<bool> CreateNewRackAsync(string grapperId, RackEntity rackEntity);
        Task<bool> UpdateRackAsync(string rackId, RackEntity rackEntity);
        Task<bool> DeleteRackAsync(string rackId);
    }
}

// Domain/Repositories/IGrapperRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGrapperRepository
    {
        Task<GrapperEntity> GetGrapperByIdAsync(string grapperId);
        Task<List<GrapperEntity>> GetListGrapperAsync(string companyId);
        Task<bool> CreateNewGrapperAsync(string companyId, GrapperEntity grapperEntity);
        Task<bool> UpdateGrapperAsync(string grapperId, GrapperEntity grapperEntity);
        Task<bool> DeleteGrapperAsync(string grapperId);
    }
}

// Domain/Repositories/IAdapterSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdapterSpecificationRepository
    {
        Task<AdapterSpecificationEntity> GetAdapterSpecificationByIdAsync(int adapterSpecificationId);
        Task<List<AdapterSpecificationEntity>> GetListAdapterSpecificationAsync(int companyId);
        Task<bool> CreateNewAdapterSpecificationAsync(int companyId, AdapterSpecificationEntity adapterSpecificationEntity);
        Task<bool> UpdateAdapterSpecificationAsync(int adapterSpecificationId, AdapterSpecificationEntity adapterSpecificationEntity);
        Task<bool> DeleteAdapterSpecificationAsync(int adapterSpecificationId);
    }
}
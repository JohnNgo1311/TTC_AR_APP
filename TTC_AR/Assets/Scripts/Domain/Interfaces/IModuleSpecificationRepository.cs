
// Domain/Repositories/IModuleSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IModuleSpecificationRepository
    {
        //! Trả về Entity
        Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(int ModuleSpecificationId);
        Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(int companyId);
        Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationEntity ModuleSpecificationEntity);
        Task<bool> UpdateModuleSpecificationAsync(int ModuleSpecificationId, ModuleSpecificationEntity ModuleSpecificationEntity);
        Task<bool> DeleteModuleSpecificationAsync(int ModuleSpecificationId);
    }
}
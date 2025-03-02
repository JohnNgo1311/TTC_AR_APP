
// Domain/Repositories/IModuleSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IModuleSpecificationRepository
    {
        Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(int moduleSpecificationId);
        Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(int companyId);
        Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationEntity moduleSpecificationEntity);
        Task<bool> UpdateModuleSpecificationAsync(int moduleSpecificationId, ModuleSpecificationEntity moduleSpecificationEntity);
        Task<bool> DeleteModuleSpecificationAsync(int moduleSpecificationId);
    }
}
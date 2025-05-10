
// Domain/Repositories/IModuleSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IModuleSpecificationRepository
    {
        //! Trả về Entity
        Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(string ModuleSpecificationId);
        Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(string companyId);
        Task<bool> CreateNewModuleSpecificationAsync(string companyId, ModuleSpecificationEntity ModuleSpecificationEntity);
        Task<bool> UpdateModuleSpecificationAsync(string ModuleSpecificationId, ModuleSpecificationEntity ModuleSpecificationEntity);
        Task<bool> DeleteModuleSpecificationAsync(string ModuleSpecificationId);
    }
}
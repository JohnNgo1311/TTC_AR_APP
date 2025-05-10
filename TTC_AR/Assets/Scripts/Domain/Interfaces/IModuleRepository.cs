
// Domain/Repositories/IModuleRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IModuleRepository
    {
        Task<ModuleEntity> GetModuleByIdAsync(string moduleId);
        Task<List<ModuleEntity>> GetListModuleAsync(string grapperId);
        Task<bool> CreateNewModuleAsync(string grapperId, ModuleEntity moduleEntity);
        Task<bool> UpdateModuleAsync(string moduleId, ModuleEntity moduleEntity);
        Task<bool> DeleteModuleAsync(string moduleId);
    }
}
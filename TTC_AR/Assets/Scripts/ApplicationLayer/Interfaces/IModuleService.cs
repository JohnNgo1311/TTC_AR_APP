
// Domain/Repositories/IModuleRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IModuleService
    {
        Task<ModuleResponseDto> GetModuleByIdAsync(string moduleId);
        Task<List<ModuleBasicDto>> GetListModuleAsync(string grapperId);
        Task<bool> CreateNewModuleAsync(string grapperId, ModuleRequestDto moduleRequestDto);
        Task<bool> UpdateModuleAsync(string moduleId, ModuleRequestDto moduleRequestDto);
        Task<bool> DeleteModuleAsync(string moduleId);
    }
}
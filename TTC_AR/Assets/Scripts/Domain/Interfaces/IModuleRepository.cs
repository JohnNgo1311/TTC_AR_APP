
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
        Task<ModuleResponseDto> GetModuleByIdAsync(int moduleId);
        Task<List<ModuleGeneralDto>> GetListModuleAsync(int grapperId);
        Task<bool> CreateNewModuleAsync(int grapperId, ModuleRequestDto moduleRequestDto);
        Task<bool> UpdateModuleAsync(int moduleId, ModuleRequestDto moduleRequestDto);
        Task<bool> DeleteModuleAsync(int moduleId);
    }
}
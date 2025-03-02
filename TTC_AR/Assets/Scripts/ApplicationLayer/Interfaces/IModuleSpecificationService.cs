
// Domain/Repositories/IModuleSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IModuleSpecificationService
    {
        Task<ModuleSpecificationResponseDto> GetModuleSpecificationByIdAsync(int moduleSpecificationId);
        Task<List<ModuleSpecificationResponseDto>> GetListModuleSpecificationAsync(int companyId);
        Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationRequestDto moduleSpecificationRequestDto);
        Task<bool> UpdateModuleSpecificationAsync(int moduleSpecificationId, ModuleSpecificationRequestDto moduleSpecificationRequestDto);
        Task<bool> DeleteModuleSpecificationAsync(int moduleSpecificationId);
    }
}
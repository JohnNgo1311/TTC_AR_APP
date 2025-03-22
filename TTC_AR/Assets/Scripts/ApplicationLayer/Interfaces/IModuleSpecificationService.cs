
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IModuleSpecificationService
    {   //! Tham số là Dto, tả về Dto
        Task<ModuleSpecificationResponseDto> GetModuleSpecificationByIdAsync(string moduleSpecificationId);
        Task<List<ModuleSpecificationBasicDto>> GetListModuleSpecificationAsync(string companyId);
        Task<bool> CreateNewModuleSpecificationAsync(string companyId, ModuleSpecificationRequestDto moduleSpecificationRequestDto);
        Task<bool> UpdateModuleSpecificationAsync(string moduleSpecificationId, ModuleSpecificationRequestDto moduleSpecificationRequestDto);
        Task<bool> DeleteModuleSpecificationAsync(string moduleSpecificationId);
    }
}
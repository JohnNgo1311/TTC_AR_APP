
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ModuleSpecification;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services
{
    //! Không bắt lỗi tại đây
    public class ModuleSpecificationService : IModuleSpecificationService
    {
        private readonly ModuleSpecificationUseCase _ModuleSpecificationUseCase;
        public ModuleSpecificationService(ModuleSpecificationUseCase ModuleSpecificationUseCase)
        {
            _ModuleSpecificationUseCase = ModuleSpecificationUseCase;
        }

        //! Dữ liệu trả về là Dto

        public async Task<ModuleSpecificationResponseDto> GetModuleSpecificationByIdAsync(int ModuleSpecificationId)
        {
            return await _ModuleSpecificationUseCase.GetModuleSpecificationByIdAsync(ModuleSpecificationId);
        }

        public async Task<List<ModuleSpecificationBasicDto>> GetListModuleSpecificationAsync(int companyId)
        {
            return await _ModuleSpecificationUseCase.GetListModuleSpecificationAsync(companyId);
        }

        public async Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
        {
            return await _ModuleSpecificationUseCase.CreateNewModuleSpecificationAsync(companyId, ModuleSpecificationRequestDto);
        }
        public async Task<bool> UpdateModuleSpecificationAsync(int ModuleSpecificationId, ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
        {
            return await _ModuleSpecificationUseCase.UpdateModuleSpecificationAsync(ModuleSpecificationId, ModuleSpecificationRequestDto);
        }
        public async Task<bool> DeleteModuleSpecificationAsync(int ModuleSpecificationId)
        {
            return await _ModuleSpecificationUseCase.DeleteModuleSpecificationAsync(ModuleSpecificationId);
        }
    }

}
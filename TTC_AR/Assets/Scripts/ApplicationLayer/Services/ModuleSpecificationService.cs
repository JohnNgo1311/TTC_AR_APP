
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
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

        public async Task<List<ModuleSpecificationResponseDto>> GetListModuleSpecificationAsync(int grapperId)
        {
            return await _ModuleSpecificationUseCase.GetListModuleSpecificationAsync(grapperId);
        }

        public async Task<bool> CreateNewModuleSpecificationAsync(int grapperId, ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
        {
            return await _ModuleSpecificationUseCase.CreateNewModuleSpecificationAsync(grapperId, ModuleSpecificationRequestDto);
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
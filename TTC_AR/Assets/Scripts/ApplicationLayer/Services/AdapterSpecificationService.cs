
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services
{
    //! Không bắt lỗi tại đây
    public class AdapterSpecificationService : IAdapterSpecificationService
    {
        private readonly AdapterSpecificationUseCase _AdapterSpecificationUseCase;
        public AdapterSpecificationService(AdapterSpecificationUseCase adapterSpecificationUseCase)
        {
            _AdapterSpecificationUseCase = adapterSpecificationUseCase;
        }

        //! Dữ liệu trả về là Dto

        public async Task<AdapterSpecificationResponseDto> GetAdapterSpecificationByIdAsync(string adapterSpecificationId)
        {
            return await _AdapterSpecificationUseCase.GetAdapterSpecificationByIdAsync(adapterSpecificationId);
        }

        public async Task<List<AdapterSpecificationBasicDto>> GetListAdapterSpecificationAsync(string companyId)
        {
            return await _AdapterSpecificationUseCase.GetListAdapterSpecificationAsync(companyId);
        }

        public async Task<bool> CreateNewAdapterSpecificationAsync(string companyId, AdapterSpecificationRequestDto adapterSpecificationRequestDto)
        {
            return await _AdapterSpecificationUseCase.CreateNewAdapterSpecificationAsync(companyId, adapterSpecificationRequestDto);
        }
        public async Task<bool> UpdateAdapterSpecificationAsync(string adapterSpecificationId, AdapterSpecificationRequestDto adapterSpecificationRequestDto)
        {
            return await _AdapterSpecificationUseCase.UpdateAdapterSpecificationAsync(adapterSpecificationId, adapterSpecificationRequestDto);
        }
        public async Task<bool> DeleteAdapterSpecificationAsync(string adapterSpecificationId)
        {
            return await _AdapterSpecificationUseCase.DeleteAdapterSpecificationAsync(adapterSpecificationId);
        }
    }

}
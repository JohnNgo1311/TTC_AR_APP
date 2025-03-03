
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IAdapterSpecificationService
    {
        //! Tham số là Dto, tả về Dto
        Task<AdapterSpecificationResponseDto> GetAdapterSpecificationByIdAsync(int adapterSpecificationId);
        Task<List<AdapterSpecificationResponseDto>> GetListAdapterSpecificationAsync(int companyId);
        Task<bool> CreateNewAdapterSpecificationAsync(int companyId, AdapterSpecificationRequestDto adapterSpecificationRequestDto);
        Task<bool> UpdateAdapterSpecificationAsync(int adapterSpecificationId, AdapterSpecificationRequestDto adapterSpecificationRequestDto);
        Task<bool> DeleteAdapterSpecificationAsync(int adapterSpecificationId);
    }
}
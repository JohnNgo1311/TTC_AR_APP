
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.AdapterSpecification;

namespace ApplicationLayer.Interfaces
{
    public interface IAdapterSpecificationService
    {
        //! Tham số là Dto, tả về Dto
        Task<AdapterSpecificationResponseDto> GetAdapterSpecificationByIdAsync(string adapterSpecificationId);
        Task<List<AdapterSpecificationBasicDto>> GetListAdapterSpecificationAsync(string companyId);
        Task<bool> CreateNewAdapterSpecificationAsync(string companyId, AdapterSpecificationRequestDto adapterSpecificationRequestDto);
        Task<bool> UpdateAdapterSpecificationAsync(string adapterSpecificationId, AdapterSpecificationRequestDto adapterSpecificationRequestDto);
        Task<bool> DeleteAdapterSpecificationAsync(string adapterSpecificationId);
    }
}
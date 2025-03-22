
// Domain/Repositories/IAdapterSpecificationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdapterSpecificationRepository
    {
        //! Trả về Entity do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        Task<AdapterSpecificationEntity> GetAdapterSpecificationByIdAsync(string adapterSpecificationId);
        Task<List<AdapterSpecificationEntity>> GetListAdapterSpecificationAsync(string companyId);
        Task<bool> CreateNewAdapterSpecificationAsync(string companyId, AdapterSpecificationEntity adapterSpecificationEntity);
        Task<bool> UpdateAdapterSpecificationAsync(string adapterSpecificationId, AdapterSpecificationEntity adapterSpecificationEntity);
        Task<bool> DeleteAdapterSpecificationAsync(string adapterSpecificationId);
    }
}
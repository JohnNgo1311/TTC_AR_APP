
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Grapper;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IGrapperService
    {
        Task<GrapperResponseDto> GetGrapperByIdAsync(string grapperId);
        Task<List<GrapperBasicDto>> GetListGrapperAsync(string companyId);
        Task<bool> CreateNewGrapperAsync(string companyId, GrapperRequestDto grapperRequestDto);
        Task<bool> UpdateGrapperAsync(string grapperId, GrapperRequestDto grapperRequestDto);
        Task<bool> DeleteGrapperAsync(string grapperId);
    }
}
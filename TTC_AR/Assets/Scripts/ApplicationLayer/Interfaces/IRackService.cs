
// Domain/Repositories/IRackRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Rack;

namespace ApplicationLayer.Interfaces
{
    public interface IRackService
    {
        Task<RackResponseDto> GetRackByIdAsync(string rackId);
        Task<List<RackBasicDto>> GetListRackAsync(string grapperId);
        Task<bool> CreateNewRackAsync(string grapperId, RackRequestDto rackRequestDto);
        Task<bool> UpdateRackAsync(string rackId, RackRequestDto rackRequestDto);
        Task<bool> DeleteRackAsync(string rackId);
    }
}
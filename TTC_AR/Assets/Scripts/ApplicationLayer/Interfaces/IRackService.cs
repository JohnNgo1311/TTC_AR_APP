
// Domain/Repositories/IRackRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IRackService
    {
        Task<RackGeneralDto> GetRackByIdAsync(int rackId);
        Task<List<RackGeneralDto>> GetListRackAsync(int grapperId);
        Task<bool> CreateNewRackAsync(int grapperId, RackRequestDto rackRequestDto);
        Task<bool> UpdateRackAsync(int rackId, RackRequestDto rackRequestDto);
        Task<bool> DeleteRackAsync(int rackId);
    }
}
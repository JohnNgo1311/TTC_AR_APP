
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Mcc;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IMccService
    {
        Task<MccResponseDto> GetMccByIdAsync(string MccId);
        Task<IEnumerable<MccBasicDto>> GetListMccAsync(string grapperId);
        Task<bool> CreateNewMccAsync(string grapperId, MccRequestDto mccRequestDto);
        Task<bool> UpdateMccAsync(string mccId, MccRequestDto mccRequestDto);
        Task<bool> DeleteMccAsync(string MccId);
    }
}
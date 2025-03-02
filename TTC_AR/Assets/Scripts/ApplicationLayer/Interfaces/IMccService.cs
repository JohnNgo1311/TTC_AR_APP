
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IMccService
    {
        Task<MccResponseDto> GetMccByIdAsync(int MccId);
        Task<List<MccResponseDto>> GetListMccAsync(int grapperId);
        Task<bool> CreateNewMccAsync(int grapperId, MccRequestDto mccRequestDto);
        Task<bool> UpdateMccAsync(int mccId, MccRequestDto mccRequestDto);
        Task<bool> DeleteMccAsync(int MccId);
    }
}
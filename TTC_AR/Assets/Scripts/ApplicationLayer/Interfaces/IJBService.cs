
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;


namespace ApplicationLayer.Interfaces
{
    public interface IJBService
    {
        Task<JBResponseDto> GetJBByIdAsync(int JBId);
        Task<List<JBResponseDto>> GetListJBAsync(int grapperId);
        Task<bool> CreateNewJBAsync(int grapperId, JBRequestDto JBRequestDto);
        Task<bool> UpdateJBAsync(int JBId, JBRequestDto JBRequestDto);
        Task<bool> DeleteJBAsync(int JBId);
    }

}
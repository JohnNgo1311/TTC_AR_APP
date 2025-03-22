
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.JB;


namespace ApplicationLayer.Interfaces
{
    public interface IJBService
    {
        //! Tham số là Dto, tả về Dto
        Task<JBResponseDto> GetJBByIdAsync(string JBId);
        Task<List<JBGeneralDto>> GetListJBInformationAsync(string grapperId);
        Task<List<JBBasicDto>> GetListJBGeneralAsync(string grapperId);
        Task<bool> CreateNewJBAsync(string grapperId, JBRequestDto JBRequestDto);
        Task<bool> UpdateJBAsync(string JBId, JBRequestDto JBRequestDto);
        Task<bool> DeleteJBAsync(string JBId);
    }

}
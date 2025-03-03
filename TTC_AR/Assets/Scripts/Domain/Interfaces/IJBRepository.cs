
// Domain/Repositories/IJBRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJBRepository
    {
        //! Tùy trường hớp sẽ trả về Entity hoặc Dto, trường hợp này đặc biệt do server chỉ response 1 phần trong JBEntity
        //! Tham số là Entity
        Task<JBResponseDto> GetJBByIdAsync(int JBId);
        Task<List<JBResponseDto>> GetListJBAsync(int grapperId);
        //  Task<List<JBEntity>> GetListJBAsync(int grapperId);
        Task<bool> CreateNewJBAsync(int grapperId, JBEntity JBEntity);
        Task<bool> UpdateJBAsync(int JBId, JBEntity JBEntity);
        Task<bool> DeleteJBAsync(int JBId);
    }
    //! Được Implement ở JBRepository.cs trong Infrastructure Layer
}
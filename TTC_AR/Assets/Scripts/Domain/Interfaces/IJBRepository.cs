
// Domain/Repositories/IJBRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.JB;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJBRepository
    {
        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        //! Tham số là Entity
        Task<JBEntity> GetJBByIdAsync(string JBId);
        Task<List<JBEntity>> GetListJBGeneralAsync(string grapperId);
        Task<List<JBEntity>> GetListJBInformationAsync(string grapperId);

        Task<bool> CreateNewJBAsync(string grapperId, JBEntity jBEntity);
        Task<bool> UpdateJBAsync(string JBId, JBEntity jBEntity);
        Task<bool> DeleteJBAsync(string JBId);
    }
    //! Được Implement ở JBRepository.cs trong Infrastructure Layer
}
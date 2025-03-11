
// Domain/Repositories/IImageRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        //! Tham số là Entity
        Task<ImageEntity> GetImageByIdAsync(string ImageId);
        Task<List<ImageEntity>> GetListImageAsync(string grapperId);
        Task<bool> CreateNewImageAsync(string grapperId, ImageEntity ImageEntity);
        Task<bool> UpdateImageAsync(string ImageId, ImageEntity ImageEntity);
        Task<bool> DeleteImageAsync(string ImageId);
    }
    //! Được Implement ở ImageRepository.cs trong Infrastructure Layer
}
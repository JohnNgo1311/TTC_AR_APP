
// Domain/Repositories/IImageRepository.cs
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using UnityEngine;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        //! Tham số là Entity
        Task<ImageEntity> GetImageByIdAsync(string ImageId);
        Task<List<ImageEntity>> GetListImageAsync(string grapperId);
        Task<bool> CreateNewImageAsync(string grapperId, ImageEntity ImageEntity);
        Task<bool> DeleteImageAsync(string ImageId);
        Task<bool> UploadNewImageFromGallery(string grapperId, Texture2D texture, string fieldName, string fileName, string filePath, string mimeType);
        Task<bool> UploadNewImageFromCamera(string grapperId, Texture2D texture, string fieldName, string fileName);

    }
    //! Được Implement ở ImageRepository.cs trong Infrastructure Layer
}
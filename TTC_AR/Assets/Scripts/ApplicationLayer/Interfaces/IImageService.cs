
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Image;
using UnityEngine;


namespace ApplicationLayer.Interfaces
{
    public interface IImageService
    {
        //! Tham số là Dto, tả về Dto
        Task<ImageBasicDto> GetImageByIdAsync(string imageId);
        Task<List<ImageBasicDto>> GetListImageAsync(string grapperId);
        Task<bool> CreateNewImageAsync(string grapperId, ImageRequestDto ImageRequestDto);
        Task<bool> DeleteImageAsync(string imageId);
        Task<bool> UploadNewImageFromGallery(string grapperId, Texture2D texture, string filePath, string fieldName, string fileName);
        Task<bool> UploadNewImageFromCamera(string grapperId, Texture2D texture, string fieldName, string fileName);

    }

}
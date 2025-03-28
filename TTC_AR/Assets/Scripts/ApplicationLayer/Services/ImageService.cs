
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;
using UnityEngine;

namespace ApplicationLayer.Services
{  //! Không bắt lỗi tại đây
    public class ImageService : IImageService
    {
        private readonly ImageUseCase _ImageUseCase;
        public ImageService(ImageUseCase ImageUseCase)
        {
            _ImageUseCase = ImageUseCase;
        }


        //! Dữ liệu trả về là Dto
        public async Task<ImageResponseDto> GetImageByIdAsync(string imageId)
        {
            return await _ImageUseCase.GetImageByIdAsync(imageId);
        }

        public async Task<List<ImageBasicDto>> GetListImageAsync(string grapperId)
        {
            return await _ImageUseCase.GetListImageAsync(grapperId);
        }

        public async Task<bool> CreateNewImageAsync(string grapperId, ImageRequestDto ImageRequestDto)
        {
            return await _ImageUseCase.CreateNewImageAsync(grapperId, ImageRequestDto);
        }
        public async Task<bool> DeleteImageAsync(string ImageId)
        {
            return await _ImageUseCase.DeleteImageAsync(ImageId);
        }
        public async Task<bool> UploadNewImageFromGallery(string grapperId, Texture2D texture, string filePath, string fieldName, string fileName)
        {
            Debug.Log("Run Service");
            Debug.Log($"UploadNewImageFromGallery: {grapperId} {texture} {filePath} {fieldName} {fileName}");
            return await _ImageUseCase.UploadImageFromGallery(grapperId, texture, filePath, fieldName, fileName);
        }
        public async Task<bool> UploadNewImageFromCamera(string grapperId, Texture2D texture, string fieldName, string fileName)
        {
            Debug.Log("Run Service");
            Debug.Log($"UploadNewImageFromCamera: {grapperId} {texture} {fieldName} {fileName}");
            return await _ImageUseCase.UploadImageFromCamera(grapperId, texture, fieldName, fileName);

        }
    }

}
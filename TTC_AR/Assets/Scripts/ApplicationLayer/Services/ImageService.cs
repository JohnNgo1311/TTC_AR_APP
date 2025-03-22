
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

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
        public async Task<bool> UpdateImageAsync(string ImageId, ImageRequestDto ImageRequestDto)
        {
            return await _ImageUseCase.UpdateImageAsync(ImageId, ImageRequestDto);
        }
        public async Task<bool> DeleteImageAsync(string ImageId)
        {
            return await _ImageUseCase.DeleteImageAsync(ImageId);
        }
    }

}
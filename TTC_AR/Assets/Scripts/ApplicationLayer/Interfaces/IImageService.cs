
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Image;


namespace ApplicationLayer.Interfaces
{
    public interface IImageService
    {
        //! Tham số là Dto, tả về Dto
        Task<ImageResponseDto> GetImageByIdAsync(string imageId);
        Task<List<ImageBasicDto>> GetListImageAsync(string grapperId);
        Task<bool> CreateNewImageAsync(string grapperId, ImageRequestDto ImageRequestDto);
        Task<bool> DeleteImageAsync(string imageId);
    }

}
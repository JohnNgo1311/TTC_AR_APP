using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Image;

using Domain.Entities;
using Domain.Interfaces;


namespace ApplicationLayer.UseCases
{
    public class ImageUseCase
    {
        private IImageRepository _IImageRepository;

        public ImageUseCase(IImageRepository IImageRepository)
        {
            _IImageRepository = IImageRepository;
        }
        public async Task<List<ImageBasicDto>> GetListImageAsync(string grapperId)
        {
            try
            {
                var ImageEntities = await _IImageRepository.GetListImageAsync(grapperId);

                if (ImageEntities == null)
                {
                    throw new ApplicationException("Failed to get Image list");
                }
                else
                {  // Ánh xạ từ ImageResponseDto sang ImageEntity (nếu cần logic nghiệp vụ) rồi sang ImageResponseDto
                   // var ImageEntities = ImageListDtos.Select(dto => new ImageEntity(dto.Name)
                   // {
                   //     Id = dto.Id,

                    //     Location = dto.Location,

                    //     DeviceEntities = dto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),

                    //     ModuleEntities = dto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),

                    //     OutdoorImageEntity = dto.OutdoorImageResponseDto != null
                    //         ? new ImageEntity(dto.OutdoorImageResponseDto.Id, dto.OutdoorImageResponseDto.Name, dto.OutdoorImageResponseDto.Url)
                    //         : null,

                    //     ConnectionImageEntities = dto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.Url)).ToList()

                    // }).ToList();

                    // // Ánh xạ từ ImageEntity sang ImageResponseDto
                    var ImageBasicDtos = ImageEntities.Select(ImageEntity => MapToBasicDto(ImageEntity)).ToList();
                    GlobalVariable.temp_Dictionary_ImageInformationModel = ImageBasicDtos.ToDictionary(dto => dto.Name, dto => new ImageInformationModel(id: dto.Id, name: dto.Name, url: ""));

                    GlobalVariable.temp_List_ImageInformationModel = ImageBasicDtos.Select(dto => new ImageInformationModel(dto.Id, dto.Name, "")).ToList();

                    foreach (var item in GlobalVariable.temp_Dictionary_ImageInformationModel)
                    {
                        UnityEngine.Debug.Log("Key: " + item.Key + " Value: " + item.Value.Id + " " + item.Value.Name);
                    }

                    return ImageBasicDtos;
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Image list", ex);
            }
        }
        public async Task<ImageResponseDto> GetImageByIdAsync(string ImageId)
        {
            try
            {
                var ImageEntity = await _IImageRepository.GetImageByIdAsync(ImageId);

                if (ImageEntity == null)
                {
                    throw new ApplicationException("Failed to get Image");
                }
                else
                {
                    // Ánh xạ từ ImageEntity sang ImageEntity để check các lỗi nghiệp vụ
                    var ImageResponseDto = MapToResponseDto(ImageEntity);
                    // Ánh xạ từ ImageEntity sang ImageResponseDto để đưa giá trị trả về
                    return ImageResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Image", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewImageAsync(string grapperId, ImageRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ ImageRequestDto sang ImageEntity để check các nghiệp vụ
                var ImageEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(ImageEntity);

                if (ImageEntity == null)
                {
                    throw new ApplicationException("Failed to create Image cause ImageEntity is Null");
                }

                else
                {
                    return await _IImageRepository.CreateNewImageAsync(grapperId, ImageEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Image", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateImageAsync(string ImageId, ImageRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ ImageRequestDto sang ImageEntity để check các nghiệp vụ
                var ImageEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(ImageEntity);

                if (ImageEntity == null)
                {
                    throw new ApplicationException("Failed to update Image cause ImageEntity is Null");
                }

                else
                {
                    return await _IImageRepository.UpdateImageAsync(ImageId, ImageEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Image", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteImageAsync(string ImageId)
        {
            try
            {
                var deletedImageResult = await _IImageRepository.DeleteImageAsync(ImageId);
                return deletedImageResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete Image", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Dto => Entity
        private ImageEntity MapRequestToEntity(ImageRequestDto ImageRequestDto)
        {
            return new ImageEntity(

            );
        }

        // private ImageEntity MapToResponseEntity(ImageResponseDto ImageResponseDto)
        // {
        //     return new ImageEntity(
        //         ImageResponseDto.Id,
        //         ImageResponseDto.Name,
        //         ImageResponseDto.Location,
        //         ImageResponseDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
        //         ImageResponseDto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),
        //         ImageResponseDto.OutdoorImageResponseDto != null ? new ImageEntity(ImageResponseDto.OutdoorImageResponseDto.Id, ImageResponseDto.OutdoorImageResponseDto.Name, ImageResponseDto.OutdoorImageResponseDto.Url) : null!,
        //        ImageResponseDto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.Url)).ToList()
        //     );
        // }


        //! Entity => Dto
        private ImageResponseDto MapToResponseDto(ImageEntity ImageEntity)
        {
            return new ImageResponseDto(
                ImageEntity.Id,

                ImageEntity.Name,
                ImageEntity.Url);

            //     ImageEntity.Location ?? "chưa cập nhật",

            //    (ImageEntity.DeviceEntities == null || (ImageEntity.DeviceEntities != null && ImageEntity.DeviceEntities.Count <= 0)) ?
            //      new List<DeviceBasicDto>() : ImageEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),

            //     (ImageEntity.ModuleEntities == null || (ImageEntity.ModuleEntities != null && ImageEntity.ModuleEntities.Count <= 0)) ?
            //      new List<ModuleBasicDto>() : ImageEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),

            //     ImageEntity.OutdoorImageEntity == null ?
            //      null : new ImageResponseDto(ImageEntity.OutdoorImageEntity.Id, ImageEntity.OutdoorImageEntity.Name, ImageEntity.OutdoorImageEntity.Url),

            //     (ImageEntity.ConnectionImageEntities == null || (ImageEntity.ConnectionImageEntities != null && ImageEntity.ConnectionImageEntities.Count <= 0)) ?
            //      new List<ImageResponseDto>() : ImageEntity.ConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList()
            // );
        }

        private ImageBasicDto MapToBasicDto(ImageEntity ImageEntity)
        {
            return new ImageBasicDto(
                ImageEntity.Id,

                ImageEntity.Name
                 );
        }
        // private ImageRequestDto MapToRequestDto(ImageEntity ImageEntity)
        // {
        //     return new ImageRequestDto(
        //         ImageEntity.Name,
        //         ImageEntity.Location ?? "chưa cập nhật",
        //         ImageEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
        //         ImageEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
        //         ImageEntity.OutdoorImageEntity != null ? new ImageBasicDto(ImageEntity.OutdoorImageEntity.Id, ImageEntity.OutdoorImageEntity.Name) : null!,
        //         ImageEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList()
        //     );
        // }


    }
}
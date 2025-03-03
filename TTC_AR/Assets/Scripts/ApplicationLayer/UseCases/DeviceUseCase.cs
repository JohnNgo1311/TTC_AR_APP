using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class DeviceUseCase
    {
        private IDeviceRepository _IDeviceRepository;

        public DeviceUseCase(IDeviceRepository deviceEntityRepository)
        {
            _IDeviceRepository = deviceEntityRepository;
        }
        public async Task<List<DeviceResponseDto>> GetListDeviceAsync(int grapperId)
        {
            try
            {
                var deviceResponseDtos = await _IDeviceRepository.GetListDeviceAsync(grapperId); //! Gọi _IDeviceRepository từ Infrastructure Layer

                if (deviceResponseDtos == null)
                {
                    throw new ApplicationException("Failed to get Device list");
                }
                else
                {
                    //! Đưa về Entity để xử lý logic nghiệp vụ
                    var deviceEntities = deviceResponseDtos.Select(MapResponseToEntity).ToList();
                    return deviceEntities.Select(MapToResponseDto).ToList();
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Device list", ex); // Bao bọc lỗi từ Repository
            }

        }
        public async Task<DeviceResponseDto> GetDeviceByIdAsync(int DeviceId)
        {
            try
            {
                var deviceResponseDto = await _IDeviceRepository.GetDeviceByIdAsync(DeviceId);
                if (deviceResponseDto == null)
                {
                    throw new ApplicationException("Failed to get Device");
                }
                else
                {  //! Đưa về Entity để xử lý logic nghiệp vụ
                    var deviceEntity = MapResponseToEntity(deviceResponseDto);
                    return MapToResponseDto(deviceEntity);
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Device", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewDeviceAsync(int grapperId, DeviceRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                    throw new ArgumentException("Name cannot be empty");
                // Map DTO to Entity

                var deviceEntity = MapRequestToEntity(requestDto);
                // var deviceEntity = new DeviceEntity(requestDto.Code)
                // {
                //     Code = requestDto.Code,
                //     Function = requestDto.Function,
                //     Range = requestDto.Range,
                //     Unit = requestDto.Unit,
                //     IOAddress = requestDto.IOAddress,
                //     JBEntity = new JBEntity(requestDto.JBBasicDto.Id, requestDto.JBBasicDto.Name),
                //     ModuleEntity = new ModuleEntity(requestDto.ModuleBasicDto.Id, requestDto.ModuleBasicDto.Name),
                //     AdditionalConnectionImageEntities = requestDto.AdditionalImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
                // };

                //! Bắt đầu từ Repository thì sẽ chuyển sang tham số là Entity
                var createdDeviceResult = await _IDeviceRepository.CreateNewDeviceAsync(grapperId, deviceEntity);
                return createdDeviceResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Device", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateDeviceAsync(int DeviceId, DeviceRequestDto requestDto)
        {
            // Validate
            try
            {
                if (string.IsNullOrEmpty(requestDto.Code))
                    throw new ArgumentException("Name cannot be empty");
                var deviceEntity = MapRequestToEntity(requestDto);

                // Map DTO to Entity
                // var deviceEntity = new DeviceEntity(requestDto.Code)
                // {
                //     Code = requestDto.Code,
                //     Function = requestDto.Function,
                //     Range = requestDto.Range,
                //     Unit = requestDto.Unit,
                //     IOAddress = requestDto.IOAddress,
                //     JBEntity = new JBEntity(requestDto.JBBasicDto.Id, requestDto.JBBasicDto.Name),
                //     ModuleEntity = new ModuleEntity(requestDto.ModuleBasicDto.Id, requestDto.ModuleBasicDto.Name),
                //     AdditionalConnectionImageEntities = requestDto.AdditionalImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
                // };
                var createdDeviceResult = await _IDeviceRepository.UpdateDeviceAsync(DeviceId, deviceEntity);
                return createdDeviceResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update Device", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteDeviceAsync(int DeviceId)
        {
            try
            {
                var deletedDeviceResult = await _IDeviceRepository.DeleteDeviceAsync(DeviceId);
                return deletedDeviceResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete Device", ex); // Bao bọc lỗi từ Repository
            }
        }

        private DeviceEntity MapRequestToEntity(DeviceRequestDto requestDto)
        {
            return new DeviceEntity(
                requestDto.Code,
                requestDto.Function,
                requestDto.Range,
                requestDto.Unit,
                requestDto.IOAddress,
                new ModuleEntity(requestDto.ModuleBasicDto.Id, requestDto.ModuleBasicDto.Name),
                new JBEntity(requestDto.JBBasicDto.Id, requestDto.JBBasicDto.Name),
                requestDto.AdditionalImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
            );
        }

        private DeviceEntity MapResponseToEntity(DeviceResponseDto responseDto)
        {
            return new DeviceEntity(
                responseDto.Id,
                responseDto.Code,
                responseDto.Function,
                responseDto.Range,
                responseDto.Unit,
                responseDto.IOAddress,
                new ModuleEntity(responseDto.ModuleBasicDto.Id, responseDto.ModuleBasicDto.Name),
                new JBEntity(responseDto.JBGeneralDto.Id, responseDto.JBGeneralDto.Name, responseDto.JBGeneralDto.Location,
                new ImageEntity(responseDto.JBGeneralDto.OutdoorImageResponseDto.Id, responseDto.JBGeneralDto.OutdoorImageResponseDto.Name, responseDto.JBGeneralDto.OutdoorImageResponseDto.url),
                responseDto.JBGeneralDto.ConnectionImageResponseDtos.Select(
                    imageResponseDto => new ImageEntity(imageResponseDto.Id, imageResponseDto.Name, imageResponseDto.url)).ToList()
                ),
                responseDto.AdditionalImageResponseDtos.Select(
                    imageResponseDto => new ImageEntity(imageResponseDto.Id, imageResponseDto.Name, imageResponseDto.url)).ToList()
            );
        }
        private DeviceResponseDto MapToResponseDto(DeviceEntity deviceEntity)
        {
            return new DeviceResponseDto(
                id: deviceEntity.Id,
                code: deviceEntity.Code,
                function: deviceEntity.Function,
                range: deviceEntity.Range,
                unit: deviceEntity.Unit,
                ioAddress: deviceEntity.IOAddress,

                moduleBasicDto: new ModuleBasicDto(deviceEntity.ModuleEntity.Id, deviceEntity.ModuleEntity.Name),

                jbGeneralDto: new JBGeneralDto
                (deviceEntity.JBEntity.Id,
                deviceEntity.JBEntity.Name,
                 deviceEntity.JBEntity.Location,
                new ImageResponseDto(deviceEntity.JBEntity.OutdoorImageEntity.Id, deviceEntity.JBEntity.OutdoorImageEntity.Name, deviceEntity.JBEntity.OutdoorImageEntity.Url),
                deviceEntity.JBEntity.ConnectionImageEntities.Select(
                    imageEntity => new ImageResponseDto(imageEntity.Id, imageEntity.Name, imageEntity.Url)).ToList()
                 ),

                additionalImageResponseDtos: deviceEntity.AdditionalConnectionImageEntities.Select(
                    imageEntity => new ImageResponseDto(imageEntity.Id, imageEntity.Name, imageEntity.Url)).ToList()


            );
        }
    }
}
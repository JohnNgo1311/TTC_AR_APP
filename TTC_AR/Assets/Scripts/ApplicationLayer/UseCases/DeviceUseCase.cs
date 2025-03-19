using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Module;
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
        #region  GET List Device



        //! Get List Device General
        public async Task<List<DeviceBasicDto>> GetListDeviceGeneralAsync(string grapperId)
        {
            // try
            // {
            // //! Gọi _IDeviceRepository từ Infrastructure Layer
            // var deviceEntities = await _IDeviceRepository.GetListDeviceGeneralAsync(grapperId)
            // ?? throw new ApplicationException("Failed to get Device list");
            // return deviceEntities.Select(MapToBasicDto).ToList();

            try
            {
                var DeviceEntities = await _IDeviceRepository.GetListDeviceGeneralAsync(grapperId) ??

                throw new ApplicationException("Failed to get Device list");

                int count = DeviceEntities.Count;

                var DeviceBasicDtos = new List<DeviceBasicDto>(count);

                var listDeviceInfo = new List<DeviceInformationModel>(count);

                var dictDeviceInfo = new Dictionary<string, DeviceInformationModel>(count);

                foreach (var DeviceEntity in DeviceEntities)
                {
                    var dto = MapToBasicDto(DeviceEntity);
                    var model = new DeviceInformationModel(dto.Id, dto.Code);

                    DeviceBasicDtos.Add(dto);
                    listDeviceInfo.Add(model);
                    dictDeviceInfo[dto.Code] = model;
                }

                GlobalVariable.temp_List_DeviceInformationModel = listDeviceInfo;
                GlobalVariable.temp_Dictionary_DeviceInformationModel = dictDeviceInfo;

                return DeviceBasicDtos;
            }
            // }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Device list", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Get List Device Information từ Grapper
        public async Task<IEnumerable<DeviceResponseDto>> GetListDeviceInformationFromGrapperAsync(string grapperId)
        {
            try
            {
                //! Gọi _IDeviceRepository từ Infrastructure Layer
                var deviceEntities = await _IDeviceRepository.GetListDeviceInformationFromGrapperAsync(grapperId) ??
                throw new ApplicationException("Failed to get Device list");
                return deviceEntities.Select(MapToResponseDto).ToList();
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


        //! Get List Device Information từ Module
        public async Task<IEnumerable<DeviceResponseDto>> GetListDeviceInformationFromModuleAsync(string moduleId)
        {
            try
            {
                //! Gọi _IDeviceRepository từ Infrastructure Layer
                var deviceEntities = await _IDeviceRepository.GetListDeviceInformationFromModuleAsync(moduleId)
                ?? throw new ApplicationException("Failed to get Device list"); ;
                //! Đưa về Entity để xử lý logic nghiệp vụ 
                return deviceEntities.Select(MapToResponseDto).ToList();
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
        #endregion

        #region GET Specific Device
        public async Task<DeviceResponseDto> GetDeviceByIdAsync(string DeviceId)
        {
            try
            {
                UnityEngine.Debug.Log("Run UseCase");

                var deviceEntity = await _IDeviceRepository.GetDeviceByIdAsync(DeviceId);
                UnityEngine.Debug.Log("DeviceEntity: " + deviceEntity.Code);
                if (deviceEntity == null)
                {
                    UnityEngine.Debug.Log("Entity null value");
                    throw new ApplicationException("Failed to get Device");
                }
                else
                {
                    //! Đưa về Entity để xử lý logic nghiệp vụ
                    UnityEngine.Debug.Log("DeviceEntity: " + deviceEntity.Id);
                    // var deviceEntity = MapResponseToEntity(deviceEntity);
                    var deviceResponseDto = MapToResponseDto(deviceEntity);
                    UnityEngine.Debug.Log("DeviceResponseDto: " + deviceResponseDto.Code);
                    return deviceResponseDto;
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
        #endregion

        #region POST New Device
        public async Task<bool> CreateNewDeviceAsync(string grapperId, DeviceRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                    throw new ArgumentException("Name cannot be empty");
                // Map DTO to Entity
                var deviceEntity = MapRequestToEntity(requestDto);
                //!  var requestData = MapToRequestDto(deviceEntity);
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

                //! Cứ đưa vào Entity, khi sang Repository, tùy vào ngữ cảnh sẽ filter lại dữ liệu để gửi lên server
                //! Tham số mặc định của Repository sẽ là Entity
                var createdDeviceResult = await _IDeviceRepository.CreateNewDeviceAsync(grapperId, deviceEntity);

                if (!createdDeviceResult)
                {
                    throw new ApplicationException("Failed to create Device");
                }
                return true;
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
        #endregion

        #region PUT Device
        public async Task<bool> UpdateDeviceAsync(string deviceId, DeviceRequestDto requestDto)
        {
            deviceId = GlobalVariable.DeviceId;
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                    throw new ArgumentException("Name cannot be empty");
                // Map DTO to Entity
                var deviceEntity = MapRequestToEntity(requestDto);
                // var requestData = MapToRequestDto(deviceEntity);
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
                //! Cứ đưa vào Entity, khi sang Repository, tùy vào ngữ cảnh sẽ filter lại dữ liệu để gửi lên server
                //!Tham số mặc định của Repository sẽ là Entity
                var updatedDeviceResult = await _IDeviceRepository.UpdateDeviceAsync(deviceId, deviceEntity);
                if (!updatedDeviceResult)
                {
                    throw new ApplicationException("Failed to update Device");
                }
                return true;
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
        #endregion
        #region  DELETE Device
        public async Task<bool> DeleteDeviceAsync(string deviceId)
        {
            deviceId = GlobalVariable.DeviceId;
            try
            {
                var deletedDeviceResult = await _IDeviceRepository.DeleteDeviceAsync(deviceId);
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
        #endregion

        //! Dto => Entity
        private DeviceEntity MapRequestToEntity(DeviceRequestDto requestDto)
        {
            return new DeviceEntity(
                requestDto.Code,
                requestDto.Function,
                requestDto.Range,
                requestDto.Unit,
                requestDto.IOAddress,
                requestDto.ModuleBasicDto == null ? null : new ModuleEntity(requestDto.ModuleBasicDto.Id, requestDto.ModuleBasicDto.Name),
                requestDto.JBBasicDto == null ? null : new JBEntity(requestDto.JBBasicDto.Id, requestDto.JBBasicDto.Name),
                (requestDto.AdditionalImageBasicDtos == null || (requestDto.AdditionalImageBasicDtos != null && requestDto.AdditionalImageBasicDtos.Count <= 0)) ? new List<ImageEntity>()
                 : requestDto.AdditionalImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
            );
        }
        // private DeviceEntity MapResponseToEntity(DeviceResponseDto responseDto)
        // {
        //     return new DeviceEntity(
        //         responseDto.Id,
        //         responseDto.Code,
        //         responseDto.Function,
        //         responseDto.Range,
        //         responseDto.Unit,
        //         responseDto.IOAddress,
        //         new ModuleEntity(responseDto.ModuleBasicDto.Id, responseDto.ModuleBasicDto.Name),
        //         new JBEntity(responseDto.JBGeneralDto.Id, responseDto.JBGeneralDto.Name, responseDto.JBGeneralDto.Location,
        //         new ImageEntity(responseDto.JBGeneralDto.OutdoorImageResponseDto.Id, responseDto.JBGeneralDto.OutdoorImageResponseDto.Name, responseDto.JBGeneralDto.OutdoorImageResponseDto.Url),
        //         responseDto.JBGeneralDto.ConnectionImageResponseDtos.Select(
        //             imageResponseDto => new ImageEntity(imageResponseDto.Id, imageResponseDto.Name, imageResponseDto.Url)).ToList()
        //         ),
        //         responseDto.AdditionalImageResponseDtos.Select(
        //             imageResponseDto => new ImageEntity(imageResponseDto.Id, imageResponseDto.Name, imageResponseDto.Url)).ToList()
        //     );
        // }

        //! Entity => Dto

        private DeviceResponseDto MapToResponseDto(DeviceEntity deviceEntity)
        {
            return new DeviceResponseDto(
                id: deviceEntity.Id,
                code: deviceEntity.Code,
                function: deviceEntity.Function,
                range: deviceEntity.Range,
                unit: deviceEntity.Unit,
                ioAddress: deviceEntity.IOAddress,
                moduleBasicDto: deviceEntity.ModuleEntity != null ? new ModuleBasicDto(
                    id: deviceEntity.ModuleEntity.Id,
                    name: deviceEntity.ModuleEntity.Name) : null,
                jbGeneralDto: deviceEntity.JBEntity != null
                ? new JBGeneralDto(
                       id: deviceEntity.JBEntity.Id,
                       name: deviceEntity.JBEntity.Name,
                   location: deviceEntity.JBEntity.Location,
                   outdoorImageResponseDto: deviceEntity.JBEntity.OutdoorImageEntity != null ? new ImageResponseDto(
                   id: deviceEntity.JBEntity.OutdoorImageEntity.Id,
                   name: deviceEntity.JBEntity.OutdoorImageEntity.Name,
                   url: deviceEntity.JBEntity.OutdoorImageEntity.Url) : null,
                connectionImageResponseDtos: deviceEntity.JBEntity.ConnectionImageEntities.Any() ? deviceEntity.JBEntity.ConnectionImageEntities.Select(imageEntity => new ImageResponseDto(
                    id: imageEntity.Id,
                    name: imageEntity.Name,
                    url: imageEntity.Url)).ToList() : new List<ImageResponseDto>()
                ) : null,
                additionalImageResponseDtos: deviceEntity.AdditionalConnectionImageEntities.Any() ? deviceEntity.AdditionalConnectionImageEntities.Select(imageEntity => new ImageResponseDto(
                        id: imageEntity.Id,
                        name: imageEntity.Name,
                        url: imageEntity.Url)).ToList() : new List<ImageResponseDto>()
            );
        }
        private DeviceBasicDto MapToBasicDto(DeviceEntity deviceEntity)
        {
            return new DeviceBasicDto(
                id: deviceEntity.Id,
                code: deviceEntity.Code
            );
        }
    }
}
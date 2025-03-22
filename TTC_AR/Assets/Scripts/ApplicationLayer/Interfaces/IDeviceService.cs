
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Device;
using Domain.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IDeviceService

    {
        //! Tham số là Dto, Dữ liệu trả về là Dto
        Task<DeviceResponseDto> GetDeviceByIdAsync(string deviceId);
        Task<List<DeviceBasicDto>> GetListDeviceGeneralAsync(string grapperId);
        Task<List<DeviceResponseDto>> GetListDeviceInformationFromGrapperAsync(string grapperId);
        Task<List<DeviceResponseDto>> GetListDeviceInformationFromModuleAsync(string moduleId);
        Task<bool> CreateNewDeviceAsync(string grapperId, DeviceRequestDto deviceRequestDto);
        Task<bool> UpdateDeviceAsync(string deviceId, DeviceRequestDto deviceRequestDto);
        Task<bool> DeleteDeviceAsync(string deviceId);
    }
}
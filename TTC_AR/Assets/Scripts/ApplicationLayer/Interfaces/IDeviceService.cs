
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
        Task<List<DeviceResponseDto>> GetListDeviceAsync(string grapperId);
        Task<bool> CreateNewDeviceAsync(string grapperId, DeviceRequestDto deviceRequestDto);
        Task<bool> UpdateDeviceAsync(string deviceId, DeviceRequestDto deviceRequestDto);
        Task<bool> DeleteDeviceAsync(string deviceId);
    }
}
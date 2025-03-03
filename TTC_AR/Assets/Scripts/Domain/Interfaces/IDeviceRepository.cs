
// Domain/Repositories/IDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceResponseDto> GetDeviceByIdAsync(int deviceId);
        Task<List<DeviceResponseDto>> GetListDeviceAsync(int grapperId);
        Task<bool> CreateNewDeviceAsync(int grapperId, DeviceEntity deviceEntity);
        Task<bool> UpdateDeviceAsync(int deviceId, DeviceEntity deviceEntity);
        Task<bool> DeleteDeviceAsync(int deviceId);
    }
}
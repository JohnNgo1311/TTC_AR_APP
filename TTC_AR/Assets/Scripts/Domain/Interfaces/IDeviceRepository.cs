
// Domain/Repositories/IDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceEntity> GetDeviceByIdAsync(int deviceId);
        Task<List<DeviceEntity>> GetListDeviceAsync(int grapperId);
        Task<bool> CreateNewDeviceAsync(int grapperId, DeviceEntity deviceEntity);
        Task<bool> UpdateDeviceAsync(int deviceId, DeviceEntity deviceEntity);
        Task<bool> DeleteDeviceAsync(int deviceId);
    }
}
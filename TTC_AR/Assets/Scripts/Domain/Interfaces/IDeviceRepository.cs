
// Domain/Repositories/IDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeviceRepository
    {
        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        Task<DeviceEntity> GetDeviceByIdAsync(string deviceId);
        Task<List<DeviceEntity>> GetListDeviceAsync(string grapperId);
        Task<bool> CreateNewDeviceAsync(string grapperId, DeviceEntity deviceEntity);
        Task<bool> UpdateDeviceAsync(string deviceId, DeviceEntity deviceEntity);
        Task<bool> DeleteDeviceAsync(string deviceId);
    }
}

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
        Task<DeviceEntity> GetDeviceByIdAsync(int deviceId);
        Task<List<DeviceEntity>> GetListDeviceAsync(int grapperId);
        Task<bool> CreateNewDeviceAsync(int grapperId, DeviceEntity deviceEntity);
        Task<bool> UpdateDeviceAsync(int deviceId, DeviceEntity deviceEntity);
        Task<bool> DeleteDeviceAsync(int deviceId);
    }
}
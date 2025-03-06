
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services

{

    //! Không bắt lỗi tại đây



    public class DeviceService : IDeviceService
    {
        private readonly DeviceUseCase _DeviceUseCase;
        public DeviceService(DeviceUseCase DeviceUseCase)
        {
            _DeviceUseCase = DeviceUseCase;
        }

        //! Tham số là Dto, Dữ liệu trả về là Dto
        public async Task<DeviceResponseDto> GetDeviceByIdAsync(int id)
        {
            return await _DeviceUseCase.GetDeviceByIdAsync(id);
        }
        public async Task<List<DeviceResponseDto>> GetListDeviceAsync(int grapperId)
        {
            return await _DeviceUseCase.GetListDeviceAsync(grapperId);
        }
        public async Task<bool> CreateNewDeviceAsync(int grapperId, DeviceRequestDto DeviceRequestDto)
        {
            return await _DeviceUseCase.CreateNewDeviceAsync(grapperId, DeviceRequestDto);
        }
        public async Task<bool> UpdateDeviceAsync(int DeviceId, DeviceRequestDto DeviceRequestDto)
        {
            return await _DeviceUseCase.UpdateDeviceAsync(DeviceId, DeviceRequestDto);
        }
        public async Task<bool> DeleteDeviceAsync(int DeviceId)
        {
            return await _DeviceUseCase.DeleteDeviceAsync(DeviceId);
        }
    }

}
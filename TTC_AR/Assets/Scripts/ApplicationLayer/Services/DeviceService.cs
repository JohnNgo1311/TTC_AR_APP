
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
        public async Task<DeviceResponseDto> GetDeviceByIdAsync(string id)
        {
            return await _DeviceUseCase.GetDeviceByIdAsync(id);
        }
        public async Task<List<DeviceBasicDto>> GetListDeviceGeneralAsync(string grapperId)
        {
            return await _DeviceUseCase.GetListDeviceGeneralAsync(grapperId);
        }
        public async Task<List<DeviceResponseDto>> GetListDeviceInformationFromGrapperAsync(string grapperId)
        {
            return await _DeviceUseCase.GetListDeviceInformationFromGrapperAsync(grapperId);
        }
        public async Task<List<DeviceResponseDto>> GetListDeviceInformationFromModuleAsync(string moduleId)
        {
            return await _DeviceUseCase.GetListDeviceInformationFromModuleAsync(moduleId);
        }
        public async Task<bool> CreateNewDeviceAsync(string grapperId, DeviceRequestDto DeviceRequestDto)
        {
            return await _DeviceUseCase.CreateNewDeviceAsync(grapperId, DeviceRequestDto);
        }
        public async Task<bool> UpdateDeviceAsync(string DeviceId, DeviceRequestDto DeviceRequestDto)
        {
            return await _DeviceUseCase.UpdateDeviceAsync(DeviceId, DeviceRequestDto);
        }
        public async Task<bool> DeleteDeviceAsync(string DeviceId)
        {
            return await _DeviceUseCase.DeleteDeviceAsync(DeviceId);
        }
    }

}
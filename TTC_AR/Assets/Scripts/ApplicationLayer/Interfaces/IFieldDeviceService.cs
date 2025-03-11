
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.FieldDevice;

namespace ApplicationLayer.Interfaces
{
    public interface IFieldDeviceService
    {
        Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(string fieldDeviceId);
        Task<List<FieldDeviceBasicDto>> GetListFieldDeviceAsync(string grapperId);
        Task<bool> CreateNewFieldDeviceAsync(string grapperId, FieldDeviceRequestDto fieldDeviceRequestDto);
        Task<bool> UpdateFieldDeviceAsync(string fieldDeviceId, FieldDeviceRequestDto fieldDeviceRequestDto);
        Task<bool> DeleteFieldDeviceAsync(string fieldDeviceId);
    }
}
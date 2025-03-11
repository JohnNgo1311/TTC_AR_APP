
// Domain/Repositories/IFieldDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.FieldDevice;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFieldDeviceRepository
    {
        // Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(int fieldDeviceId);
        // Task<List<FieldDeviceResponseDto>> GetListFieldDeviceAsync(int grapperId);
        Task<FieldDeviceEntity> GetFieldDeviceByIdAsync(string fieldDeviceId);
        Task<List<FieldDeviceEntity>> GetListFieldDeviceAsync(string grapperId);
        Task<bool> CreateNewFieldDeviceAsync(string grapperId, FieldDeviceEntity fieldDeviceEntity);
        Task<bool> UpdateFieldDeviceAsync(string fieldDeviceId, FieldDeviceEntity fieldDeviceEntity);
        Task<bool> DeleteFieldDeviceAsync(string fieldDeviceId);
    }
}
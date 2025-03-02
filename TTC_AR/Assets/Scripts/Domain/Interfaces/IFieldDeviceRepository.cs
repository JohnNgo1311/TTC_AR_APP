
// Domain/Repositories/IFieldDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFieldDeviceRepository
    {
        Task<FieldDeviceEntity> GetFieldDeviceByIdAsync(int fieldDeviceId);
        Task<List<FieldDeviceEntity>> GetListFieldDeviceAsync(int grapperId);
        Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceEntity fieldDeviceEntity);
        Task<bool> UpdateFieldDeviceAsync(int fieldDeviceId, FieldDeviceEntity fieldDeviceEntity);
        Task<bool> DeleteFieldDeviceAsync(int fieldDeviceId);
    }
}
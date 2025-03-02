
// Domain/Repositories/IJBRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJBRepository
    {
        public Task<JBEntity> GetByIdAsync(int JBId);
        public Task<List<JBEntity>> GetListJBAsync(int grapperId);
        public Task<bool> CreateNewJBAsync(int grapperId, JBEntity JBEntity);
        public Task<bool> UpdateJBAsync(int JBId, JBEntity JBEntity);
        public Task<bool> DeleteJBAsync(int JBId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services
{
    public class JBService : IJBService
    {
        private readonly JBUseCase _JBUseCase;
        public JBService(JBUseCase JBUseCase)
        {
            _JBUseCase = JBUseCase;
        }
        public async Task<JBResponseDto> GetJBByIdAsync(int id)
        {
            return await _JBUseCase.GetJBByIdAsync(id);
        }

        public async Task<List<JBResponseDto>> GetListJBAsync(int grapperId)
        {
            return await _JBUseCase.GetListJBAsync(grapperId);
        }

        public async Task<bool> CreateNewJBAsync(int grapperId, JBRequestDto JBRequestDto)
        {
            return await _JBUseCase.CreateNewJBAsync(grapperId, JBRequestDto);
        }
        public async Task<bool> UpdateJBAsync(int JBId, JBRequestDto JBRequestDto)
        {
            return await _JBUseCase.UpdateJBAsync(JBId, JBRequestDto);
        }
        public async Task<bool> DeleteJBAsync(int JBId)
        {
            return await _JBUseCase.DeleteJBAsync(JBId);
        }
    }

}
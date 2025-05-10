
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services
{  //! Không bắt lỗi tại đây
    public class JBService : IJBService
    {
        private readonly JBUseCase _JBUseCase;
        public JBService(JBUseCase JBUseCase)
        {
            _JBUseCase = JBUseCase;
        }


        //! Dữ liệu trả về là Dto
        public async Task<JBResponseDto> GetJBByIdAsync(string JBid)
        {
            return await _JBUseCase.GetJBByIdAsync(JBid);
        }

        public async Task<IEnumerable<JBGeneralDto>> GetListJBInformationAsync(string grapperId)
        {
            return await _JBUseCase.GetListJBInforAsync(grapperId);
        }
        public async Task<List<JBBasicDto>> GetListJBGeneralAsync(string grapperId)
        {
            return await _JBUseCase.GetListJBGeneralAsync(grapperId);
        }

        public async Task<bool> CreateNewJBAsync(string grapperId, JBRequestDto JBRequestDto)
        {
            return await _JBUseCase.CreateNewJBAsync(grapperId, JBRequestDto);
        }
        public async Task<bool> UpdateJBAsync(string JBId, JBRequestDto JBRequestDto)
        {
            return await _JBUseCase.UpdateJBAsync(JBId, JBRequestDto);
        }
        public async Task<bool> DeleteJBAsync(string JBId)
        {
            return await _JBUseCase.DeleteJBAsync(JBId);
        }
    }

}
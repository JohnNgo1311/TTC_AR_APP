
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Interfaces;
using ApplicationLayer.UseCases;

namespace ApplicationLayer.Services

{

    //! Không bắt lỗi tại đây



    public class MccService : IMccService
    {
        private readonly MccUseCase _MccUseCase;
        public MccService(MccUseCase MccUseCase)
        {
            _MccUseCase = MccUseCase;
        }

        //! Tham số là Dto, Dữ liệu trả về là Dto
        public async Task<MccResponseDto> GetMccByIdAsync(string id)
        {
            return await _MccUseCase.GetMccByIdAsync(id);
        }
        public async Task<IEnumerable<MccBasicDto>> GetListMccAsync(string grapperId)
        {
            return await _MccUseCase.GetListMccAsync(grapperId);
        }
        public async Task<bool> CreateNewMccAsync(string grapperId, MccRequestDto MccRequestDto)
        {
            return await _MccUseCase.CreateNewMccAsync(grapperId, MccRequestDto);
        }
        public async Task<bool> UpdateMccAsync(string MccId, MccRequestDto MccRequestDto)
        {
            return await _MccUseCase.UpdateMccAsync(MccId, MccRequestDto);
        }
        public async Task<bool> DeleteMccAsync(string MccId)
        {
            return await _MccUseCase.DeleteMccAsync(MccId);
        }
    }

}

using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos.Mcc;
using Domain.Entities;
using ApplicationLayer.Dtos.Image;
using System.Collections.Generic;
using ApplicationLayer.Dtos.FieldDevice;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class MccUseCase
    {
        private readonly IMccRepository _MccRepository;

        public MccUseCase(IMccRepository MccRepository)
        {
            _MccRepository = MccRepository;
        }

        public async Task<List<MccBasicDto>> GetListMccAsync(string grapperId)
        {
            var entities = await _MccRepository.GetListMccAsync(grapperId);

            var MccBasicDtos = entities.Select(entity => MapToBasicDto(entity)).ToList();

            return MccBasicDtos;
        }


        public async Task<MccResponseDto> GetMccByIdAsync(string MccId)
        {
            var entity = await _MccRepository.GetMccByIdAsync(MccId);

            var MccResponseDto = MapToResponseDto(entity);

            return MccResponseDto;
        }

        public async Task<bool> CreateNewMccAsync(string grapperId, MccRequestDto requestDto)
        {
            //! Không truyền List<MccEntity> MccEntities vào MccEntity để cho giá trị bị null
            //! Khi đó MccEntity chỉ có Id và CabinetCode

            var MccEntity = MapRequestToEntity(requestDto);

            var createdEntity = await _MccRepository.CreateNewMccAsync(grapperId, MccEntity);

            return createdEntity;
            // return new MccResponseDto(
            //     createdEntity.Id,
            //     createdEntity.Name,
            //     new MccBasicDto(createdEntity.Mcc.Id, createdEntity.Mcc.CabinetCode),
            //     createdEntity.RatedPower,
            //     createdEntity.RatedCurrent,
            //     createdEntity.ActiveCurrent,
            //     createdEntity.ConnectionImages.Select(img => new ImageResponseDto(img.Id, img.Name, img.Url)).ToList(),
            //     createdEntity.Note
            // );
        }

        public async Task<bool> UpdateMccAsync(string MccId, MccRequestDto requestDto)
        {
            MccId = GlobalVariable.MccId;

            var MccEntity = MapRequestToEntity(requestDto);

            var updatedEntity = await _MccRepository.UpdateMccAsync(MccId, MccEntity);

            return updatedEntity;

            // return new MccResponseDto(
            //     updatedEntity.Id,
            //     updatedEntity.Name,
            //     new MccBasicDto(updatedEntity.Mcc.Id, updatedEntity.Mcc.CabinetCode),
            //     updatedEntity.RatedPower,
            //     updatedEntity.RatedCurrent,
            //     updatedEntity.ActiveCurrent,
            //     updatedEntity.ConnectionImages.Select(img => new ImageResponseDto(img.Id, img.Name, img.Url)).ToList(),
            //     updatedEntity.Note
            // );
        }

        public async Task<bool> DeleteMccAsync(string MccId)
        {
            // MccId = GlobalVariable.MccId;

            var deletedEntity = await _MccRepository.DeleteMccAsync(MccId);

            return deletedEntity;
        }

        //! Dto => Entity
        private MccEntity MapRequestToEntity(MccRequestDto MccRequestDto)
        {
            return new MccEntity(
            cabinetCode: MccRequestDto.CabinetCode,

            fieldDeviceEntities: (MccRequestDto.FieldDeviceBasicDtos == null || (MccRequestDto.FieldDeviceBasicDtos != null && MccRequestDto.FieldDeviceBasicDtos.Count <= 0)) ?
            new List<FieldDeviceEntity>()
            : MccRequestDto.FieldDeviceBasicDtos.Select(fd => new FieldDeviceEntity(fd.Id.ToString(), fd.Name)).ToList(),

            brand: MccRequestDto.Brand,

            note: MccRequestDto.Note
            );
        }
        //! Entity => Dto
        private MccBasicDto MapToBasicDto(MccEntity entity)
        {
            return new MccBasicDto(
                entity.Id,
                entity.CabinetCode
            );
        }
        private MccResponseDto MapToResponseDto(MccEntity entity)
        {
            return new MccResponseDto(
              id: entity.Id,
              cabinetCode: entity.CabinetCode,
              brand: entity.Brand,
              fieldDeviceBasicDtos: entity.FieldDeviceEntities.Count > 0 ? entity.FieldDeviceEntities.Select(fd => new FieldDeviceBasicDto(fd.Id, fd.Name)).ToList() : new List<FieldDeviceBasicDto>(),
              note: entity.Note
            );
        }
    }
}
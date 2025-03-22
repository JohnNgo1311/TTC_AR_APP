
using System.Linq;
using System.Threading.Tasks;

using ApplicationLayer.Dtos.FieldDevice;
using Domain.Entities;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Dtos.Image;
using System.Collections.Generic;
using Domain.Interfaces;
using System.Diagnostics;

namespace ApplicationLayer.UseCases
{
    public class FieldDeviceUseCase
    {
        private readonly IFieldDeviceRepository _fieldDeviceRepository;

        public FieldDeviceUseCase(IFieldDeviceRepository fieldDeviceRepository)
        {
            _fieldDeviceRepository = fieldDeviceRepository;
        }

        public async Task<List<FieldDeviceBasicDto>> GetListFieldDeviceAsync(string grapperId)
        {
            var entities = await _fieldDeviceRepository.GetListFieldDeviceAsync(grapperId);

            var fieldDeviceBasicDtos = entities.Select(entity => MapToBasicDto(entity)).ToList();

            GlobalVariable.temp_Dictionary_FieldDeviceInformationModel =
            fieldDeviceBasicDtos.ToDictionary(dto => dto.Name, dto => new FieldDeviceInformationModel(dto.Id, dto.Name));

            GlobalVariable.temp_List_FieldDeviceInformationModel = fieldDeviceBasicDtos.Select(dto => new FieldDeviceInformationModel(dto.Id, dto.Name)).ToList();

            UnityEngine.Debug.Log(GlobalVariable.temp_Dictionary_FieldDeviceInformationModel.Count);
            UnityEngine.Debug.Log(GlobalVariable.temp_List_FieldDeviceInformationModel.Count);

            return fieldDeviceBasicDtos;
        }


        public async Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(string fieldDeviceId)
        {
            var entity = await _fieldDeviceRepository.GetFieldDeviceByIdAsync(fieldDeviceId);

            var fieldDeviceResponseDto = MapToResponseDto(entity);

            return fieldDeviceResponseDto;
        }

        public async Task<bool> CreateNewFieldDeviceAsync(string grapperId, FieldDeviceRequestDto requestDto)
        {
            //! Không truyền List<FieldDeviceEntity> fieldDeviceEntities vào MccEntity để cho giá trị bị null
            //! Khi đó MccEntity chỉ có Id và CabinetCode

            var fieldDeviceEntity = MapRequestToEntity(requestDto);

            var createdEntity = await _fieldDeviceRepository.CreateNewFieldDeviceAsync(grapperId, fieldDeviceEntity);

            return createdEntity;
            // return new FieldDeviceResponseDto(
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

        public async Task<bool> UpdateFieldDeviceAsync(string fieldDeviceId, FieldDeviceRequestDto requestDto)
        {
            fieldDeviceId = GlobalVariable.FieldDeviceId;

            var fieldDeviceEntity = MapRequestToEntity(requestDto);

            var updatedEntity = await _fieldDeviceRepository.UpdateFieldDeviceAsync(fieldDeviceId, fieldDeviceEntity);

            return updatedEntity;

            // return new FieldDeviceResponseDto(
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

        public async Task<bool> DeleteFieldDeviceAsync(string fieldDeviceId)
        {

            var deletedEntity = await _fieldDeviceRepository.DeleteFieldDeviceAsync(fieldDeviceId);

            return deletedEntity;
        }

        //! Dto => Entity
        private FieldDeviceEntity MapRequestToEntity(FieldDeviceRequestDto fieldDeviceRequestDto)
        {
            return new FieldDeviceEntity(
                name: fieldDeviceRequestDto.Name,
                ratedPower: fieldDeviceRequestDto.RatedPower,
                ratedCurrent: fieldDeviceRequestDto.RatedCurrent,
                activeCurrent: fieldDeviceRequestDto.ActiveCurrent,
                connectionImageEntities: (fieldDeviceRequestDto.ConnectionImageBasicDtos == null || (fieldDeviceRequestDto.ConnectionImageBasicDtos != null && fieldDeviceRequestDto.ConnectionImageBasicDtos.Count <= 0)) ? new List<ImageEntity>() :
                fieldDeviceRequestDto.ConnectionImageBasicDtos.Select(i => new ImageEntity(i.Id, i.Name)).ToList(),
                note: fieldDeviceRequestDto.Note
            );
        }
        //! Entity => Dto
        private FieldDeviceBasicDto MapToBasicDto(FieldDeviceEntity entity)
        {
            return new FieldDeviceBasicDto(
            id: entity.Id,
              name: entity.Name
            );
        }
        private FieldDeviceResponseDto MapToResponseDto(FieldDeviceEntity entity)
        {
            return new FieldDeviceResponseDto(
               id: entity.Id,
              name: entity.Name,
              mcc: entity.MccEntity == null ? null : new MccBasicDto(entity.MccEntity.Id, entity.MccEntity.CabinetCode),
              ratedPower: entity.RatedPower,
              ratedCurrent: entity.RatedCurrent,
              activeCurrent: entity.ActiveCurrent,
              connectionImages: entity.ConnectionImageEntities.Count > 0 ? entity.ConnectionImageEntities.Select(img => new ImageResponseDto(img.Id, img.Name, img.Url)).ToList() : new List<ImageResponseDto>(),
              note: entity.Note
            );
        }
    }
}
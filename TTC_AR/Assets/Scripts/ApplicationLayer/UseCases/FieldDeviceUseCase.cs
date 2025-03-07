// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ApplicationLayer.Dtos;
// using ApplicationLayer.Dtos.FieldDevice;
// using ApplicationLayer.Dtos.Image;
// using ApplicationLayer.Dtos.Mcc;
// using Domain.Entities;
// using Domain.Interfaces;

// namespace ApplicationLayer.UseCases
// {
//     public class FieldDeviceUseCase
//     {
//         private IFieldDeviceRepository _IFieldDeviceRepository;

//         public FieldDeviceUseCase(IFieldDeviceRepository IFieldDeviceRepository)
//         {
//             _IFieldDeviceRepository = IFieldDeviceRepository;
//         }
//         #region  Get List
//         public async Task<List<FieldDeviceResponseDto>> GetListFieldDeviceAsync(int grapperId)
//         {
//             try
//             {
//                 var fieldDeviceResponseDtos = await _IFieldDeviceRepository.GetListFieldDeviceAsync(grapperId);

//                 if (fieldDeviceResponseDtos == null)
//                 {
//                     throw new ApplicationException("Failed to get FieldDevice list");
//                 }
//                 else
//                 {
//                     return fieldDeviceResponseDtos;
//                     // var fieldDeviceEntities = fieldDeviceResponseDtos.Select(fd => MapToResponseEntity(fd)).ToList();
//                     // return fieldDeviceEntities.Select(entity => MapToResponseDto(entity)).ToList();
//                 }

//             }
//             catch (ArgumentException)
//             {
//                 throw; // Ném lại lỗi validation cho Unity xử lý
//             }
//             catch (Exception ex)
//             {
//                 throw new ApplicationException("Failed to get FieldDevice list", ex);
//             }
//         }
//         #endregion

//         #region  Get Specific
//         public async Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(int fieldDeviceId)
//         {
//             try
//             {
//                 var fieldDeviceResponseDto = await _IFieldDeviceRepository.GetFieldDeviceByIdAsync(fieldDeviceId);

//                 if (fieldDeviceResponseDto == null)
//                 {
//                     throw new ApplicationException("Failed to get FieldDevice");
//                 }
//                 else
//                 {
//                     return fieldDeviceResponseDto;
//                     // var fieldDeviceResponseEntity = MapToResponseEntity(fieldDeviceResponseDto);
//                     // return MapToResponseDto(fieldDeviceResponseEntity);
//                 }
//             }
//             catch (ArgumentException)
//             {
//                 throw; // Ném lại lỗi validation cho Unity xử lý
//             }
//             catch (Exception ex)
//             {
//                 throw new ApplicationException("Failed to get FieldDevice", ex); // Bao bọc lỗi từ Repository
//             }
//         }
//         #endregion


//         #region Create New 

//         public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceRequestDto requestDto)
//         {
//             try
//             {
//                 // POST: Không cần Id
//                 var entity = new FieldDeviceEntity(
//                     name: requestDto.Name,
//                     mccEntity: requestDto.MccBasicDto != null ? new MccEntity(requestDto.MccBasicDto.Id, requestDto.MccBasicDto.CabinetCode) : throw new ArgumentException("Mcc cannot be null"),
//                     ratedPower: requestDto.RatedPower,
//                     ratedCurrent: requestDto.RatedCurrent,
//                     activeCurrent: requestDto.ActiveCurrent,
//                     connectionImageEntities: requestDto.ConnectionImageBasicDtos?
//                         .Select(i => new ImageEntity(i.Id, i.Name)).ToList(),
//                     note: requestDto.Note
//                 );
//                 return await _IFieldDeviceRepository.CreateNewFieldDeviceAsync(grapperId, entity);
//             }
//             catch (ArgumentException ex) { throw ex; }
//             catch (Exception ex) { throw new ApplicationException("Failed to create FieldDevice", ex); }
//         }
//         // public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceRequestDto requestDto)
//         // {
//         //     try
//         //     {

//         //         // Ánh xạ DTO sang Entity để tận dụng validation
//         //         var createNewFieldDeviceEntity = MapRequestToEntity(requestDto);

//         //         var requestData = MapToRequestData(createNewFieldDeviceEntity);

//         //         var createdFieldDeviceResult = await _IFieldDeviceRepository.CreateNewFieldDeviceAsync(grapperId, requestData);

//         //         return createdFieldDeviceResult;

//         //     }
//         //     catch (ArgumentException)
//         //     {
//         //         throw; // Ném lại lỗi validation cho Unity xử lý
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         throw new ApplicationException("Failed to create FieldDevice", ex); // Bao bọc lỗi từ Repository
//         //     }
//         // }
//         #endregion

//         #region 
//         public async Task<bool> UpdateFieldDeviceAsync(int FieldDeviceId, FieldDeviceRequestDto requestDto)
//         {
//             // Validate
//             try
//             {
//                 // Ánh xạ DTO sang Entity để tận dụng validation
//                 var updateNewFieldDeviceEntity = MapRequestToEntity(requestDto);
//                 // var requestData = MapToRequestData(updateNewFieldDeviceEntity);
//                 var updatedFieldDeviceResult = await _IFieldDeviceRepository.UpdateFieldDeviceAsync(FieldDeviceId, updateNewFieldDeviceEntity);

//                 return updatedFieldDeviceResult;
//             }
//             catch (ArgumentException)
//             {
//                 throw; // Ném lại lỗi validation cho Unity xử lý
//             }
//             catch (Exception ex)
//             {
//                 throw new ApplicationException("Failed to update FieldDevice", ex); // Bao bọc lỗi từ Repository
//             }
//         }
//         #endregion

//         #region Delete
//         public async Task<bool> DeleteFieldDeviceAsync(int FieldDeviceId)
//         {
//             try
//             {
//                 var deletedFieldDeviceResult = await _IFieldDeviceRepository.DeleteFieldDeviceAsync(FieldDeviceId);
//                 return deletedFieldDeviceResult;
//             }
//             catch (ArgumentException)
//             {
//                 throw; // Ném lại lỗi validation cho Unity xử lý
//             }
//             catch (Exception ex)
//             {
//                 throw new ApplicationException("Failed to delete FieldDevice", ex); // Bao bọc lỗi từ Repository
//             }
//         }
//         #endregion


//         //! Dto => Entity
//         private FieldDeviceEntity MapRequestToEntity(FieldDeviceRequestDto fieldDeviceRequestDto)
//         {
//             return new FieldDeviceEntity(
//                 name: fieldDeviceRequestDto.Name,
//                 mccEntity: new MccEntity(fieldDeviceRequestDto.MccBasicDto.Id, fieldDeviceRequestDto.MccBasicDto.CabinetCode),
//                 ratedPower: fieldDeviceRequestDto.RatedPower,
//                 ratedCurrent: fieldDeviceRequestDto.RatedCurrent,
//                 activeCurrent: fieldDeviceRequestDto.ActiveCurrent,
//                 listConnectionImageEntities: fieldDeviceRequestDto.ConnectionImageBasicDtos.Select(i => new ImageEntity(i.Id, i.Name)).ToList(),
//                 note: fieldDeviceRequestDto.Note
//                 );


//         }
//         //! Entity => Dto
//         private FieldDeviceRequestDto MapToRequestData(FieldDeviceEntity entity)
//         {
//             return new FieldDeviceRequestDto(
//                 name: entity.Name,
//                 mccBasicDto: new MccBasicDto(entity.MccEntity.Id, entity.MccEntity.CabinetCode),
//                 ratedPower: string.IsNullOrEmpty(entity.RatedPower) ? string.Empty : entity.RatedPower,
//                 ratedCurrent: string.IsNullOrEmpty(entity.RatedCurrent) ? string.Empty : entity.RatedCurrent,
//                 activeCurrent: string.IsNullOrEmpty(entity.ActiveCurrent) ? string.Empty : entity.ActiveCurrent,
//                 connectionImageBasicDtos: entity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList(),
//                 note: string.IsNullOrEmpty(entity.Note) ? string.Empty : entity.Note
//             );
//         }

//         // private FieldDeviceEntity MapToResponseEntity(FieldDeviceResponseDto fieldDeviceResponseDto)
//         // {
//         //     return new FieldDeviceEntity(fieldDeviceResponseDto.Name)
//         //     {
//         //         Id = fieldDeviceResponseDto.Id,
//         //         Name = fieldDeviceResponseDto.Name,
//         //         Mcc = new MccEntity(fieldDeviceResponseDto.MccBasicDto.Id, fieldDeviceResponseDto.MccBasicDto.CabinetCode),
//         //         RatedPower = fieldDeviceResponseDto.RatedPower,
//         //         RatedCurrent = fieldDeviceResponseDto.RatedCurrent,
//         //         ActiveCurrent = fieldDeviceResponseDto.ActiveCurrent,
//         //         ListConnectionImageEntities = fieldDeviceResponseDto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.url)).ToList(),
//         //         Note = fieldDeviceResponseDto.Note,
//         //     };
//         // }
//         // private FieldDeviceResponseDto MapToResponseDto(FieldDeviceEntity fieldDeviceEntity)
//         // {
//         //     return new FieldDeviceResponseDto(
//         //       id: fieldDeviceEntity.Id,
//         //       name: fieldDeviceEntity.Name,
//         //       mcc: new MccBasicDto(fieldDeviceEntity.MccBasicDto.Id, fieldDeviceEntity.MccBasicDto.CabinetCode),
//         //       ratedPower: fieldDeviceEntity.RatedPower,
//         //       ratedCurrent: fieldDeviceEntity.RatedCurrent,
//         //       activeCurrent: fieldDeviceEntity.ActiveCurrent,
//         //       connectionImageResponseDtos: fieldDeviceEntity.ListConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList(),
//         //       note: fieldDeviceEntity.Note
//         //     );
//         // }

//     }
// }


using System.Linq;
using System.Threading.Tasks;

using ApplicationLayer.Dtos.FieldDevice;
using Domain.Entities;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Dtos.Image;
using Domain.Interfaces;
using System.Collections.Generic;

namespace ApplicationLayer.UseCases
{
    public class FieldDeviceUseCase
    {
        private readonly IFieldDeviceRepository _fieldDeviceRepository;

        public FieldDeviceUseCase(IFieldDeviceRepository fieldDeviceRepository)
        {
            _fieldDeviceRepository = fieldDeviceRepository;
        }

        public async Task<List<FieldDeviceBasicDto>> GetListFieldDeviceAsync(int grapperId)
        {
            var entities = await _fieldDeviceRepository.GetListFieldDeviceAsync(grapperId);

            var fieldDeviceBasicDtos = entities.Select(entity => MapToBasicDto(entity)).ToList();

            return fieldDeviceBasicDtos;
        }


        public async Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(int fieldDeviceId)
        {
            var entity = await _fieldDeviceRepository.GetFieldDeviceByIdAsync(fieldDeviceId);

            var fieldDeviceResponseDto = MapToResponseDto(entity);

            return fieldDeviceResponseDto;
        }

        public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceRequestDto requestDto)
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

        public async Task<bool> UpdateFieldDeviceAsync(int fieldDeviceId, FieldDeviceRequestDto requestDto)
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

        public async Task<bool> DeleteFieldDeviceAsync(int fieldDeviceId)
        {
            fieldDeviceId = GlobalVariable.FieldDeviceId;

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
                entity.Id,
                entity.Name
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
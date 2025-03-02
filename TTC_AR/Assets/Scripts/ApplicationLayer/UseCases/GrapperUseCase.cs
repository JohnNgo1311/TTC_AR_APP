// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ApplicationLayer.Dtos;
// using Domain.Entities;
// using Domain.Interfaces;

// public class JBUseCase
// {
//     private IJBRepository _jbRepository;

//     public JBUseCase(IJBRepository jbRepository)
//     {
//         _jbRepository = jbRepository;
//     }

//     public async Task<bool> CreateNewJBAsync(JBRequestDto requestDto)
//     {
//         // Validate
//         if (string.IsNullOrEmpty(requestDto.Name))
//             throw new ArgumentException("Name cannot be empty");
//         // Map DTO to Entity
//         var jbEntity = new JBEntity(requestDto.Name)
//         {
//             Location = requestDto.Location,
//             //! Mỗi Device chỉ cần Id và Code
//             DeviceEntities = requestDto.DeviceBasicDtos.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList(),
//             //! Mỗi Module chỉ cần Id và Name
//             ModuleEntities = requestDto.ModuleBasicDtos.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList(),
//             //! OutdoorImage chỉ cần Id và Name
//             OutdoorImageEntity = new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name),
//             //! Mỗi ConnectionImage chỉ cần Id và Name
//             ConnectionImageEntities = requestDto.ConnectionImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
//         };
//         var createdJBResult = await _jbRepository.CreateNewJBAsync(GlobalVariable.GrapperId, jbEntity);
//         return createdJBResult;
//     }
//     public async Task<bool> UpdateJBAsync(int JBId, JBRequestDto requestDto)
//     {
//         // Validate
//         if (string.IsNullOrEmpty(requestDto.Name))
//             throw new ArgumentException("Name cannot be empty");

//         // Map DTO to Entity
//         var jbEntity = new JBEntity(requestDto.Name)
//         {
//             Location = requestDto.Location,
//             //! Mỗi Device chỉ cần Id và Code
//             DeviceEntities = requestDto.DeviceBasicDtos.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList(),
//             //! Mỗi Module chỉ cần Id và Name
//             ModuleEntities = requestDto.ModuleBasicDtos.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList(),
//             //! OutdoorImage chỉ cần Id và Name
//             OutdoorImageEntity = new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name),
//             //! Mỗi ConnectionImage chỉ cần Id và Name
//             ConnectionImageEntities = requestDto.ConnectionImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
//         };
//         var createdJBResult = await _jbRepository.UpdateJBAsync(JBId, jbEntity);
//         return createdJBResult;
//     }

//     public async Task<bool> DeleteJBAsync(int JBId)
//     {
//         var deletedJBResult = await _jbRepository.DeleteJBAsync(JBId);
//         return deletedJBResult;
//     }


//     // private JBResponseDto MapToResponseDto(JBEntity jb)
//     // {
//     //     return new JBResponseDto(
//     //         id: jb.Id,
//     //         name: jb.Name,
//     //         location: jb.Location,
//     //         deviceBasicDtos: jb.DeviceEntities.Select(device => new DeviceBasicDto(device.Id, device.Code)).ToList(),
//     //         moduleBasicDtos: jb.ModuleEntities.Select(module => new ModuleBasicDto(module.Id, module.Name)).ToList(),
//     //         outdoorImageResponseDto: jb.OutdoorImageEntity != null
//     //             ? new ImageResponseDto(jb.OutdoorImageEntity.Id, jb.OutdoorImageEntity.Name)
//     //             : null,
//     //         connectionImageResponseDtos: jb.ConnectionImageEntities.Select(image => new ImageResponseDto(image.Id, image.Name)).ToList()

//     //     );
//     // }
// }
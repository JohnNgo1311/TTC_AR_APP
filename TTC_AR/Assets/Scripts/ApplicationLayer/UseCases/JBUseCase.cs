using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class JBUseCase
    {
        private IJBRepository _IJBRepository;

        public JBUseCase(IJBRepository jbRepository)
        {
            _IJBRepository = jbRepository;
        }
        public async Task<List<JBResponseDto>> GetListJBAsync(int grapperId)
        {
            var jbEntities = await _IJBRepository.GetListJBAsync(grapperId);
            return jbEntities.Select(jb => MapToResponseDto(jb)).ToList();
        }
        public async Task<JBResponseDto> GetJBByIdAsync(int JBId)
        {
            var jbEntity = await _IJBRepository.GetByIdAsync(JBId);
            return MapToResponseDto(jbEntity);
        }
        public async Task<bool> CreateNewJBAsync(int grapperId, JBRequestDto requestDto)
        {
            // Validate
            if (string.IsNullOrEmpty(requestDto.Name))
                throw new ArgumentException("Name cannot be empty");
            // Map DTO to Entity
            var jbEntity = new JBEntity(requestDto.Name)
            {
                Location = requestDto.Location,
                //! Mỗi Device chỉ cần Id và Code
                DeviceEntities = requestDto.DeviceBasicDtos.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList(),
                //! Mỗi Module chỉ cần Id và Name
                ModuleEntities = requestDto.ModuleBasicDtos.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList(),
                //! OutdoorImage chỉ cần Id và Name
                OutdoorImageEntity = new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name),
                //! Mỗi ConnectionImage chỉ cần Id và Name
                ConnectionImageEntities = requestDto.ConnectionImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
            };
            var createdJBResult = await _IJBRepository.CreateNewJBAsync(grapperId, jbEntity);
            return createdJBResult;
        }
        public async Task<bool> UpdateJBAsync(int JBId, JBRequestDto requestDto)
        {
            // Validate
            if (string.IsNullOrEmpty(requestDto.Name))
                throw new ArgumentException("Name cannot be empty");

            // Map DTO to Entity
            var jbEntity = new JBEntity(requestDto.Name)
            {
                Location = requestDto.Location,
                //! Mỗi Device chỉ cần Id và Code
                DeviceEntities = requestDto.DeviceBasicDtos.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList(),
                //! Mỗi Module chỉ cần Id và Name
                ModuleEntities = requestDto.ModuleBasicDtos.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList(),
                //! OutdoorImage chỉ cần Id và Name
                OutdoorImageEntity = new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name),
                //! Mỗi ConnectionImage chỉ cần Id và Name
                ConnectionImageEntities = requestDto.ConnectionImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
            };
            var createdJBResult = await _IJBRepository.UpdateJBAsync(JBId, jbEntity);
            return createdJBResult;
        }

        public async Task<bool> DeleteJBAsync(int JBId)
        {
            var deletedJBResult = await _IJBRepository.DeleteJBAsync(JBId);
            return deletedJBResult;
        }

        private JBResponseDto MapToResponseDto(JBEntity jbEntity)
        {
            return new JBResponseDto(
                id: jbEntity.Id,
                name: jbEntity.Name,
                location: jbEntity.Location,
                deviceBasicDtos: jbEntity.DeviceEntities.Select(device => new DeviceBasicDto(device.Id, device.Code)).ToList(),
                moduleBasicDtos: jbEntity.ModuleEntities.Select(module => new ModuleBasicDto(module.Id, module.Name)).ToList(),
                outdoorImageResponseDto: jbEntity.OutdoorImageEntity == null ? null :
                new ImageResponseDto(jbEntity.OutdoorImageEntity.Id, jbEntity.OutdoorImageEntity.Name, jbEntity.OutdoorImageEntity.Url),
                connectionImageResponseDtos:
                jbEntity.ConnectionImageEntities.Select(image => new ImageResponseDto(image.Id, image.Name, image.Url)).ToList()
            );
        }
    }
}
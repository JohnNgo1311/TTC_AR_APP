using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class JBUseCase
    {
        private IJBRepository _IJBRepository;

        public JBUseCase(IJBRepository IjbRepository)
        {
            _IJBRepository = IjbRepository;
        }
        public async Task<List<JBGeneralDto>> GetListJBAsync(int grapperId)
        {
            try
            {
                var jBEntities = await _IJBRepository.GetListJBAsync(grapperId);

                if (jBEntities == null)
                {
                    throw new ApplicationException("Failed to get JB list");
                }
                else
                {  // Ánh xạ từ JBResponseDto sang JBEntity (nếu cần logic nghiệp vụ) rồi sang JBResponseDto
                   // var jbEntities = jbListDtos.Select(dto => new JBEntity(dto.Name)
                   // {
                   //     Id = dto.Id,

                    //     Location = dto.Location,

                    //     DeviceEntities = dto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),

                    //     ModuleEntities = dto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),

                    //     OutdoorImageEntity = dto.OutdoorImageResponseDto != null
                    //         ? new ImageEntity(dto.OutdoorImageResponseDto.Id, dto.OutdoorImageResponseDto.Name, dto.OutdoorImageResponseDto.Url)
                    //         : null,

                    //     ConnectionImageEntities = dto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.Url)).ToList()

                    // }).ToList();

                    // // Ánh xạ từ JBEntity sang JBResponseDto
                    var jbResponseDtos = jBEntities.Select(jbEntity => MapToGeneralDto(jbEntity)).ToList();
                    return jbResponseDtos;
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get JB list", ex);
            }
        }
        public async Task<JBResponseDto> GetJBByIdAsync(int JBId)
        {
            try
            {
                var jbEntity = await _IJBRepository.GetJBByIdAsync(JBId);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to get JB");
                }
                else
                {
                    // Ánh xạ từ jbEntity sang JBEntity để check các lỗi nghiệp vụ
                    var jbResponseDto = MapToResponseDto(jbEntity);
                    // Ánh xạ từ JBEntity sang JBResponseDto để đưa giá trị trả về
                    return jbResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewJBAsync(int grapperId, JBRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ JBRequestDto sang JBEntity để check các nghiệp vụ
                var jbEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(jbEntity);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to create JB cause jbEntity is Null");
                }

                else
                {
                    return await _IJBRepository.CreateNewJBAsync(grapperId, jbEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateJBAsync(int JBId, JBRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ JBRequestDto sang JBEntity để check các nghiệp vụ
                var jbEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(jbEntity);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to update JB cause jbEntity is Null");
                }

                else
                {
                    return await _IJBRepository.UpdateJBAsync(JBId, jbEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteJBAsync(int JBId)
        {
            try
            {
                var deletedJBResult = await _IJBRepository.DeleteJBAsync(JBId);
                return deletedJBResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete JB", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Dto => Entity
        private JBEntity MapRequestToEntity(JBRequestDto jBRequestDto)
        {
            return new JBEntity(
                jBRequestDto.Name,
                 string.IsNullOrEmpty(jBRequestDto.Location) ? "chưa cập nhật" : jBRequestDto.Location,
                jBRequestDto.DeviceBasicDtos == null ? null : jBRequestDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                jBRequestDto.ModuleBasicDtos == null ? null : jBRequestDto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),
                jBRequestDto.OutdoorImageBasicDto == null ? null : new ImageEntity(jBRequestDto.OutdoorImageBasicDto.Id, jBRequestDto.OutdoorImageBasicDto.Name),
                jBRequestDto.ConnectionImageBasicDtos == null ? null : jBRequestDto.ConnectionImageBasicDtos.Select(i => new ImageEntity(i.Id, i.Name)).ToList()
            );
        }

        // private JBEntity MapToResponseEntity(JBResponseDto jBResponseDto)
        // {
        //     return new JBEntity(
        //         jBResponseDto.Id,
        //         jBResponseDto.Name,
        //         jBResponseDto.Location,
        //         jBResponseDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
        //         jBResponseDto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),
        //         jBResponseDto.OutdoorImageResponseDto != null ? new ImageEntity(jBResponseDto.OutdoorImageResponseDto.Id, jBResponseDto.OutdoorImageResponseDto.Name, jBResponseDto.OutdoorImageResponseDto.Url) : null!,
        //        jBResponseDto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.Url)).ToList()
        //     );
        // }


        //! Entity => Dto
        private JBResponseDto MapToResponseDto(JBEntity jBEntity)
        {
            return new JBResponseDto(
                jBEntity.Id,

                jBEntity.Name,

                jBEntity.Location ?? "chưa cập nhật",

               (jBEntity.DeviceEntities == null || (jBEntity.DeviceEntities != null && jBEntity.DeviceEntities.Count <= 0)) ?
                 new List<DeviceBasicDto>() : jBEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),

                (jBEntity.ModuleEntities == null || (jBEntity.ModuleEntities != null && jBEntity.ModuleEntities.Count <= 0)) ?
                 new List<ModuleBasicDto>() : jBEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),

                jBEntity.OutdoorImageEntity == null ?
                 null : new ImageResponseDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name, jBEntity.OutdoorImageEntity.Url),

                (jBEntity.ConnectionImageEntities == null || (jBEntity.ConnectionImageEntities != null && jBEntity.ConnectionImageEntities.Count <= 0)) ?
                 new List<ImageResponseDto>() : jBEntity.ConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList()
            );
        }

        private JBGeneralDto MapToGeneralDto(JBEntity jBEntity)
        {
            return new JBGeneralDto(
                jBEntity.Id,

                jBEntity.Name,

                jBEntity.Location ?? "chưa cập nhật",

                jBEntity.OutdoorImageEntity == null ?
                 null : new ImageResponseDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name, jBEntity.OutdoorImageEntity.Url),

                (jBEntity.ConnectionImageEntities == null || (jBEntity.ConnectionImageEntities != null && jBEntity.ConnectionImageEntities.Count <= 0)) ?
                 new List<ImageResponseDto>() : jBEntity.ConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList()
             );
        }
        // private JBRequestDto MapToRequestDto(JBEntity jBEntity)
        // {
        //     return new JBRequestDto(
        //         jBEntity.Name,
        //         jBEntity.Location ?? "chưa cập nhật",
        //         jBEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
        //         jBEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
        //         jBEntity.OutdoorImageEntity != null ? new ImageBasicDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name) : null!,
        //         jBEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList()
        //     );
        // }


    }
}
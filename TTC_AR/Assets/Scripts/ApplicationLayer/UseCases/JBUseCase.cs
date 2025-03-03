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

        public JBUseCase(IJBRepository IjbRepository)
        {
            _IJBRepository = IjbRepository;
        }
        public async Task<List<JBResponseDto>> GetListJBAsync(int grapperId)
        {
            try
            {
                var jbListDtos = await _IJBRepository.GetListJBAsync(grapperId);

                if (jbListDtos == null)
                {
                    throw new ApplicationException("Failed to get JB list");
                }
                else
                {  // Ánh xạ từ JBResponseDto sang JBEntity (nếu cần logic nghiệp vụ) rồi sang JBResponseDto
                    var jbEntities = jbListDtos.Select(dto => new JBEntity(dto.Name)
                    {
                        Id = dto.Id,

                        Location = dto.Location,

                        DeviceEntities = dto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),

                        ModuleEntities = dto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),

                        OutdoorImageEntity = dto.OutdoorImageResponseDto != null
                            ? new ImageEntity(dto.OutdoorImageResponseDto.Id, dto.OutdoorImageResponseDto.Name, dto.OutdoorImageResponseDto.url)
                            : null,

                        ConnectionImageEntities = dto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.url)).ToList()

                    }).ToList();

                    // Ánh xạ từ JBEntity sang JBResponseDto
                    return jbEntities.Select(jb => new JBResponseDto(
                        jb.Id, jb.Name, jb.Location,
                        jb.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
                        jb.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
                        jb.OutdoorImageEntity != null ? new ImageResponseDto(jb.OutdoorImageEntity.Id, jb.OutdoorImageEntity.Name, jb.OutdoorImageEntity.Url) : null!,
                        jb.ConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList()
                    )).ToList();
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
                var jBDto = await _IJBRepository.GetJBByIdAsync(JBId);
                if (jBDto == null)
                {
                    throw new ApplicationException("Failed to get JB");
                }
                else
                {
                    // Ánh xạ từ JBResponseDto sang JBEntity để check các lỗi nghiệp vụ
                    var jbEntity = MapToResponseEntity(jBDto);
                    // Ánh xạ từ JBEntity sang JBResponseDto để đưa giá trị trả về
                    return MapToResponseDto(jbEntity);
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
                var jbEntity = new JBEntity(requestDto.Name) // Validation Name trong constructor
                {
                    Location = requestDto.Location, // Có thể "" nếu null
                    DeviceEntities = requestDto.DeviceBasicDtos?.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList() ?? new List<DeviceEntity>(),
                    ModuleEntities = requestDto.ModuleBasicDtos?.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList() ?? new List<ModuleEntity>(),
                    OutdoorImageEntity = requestDto.OutdoorImageBasicDto != null
                        ? new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name)
                        : null,
                    ConnectionImageEntities = requestDto.ConnectionImageBasicDtos?.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList() ?? new List<ImageEntity>()
                };

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
            // Validate
            try
            {
                if (string.IsNullOrEmpty(requestDto.Name))
                    throw new ArgumentException("Name cannot be empty");

                // Ánh xạ từ JBRequestDto sang JBEntity để check các nghiệp vụ
                var jbEntity = new JBEntity(requestDto.Name) // Validation Name trong constructor
                {
                    Location = requestDto.Location, // Có thể "" nếu null
                    DeviceEntities = requestDto.DeviceBasicDtos?.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList() ?? new List<DeviceEntity>(),
                    ModuleEntities = requestDto.ModuleBasicDtos?.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList() ?? new List<ModuleEntity>(),
                    OutdoorImageEntity = requestDto.OutdoorImageBasicDto != null
                        ? new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name)
                        : null,
                    ConnectionImageEntities = requestDto.ConnectionImageBasicDtos?.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList() ?? new List<ImageEntity>()
                };

                var createdJBResult = await _IJBRepository.UpdateJBAsync(JBId, jbEntity);
                return createdJBResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update JB", ex); // Bao bọc lỗi từ Repository
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

        private JBResponseDto MapToResponseDto(JBEntity jbEntity)
        {
            return new JBResponseDto(jbEntity.Id, jbEntity.Name, jbEntity.Location,
                jbEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
                jbEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
                jbEntity.OutdoorImageEntity != null ? new ImageResponseDto(jbEntity.OutdoorImageEntity.Id, jbEntity.OutdoorImageEntity.Name, jbEntity.OutdoorImageEntity.Url) : null!,
                jbEntity.ConnectionImageEntities.Select(i => new ImageResponseDto(i.Id, i.Name, i.Url)).ToList()
            );
        }
        private JBEntity MapToResponseEntity(JBResponseDto jBResponseDto)
        {
            return new JBEntity(
                jBResponseDto.Id,
                jBResponseDto.Name,
                jBResponseDto.Location,
                jBResponseDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                jBResponseDto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),
                jBResponseDto.OutdoorImageResponseDto != null ? new ImageEntity(jBResponseDto.OutdoorImageResponseDto.Id, jBResponseDto.OutdoorImageResponseDto.Name, jBResponseDto.OutdoorImageResponseDto.url) : null!,
                jBResponseDto.ConnectionImageResponseDtos.Select(i => new ImageEntity(i.Id, i.Name, i.url)).ToList()
            );
        }
        private JBEntity MapToEntity(JBRequestDto requestDto)
        {
            return new JBEntity(requestDto.Name)
            {
                Location = requestDto.Location,
                DeviceEntities = requestDto.DeviceBasicDtos.Select(dto => new DeviceEntity(dto.Id, dto.Code)).ToList(),
                ModuleEntities = requestDto.ModuleBasicDtos.Select(dto => new ModuleEntity(dto.Id, dto.Name)).ToList(),
                OutdoorImageEntity = new ImageEntity(requestDto.OutdoorImageBasicDto.Id, requestDto.OutdoorImageBasicDto.Name),
                ConnectionImageEntities = requestDto.ConnectionImageBasicDtos.Select(dto => new ImageEntity(dto.Id, dto.Name)).ToList()
            };
        }
    }
}
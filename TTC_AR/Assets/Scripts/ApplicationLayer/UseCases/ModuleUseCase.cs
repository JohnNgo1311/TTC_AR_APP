using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Module;
using ApplicationLayer.Dtos.ModuleSpecification;
using ApplicationLayer.Dtos.Rack;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class ModuleUseCase
    {
        private IModuleRepository _IModuleRepository;

        public ModuleUseCase(IModuleRepository IModuleRepository)
        {
            _IModuleRepository = IModuleRepository;
        }
        #region GET List Module
        public async Task<List<ModuleGeneralDto>> GetListModuleAsync(int grapperId)
        {
            try
            {
                var ModuleListGeneralDtos = await _IModuleRepository.GetListModuleAsync(grapperId);

                if (ModuleListGeneralDtos == null)
                {
                    throw new ApplicationException("Failed to get Module list");
                }
                else
                {  // Ánh xạ từ ModuleGeneralDto sang ModuleEntity để check các lỗi nghiệp vụ
                    var ModuleEntities = ModuleListGeneralDtos.Select(dto => MapGeneralToModuleEntity(dto)).ToList();
                    // Ánh xạ từ ModuleEntity sang ModuleGeneralDto để đưa giá trị trả về
                    return ModuleListGeneralDtos;

                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Module list", ex);
            }
        }
        #endregion

        #region GET Specific Module
        public async Task<ModuleResponseDto> GetModuleByIdAsync(int ModuleId)
        {
            try
            {
                var ModuleResponseDto = await _IModuleRepository.GetModuleByIdAsync(ModuleId);
                if (ModuleResponseDto == null)
                {
                    throw new ApplicationException("Failed to get Module");
                }
                else
                {
                    // Ánh xạ từ ModuleResponseDto sang ModuleEntity để check các lỗi nghiệp vụ
                    var ModuleEntity = MapResponseToModuleEntity(ModuleResponseDto);
                    // Ánh xạ từ ModuleEntity sang ModuleResponseDto để đưa giá trị trả về
                    return ModuleResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Module", ex); // Bao bọc lỗi từ Repository
            }
        }
        #endregion

        #region POST CREATE Module
        public async Task<bool> CreateNewModuleAsync(int grapperId, ModuleRequestDto requestDto)
        {
            try
            {
                // Ánh xạ từ ModuleRequestDto sang ModuleEntity để check các nghiệp vụ
                var ModuleEntity = MapRequestToModuleEntity(requestDto);
                var requestData = MapEntityToRequestDto(ModuleEntity);

                if (ModuleEntity == null)
                {
                    throw new ApplicationException("Failed to create Module cause ModuleEntity is Null");
                }
                else
                {
                    var createdModuleResult = await _IModuleRepository.CreateNewModuleAsync(grapperId, requestData);
                    if (createdModuleResult == false)
                    {
                        throw new ApplicationException("Failed to update Module");
                    }
                    else return true;
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Module", ex); // Bao bọc lỗi từ Repository
            }
        }
        #endregion

        #region  PUT UPDATE Module
        public async Task<bool> UpdateModuleAsync(int moduleId, ModuleRequestDto requestDto)
        {
            try
            {
                // Ánh xạ từ ModuleRequestDto sang ModuleEntity để check các nghiệp vụ
                var ModuleEntity = MapRequestToModuleEntity(requestDto);
                var requestData = MapEntityToRequestDto(ModuleEntity);

                if (ModuleEntity == null)
                {
                    throw new ApplicationException("Failed to create Module cause ModuleEntity is Null");
                }
                else
                {
                    var updatedModuleResult = await _IModuleRepository.UpdateModuleAsync(moduleId, requestData);

                    if (updatedModuleResult == false)
                    {
                        throw new ApplicationException("Failed to update Module");
                    }
                    else return true;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update Module", ex); // Bao bọc lỗi từ Repository
            }
        }
        #endregion
        #region DELETE Module
        public async Task<bool> DeleteModuleAsync(int ModuleId)
        {
            try
            {
                var deletedModuleResult = await _IModuleRepository.DeleteModuleAsync(ModuleId);
                return deletedModuleResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete Module", ex); // Bao bọc lỗi từ Repository
            }
        }
        #endregion

        //! Entity => Dto
        private ModuleRequestDto MapEntityToRequestDto(ModuleEntity moduleEntity)
        {
            return new ModuleRequestDto(
                moduleEntity.Name,
                new RackBasicDto(moduleEntity.RackEntity.Id, moduleEntity.RackEntity.Name),
                moduleEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
                moduleEntity.JBEntities.Select(j => new JBBasicDto(j.Id, j.Name)).ToList(),
                new ModuleSpecificationBasicDto(moduleEntity.ModuleSpecificationEntity.Id, moduleEntity.ModuleSpecificationEntity.Code),
                new AdapterSpecificationBasicDto(moduleEntity.AdapterSpecificationEntity.Id, moduleEntity.AdapterSpecificationEntity.Code)
            );

        }

        //! Dto => Entity
        private ModuleEntity MapResponseToModuleEntity(ModuleResponseDto moduleResponseDto)
        {
            return new ModuleEntity
            (moduleResponseDto.Id,
                moduleResponseDto.Name
            )
            {
                RackEntity = new RackEntity(moduleResponseDto.RackBasicDto.Id, moduleResponseDto.RackBasicDto.Name),
                DeviceEntities = moduleResponseDto.DeviceGeneralDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                JBEntities = moduleResponseDto.JBGeneralDtos.Select(j => new JBEntity(j.Id, j.Name)).ToList(),
                ModuleSpecificationEntity = new ModuleSpecificationEntity(
                    moduleResponseDto.ModuleSpecificationResponseDto.Id,
                    moduleResponseDto.ModuleSpecificationResponseDto.Code,
                    moduleResponseDto.ModuleSpecificationResponseDto.Type,
                    moduleResponseDto.ModuleSpecificationResponseDto.NumOfIO,
                    moduleResponseDto.ModuleSpecificationResponseDto.SignalType,
                    moduleResponseDto.ModuleSpecificationResponseDto.CompatibleTBUs,
                    moduleResponseDto.ModuleSpecificationResponseDto.OperatingVoltage,
                    moduleResponseDto.ModuleSpecificationResponseDto.OperatingCurrent,
                    moduleResponseDto.ModuleSpecificationResponseDto.FlexbusCurrent,
                    moduleResponseDto.ModuleSpecificationResponseDto.Alarm,
                    moduleResponseDto.ModuleSpecificationResponseDto.Code,
                    moduleResponseDto.ModuleSpecificationResponseDto.PdfManual

                    ),
                AdapterSpecificationEntity = new AdapterSpecificationEntity(
                    moduleResponseDto.AdapterSpecificationResponseDto.Id,
                    moduleResponseDto.AdapterSpecificationResponseDto.Code,
                    moduleResponseDto.AdapterSpecificationResponseDto.Type,
                    moduleResponseDto.AdapterSpecificationResponseDto.Communication,
                    moduleResponseDto.AdapterSpecificationResponseDto.NumOfModulesAllowed,
                    moduleResponseDto.AdapterSpecificationResponseDto.CommSpeed,
                    moduleResponseDto.AdapterSpecificationResponseDto.InputSupply,
                    moduleResponseDto.AdapterSpecificationResponseDto.OutputSupply,
                    moduleResponseDto.AdapterSpecificationResponseDto.InrushCurrent,
                    moduleResponseDto.AdapterSpecificationResponseDto.Alarm,
                    moduleResponseDto.AdapterSpecificationResponseDto.Note,
                    moduleResponseDto.AdapterSpecificationResponseDto.PdfManual
                    )
            };
        }
        private ModuleEntity MapRequestToModuleEntity(ModuleRequestDto moduleRequestDto)
        {
            return new ModuleEntity
            (
                moduleRequestDto.Name
            )
            {
                RackEntity = new RackEntity(moduleRequestDto.RackBasicDto.Id, moduleRequestDto.RackBasicDto.Name),
                DeviceEntities = moduleRequestDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                JBEntities = moduleRequestDto.JBBasicDtos.Select(j => new JBEntity(j.Id, j.Name)).ToList(),
                ModuleSpecificationEntity = new ModuleSpecificationEntity(moduleRequestDto.ModuleSpecificationBasicDto.Id, moduleRequestDto.ModuleSpecificationBasicDto.Code),
                AdapterSpecificationEntity = new AdapterSpecificationEntity(moduleRequestDto.AdapterSpecificationBasicDto.Id, moduleRequestDto.AdapterSpecificationBasicDto.Code)
            };

        }
        private ModuleEntity MapGeneralToModuleEntity(ModuleGeneralDto moduleGeneralDto)
        {
            return new ModuleEntity
            (moduleGeneralDto.Id,
                moduleGeneralDto.Name
            )
            {
                RackEntity = new RackEntity(moduleGeneralDto.RackBasicDto.Id, moduleGeneralDto.RackBasicDto.Name),
                DeviceEntities = moduleGeneralDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                JBEntities = moduleGeneralDto.JBBasicDtos.Select(j => new JBEntity(j.Id, j.Name)).ToList(),
                ModuleSpecificationEntity = new ModuleSpecificationEntity(moduleGeneralDto.ModuleSpecificationBasicDto.Id, moduleGeneralDto.ModuleSpecificationBasicDto.Code),
                AdapterSpecificationEntity = new AdapterSpecificationEntity(moduleGeneralDto.AdapterSpecificationBasicDto.Id, moduleGeneralDto.AdapterSpecificationBasicDto.Code)
            };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Grapper;
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
        public async Task<List<ModuleBasicDto>> GetListModuleAsync(string grapperId)
        {
            try
            {
                var moduleEntities = await _IModuleRepository.GetListModuleAsync(grapperId);

                if (moduleEntities == null)
                {
                    throw new ApplicationException("Failed to get Module list");
                }
                else
                {
                    var moduleBasicDtos = moduleEntities.Select(moduleEntity => MapEntityToBasicDto(moduleEntity)).ToList();
                    GlobalVariable.temp_List_ModuleInformationModel = moduleBasicDtos.Select(dto => new ModuleInformationModel(dto.Id, dto.Name)).ToList();
                    GlobalVariable.temp_Dictionary_ModuleInformationModel = moduleBasicDtos.ToDictionary(dto => dto.Name, dto => new ModuleInformationModel(dto.Id, dto.Name));
                    return moduleBasicDtos;
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
        public async Task<ModuleResponseDto> GetModuleByIdAsync(string ModuleId)
        {
            try
            {
                var moduleEntity = await _IModuleRepository.GetModuleByIdAsync(ModuleId);
                if (moduleEntity == null)
                {
                    throw new ApplicationException("Failed to get Module");
                }
                else
                {
                    // Ánh xạ từ moduleEntity sang ModuleEntity để check các lỗi nghiệp vụ
                    var ModuleResponseDto = MapEntityToResponseDto(moduleEntity);
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
        public async Task<bool> CreateNewModuleAsync(string grapperId, ModuleRequestDto requestDto)
        {
            try
            {
                // Ánh xạ từ ModuleRequestDto sang ModuleEntity để check các nghiệp vụ
                var ModuleEntity = MapRequestToModuleEntity(requestDto);

                if (ModuleEntity == null)
                {
                    throw new ApplicationException("Failed to create Module cause ModuleEntity is Null");
                }
                else
                {
                    var createdModuleResult = await _IModuleRepository.CreateNewModuleAsync(grapperId, ModuleEntity);

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
        public async Task<bool> UpdateModuleAsync(string moduleId, ModuleRequestDto requestDto)
        {
            try
            {
                // Ánh xạ từ ModuleRequestDto sang ModuleEntity để check các nghiệp vụ
                var ModuleEntity = MapRequestToModuleEntity(requestDto);

                if (ModuleEntity == null)
                {
                    throw new ApplicationException("Failed to create Module cause ModuleEntity is Null");
                }
                else
                {
                    var updatedModuleResult = await _IModuleRepository.UpdateModuleAsync(moduleId, ModuleEntity);

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
        public async Task<bool> DeleteModuleAsync(string ModuleId)
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
        private ModuleBasicDto MapEntityToBasicDto(ModuleEntity moduleEntity)
        {
            return new ModuleBasicDto(moduleEntity.Id, moduleEntity.Name);
        }
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

        private ModuleResponseDto MapEntityToResponseDto(ModuleEntity moduleEntity)
        {
            return new ModuleResponseDto(
            id: moduleEntity.Id,

            name: moduleEntity.Name,
            grapperBasicDto: new GrapperBasicDto(moduleEntity.GrapperEntity.Id, moduleEntity.GrapperEntity.Name),
            rackBasicDto: new RackBasicDto(moduleEntity.RackEntity.Id, moduleEntity.RackEntity.Name),
            deviceBasicDtos: moduleEntity.DeviceEntities.Count > 0 ? moduleEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList() : new List<DeviceBasicDto>(),
            jbBasicDtos: moduleEntity.JBEntities.Count > 0 ? moduleEntity.JBEntities.Select(j => new JBBasicDto(j.Id, j.Name)).ToList() : new List<JBBasicDto>(),
            moduleSpecificationResponseDto: MapToEntityToModuleSpecificationResponseDto(moduleEntity.ModuleSpecificationEntity),
            adapterSpecificationResponseDto: MapToEntityToAdapterSpecificationResponseDto(moduleEntity.AdapterSpecificationEntity)
            );
        }
        private ModuleSpecificationResponseDto MapToEntityToModuleSpecificationResponseDto(ModuleSpecificationEntity moduleSpecificationEntity)
        {
            return new ModuleSpecificationResponseDto(
             id: moduleSpecificationEntity.Id,
             code: moduleSpecificationEntity.Code,
              type: moduleSpecificationEntity.Type,
                numOfIO: moduleSpecificationEntity.NumOfIO,
                signalType: moduleSpecificationEntity.SignalType,
                compatibleTBUs: moduleSpecificationEntity.CompatibleTBUs,
                operatingVoltage: moduleSpecificationEntity.OperatingVoltage,
                operatingCurrent: moduleSpecificationEntity.OperatingCurrent,
                flexbusCurrent: moduleSpecificationEntity.FlexbusCurrent,
                alarm: moduleSpecificationEntity.Alarm,
                note: moduleSpecificationEntity.Note,
                pdfManual: moduleSpecificationEntity.PdfManual


                    );
        }
        private AdapterSpecificationResponseDto MapToEntityToAdapterSpecificationResponseDto(AdapterSpecificationEntity adapterSpecificationEntity)
        {
            return new AdapterSpecificationResponseDto(
                   id: adapterSpecificationEntity.Id,
                    code: adapterSpecificationEntity.Code,
                    type: adapterSpecificationEntity.Type,
                    communication: adapterSpecificationEntity.Communication,
                    numOfModulesAllowed: adapterSpecificationEntity.NumOfModulesAllowed,
                    commSpeed: adapterSpecificationEntity.CommSpeed,
                    inputSupply: adapterSpecificationEntity.InputSupply,
                    outputSupply: adapterSpecificationEntity.OutputSupply,
                    inrushCurrent: adapterSpecificationEntity.InrushCurrent,
                    alarm: adapterSpecificationEntity.Alarm,
                    note: adapterSpecificationEntity.Note,
                    pdfManual: adapterSpecificationEntity.PdfManual
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
                DeviceEntities = moduleResponseDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                JBEntities = moduleResponseDto.JBBasicDtos.Select(j => new JBEntity(j.Id, j.Name)).ToList(),
                ModuleSpecificationEntity = MapToModuleSpecificationResponseEntity(moduleResponseDto),
                AdapterSpecificationEntity = MapToAdapterSpecificationResponseEntity(moduleResponseDto)
            };
        }

        private ModuleSpecificationEntity MapToModuleSpecificationResponseEntity(ModuleResponseDto moduleResponseDto)
        {
            return new ModuleSpecificationEntity(
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
                    );
        }

        private AdapterSpecificationEntity MapToAdapterSpecificationResponseEntity(ModuleResponseDto moduleResponseDto)
        {
            return new AdapterSpecificationEntity(
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
                    );
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
        private ModuleEntity MapBasicToModuleEntity(ModuleBasicDto moduleGeneralDto)
        {
            return new ModuleEntity
            (moduleGeneralDto.Id,
                moduleGeneralDto.Name
            );
        }

    }
}
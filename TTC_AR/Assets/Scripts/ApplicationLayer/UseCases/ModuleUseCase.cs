using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    int count = moduleEntities.Count;
                    var listModuleInfo = new List<ModuleInformationModel>(count);
                    var dictModuleInfo = new Dictionary<string, ModuleInformationModel>(count);
                    var moduleBasicDtos = new List<ModuleBasicDto>(count);
                    foreach (var moduleEntity in moduleEntities)
                    {
                        var dto = MapEntityToBasicDto(moduleEntity);
                        var model = new ModuleInformationModel(dto.Id, dto.Name);
                        moduleBasicDtos.Add(dto);
                        listModuleInfo.Add(model);
                        dictModuleInfo[dto.Name] = model;
                    }

                    GlobalVariable.temp_List_ModuleInformationModel = listModuleInfo;
                    GlobalVariable.temp_Dictionary_ModuleInformationModel = dictModuleInfo;
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
                UnityEngine.Debug.Log("Run UseCase");
                var moduleEntity = await _IModuleRepository.GetModuleByIdAsync(ModuleId) ??
                    throw new ApplicationException("Failed to get Module");

                UnityEngine.Debug.Log("Let Convert to ResponseDto");

                return MapEntityToResponseDto(moduleEntity);

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
                UnityEngine.Debug.Log("Run UseCase");
                // Ánh xạ từ ModuleRequestDto sang ModuleEntity để check các nghiệp vụ
                var ModuleEntity = MapRequestToModuleEntity(requestDto);

                UnityEngine.Debug.Log("UseCase Send to Repository");

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
            catch (ArgumentException ex)
            {
                UnityEngine.Debug.Log(ex);
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log(ex);
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
                UnityEngine.Debug.Log("Run UseCase");
                var ModuleEntity = MapRequestToModuleEntity(requestDto);
                UnityEngine.Debug.Log("Convert to Entity Successfully");
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
            var deviceEntities = moduleEntity.DeviceEntities ?? Enumerable.Empty<DeviceEntity>();
            var jbEntities = moduleEntity.JBEntities ?? Enumerable.Empty<JBEntity>();

            return new ModuleRequestDto(
          name: moduleEntity.Name,
          rackBasicDto: new RackBasicDto(moduleEntity.RackEntity.Id, moduleEntity.RackEntity.Name),
          deviceBasicDtos: deviceEntities.Any()
          ? moduleEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList()
          : new List<DeviceBasicDto>(),
           jBBasicDtos: jbEntities.Any()
           ? moduleEntity.JBEntities.Select(j => new JBBasicDto(j.Id, j.Name)).ToList()
           : new List<JBBasicDto>(),
           moduleSpecificationBasicDto: new ModuleSpecificationBasicDto(moduleEntity.ModuleSpecificationEntity.Id, moduleEntity.ModuleSpecificationEntity.Code),
           adapterSpecificationBasicDto: new AdapterSpecificationBasicDto(moduleEntity.AdapterSpecificationEntity.Id, moduleEntity.AdapterSpecificationEntity.Code)
            );
        }

        private ModuleResponseDto MapEntityToResponseDto(ModuleEntity moduleEntity)
        {
            var deviceEntities = moduleEntity.DeviceEntities ?? Enumerable.Empty<DeviceEntity>();
            var jbEntities = moduleEntity.JBEntities ?? Enumerable.Empty<JBEntity>();

            return new ModuleResponseDto(
                id: moduleEntity.Id,
                name: moduleEntity.Name,
                grapperBasicDto: new GrapperBasicDto(moduleEntity.GrapperEntity.Id, moduleEntity.GrapperEntity.Name),
                rackBasicDto: moduleEntity.RackEntity != null ? new RackBasicDto(moduleEntity.RackEntity.Id, moduleEntity.RackEntity.Name) : null,
                deviceBasicDtos: deviceEntities.Any()
                    ? new List<DeviceBasicDto>(deviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)))
                    : new List<DeviceBasicDto>(),
                jbBasicDtos: jbEntities.Any()
                    ? new List<JBBasicDto>(jbEntities.Select(j => new JBBasicDto(j.Id, j.Name)))
                    : new List<JBBasicDto>(),
                moduleSpecificationResponseDto: moduleEntity.ModuleSpecificationEntity != null ? MapToEntityToModuleSpecificationResponseDto(moduleEntity.ModuleSpecificationEntity) : null,
                adapterSpecificationResponseDto: moduleEntity.AdapterSpecificationEntity != null ? MapToEntityToAdapterSpecificationResponseDto(moduleEntity.AdapterSpecificationEntity) : null
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
              name: moduleRequestDto.Name,
              rack: moduleRequestDto.RackBasicDto != null ? new RackEntity(
                id: moduleRequestDto.RackBasicDto.Id,
                name: moduleRequestDto.RackBasicDto.Name) : null,
              deviceEntities: moduleRequestDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
              jbEntities: moduleRequestDto.JBBasicDtos.Select(j => new JBEntity(j.Id, j.Name)).ToList(),
              moduleSpecificationEntity: moduleRequestDto.ModuleSpecificationBasicDto != null ? new ModuleSpecificationEntity(moduleRequestDto.ModuleSpecificationBasicDto.Id, moduleRequestDto.ModuleSpecificationBasicDto.Code) : null,
              adapterSpecificationEntity: moduleRequestDto.AdapterSpecificationBasicDto != null ? new AdapterSpecificationEntity(moduleRequestDto.AdapterSpecificationBasicDto.Id, moduleRequestDto.AdapterSpecificationBasicDto.Code) : null);
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
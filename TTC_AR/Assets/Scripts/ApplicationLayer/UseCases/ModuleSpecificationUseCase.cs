using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class ModuleSpecificationUseCase
    {
        private IModuleSpecificationRepository _IModuleSpecificationRepository;

        public ModuleSpecificationUseCase(IModuleSpecificationRepository IModuleSpecificationRepository)
        {
            _IModuleSpecificationRepository = IModuleSpecificationRepository;
        }
        public async Task<List<ModuleSpecificationResponseDto>> GetListModuleSpecificationAsync(int companyId)
        {
            try
            {
                var ModuleSpecificationEntities = await _IModuleSpecificationRepository.GetListModuleSpecificationAsync(companyId);

                if (ModuleSpecificationEntities == null)
                {
                    throw new ApplicationException("Failed to get ModuleSpecification list");
                }
                else
                {

                    return ModuleSpecificationEntities.Select(MapToResponseDto).ToList();
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get ModuleSpecification list", ex);
            }
        }
        public async Task<ModuleSpecificationResponseDto> GetModuleSpecificationByIdAsync(int ModuleSpecificationId)
        {
            try
            {
                var ModuleSpecificationEntity = await _IModuleSpecificationRepository.GetModuleSpecificationByIdAsync(ModuleSpecificationId);
                if (ModuleSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to get ModuleSpecification");
                }
                else
                {
                    return MapToResponseDto(ModuleSpecificationEntity);
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get ModuleSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var createNewModuleSpecificationEntity = MapRequestToEntity(requestDto);
                //   var requestData = MapToRequestDto(createNewModuleSpecificationEntity);
                if (createNewModuleSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to create ModuleSpecification cause ModuleSpecificationEntity is Null");
                }
                else
                {
                    return await _IModuleSpecificationRepository.CreateNewModuleSpecificationAsync(companyId, createNewModuleSpecificationEntity);
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create ModuleSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateModuleSpecificationAsync(int moduleSpecificationId, ModuleSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var updateModuleSpecificationEntity = MapRequestToEntity(requestDto);
                //  var requestData = MapToRequestDto(updateModuleSpecificationEntity);
                if (updateModuleSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to update ModuleSpecification cause ModuleSpecificationEntity is Null");
                }
                else
                {
                    return await _IModuleSpecificationRepository.UpdateModuleSpecificationAsync(moduleSpecificationId, updateModuleSpecificationEntity);
                }
            }

            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update ModuleSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteModuleSpecificationAsync(int ModuleSpecificationId)
        {
            try
            {
                var deleteModuleSpecificationResult = await _IModuleSpecificationRepository.DeleteModuleSpecificationAsync(ModuleSpecificationId);
                return deleteModuleSpecificationResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete ModuleSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Entity => Dto
        private ModuleSpecificationResponseDto MapToResponseDto(ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            return new ModuleSpecificationResponseDto(
                ModuleSpecificationEntity.Id,
                ModuleSpecificationEntity.Code,
                ModuleSpecificationEntity.Type,
                ModuleSpecificationEntity.NumOfIO,
                ModuleSpecificationEntity.SignalType,
                ModuleSpecificationEntity.CompatibleTBUs,
                ModuleSpecificationEntity.OperatingVoltage,
                ModuleSpecificationEntity.OperatingCurrent,
                ModuleSpecificationEntity.FlexbusCurrent,
                ModuleSpecificationEntity.Alarm,
                ModuleSpecificationEntity.Note,
                ModuleSpecificationEntity.PdfManual
            )
            {
                Id = ModuleSpecificationEntity.Id,
                Code = ModuleSpecificationEntity.Code,
                Type = ModuleSpecificationEntity.Type,
                SignalType = ModuleSpecificationEntity.SignalType,
                CompatibleTBUs = ModuleSpecificationEntity.CompatibleTBUs,
                OperatingVoltage = ModuleSpecificationEntity.OperatingVoltage,
                OperatingCurrent = ModuleSpecificationEntity.OperatingCurrent,
                FlexbusCurrent = ModuleSpecificationEntity.FlexbusCurrent,
                Alarm = ModuleSpecificationEntity.Alarm,
                Note = ModuleSpecificationEntity.Note,
                PdfManual = ModuleSpecificationEntity.PdfManual
            };
        }
        private ModuleSpecificationRequestDto MapToRequestDto(ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            return new ModuleSpecificationRequestDto(
                ModuleSpecificationEntity.Code,
                ModuleSpecificationEntity.Type,
                ModuleSpecificationEntity.NumOfIO,
                ModuleSpecificationEntity.SignalType,
                ModuleSpecificationEntity.CompatibleTBUs,
                ModuleSpecificationEntity.OperatingVoltage,
                ModuleSpecificationEntity.OperatingCurrent,
                ModuleSpecificationEntity.FlexbusCurrent,
                ModuleSpecificationEntity.Alarm,
                ModuleSpecificationEntity.Note,
                ModuleSpecificationEntity.PdfManual
            )
            {
                Code = ModuleSpecificationEntity.Code,
                Type = ModuleSpecificationEntity.Type,
                SignalType = ModuleSpecificationEntity.SignalType,
                CompatibleTBUs = ModuleSpecificationEntity.CompatibleTBUs,
                OperatingVoltage = ModuleSpecificationEntity.OperatingVoltage,
                OperatingCurrent = ModuleSpecificationEntity.OperatingCurrent,
                FlexbusCurrent = ModuleSpecificationEntity.FlexbusCurrent,
                Alarm = ModuleSpecificationEntity.Alarm,
                Note = ModuleSpecificationEntity.Note,
                PdfManual = ModuleSpecificationEntity.PdfManual
            };
        }

        //! Dto => Entity
        private ModuleSpecificationEntity MapRequestToEntity(ModuleSpecificationRequestDto requestDto)
        {
            return new ModuleSpecificationEntity(
                code: requestDto.Code,
                type: requestDto.Type,
                numOfIO: requestDto.NumOfIO,
                signalType: requestDto.SignalType,
                compatibleTBUs: requestDto.CompatibleTBUs,
                operatingVoltage: requestDto.OperatingVoltage,
                operatingCurrent: requestDto.OperatingCurrent,
                flexbusCurrent: requestDto.FlexbusCurrent,
                alarm: requestDto.Alarm,
                note: requestDto.Note,
                pdfManual: requestDto.PdfManual
                );


        }
        private ModuleSpecificationEntity MapToResponseEntity(ModuleSpecificationResponseDto ModuleSpecificationResponseDto)
        {
            return new ModuleSpecificationEntity(ModuleSpecificationResponseDto.Code)
            {
                Id = ModuleSpecificationResponseDto.Id,
                Type = ModuleSpecificationResponseDto.Type,
                SignalType = ModuleSpecificationResponseDto.SignalType,
                CompatibleTBUs = ModuleSpecificationResponseDto.CompatibleTBUs,
                OperatingVoltage = ModuleSpecificationResponseDto.OperatingVoltage,
                OperatingCurrent = ModuleSpecificationResponseDto.OperatingCurrent,
                FlexbusCurrent = ModuleSpecificationResponseDto.FlexbusCurrent,
                Alarm = ModuleSpecificationResponseDto.Alarm,
                Note = ModuleSpecificationResponseDto.Note,
                PdfManual = ModuleSpecificationResponseDto.PdfManual

            };
        }
    }
}
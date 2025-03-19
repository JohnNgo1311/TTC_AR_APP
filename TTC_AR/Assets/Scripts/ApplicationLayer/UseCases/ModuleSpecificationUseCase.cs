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
        public async Task<IEnumerable<ModuleSpecificationBasicDto>> GetListModuleSpecificationAsync(string companyId)
        {
            try
            {
                var ModuleSpecificationEntities = await _IModuleSpecificationRepository.GetListModuleSpecificationAsync(companyId) ??
                throw new ApplicationException("Failed to get ModuleSpecification list");
                return ModuleSpecificationEntities.Select(MapToBasicDto).ToList();
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
        public async Task<ModuleSpecificationResponseDto> GetModuleSpecificationByIdAsync(string moduleSpecificationId)
        {
            try
            {
                var ModuleSpecificationEntity = await _IModuleSpecificationRepository.GetModuleSpecificationByIdAsync(moduleSpecificationId)
                ?? throw new ApplicationException("Failed to get ModuleSpecification");
                return MapToResponseDto(ModuleSpecificationEntity);
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
        public async Task<bool> CreateNewModuleSpecificationAsync(string companyId, ModuleSpecificationRequestDto requestDto)
        {
            companyId = GlobalVariable.companyId;
            try
            {
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
        public async Task<bool> UpdateModuleSpecificationAsync(string moduleSpecificationId, ModuleSpecificationRequestDto requestDto)
        {
            moduleSpecificationId = GlobalVariable.ModuleSpecificationId;
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
        public async Task<bool> DeleteModuleSpecificationAsync(string moduleSpecificationId)
        {
            // moduleSpecificationId = GlobalVariable.ModuleSpecificationId;

            try
            {
                var deleteModuleSpecificationResult = await _IModuleSpecificationRepository.DeleteModuleSpecificationAsync(moduleSpecificationId);
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
        private ModuleSpecificationBasicDto MapToBasicDto(ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            return new ModuleSpecificationBasicDto(
                ModuleSpecificationEntity.Id,
                 ModuleSpecificationEntity.Code);
        }


        private ModuleSpecificationResponseDto MapToResponseDto(ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            return new ModuleSpecificationResponseDto(
            id: ModuleSpecificationEntity.Id,
            code: ModuleSpecificationEntity.Code,
            type: ModuleSpecificationEntity.Type,
            signalType: ModuleSpecificationEntity.SignalType,
            numOfIO: ModuleSpecificationEntity.NumOfIO,
            compatibleTBUs: ModuleSpecificationEntity.CompatibleTBUs,
            operatingVoltage: ModuleSpecificationEntity.OperatingVoltage,
            operatingCurrent: ModuleSpecificationEntity.OperatingCurrent,
            flexbusCurrent: ModuleSpecificationEntity.FlexbusCurrent,
            alarm: ModuleSpecificationEntity.Alarm,
            note: ModuleSpecificationEntity.Note,
            pdfManual: ModuleSpecificationEntity.PdfManual
            );

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
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
        public async Task<List<ModuleSpecificationResponseDto>> GetListModuleSpecificationAsync(int grapperId)
        {
            try
            {
                var ModuleSpecificationEntities = await _IModuleSpecificationRepository.GetListModuleSpecificationAsync(grapperId);

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
        public async Task<bool> CreateNewModuleSpecificationAsync(int grapperId, ModuleSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var createNewModuleSpecificationEntity = MapRequestToEntity(requestDto);
                if (createNewModuleSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to create ModuleSpecification cause ModuleSpecificationEntity is Null");
                }
                else
                {
                    return await _IModuleSpecificationRepository.CreateNewModuleSpecificationAsync(grapperId, createNewModuleSpecificationEntity);
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
        public async Task<bool> UpdateModuleSpecificationAsync(int ModuleSpecificationId, ModuleSpecificationRequestDto requestDto)
        {
            // Validate
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var updateNewModuleSpecificationEntity = MapRequestToEntity(requestDto);

                if (updateNewModuleSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to update ModuleSpecification cause ModuleSpecificationEntity is Null");
                }
                else
                {
                    var updatedModuleSpecificationResult = await _IModuleSpecificationRepository.UpdateModuleSpecificationAsync(ModuleSpecificationId, updateNewModuleSpecificationEntity);
                    return updatedModuleSpecificationResult;
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

        private ModuleSpecificationEntity MapRequestToEntity(ModuleSpecificationRequestDto requestDto)
        {
            return new ModuleSpecificationEntity(requestDto.Code)
            {
                Type = requestDto.Type,
                SignalType = requestDto.SignalType,
                CompatibleTBUs = requestDto.CompatibleTBUs,
                OperatingVoltage = requestDto.OperatingVoltage,
                OperatingCurrent = requestDto.OperatingCurrent,
                FlexbusCurrent = requestDto.FlexbusCurrent,
                Alarm = requestDto.Alarm,
                Note = requestDto.Note,
                PdfManual = requestDto.PdfManual
            };
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
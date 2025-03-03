using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class AdapterSpecificationUseCase
    {
        private IAdapterSpecificationRepository _IAdapterSpecificationRepository;

        public AdapterSpecificationUseCase(IAdapterSpecificationRepository IAdapterSpecificationRepository)
        {
            _IAdapterSpecificationRepository = IAdapterSpecificationRepository;
        }
        public async Task<List<AdapterSpecificationResponseDto>> GetListAdapterSpecificationAsync(int grapperId)
        {
            try
            {
                var AdapterSpecificationEntities = await _IAdapterSpecificationRepository.GetListAdapterSpecificationAsync(grapperId);

                if (AdapterSpecificationEntities == null)
                {
                    throw new ApplicationException("Failed to get AdapterSpecification list");
                }
                else
                {

                    return AdapterSpecificationEntities.Select(MapToResponseDto).ToList();
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get AdapterSpecification list", ex);
            }
        }
        public async Task<AdapterSpecificationResponseDto> GetAdapterSpecificationByIdAsync(int adapterSpecificationId)
        {
            try
            {
                var AdapterSpecificationEntity = await _IAdapterSpecificationRepository.GetAdapterSpecificationByIdAsync(adapterSpecificationId);
                if (AdapterSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to get AdapterSpecification");
                }
                else
                {
                    return MapToResponseDto(AdapterSpecificationEntity);
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get AdapterSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewAdapterSpecificationAsync(int grapperId, AdapterSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var createNewAdapterSpecificationEntity = MapRequestToEntity(requestDto);
                if (createNewAdapterSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to create AdapterSpecification cause AdapterSpecificationEntity is Null");
                }
                else
                {
                    return await _IAdapterSpecificationRepository.CreateNewAdapterSpecificationAsync(grapperId, createNewAdapterSpecificationEntity);
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create AdapterSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateAdapterSpecificationAsync(int AdapterSpecificationId, AdapterSpecificationRequestDto requestDto)
        {
            // Validate
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var updateNewAdapterSpecificationEntity = MapRequestToEntity(requestDto);

                if (updateNewAdapterSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to update AdapterSpecification cause AdapterSpecificationEntity is Null");
                }
                else
                {
                    var updatedAdapterSpecificationResult = await _IAdapterSpecificationRepository.UpdateAdapterSpecificationAsync(AdapterSpecificationId, updateNewAdapterSpecificationEntity);
                    return updatedAdapterSpecificationResult;
                }


            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update AdapterSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteAdapterSpecificationAsync(int AdapterSpecificationId)
        {
            try
            {
                var deletedAdapterSpecificationResult = await _IAdapterSpecificationRepository.DeleteAdapterSpecificationAsync(AdapterSpecificationId);
                return deletedAdapterSpecificationResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete AdapterSpecification", ex); // Bao bọc lỗi từ Repository
            }
        }

        private AdapterSpecificationResponseDto MapToResponseDto(AdapterSpecificationEntity AdapterSpecificationEntity)
        {
            return new AdapterSpecificationResponseDto(
                AdapterSpecificationEntity.Id,
                AdapterSpecificationEntity.Code,
                AdapterSpecificationEntity.Type,
                AdapterSpecificationEntity.Communication,
                AdapterSpecificationEntity.NumOfModulesAllowed,
                AdapterSpecificationEntity.CommSpeed,
                AdapterSpecificationEntity.InputSupply,
                AdapterSpecificationEntity.OutputSupply,
                AdapterSpecificationEntity.InrushCurrent,
                AdapterSpecificationEntity.Alarm,
                AdapterSpecificationEntity.Noted,
                AdapterSpecificationEntity.PdfManual
            )
            {
                Id = AdapterSpecificationEntity.Id,
                Code = AdapterSpecificationEntity.Code,
                Type = AdapterSpecificationEntity.Type,
                Communication = AdapterSpecificationEntity.Communication,
                NumOfModulesAllowed = AdapterSpecificationEntity.NumOfModulesAllowed,
                CommSpeed = AdapterSpecificationEntity.CommSpeed,
                InputSupply = AdapterSpecificationEntity.InputSupply,
                OutputSupply = AdapterSpecificationEntity.OutputSupply,
                InrushCurrent = AdapterSpecificationEntity.InrushCurrent,
                Alarm = AdapterSpecificationEntity.Alarm,
                Noted = AdapterSpecificationEntity.Noted,
                PdfManual = AdapterSpecificationEntity.PdfManual

            };

        }

        private AdapterSpecificationEntity MapRequestToEntity(AdapterSpecificationRequestDto requestDto)
        {
            return new AdapterSpecificationEntity(requestDto.Code)
            {
                Type = requestDto.Type,
                Communication = requestDto.Communication,
                NumOfModulesAllowed = requestDto.NumOfModulesAllowed,

                CommSpeed = requestDto.CommSpeed,

                InputSupply = requestDto.InputSupply,
                OutputSupply = requestDto.OutputSupply,

                InrushCurrent = requestDto.InrushCurrent,
                Alarm = requestDto.Alarm,
                Noted = requestDto.Noted,
                PdfManual = requestDto.PdfManual
            };
        }
        private AdapterSpecificationEntity MapToResponseEntity(AdapterSpecificationResponseDto AdapterSpecificationResponseDto)
        {
            return new AdapterSpecificationEntity(AdapterSpecificationResponseDto.Code)
            {
                Id = AdapterSpecificationResponseDto.Id,
                Type = AdapterSpecificationResponseDto.Type,
                Communication = AdapterSpecificationResponseDto.Communication,
                NumOfModulesAllowed = AdapterSpecificationResponseDto.NumOfModulesAllowed,
                CommSpeed = AdapterSpecificationResponseDto.CommSpeed,
                InputSupply = AdapterSpecificationResponseDto.InputSupply,
                OutputSupply = AdapterSpecificationResponseDto.OutputSupply,
                InrushCurrent = AdapterSpecificationResponseDto.InrushCurrent,
                Alarm = AdapterSpecificationResponseDto.Alarm,
                Noted = AdapterSpecificationResponseDto.Noted,
                PdfManual = AdapterSpecificationResponseDto.PdfManual
            };


        }
    }
}
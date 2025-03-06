using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
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
        public async Task<AdapterSpecificationResponseDto> GetAdapterSpecificationByIdAsync(string adapterSpecificationId)
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
        public async Task<bool> CreateNewAdapterSpecificationAsync(int companyId, AdapterSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var createNewAdapterSpecificationEntity = MapRequestToEntity(requestDto);
                // var requestData = adapterSpecificationRequestDto(createNewAdapterSpecificationEntity);
                if (createNewAdapterSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to create AdapterSpecification cause AdapterSpecificationEntity is Null");
                }
                else
                {
                    return await _IAdapterSpecificationRepository.CreateNewAdapterSpecificationAsync(companyId, createNewAdapterSpecificationEntity);
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
        public async Task<bool> UpdateAdapterSpecificationAsync(int adapterSpecificationId, AdapterSpecificationRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Code))
                {
                    throw new ArgumentException("Code cannot be empty");
                }
                var updateAdapterSpecificationEntity = MapRequestToEntity(requestDto);

                //var requestData = adapterSpecificationRequestDto(updateAdapterSpecificationEntity);

                if (updateAdapterSpecificationEntity == null)
                {
                    throw new ApplicationException("Failed to update AdapterSpecification cause AdapterSpecificationEntity is Null");
                }
                else
                {
                    return await _IAdapterSpecificationRepository.UpdateAdapterSpecificationAsync(adapterSpecificationId, updateAdapterSpecificationEntity);
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
        //! Dto => Entity
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
                AdapterSpecificationEntity.Note,
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
                Note = AdapterSpecificationEntity.Note,
                PdfManual = AdapterSpecificationEntity.PdfManual

            };
        }
        private AdapterSpecificationRequestDto adapterSpecificationRequestDto(AdapterSpecificationEntity adapterSpecificationEntity)
        {
            return new AdapterSpecificationRequestDto
            (
            adapterSpecificationEntity.Code,
            adapterSpecificationEntity.Type,
            adapterSpecificationEntity.Communication,
            adapterSpecificationEntity.NumOfModulesAllowed,
            adapterSpecificationEntity.CommSpeed,
            adapterSpecificationEntity.InputSupply,
            adapterSpecificationEntity.OutputSupply,
            adapterSpecificationEntity.InrushCurrent,
            adapterSpecificationEntity.Alarm,
            adapterSpecificationEntity.Note,
            adapterSpecificationEntity.PdfManual
            )
            {
                Code = adapterSpecificationEntity.Code,
                Type = adapterSpecificationEntity.Type,
                Communication = adapterSpecificationEntity.Communication,
                NumOfModulesAllowed = adapterSpecificationEntity.NumOfModulesAllowed,
                CommSpeed = adapterSpecificationEntity.CommSpeed,
                InputSupply = adapterSpecificationEntity.InputSupply,
                OutputSupply = adapterSpecificationEntity.OutputSupply,
                InrushCurrent = adapterSpecificationEntity.InrushCurrent,
                Alarm = adapterSpecificationEntity.Alarm,
                Note = adapterSpecificationEntity.Note,
                PdfManual = adapterSpecificationEntity.PdfManual
            };
        }
        //! Dto => Entity
        private AdapterSpecificationEntity MapRequestToEntity(AdapterSpecificationRequestDto requestDto)
        {
            return new AdapterSpecificationEntity(
                requestDto.Code,
                requestDto.Type,
                requestDto.Communication,
                requestDto.NumOfModulesAllowed,
                requestDto.CommSpeed,
                requestDto.InputSupply,
                requestDto.OutputSupply,
                requestDto.InrushCurrent,
                requestDto.Alarm,
                requestDto.Note,
                requestDto.PdfManual
            );
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
                Note = AdapterSpecificationResponseDto.Note,
                PdfManual = AdapterSpecificationResponseDto.PdfManual
            };
        }

    }
}
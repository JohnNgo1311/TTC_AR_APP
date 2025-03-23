using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Company;
using ApplicationLayer.Dtos.Grapper;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class CompanyUseCase
    {
        private ICompanyRepository _ICompanyRepository;

        public CompanyUseCase(ICompanyRepository ICompanyRepository)
        {
            _ICompanyRepository = ICompanyRepository;
        }

        public async Task<List<CompanyResponseDto>> GetListCompanyAsync()
        {
            try
            {
                var CompanyEntities = await _ICompanyRepository.GetListCompanyAsync();

                if (CompanyEntities == null)
                {
                    throw new ApplicationException("Failed to get Company list");
                }
                else
                {

                    return CompanyEntities.Select(MapToResponseDto).ToList();
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Company list", ex);
            }
        }
        public async Task<CompanyResponseDto> GetCompanyByIdAsync(string CompanyId)
        {
            try
            {
                var companyEntity = await _ICompanyRepository.GetCompanyByIdAsync(CompanyId);

                if (companyEntity == null)
                {
                    UnityEngine.Debug.LogError("Failed to get Company");
                    throw new ApplicationException("Failed to get Company");
                }
                else
                {
                    UnityEngine.Debug.Log($"Company: {companyEntity.Name}");
                    var companyResponseDto = MapToResponseDto(companyEntity);

                    return companyResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Company", ex); // Bao bọc lỗi từ Repository
            }
        }

        private CompanyResponseDto MapToResponseDto(CompanyEntity companyEntity)
        {
            return new CompanyResponseDto(
                id: companyEntity.Id,
                name: companyEntity.Name,
                grapperBasicDtos: companyEntity.GrapperEntities.Any() ? companyEntity.GrapperEntities.Select(grapperEntity => new GrapperBasicDto(grapperEntity.Id, grapperEntity.Name)).ToList() : new List<GrapperBasicDto>(),
                moduleSpecificationBasicDtos: companyEntity.ModuleSpecificationEntities.Any() ? companyEntity.ModuleSpecificationEntities.Select(moduleSpecificationEntity => new ModuleSpecificationBasicDto(moduleSpecificationEntity.Id, moduleSpecificationEntity.Code)).ToList() : new List<ModuleSpecificationBasicDto>(),
                adapterSpecificationBasicDtos: companyEntity.AdapterSpecificationEntities.Any() ? companyEntity.AdapterSpecificationEntities.Select(adapterSpecificationEntity => new AdapterSpecificationBasicDto(adapterSpecificationEntity.Id, adapterSpecificationEntity.Code)).ToList() : new List<AdapterSpecificationBasicDto>()
            );
        }

    }
}
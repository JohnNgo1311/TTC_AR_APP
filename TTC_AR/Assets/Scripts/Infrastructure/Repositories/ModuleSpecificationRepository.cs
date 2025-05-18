using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Linq;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ModuleSpecificationRepository : IModuleSpecificationRepository
    {
        private readonly HttpClient _httpClient;

        // private const string GlobalVariable.baseUrl = "https://677ba70820824100c07a4e9f.mockapi.io/api/v3/ModuleSpecification"; // URL server ngoài thực tế

        public ModuleSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // _httpClient.BaseAddress = new Uri(GlobalVariable.baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        public async Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(int moduleSpecificationId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"/api/ModuleSpecification/{moduleSpecificationId}");
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/ModuleSpecifications/{moduleSpecificationId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get ModuleSpecification. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<ModuleSpecificationEntity>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch ModuleSpecification", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        //! Trả về List<Entity> do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        public async Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(int companyId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"/api/ModuleSpecification/grapper/{companyId}");
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/Grappers/{companyId}/moduleSpecificationsGeneral");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get ModuleSpecification list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ModuleSpecificationEntity>>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch ModuleSpecification", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                //  var ModuleSpecificationEntity = ConvertModuleSpecificationEntity(ModuleSpecificationEntity);
                var json = JsonConvert.SerializeObject(moduleSpecificationEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var response = await _httpClient.PostAsync($"/api/ModuleSpecification/grapper/{companyId}", content);
                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}/ModuleSpecifications/companyId?companyId={companyId}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create ModuleSpecification", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UpdateModuleSpecificationAsync(int moduleSpecificationId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(moduleSpecificationEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var response = await _httpClient.PutAsync($"/api/ModuleSpecification/{moduleSpecificationId}", content);
                var response = await _httpClient.PutAsync($"{GlobalVariable.baseUrl}/ModuleSpecifications/{moduleSpecificationId}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;

            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to update ModuleSpecification", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteModuleSpecificationAsync(int moduleSpecificationId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalVariable.baseUrl}/ModuleSpecifications/{moduleSpecificationId}");

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete ModuleSpecification", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }

    }
}
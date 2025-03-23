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

        private const string BaseUrl = "https://677ba70820824100c07a4e9f.mockapi.io/api/v3/ModuleSpecification"; // URL server ngoài thực tế

        public ModuleSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        public async Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(string ModuleSpecificationId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"/api/ModuleSpecification/{ModuleSpecificationId}");
                var response = await _httpClient.GetAsync($"{BaseUrl}/{ModuleSpecificationId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get ModuleSpecification. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ModuleSpecificationEntity>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        //! Trả về List<Entity> do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        public async Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(string companyId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"/api/ModuleSpecification/grapper/{companyId}");
                var response = await _httpClient.GetAsync($"{BaseUrl}");

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
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex);
            }
        }

        public async Task<bool> CreateNewModuleSpecificationAsync(string companyId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                //  var ModuleSpecificationEntity = ConvertModuleSpecificationEntity(ModuleSpecificationEntity);
                var json = JsonConvert.SerializeObject(moduleSpecificationEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var response = await _httpClient.PostAsync($"/api/ModuleSpecification/grapper/{companyId}", content);
                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create ModuleSpecification. Status: {response.StatusCode}");
                else { return true; }
            }
            catch (HttpRequestException ex)
            {
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UpdateModuleSpecificationAsync(string ModuleSpecificationId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(moduleSpecificationEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var response = await _httpClient.PutAsync($"/api/ModuleSpecification/{ModuleSpecificationId}", content);
                var response = await _httpClient.PutAsync($"{BaseUrl}/{ModuleSpecificationId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to update ModuleSpecification. Status: {response.StatusCode}");
                else { return true; }

            }
            catch (HttpRequestException ex)
            {
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteModuleSpecificationAsync(string ModuleSpecificationId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{ModuleSpecificationId}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create ModuleSpecification. Status: {response.StatusCode}");
                else return true;

            }
            catch (HttpRequestException ex)
            {
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }

    }
}
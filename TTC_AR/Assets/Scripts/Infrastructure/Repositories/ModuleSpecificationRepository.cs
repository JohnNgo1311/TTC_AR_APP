using Domain.Entities;
using Domain.Interfaces;
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

namespace Infrastructure.Repositories
{
    public class ModuleSpecificationRepository : IModuleSpecificationRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company"; // URL server ngoài thực tế

        public ModuleSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity do kết quả server trả về hoàn toàn giống hoặc gần giống với Entity
        public async Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(int ModuleSpecificationId)
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
        public async Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(int companyId)
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

        public async Task<bool> CreateNewModuleSpecificationAsync(int companyId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                if (moduleSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(moduleSpecificationEntity), "Entity cannot be null");
                // Tạo dữ liệu tối giản gửi lên server
                else
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

        public async Task<bool> UpdateModuleSpecificationAsync(int ModuleSpecificationId, ModuleSpecificationEntity moduleSpecificationEntity)
        {
            try
            {
                if (moduleSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(moduleSpecificationEntity), "Entity cannot be null");
                else
                {
                    //var ModuleSpecificationEntity = ConvertModuleSpecificationEntity(ModuleSpecificationEntity);

                    var json = JsonConvert.SerializeObject(moduleSpecificationEntity);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // var response = await _httpClient.PutAsync($"/api/ModuleSpecification/{ModuleSpecificationId}", content);
                    var response = await _httpClient.PutAsync($"{BaseUrl}/{ModuleSpecificationId}", content);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Failed to update ModuleSpecification. Status: {response.StatusCode}");
                    else { return true; }

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

        public async Task<bool> DeleteModuleSpecificationAsync(int ModuleSpecificationId)
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
        // private object ConvertModuleSpecificationEntity(ModuleSpecificationEntity ModuleSpecificationEntity)
        // {
        //     return new
        //     {
        //         code = ModuleSpecificationEntity.Code,
        //         ModuleSpecificationEntity.Type,
        //         ModuleSpecificationEntity.NumOfIO,
        //         ModuleSpecificationEntity.SignalType,
        //         ModuleSpecificationEntity.CompatibleTBUs,
        //         ModuleSpecificationEntity.OperatingVoltage,
        //         ModuleSpecificationEntity.OperatingCurrent,
        //         ModuleSpecificationEntity.FlexbusCurrent,
        //         ModuleSpecificationEntity.Alarm,
        //         ModuleSpecificationEntity.Note,
        //         PDFManual = ModuleSpecificationEntity.PdfManual
        //     };
        // }

    }
}
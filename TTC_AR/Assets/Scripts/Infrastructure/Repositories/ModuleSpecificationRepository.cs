using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Infrastructure.Dtos;
using System.Linq;
using ApplicationLayer.Dtos;

namespace Infrastructure.Repositories
{
    public class ModuleSpecificationRepository : IModuleSpecificationRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://external-server-api.com"; // URL server ngoài thực tế

        public ModuleSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity
        public async Task<ModuleSpecificationEntity> GetModuleSpecificationByIdAsync(int ModuleSpecificationId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ModuleSpecification/{ModuleSpecificationId}");
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

        //! Trả về List<Entity>
        public async Task<List<ModuleSpecificationEntity>> GetListModuleSpecificationAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ModuleSpecification/grapper/{grapperId}");
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

        public async Task<bool> CreateNewModuleSpecificationAsync(int grapperId, ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            try
            {

                if (ModuleSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(ModuleSpecificationEntity), "Entity cannot be null");
                // Tạo dữ liệu tối giản gửi lên server
                else
                {
                    var ModuleSpecificationRequestData = new
                    {
                        code = ModuleSpecificationEntity.Code, // Mandatory
                        Type = ModuleSpecificationEntity.Type ?? "", // Nullable/Empty
                        NumOfIO = ModuleSpecificationEntity.NumOfIO ?? "",
                        SignalType = ModuleSpecificationEntity.SignalType ?? "",
                        CompatibleTBUs = ModuleSpecificationEntity.CompatibleTBUs ?? "",
                        OperatingVoltage = ModuleSpecificationEntity.OperatingVoltage ?? "",
                        OperatingCurrent = ModuleSpecificationEntity.OperatingCurrent ?? "",
                        FlexbusCurrent = ModuleSpecificationEntity.FlexbusCurrent ?? "",
                        Alarm = ModuleSpecificationEntity.Alarm ?? "",
                        Note = ModuleSpecificationEntity.Note ?? "",
                        PDFManual = ModuleSpecificationEntity.PdfManual ?? ""
                    };

                    var json = JsonConvert.SerializeObject(ModuleSpecificationRequestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"/api/ModuleSpecification/grapper/{grapperId}", content);

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

        public async Task<bool> UpdateModuleSpecificationAsync(int ModuleSpecificationId, ModuleSpecificationEntity ModuleSpecificationEntity)
        {
            try
            {
                if (ModuleSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(ModuleSpecificationEntity), "Entity cannot be null");
                else
                {
                    var ModuleSpecificationRequestData = new
                    {
                        code = ModuleSpecificationEntity.Code, // Mandatory
                        Type = ModuleSpecificationEntity.Type ?? "", // Nullable/Empty
                        NumOfIO = ModuleSpecificationEntity.NumOfIO ?? "",
                        SignalType = ModuleSpecificationEntity.SignalType ?? "",
                        CompatibleTBUs = ModuleSpecificationEntity.CompatibleTBUs ?? "",
                        OperatingVoltage = ModuleSpecificationEntity.OperatingVoltage ?? "",
                        OperatingCurrent = ModuleSpecificationEntity.OperatingCurrent ?? "",
                        FlexbusCurrent = ModuleSpecificationEntity.FlexbusCurrent ?? "",
                        Alarm = ModuleSpecificationEntity.Alarm ?? "",
                        Note = ModuleSpecificationEntity.Note ?? "",
                        PDFManual = ModuleSpecificationEntity.PdfManual ?? ""
                    };

                    var json = JsonConvert.SerializeObject(ModuleSpecificationRequestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"/api/ModuleSpecification/{ModuleSpecificationId}", content);

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
                var response = await _httpClient.DeleteAsync($"/api/ModuleSpecification/{ModuleSpecificationId}");

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
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.iOS;
using Infrastructure.Dtos;
using System.Linq;
using ApplicationLayer.Dtos;

namespace Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://external-server-api.com"; // URL server ngoài thực tế

        public DeviceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<DeviceResponseDto> GetDeviceByIdAsync(int deviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/device/{deviceId}");
                if (!response.IsSuccessStatusCode)
                    return null; // Hoặc ném exception tùy yêu cầu

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceResponseDto>(content);
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

        public async Task<List<DeviceResponseDto>> GetListDeviceAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/device/grapper/{grapperId}");
                if (!response.IsSuccessStatusCode)
                    return new List<DeviceResponseDto>();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DeviceResponseDto>>(content);
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

        public async Task<bool> CreateNewDeviceAsync(int grapperId, DeviceEntity deviceEntity)
        {
            try
            {
                var deviceRequestData = new
                {
                    code = deviceEntity.Code,
                    function = deviceEntity.Function,
                    range = deviceEntity.Range,
                    unit = deviceEntity.Unit,
                    iOAddress = deviceEntity.IOAddress,
                    JBEntity = new
                    {
                        deviceEntity.JBEntity.Id,
                        deviceEntity.JBEntity.Name
                    },
                    Module = new
                    {
                        deviceEntity.ModuleEntity.Id,
                        deviceEntity.ModuleEntity.Name
                    },
                    AdditionalImageEntity = deviceEntity.AdditionalConnectionImageEntities.Select
                    (entity => new
                    {
                        entity.Id,
                        entity.Name
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(deviceRequestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/api/device/grapper/{grapperId}", content);
                return response.IsSuccessStatusCode; // Trả về true nếu thành công
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

        public async Task<bool> UpdateDeviceAsync(int deviceId, DeviceEntity deviceEntity)
        {
            try
            {

                var deviceRequestData = new
                {
                    code = deviceEntity.Code,
                    function = deviceEntity.Function,
                    range = deviceEntity.Range,
                    unit = deviceEntity.Unit,
                    iOAddress = deviceEntity.IOAddress,
                    JBEntity = new
                    {
                        deviceEntity.JBEntity.Id,
                        deviceEntity.JBEntity.Name
                    },
                    Module = new
                    {
                        deviceEntity.ModuleEntity.Id,
                        deviceEntity.ModuleEntity.Name
                    },
                    AdditionalImageEntity = deviceEntity.AdditionalConnectionImageEntities.Select
                    (entity => new
                    {
                        entity.Id,
                        entity.Name
                    }).ToList()
                };


                var json = JsonConvert.SerializeObject(deviceRequestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/device/{deviceId}", content);
                return response.IsSuccessStatusCode;
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

        public async Task<bool> DeleteDeviceAsync(int deviceId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/device/{deviceId}");
                return response.IsSuccessStatusCode;
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
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.iOS;
using System.Linq;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company"; // URL server ngoài thực tế

        public DeviceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        public async Task<DeviceEntity> GetDeviceByIdAsync(string deviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{deviceId}");

                if (!response.IsSuccessStatusCode)
                    return null; // Hoặc ném exception tùy yêu cầu

                var content = await response.Content.ReadAsStringAsync();
                UnityEngine.Debug.Log(content);

                return JsonConvert.DeserializeObject<DeviceEntity>(content);
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

        //!  Do kết quả server trả về là tập hợp con của DeviceEntity nên sẽ lựa chọn hàm trả veỀ DeviceResponseDto
        public async Task<List<DeviceEntity>> GetListDeviceAsync(string grapperId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"{BaseUrl}/{grapperId}");
                var response = await _httpClient.GetAsync($"{BaseUrl}");
                if (!response.IsSuccessStatusCode)
                    return new List<DeviceEntity>();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DeviceEntity>>(content);
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

        public async Task<bool> CreateNewDeviceAsync(string grapperId, DeviceEntity deviceEntity)
        {
            try
            {
                // var deviceRequestData = ConvertDeviceRequestData(deviceEntity);

                // var json = JsonConvert.SerializeObject(deviceEntity, new JsonSerializerSettings
                // {
                //     NullValueHandling = NullValueHandling.Ignore
                // });

                var json = JsonConvert.SerializeObject(deviceEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var response = await _httpClient.PostAsync($"{BaseUrl}/{grapperId}", content);
                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create JB. Status: {response.StatusCode}");

                return true;
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

        public async Task<bool> UpdateDeviceAsync(string deviceId, DeviceEntity deviceEntity)
        {
            try
            {

                // var deviceRequestData = ConvertDeviceRequestData(deviceEntity);
                // var json = JsonConvert.SerializeObject(deviceEntity, new JsonSerializerSettings
                // {
                //     NullValueHandling = NullValueHandling.Ignore
                // });

                var json = JsonConvert.SerializeObject(deviceEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{BaseUrl}/{deviceId}", content)
                ;
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create JB. Status: {response.StatusCode}");

                return true;
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

        public async Task<bool> DeleteDeviceAsync(string deviceId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{deviceId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create JB. Status: {response.StatusCode}");

                return true;
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

        private object ConvertDeviceRequestData(DeviceEntity deviceEntity)
        {
            return new
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
        }


    }
}
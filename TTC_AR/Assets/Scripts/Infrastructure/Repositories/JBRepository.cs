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
using ApplicationLayer.Dtos.JB;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class JBRepository : IJBRepository
    {
        private readonly HttpClient _httpClient;


        public JBRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // _httpClient.BaseAddress = new Uri(GlobalVariable.baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về JBEntity
        public async Task<JBEntity> GetJBByIdAsync(int JBId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/{JBId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get JB. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<JBEntity>(content);

                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch JB", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }

        //! Trả về List<JBEntity>
        public async Task<List<JBEntity>> GetListJBGeneralAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get JB list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<JBEntity>>(content);
                }

            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch JB list", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex);
            }
        }
        //! Trả về List<JBEntity>
        public async Task<List<JBEntity>> GetListJBInformationAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync("https://677ba70820824100c07a4e9f.mockapi.io/api/v3/ListJBInformation");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get JB list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<List<JBEntity>>(content);
                }

            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch JB list", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex);
            }
        }


        public async Task<bool> CreateNewJBAsync(int grapperId, JBEntity jBEntity)
        {
            try
            {
                if (jBEntity == null)
                    throw new ArgumentNullException(nameof(jBEntity), "Request data cannot be null");

                // var json = JsonConvert.SerializeObject(jBEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var jbjBEntity = ConvertJBjBEntity(jbEntity);

                var json = JsonConvert.SerializeObject(jBEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create JB", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Failed to deserialize JSON", ex); // Xử lý lỗi deserialize
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UpdateJBAsync(int JBId, JBEntity jBEntity)
        {
            try
            {
                if (jBEntity == null)
                    throw new ArgumentNullException(nameof(jBEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(jBEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var jbjBEntity = ConvertJBjBEntity(jbEntity);

                // var json = JsonConvert.SerializeObject(jBEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{GlobalVariable.baseUrl}/{JBId}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to update JB", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Failed to deserialize JSON", ex); // Xử lý lỗi deserialize
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteJBAsync(int jbId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalVariable.baseUrl}/{jbId}");
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete JB", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Failed to deserialize JSON", ex); // Xử lý lỗi deserialize
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }
        // private object ConvertJBRequestData(JBEntity jbEntity)
        // {
        //     return new
        //     {
        //         name = jbEntity.Name,
        //         Location = jbEntity.Location ?? "",
        //         ListDevices = jbEntity.DeviceEntities?
        //             .Where(d => d != null)
        //             .Select(d => new DeviceBasicDto(d.Id, d.Code))
        //             .ToList() ?? new List<DeviceBasicDto>(),
        //         ListModules = jbEntity.ModuleEntities
        //             .Where(m => m != null)
        //             .Select(m => new ModuleBasicDto(m.Id, m.Name))
        //             .ToList() ?? new List<ModuleBasicDto>(),
        //         OutdoorImage = jbEntity.OutdoorImageEntity != null
        //             ? new ImageBasicDto(jbEntity.OutdoorImageEntity.Id, jbEntity.OutdoorImageEntity.Name)
        //             : null,
        //         ListConnectionImages = jbEntity.ConnectionImageEntities?
        //             .Where(i => i != null)
        //             .Select(i => new ImageBasicDto(i.Id, i.Name))
        //             .ToList() ?? new List<ImageBasicDto>()
        //     };
        // }
    }


}

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
using ApplicationLayer.Dtos.JB;

namespace Infrastructure.Repositories
{
    public class JBRepository : IJBRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company"; // URL server ngoài thực tế

        public JBRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về JBEntity
        public async Task<JBEntity> GetJBByIdAsync(int JBId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{JBId}");

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
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        //! Trả về List<JBEntity>
        public async Task<List<JBEntity>> GetListJBAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}");
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
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex);
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
                var response = await _httpClient.PutAsync($"{BaseUrl}/{JBId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to Update JB. Status: {response.StatusCode}");

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

        public async Task<bool> DeleteJBAsync(int jbId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/jb/{jbId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to delete JB. Status: {response.StatusCode}");

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
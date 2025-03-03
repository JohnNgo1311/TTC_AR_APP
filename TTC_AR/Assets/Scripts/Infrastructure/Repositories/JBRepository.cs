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
    public class JBRepository : IJBRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://external-server-api.com"; // URL server ngoài thực tế

        public JBRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        //! TRả về JBResponseDto
        public async Task<JBResponseDto> GetJBByIdAsync(int jbId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/jb/{jbId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get JB. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JBResponseDto>(content);
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

        //! Trả về List<JBResponseDto>
        public async Task<List<JBResponseDto>> GetListJBAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/jb/grapper/{grapperId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get JB list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<JBResponseDto>>(content);
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

        public async Task<bool> CreateNewJBAsync(int grapperId, JBEntity jbEntity)
        {
            try
            {
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                var jbRequestData = new
                {
                    name = jbEntity.Name, // Mandatory field
                    Location = jbEntity.Location ?? "", // Nullable, default ""
                    ListDevices = jbEntity.DeviceEntities?
                        .Where(d => d != null)
                        .Select(d => new DeviceBasicDto(d.Id, d.Code))
                        .ToList() ?? new List<DeviceBasicDto>(), // Empty list nếu null
                    ListModules = jbEntity.ModuleEntities
                        .Where(m => m != null)
                        .Select(m => new ModuleBasicDto(m.Id, m.Name))
                        .ToList() ?? new List<ModuleBasicDto>(), // Empty list nếu null
                    OutdoorImage = jbEntity.OutdoorImageEntity != null
                        ? new ImageBasicDto(jbEntity.OutdoorImageEntity.Id, jbEntity.OutdoorImageEntity.Name)
                        : null, // Null nếu không có
                    ListConnectionImages = jbEntity.ConnectionImageEntities?
                        .Where(i => i != null)
                        .Select(i => new ImageBasicDto(i.Id, i.Name))
                        .ToList() ?? new List<ImageBasicDto>() // Empty list nếu null
                };

                var json = JsonConvert.SerializeObject(jbRequestData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"/api/jb/grapper/{grapperId}", content);

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

        public async Task<bool> UpdateJBAsync(int jbId, JBEntity jbEntity)
        {
            try
            {
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                var jbRequestData = new
                {
                    name = jbEntity.Name, // Mandatory field
                    Location = jbEntity.Location ?? "", // Nullable, default ""
                    ListDevices = jbEntity.DeviceEntities?
                        .Where(d => d != null)
                        .Select(d => new DeviceBasicDto(d.Id, d.Code))
                        .ToList() ?? new List<DeviceBasicDto>(), // Empty list nếu null
                    ListModules = jbEntity.ModuleEntities
                        .Where(m => m != null)
                        .Select(m => new ModuleBasicDto(m.Id, m.Name))
                        .ToList() ?? new List<ModuleBasicDto>(), // Empty list nếu null
                    OutdoorImage = jbEntity.OutdoorImageEntity != null
                        ? new ImageBasicDto(jbEntity.OutdoorImageEntity.Id, jbEntity.OutdoorImageEntity.Name)
                        : null, // Null nếu không có
                    ListConnectionImages = jbEntity.ConnectionImageEntities?
                        .Where(i => i != null)
                        .Select(i => new ImageBasicDto(i.Id, i.Name))
                        .ToList() ?? new List<ImageBasicDto>() // Empty list nếu null
                };

                var json = JsonConvert.SerializeObject(jbRequestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/jb/{jbId}", content);
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

        public async Task<bool> DeleteJBAsync(int jbId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/jb/{jbId}");
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
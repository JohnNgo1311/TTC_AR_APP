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
using ApplicationLayer.Dtos.Grapper;

namespace Infrastructure.Repositories
{
    public class GrapperRepository : IGrapperRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company"; // URL server ngoài thực tế

        public GrapperRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về GrapperEntity
        public async Task<GrapperEntity> GetGrapperByIdAsync(int GrapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{GrapperId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get Grapper. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<GrapperEntity>(content);

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

        //! Trả về List<GrapperEntity>
        public async Task<List<GrapperEntity>> GetListGrapperAsync(int companyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get Grapper list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<GrapperEntity>>(content);
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


        public async Task<bool> CreateNewGrapperAsync(int companyId, GrapperEntity grapperEntity)
        {
            try
            {
                if (grapperEntity == null)
                    throw new ArgumentNullException(nameof(grapperEntity), "Request data cannot be null");

                var json = JsonConvert.SerializeObject(grapperEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create Grapper. Status: {response.StatusCode}");

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

        public async Task<bool> UpdateGrapperAsync(int GrapperId, GrapperEntity grapperEntity)
        {
            try
            {
                if (grapperEntity == null)
                    throw new ArgumentNullException(nameof(grapperEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(grapperEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var GrapperGrapperEntity = ConvertGrapperGrapperEntity(GrapperEntity);

                // var json = JsonConvert.SerializeObject(GrapperEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{BaseUrl}/{GrapperId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to Update Grapper. Status: {response.StatusCode}");

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

        public async Task<bool> DeleteGrapperAsync(int GrapperId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Grapper/{GrapperId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to delete Grapper. Status: {response.StatusCode}");

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
        // private object ConvertGrapperRequestData(GrapperEntity GrapperEntity)
        // {
        //     return new
        //     {
        //         name = GrapperEntity.Name,
        //         Location = GrapperEntity.Location ?? "",
        //         ListDevices = GrapperEntity.DeviceEntities?
        //             .Where(d => d != null)
        //             .Select(d => new DeviceBasicDto(d.Id, d.Code))
        //             .ToList() ?? new List<DeviceBasicDto>(),
        //         ListModules = GrapperEntity.ModuleEntities
        //             .Where(m => m != null)
        //             .Select(m => new ModuleBasicDto(m.Id, m.Name))
        //             .ToList() ?? new List<ModuleBasicDto>(),
        //         OutdoorImage = GrapperEntity.OutdoorImageEntity != null
        //             ? new ImageBasicDto(GrapperEntity.OutdoorImageEntity.Id, GrapperEntity.OutdoorImageEntity.Name)
        //             : null,
        //         ListConnectionImages = GrapperEntity.ConnectionImageEntities?
        //             .Where(i => i != null)
        //             .Select(i => new ImageBasicDto(i.Id, i.Name))
        //             .ToList() ?? new List<ImageBasicDto>()
        //     };
        // }
    }


}
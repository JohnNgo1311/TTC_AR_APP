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
using ApplicationLayer.Dtos.Rack;

namespace Infrastructure.Repositories
{
    public class RackRepository : IRackRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company"; // URL server ngoài thực tế

        public RackRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về RackEntity
        public async Task<RackEntity> GetRackByIdAsync(int RackId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{RackId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get Rack. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<RackEntity>(content);

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

        //! Trả về List<RackEntity>
        public async Task<List<RackEntity>> GetListRackAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get Rack list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RackEntity>>(content);
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


        public async Task<bool> CreateNewRackAsync(int grapperId, RackEntity RackEntity)
        {
            try
            {
                if (RackEntity == null)
                    throw new ArgumentNullException(nameof(RackEntity), "Request data cannot be null");

                // var json = JsonConvert.SerializeObject(RackEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var RackRackEntity = ConvertRackRackEntity(RackEntity);

                var json = JsonConvert.SerializeObject(RackEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create Rack. Status: {response.StatusCode}");

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

        public async Task<bool> UpdateRackAsync(int RackId, RackEntity RackEntity)
        {
            try
            {
                if (RackEntity == null)
                    throw new ArgumentNullException(nameof(RackEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(RackEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var RackRackEntity = ConvertRackRackEntity(RackEntity);

                // var json = JsonConvert.SerializeObject(RackEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{BaseUrl}/{RackId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to Update Rack. Status: {response.StatusCode}");

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

        public async Task<bool> DeleteRackAsync(int RackId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Rack/{RackId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to delete Rack. Status: {response.StatusCode}");

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

    }


}
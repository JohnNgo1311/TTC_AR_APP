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
using ApplicationLayer.Dtos.Rack;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class RackRepository : IRackRepository
    {
        private readonly HttpClient _httpClient;

        public RackRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // _httpClient.BaseAddress = new Uri(GlobalVariable.baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về RackEntity
        public async Task<RackEntity> GetRackByIdAsync(int rackId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/{rackId}");

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
                throw new ApplicationException("Failed to fetch Rack", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        //! Trả về List<RackEntity>
        public async Task<List<RackEntity>> GetListRackAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}");
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
                throw new ApplicationException($"Failed to fetch Rack data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }

        }


        public async Task<bool> CreateNewRackAsync(int grapperId, RackEntity RackEntity)
        {
            try
            {
                if (RackEntity == null)

                    throw new ArgumentNullException(nameof(RackEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(RackEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}", content);

                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(temp);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create Rack", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UpdateRackAsync(int rackId, RackEntity RackEntity)
        {
            try
            {
                if (RackEntity == null)
                    throw new ArgumentNullException(nameof(RackEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(RackEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{GlobalVariable.baseUrl}/{rackId}", content);

                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(temp);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to update Rack", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteRackAsync(int rackId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Rack/{rackId}");
                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(temp);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete Rack", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

    }


}
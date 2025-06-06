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
using ApplicationLayer.Dtos.AdapterSpecification;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class AdapterSpecificationRepository : IAdapterSpecificationRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://67da8d3b35c87309f52d09f5.mockapi.io/api/v4/AdapterSpecification"; // URL server ngoài thực tế

        public AdapterSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity
        public async Task<AdapterSpecificationEntity> GetAdapterSpecificationByIdAsync(string AdapterSpecificationId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{AdapterSpecificationId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get AdapterSpecification. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    UnityEngine.Debug.Log(content);

                    var entity = JsonConvert.DeserializeObject<AdapterSpecificationEntity>(content);

                    return entity;
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
        public async Task<List<AdapterSpecificationEntity>> GetListAdapterSpecificationAsync(string companyId)
        {
            try
            {
                // var response = await _httpClient.GetAsync($"{BaseUrl}/{companyId}");
                var response = await _httpClient.GetAsync($"{BaseUrl}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get AdapterSpecification list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AdapterSpecificationEntity>>(content);
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

        public async Task<bool> CreateNewAdapterSpecificationAsync(string companyId, AdapterSpecificationEntity adapterSpecificationEntity)
        {
            try
            {

                if (adapterSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(adapterSpecificationEntity), "Entity cannot be null");
                // Tạo dữ liệu tối giản gửi lên server
                else
                {
                    var json = JsonConvert.SerializeObject(adapterSpecificationEntity);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                    //! var response = await _httpClient.PostAsync($"{BaseUrl}/{companyId}", content);
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Failed to create AdapterSpecification. Status: {response.StatusCode}");
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


        public async Task<bool> UpdateAdapterSpecificationAsync(string adapterSpecificationId, AdapterSpecificationEntity adapterSpecificationEntity)
        {
            try
            {
                if (adapterSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(adapterSpecificationEntity), "Entity cannot be null");
                // Tạo dữ liệu tối giản gửi lên server
                else
                {

                    var json = JsonConvert.SerializeObject(adapterSpecificationEntity);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"{BaseUrl}/{adapterSpecificationId}", content);
                    // var response = await _httpClient.PutAsync($"/api/AdapterSpecification/{adapterSpecificationId}", content);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Failed to update AdapterSpecification. Status: {response.StatusCode}");
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

        public async Task<bool> DeleteAdapterSpecificationAsync(string adapterSpecificationId)
        {
            try
            {
                //! var response = await _httpClient.DeleteAsync($"/api/AdapterSpecification/{adapterSpecificationId}");
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{adapterSpecificationId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create AdapterSpecification. Status: {response.StatusCode}");
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
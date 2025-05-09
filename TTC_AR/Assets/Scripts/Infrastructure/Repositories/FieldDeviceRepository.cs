

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
namespace Infrastructure.Repositories
{
    public class FieldDeviceRepository : IFieldDeviceRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://677ba70820824100c07a4e9f.mockapi.io/api/v3/ListFieldDevice";

        public FieldDeviceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<FieldDeviceEntity>> GetListFieldDeviceAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{grapperId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get FieldDevice list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<FieldDeviceEntity>>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new ApplicationException($"Failed to fetch FieldDevice data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }

        }

        public async Task<FieldDeviceEntity> GetFieldDeviceByIdAsync(int fieldDeviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{fieldDeviceId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get FieldDevice. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FieldDeviceEntity>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new ApplicationException($"Failed to fetch FieldDevice data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }


        }

        public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceEntity fieldDeviceEntity)
        {
            try
            {

                var json = JsonConvert.SerializeObject(fieldDeviceEntity);
                //    UnityEngine.Debug.Log(json.ToString());

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;

                // var responseContent = await response.Content.ReadAsStringAsync();
                // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new ApplicationException($"Failed to create FieldDevice: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }
        }

        public async Task<bool> UpdateFieldDeviceAsync(int fieldDeviceId, FieldDeviceEntity fieldDeviceEntity)
        {
            try
            {
                // var fieldDeviceRequestData = ConvertFieldDeviceRequestData(fieldDeviceEntity);

                // var json = JsonConvert.SerializeObject(fieldDeviceEntity, new JsonSerializerSettings
                // {
                //     NullValueHandling = NullValueHandling.Ignore
                // });

                // var json = JsonConvert.SerializeObject(fieldDeviceRequestData);
                var json = JsonConvert.SerializeObject(fieldDeviceEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{BaseUrl}/{fieldDeviceId}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
                // var responseContent = await response.Content.ReadAsStringAsync();
                // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new ApplicationException($"Failed to update FieldDevice: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }
        }
        public async Task<bool> DeleteFieldDeviceAsync(int fieldDeviceId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{fieldDeviceId}");

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new ApplicationException($"Failed to delete FieldDevice: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new ApplicationException($"Failed to deserialize JSON: {ex.Message}");
            }

        }
    }
}
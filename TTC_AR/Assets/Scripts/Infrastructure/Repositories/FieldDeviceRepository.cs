

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
        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/FieldDevice";

        public FieldDeviceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<FieldDeviceEntity>> GetListFieldDeviceAsync(string grapperId)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}");
            return JsonConvert.DeserializeObject<List<FieldDeviceEntity>>(response);
        }

        public async Task<FieldDeviceEntity> GetFieldDeviceByIdAsync(string fieldDeviceid)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/{fieldDeviceid}");
            UnityEngine.Debug.Log(response.ToString());
            return JsonConvert.DeserializeObject<FieldDeviceEntity>(response);
        }

        public async Task<bool> CreateNewFieldDeviceAsync(string grapperId, FieldDeviceEntity fieldDeviceEntity)
        {
            var json = JsonConvert.SerializeObject(fieldDeviceEntity);

            UnityEngine.Debug.Log(json.ToString());

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}", content);

            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;

            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
        }

        public async Task<bool> UpdateFieldDeviceAsync(string fieldDeviceId, FieldDeviceEntity fieldDeviceEntity)
        {
            var json = JsonConvert.SerializeObject(fieldDeviceEntity);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{fieldDeviceId}", content);

            response.EnsureSuccessStatusCode();


            return response.IsSuccessStatusCode;
            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
        }
        public async Task<bool> DeleteFieldDeviceAsync(string fieldDeviceid)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{fieldDeviceid}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
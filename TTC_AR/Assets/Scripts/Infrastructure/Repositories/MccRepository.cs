
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
    public class MccRepository : IMccRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Mcc";

        public MccRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<MccEntity>> GetListMccAsync(int grapperId)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}");
            return JsonConvert.DeserializeObject<List<MccEntity>>(response);
        }

        public async Task<MccEntity> GetMccByIdAsync(int Mccid)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/{Mccid}");
            UnityEngine.Debug.Log(response.ToString());
            return JsonConvert.DeserializeObject<MccEntity>(response);
        }

        public async Task<bool> CreateNewMccAsync(int grapperId, MccEntity MccEntity)
        {
            var json = JsonConvert.SerializeObject(MccEntity);

            UnityEngine.Debug.Log(json.ToString());

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}", content);

            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;

            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<MccEntity>(responseContent);
        }

        public async Task<bool> UpdateMccAsync(int MccId, MccEntity MccEntity)
        {
            var json = JsonConvert.SerializeObject(MccEntity);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{MccId}", content);

            response.EnsureSuccessStatusCode();


            return response.IsSuccessStatusCode;
            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<MccEntity>(responseContent);
        }
        public async Task<bool> DeleteMccAsync(int MccId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{MccId}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
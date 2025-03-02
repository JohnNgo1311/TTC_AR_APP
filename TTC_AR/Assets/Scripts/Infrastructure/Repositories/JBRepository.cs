using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task<JBEntity> GetByIdAsync(int jbId)
        {
            var response = await _httpClient.GetAsync($"/api/jb/{jbId}");
            if (!response.IsSuccessStatusCode)
                return null; // Hoặc ném exception tùy yêu cầu

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JBEntity>(content);
        }

        public async Task<List<JBEntity>> GetListJBAsync(int grapperId)
        {
            var response = await _httpClient.GetAsync($"/api/jb/grapper/{grapperId}");
            if (!response.IsSuccessStatusCode)
                return new List<JBEntity>();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<JBEntity>>(content);
        }

        public async Task<bool> CreateNewJBAsync(int grapperId, JBEntity jbEntity)
        {
            var json = JsonConvert.SerializeObject(jbEntity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/jb/grapper/{grapperId}", content);
            return response.IsSuccessStatusCode; // Trả về true nếu thành công
        }

        public async Task<bool> UpdateJBAsync(int jbId, JBEntity jbEntity)
        {
            var json = JsonConvert.SerializeObject(jbEntity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/jb/{jbId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteJBAsync(int jbId)
        {
            var response = await _httpClient.DeleteAsync($"/api/jb/{jbId}");
            return response.IsSuccessStatusCode;
        }
    }
}
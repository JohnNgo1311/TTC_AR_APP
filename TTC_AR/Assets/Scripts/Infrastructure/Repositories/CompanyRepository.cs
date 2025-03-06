

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Company";

        public CompanyRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CompanyEntity> GetCompanyByIdAsync(string companyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{companyId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get company. Status: {response.StatusCode}");
                }
                UnityEngine.Debug.Log(response.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<CompanyEntity>(content);
                return entity;
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                throw new Exception($"Failed to fetch company data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi deserialize
                throw new Exception($"Failed to deserialize JSON: {ex.Message}");
            }
            // if (entity != null)
            // {
            //     UnityEngine.Debug.Log("Get Company Success");
            //     return entity;
            // }
            // else if (entity == null)
            // {
            //     UnityEngine.Debug.LogError("Failed to get Company and entity is null");
            //     throw new ApplicationException("Failed to get Company");

            // }
            // else
            // {
            //     UnityEngine.Debug.LogError("Failed to get Company ");
            //     throw new ApplicationException("Failed to get Company");
            // }

        }
        // public async Task<List<CompanyEntity>> GetListCompanyAsync(int grapperId)
        // {
        //     var response = await _httpClient.GetStringAsync($"{BaseUrl}");
        //     return JsonConvert.DeserializeObject<List<CompanyEntity>>(response);
        // }
    }
}
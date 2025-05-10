
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
    public class MccRepository : IMccRepository
    {
        private readonly HttpClient _httpClient;
        public MccRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<MccEntity>> GetListMccAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{GlobalVariable.baseUrl}");
                return JsonConvert.DeserializeObject<List<MccEntity>>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch Mcc", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<MccEntity> GetMccByIdAsync(int mccId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{GlobalVariable.baseUrl}/{mccId}");
                return JsonConvert.DeserializeObject<MccEntity>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch Mcc", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> CreateNewMccAsync(int grapperId, MccEntity MccEntity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(MccEntity);


                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}", content);

                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;

                // var responseContent = await response.Content.ReadAsStringAsync();
                // return JsonConvert.DeserializeObject<MccEntity>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create Mcc", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UpdateMccAsync(int mccId, MccEntity MccEntity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(MccEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalVariable.baseUrl}/{mccId}", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
                // var responseContent = await response.Content.ReadAsStringAsync();
                // return JsonConvert.DeserializeObject<MccEntity>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to update Mcc", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }
        public async Task<bool> DeleteMccAsync(int mccId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalVariable.baseUrl}/{mccId}");
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete Mcc", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }
    }
}
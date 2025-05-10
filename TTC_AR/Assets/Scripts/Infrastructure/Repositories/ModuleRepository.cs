using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApplicationLayer.Dtos.Module;
using Domain.Interfaces;


namespace Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly HttpClient _httpClient;


        public ModuleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // _httpClient.BaseAddress = new Uri(GlobalVariable.baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về ModuleResponseDto do kết quả server trả chỉ là một tập hợp con của Entity
        public async Task<ModuleEntity> GetModuleByIdAsync(int moduleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/{moduleId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get Module. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ModuleEntity>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch Module", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
          
        }

        //! Trả về List<ModuleGeneralDto> do kết quả server trả chỉ là một tập hợp con của Entity
        public async Task<List<ModuleEntity>> GetListModuleAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync(GlobalVariable.baseUrl);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get Module list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ModuleEntity>>(content);
                }

            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch Module list", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> CreateNewModuleAsync(int grapperId, ModuleEntity requestData)
        {
            try
            {
                // UnityEngine.Debug.Log("Run Repository");

                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                //  var ModuleRequestData = ConvertModuleRequestData(ModuleEntity);

                var json = JsonConvert.SerializeObject(requestData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create Module", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }

        public async Task<bool> UpdateModuleAsync(int moduleId, ModuleEntity requestData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(requestData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalVariable.baseUrl}/{moduleId}", content);

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to update Module", ex); // Ném lỗi HTTP lên UseCase

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteModuleAsync(int moduleId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalVariable.baseUrl}/{moduleId}");
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete Module", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }
        // private object ConvertModuleRequestData(ModuleEntity ModuleEntity)
        // {

        //     return new
        //     {
        //         Name = ModuleEntity.Name,
        //         Rack = new
        //         {
        //             ModuleEntity.RackEntity.Id,
        //             ModuleEntity.RackEntity.Name
        //         },
        //         Devices = ModuleEntity.DeviceEntities.Select(d => new
        //         {
        //             d.Id,
        //             d.Code
        //         }).ToList(),
        //         JBEntities = ModuleEntity.JBEntities.Select(j => new
        //         {
        //             j.Id,
        //             j.Name
        //         }).ToList(),

        //         ModuleSpecification = new
        //         {
        //             ModuleEntity.ModuleSpecificationEntity.Id,
        //             ModuleEntity.ModuleSpecificationEntity.Code
        //         },
        //         AdapterSpecificationEntity = new
        //         {
        //             ModuleEntity.AdapterSpecificationEntity.Id,
        //             ModuleEntity.AdapterSpecificationEntity.Code
        //         },
        //     };
        // }
    }


}
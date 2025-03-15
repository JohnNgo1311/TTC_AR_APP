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

        private const string BaseUrl = "https://6776bd1c12a55a9a7d0cbc42.mockapi.io/api/v2/Module"; // URL server ngoài thực tế

        public ModuleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về ModuleResponseDto do kết quả server trả chỉ là một tập hợp con của Entity
        public async Task<ModuleEntity> GetModuleByIdAsync(string ModuleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Module/{ModuleId}");
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
                throw ex; // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        //! Trả về List<ModuleGeneralDto> do kết quả server trả chỉ là một tập hợp con của Entity
        public async Task<List<ModuleEntity>> GetListModuleAsync(string grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
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
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error during HTTP request", ex);
            }
        }

        public async Task<bool> CreateNewModuleAsync(string grapperId, ModuleEntity requestData)
        {
            try
            {
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                //  var ModuleRequestData = ConvertModuleRequestData(ModuleEntity);

                var json = JsonConvert.SerializeObject(requestData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"/api/Module/grapper/{grapperId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create Module. Status: {response.StatusCode}");

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

        public async Task<bool> UpdateModuleAsync(string ModuleId, ModuleEntity requestData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(requestData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Module/{ModuleId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to update Module. Status: {response.StatusCode}");

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

        public async Task<bool> DeleteModuleAsync(string ModuleId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Module/{ModuleId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to Delete Module. Status: {response.StatusCode}");

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
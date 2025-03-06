// using Domain.Entities;
// using Domain.Interfaces;
// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using UnityEngine.Networking;
// using Infrastructure.Dtos;
// using System.Linq;
// using ApplicationLayer.Dtos;

// namespace Infrastructure.Repositories
// {
//     public class FieldDeviceRepository : IFieldDeviceRepository
//     {
//         private readonly HttpClient _httpClient;

//         private const string BaseUrl = "https://external-server-api.com"; // URL server ngoài thực tế

//         public FieldDeviceRepository(HttpClient httpClient)
//         {
//             _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
//             _httpClient.BaseAddress = new Uri(BaseUrl);
//             _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//         }


//         //! Trả về FieldDeviceResponseDto do server trả về tập hợp con của FieldDeviceEntity
//         public async Task<FieldDeviceResponseDto> GetFieldDeviceByIdAsync(int FieldDeviceId)
//         {
//             try
//             {
//                 var response = await _httpClient.GetAsync($"/api/FieldDevice/{FieldDeviceId}");

//                 if (!response.IsSuccessStatusCode)
//                     throw new HttpRequestException($"Failed to get FieldDevice. Status: {response.StatusCode}");

//                 var content = await response.Content.ReadAsStringAsync();

//                 return JsonConvert.DeserializeObject<FieldDeviceResponseDto>(content) ?? throw new ApplicationException("Failed to get FieldDevice");
//             }
//             catch (HttpRequestException ex)
//             {
//                 throw ex; // Ném lỗi HTTP lên UseCase
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
//             }
//         }




//         //! Trả về List<FieldDeviceResponseDto> do server trả về tập hợp con của FieldDeviceEntity
//         public async Task<List<FieldDeviceResponseDto>> GetListFieldDeviceAsync(int grapperId)
//         {
//             try
//             {
//                 var response = await _httpClient.GetAsync($"/api/FieldDevice/grapper/{grapperId}");
//                 if (!response.IsSuccessStatusCode)
//                     throw new HttpRequestException($"Failed to get FieldDevice list. Status: {response.StatusCode}");
//                 var content = await response.Content.ReadAsStringAsync();
//                 return JsonConvert.DeserializeObject<List<FieldDeviceResponseDto>>(content) ?? throw new ApplicationException("Failed to get FieldDevice list");
//             }
//             catch (HttpRequestException ex)
//             {
//                 throw ex;
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception("Unexpected error during HTTP request", ex);
//             }
//         }


//         //! Thm số là FieldDeviceRequestDto do server yêu cầu dữ liệu tương tự mà FieldDeviceEntity không hoàn toàn khớp
//         public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceEntity fieldDeviceEntity)
//         {
//             try
//             {

//                 if (fieldDeviceEntity == null)
//                     throw new ArgumentNullException(nameof(fieldDeviceEntity), "fieldDeviceEntity cannot be null");
//                 var json = JsonConvert.SerializeObject(fieldDeviceEntity, new JsonSerializerSettings
//                 {
//                     NullValueHandling = NullValueHandling.Ignore,
//                     ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
//                 });
//                 var content = new StringContent(json, Encoding.UTF8, "application/json");
//                 var response = await _httpClient.PostAsync($"/api/FieldDevice/grapper/{grapperId}", content);
//                 return response.IsSuccessStatusCode;


//                 // if (fieldDeviceEntity == null)
//                 //     throw new ArgumentNullException(nameof(fieldDeviceEntity), "Request data cannot be null");
//                 // var json = JsonConvert.SerializeObject(fieldDeviceEntity);
//                 // var content = new StringContent(json, Encoding.UTF8, "application/json");
//                 // var response = await _httpClient.PostAsync($"/api/FieldDevice/grapper/{grapperId}", content);
//                 // return response.IsSuccessStatusCode;
//             }
//             catch (HttpRequestException ex) { throw ex; }
//             catch (Exception ex) { throw new Exception("Unexpected error during HTTP request", ex); }
//         }


//         //! Thm số là FieldDeviceRequestDto do server yêu cầu dữ liệu tương tự mà FieldDeviceEntity không hoàn toàn khớp
//         public async Task<bool> UpdateFieldDeviceAsync(int fieldDeviceId, FieldDeviceEntity fieldDeviceEntity)
//         {
//             try
//             {   
//                 if (fieldDeviceEntity == null)
//                     throw new ArgumentNullException(nameof(fieldDeviceEntity), "fieldDeviceEntity cannot be null");
//                 var json = JsonConvert.SerializeObject(fieldDeviceEntity, new JsonSerializerSettings
//                 {
//                     NullValueHandling = NullValueHandling.Ignore,
//                     ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
//                 });
//                 var content = new StringContent(json, Encoding.UTF8, "application/json");
//                 var response = await _httpClient.PostAsync($"/api/FieldDevice/grapper/{fieldDeviceEntity.Id}", content);
//                 return response.IsSuccessStatusCode;

//                 // if (fieldDeviceEntity == null)
//                 //     throw new ArgumentNullException(nameof(fieldDeviceEntity), "Request data cannot be null");
//                 // var json = JsonConvert.SerializeObject(fieldDeviceEntity);
//                 // var content = new StringContent(json, Encoding.UTF8, "application/json");
//                 // var response = await _httpClient.PutAsync($"/api/FieldDevice/{fieldDeviceId}", content);
//                 // return response.IsSuccessStatusCode;
//             }
//             catch (HttpRequestException ex) { throw ex; }
//             catch (Exception ex) { throw new Exception("Unexpected error during HTTP request", ex); }
//         }


//         public async Task<bool> DeleteFieldDeviceAsync(int fieldDeviceId)
//         {
//             try
//             {
//                 var response = await _httpClient.DeleteAsync($"/api/FieldDevice/{fieldDeviceId}");

//                 if (!response.IsSuccessStatusCode)
//                     throw new HttpRequestException($"Failed to delete FieldDevice. Status: {response.StatusCode}");
//                 else return true;

//             }
//             catch (HttpRequestException ex)
//             {
//                 throw ex; // Ném lỗi HTTP lên UseCase
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
//             }

//         }
//         // public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceEntity fieldDeviceEntity)
//         // {
//         //     try
//         //     {

//         //         if (fieldDeviceEntity == null)
//         //             throw new ArgumentNullException(nameof(fieldDeviceEntity), "Entity cannot be null");
//         //         // Tạo dữ liệu tối giản gửi lên server
//         //         else
//         //         {
//         //             // Gửi Entity trực tiếp mà không cần ánh xạ trong Repository
//         //             var fieldDeviceRequestData = new
//         //             {
//         //                 Name = fieldDeviceEntity.Name,
//         //                 Mcc = new { fieldDeviceEntity.Mcc.Id, fieldDeviceEntity.Mcc.CabinetCode },
//         //                 RatedPower = string.IsNullOrEmpty(fieldDeviceEntity.RatedPower) ? string.Empty : fieldDeviceEntity.RatedPower,
//         //                 RatedCurrent = string.IsNullOrEmpty(fieldDeviceEntity.RatedCurrent) ? string.Empty : fieldDeviceEntity.RatedCurrent,
//         //                 ActiveCurrent = string.IsNullOrEmpty(fieldDeviceEntity.ActiveCurrent) ? string.Empty : fieldDeviceEntity.ActiveCurrent,
//         //                 ListConnectionImages = fieldDeviceEntity.ListConnectionImageEntities
//         //                     .Select(i => new { i.Id, i.Name }).ToList(),

//         //                 Note = string.IsNullOrEmpty(fieldDeviceEntity.Note) ? string.Empty : fieldDeviceEntity.Note
//         //             };


//         //             // var fieldDeviceRequestData = ConvertToRequestData(fieldDeviceEntity);

//         //             var json = JsonConvert.SerializeObject(fieldDeviceRequestData);
//         //             var content = new StringContent(json, Encoding.UTF8, "application/json");
//         //             var response = await _httpClient.PostAsync($"/api/FieldDevice/grapper/{grapperId}", content);

//         //             if (!response.IsSuccessStatusCode)
//         //                 throw new HttpRequestException($"Failed to create FieldDevice. Status: {response.StatusCode}");
//         //             else { return true; }

//         //         }
//         //     }
//         //     catch (HttpRequestException ex)
//         //     {
//         //         throw ex; // Ném lỗi HTTP lên UseCase
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
//         //     }
//         // }




//         // public async Task<bool> UpdateFieldDeviceAsync(int FieldDeviceId, FieldDeviceEntity fieldDeviceEntity)
//         // {
//         //     try
//         //     {
//         //         if (fieldDeviceEntity == null)
//         //             throw new ArgumentNullException(nameof(fieldDeviceEntity), "Entity cannot be null");
//         //         else
//         //         {
//         //             // var fieldDeviceRequestData = ConvertToRequestData(fieldDeviceEntity);
//         //             // Gửi Entity trực tiếp mà không cần ánh xạ trong Repository
//         //             var fieldDeviceRequestData = new
//         //             {
//         //                 Name = fieldDeviceEntity.Name,
//         //                 Mcc = new { fieldDeviceEntity.Mcc.Id, fieldDeviceEntity.Mcc.CabinetCode },
//         //                 RatedPower = string.IsNullOrEmpty(fieldDeviceEntity.RatedPower) ? string.Empty : fieldDeviceEntity.RatedPower,
//         //                 RatedCurrent = string.IsNullOrEmpty(fieldDeviceEntity.RatedCurrent) ? string.Empty : fieldDeviceEntity.RatedCurrent,
//         //                 ActiveCurrent = string.IsNullOrEmpty(fieldDeviceEntity.ActiveCurrent) ? string.Empty : fieldDeviceEntity.ActiveCurrent,
//         //                 ListConnectionImages = fieldDeviceEntity.ListConnectionImageEntities
//         //                     .Select(i => new { i.Id, i.Name }).ToList(),

//         //                 Note = string.IsNullOrEmpty(fieldDeviceEntity.Note) ? string.Empty : fieldDeviceEntity.Note
//         //             };
//         //             var json = JsonConvert.SerializeObject(fieldDeviceRequestData);
//         //             var content = new StringContent(json, Encoding.UTF8, "application/json");
//         //             var response = await _httpClient.PutAsync($"/api/FieldDevice/{FieldDeviceId}", content);

//         //             if (!response.IsSuccessStatusCode)
//         //                 throw new HttpRequestException($"Failed to update FieldDevice. Status: {response.StatusCode}");
//         //             else { return true; }

//         //         }
//         //     }
//         //     catch (HttpRequestException ex)
//         //     {
//         //         throw ex; // Ném lỗi HTTP lên UseCase
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         throw new Exception("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
//         //     }
//         // }



//         //! Ánh xạ Entity => dữ liệu gửi lên server
//         // private object ConvertToRequestData(FieldDeviceEntity fieldDeviceEntity)
//         // {
//         //     return new
//         //     {
//         //         Name = fieldDeviceEntity.Name,
//         //         Mcc = new { fieldDeviceEntity.Mcc.Id, fieldDeviceEntity.Mcc.CabinetCode },
//         //         RatedPower = string.IsNullOrEmpty(fieldDeviceEntity.RatedPower)? string.Empty : fieldDeviceEntity.RatedPower,
//         //         RatedCurrent = string.IsNullOrEmpty(fieldDeviceEntity.RatedCurrent)? string.Empty : fieldDeviceEntity.RatedCurrent,
//         //         ActiveCurrent = string.IsNullOrEmpty(fieldDeviceEntity.ActiveCurrent)? string.Empty : fieldDeviceEntity.ActiveCurrent,
//         //         ListConnectionImages = fieldDeviceEntity.ListConnectionImageEntities
//         //                     .Select(i => new { i.Id, i.Name }).ToList(),

//         //         Note =string.IsNullOrEmpty(fieldDeviceEntity.Note)? string.Empty : fieldDeviceEntity.Note
//         //     };
//         // }

//     }
// }


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


        public async Task<List<FieldDeviceEntity>> GetListFieldDeviceAsync(int grapperId)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}");
            return JsonConvert.DeserializeObject<List<FieldDeviceEntity>>(response);
        }
        public async Task<FieldDeviceEntity> GetFieldDeviceByIdAsync(int fieldDeviceid)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/{fieldDeviceid}");
            UnityEngine.Debug.Log(response.ToString());
            return JsonConvert.DeserializeObject<FieldDeviceEntity>(response);
        }

        public async Task<bool> CreateNewFieldDeviceAsync(int grapperId, FieldDeviceEntity fieldDeviceEntity)
        {
            var json = JsonConvert.SerializeObject(fieldDeviceEntity, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            UnityEngine.Debug.Log(json.ToString());

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}", content);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
        }

        public async Task<bool> UpdateFieldDeviceAsync(int fieldDeviceId, FieldDeviceEntity fieldDeviceEntity)
        {
            var json = JsonConvert.SerializeObject(fieldDeviceEntity, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{fieldDeviceId}", content);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
            // var responseContent = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<FieldDeviceEntity>(responseContent);
        }
        public async Task<bool> DeleteFieldDeviceAsync(int fieldDeviceid)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{fieldDeviceid}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
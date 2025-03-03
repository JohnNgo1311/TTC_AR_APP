using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Infrastructure.Dtos;
using System.Linq;
using ApplicationLayer.Dtos;

namespace Infrastructure.Repositories
{
    public class AdapterSpecificationRepository : IAdapterSpecificationRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://external-server-api.com"; // URL server ngoài thực tế

        public AdapterSpecificationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! Trả về Entity
        public async Task<AdapterSpecificationEntity> GetAdapterSpecificationByIdAsync(int AdapterSpecificationId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/AdapterSpecification/{AdapterSpecificationId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get AdapterSpecification. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AdapterSpecificationEntity>(content);
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
        public async Task<List<AdapterSpecificationEntity>> GetListAdapterSpecificationAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/AdapterSpecification/grapper/{grapperId}");
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

        public async Task<bool> CreateNewAdapterSpecificationAsync(int grapperId, AdapterSpecificationEntity adapterSpecificationEntity)
        {
            try
            {

                if (adapterSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(adapterSpecificationEntity), "Entity cannot be null");
                // Tạo dữ liệu tối giản gửi lên server
                else
                {
                    var adapterSpecificationRequestData = new
                    {
                        code = adapterSpecificationEntity.Code, // Mandatory
                        Type = adapterSpecificationEntity.Type ?? "", // Nullable/Empty
                        Communication = adapterSpecificationEntity.Communication ?? "",
                        NumOfModulesAllowed = adapterSpecificationEntity.NumOfModulesAllowed ?? "",
                        CommSpeed = adapterSpecificationEntity.CommSpeed ?? "",
                        InputSupply = adapterSpecificationEntity.InputSupply ?? "",
                        OutputSupply = adapterSpecificationEntity.OutputSupply ?? "",
                        InrushCurrent = adapterSpecificationEntity.InrushCurrent ?? "",
                        Alarm = adapterSpecificationEntity.Alarm ?? "",
                        Note = adapterSpecificationEntity.Noted ?? "", // Chú ý tên field "Note" thay vì "Noted"
                        PDFManual = adapterSpecificationEntity.PdfManual ?? "" // Chú ý tên field "PDFManual"
                    };

                    var json = JsonConvert.SerializeObject(adapterSpecificationRequestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"/api/AdapterSpecification/grapper/{grapperId}", content);

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

        public async Task<bool> UpdateAdapterSpecificationAsync(int AdapterSpecificationId, AdapterSpecificationEntity adapterSpecificationEntity)
        {
            try
            {
                if (adapterSpecificationEntity == null)
                    throw new ArgumentNullException(nameof(adapterSpecificationEntity), "Entity cannot be null");
                else
                {
                    var adapterSpecificationRequestData = new
                    {
                        code = adapterSpecificationEntity.Code, // Mandatory
                        Type = adapterSpecificationEntity.Type ?? "", // Nullable/Empty
                        Communication = adapterSpecificationEntity.Communication ?? "",
                        NumOfModulesAllowed = adapterSpecificationEntity.NumOfModulesAllowed ?? "",
                        CommSpeed = adapterSpecificationEntity.CommSpeed ?? "",
                        InputSupply = adapterSpecificationEntity.InputSupply ?? "",
                        OutputSupply = adapterSpecificationEntity.OutputSupply ?? "",
                        InrushCurrent = adapterSpecificationEntity.InrushCurrent ?? "",
                        Alarm = adapterSpecificationEntity.Alarm ?? "",
                        Note = adapterSpecificationEntity.Noted ?? "", // Chú ý tên field "Note" thay vì "Noted"
                        PDFManual = adapterSpecificationEntity.PdfManual ?? "" // Chú ý tên field "PDFManual"
                    };

                    var json = JsonConvert.SerializeObject(adapterSpecificationRequestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"/api/AdapterSpecification/{AdapterSpecificationId}", content);

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

        public async Task<bool> DeleteAdapterSpecificationAsync(int adapterSpecificationId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/AdapterSpecification/{adapterSpecificationId}");

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
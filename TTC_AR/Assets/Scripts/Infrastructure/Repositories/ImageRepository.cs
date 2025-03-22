using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.Interfaces;


namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://67176614b910c6a6e027ebfc.mockapi.io/api/v1/Image"; // URL server ngoài thực tế

        public ImageRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về ImageEntity
        public async Task<ImageEntity> GetImageByIdAsync(string ImageId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{ImageId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get Image. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<ImageEntity>(content);

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

        //! Trả về List<ImageEntity>
        public async Task<List<ImageEntity>> GetListImageAsync(string grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get Image list. Status: {response.StatusCode}");
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ImageEntity>>(content);
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


        public async Task<bool> CreateNewImageAsync(string grapperId, ImageEntity ImageEntity)
        {
            try
            {
                if (ImageEntity == null)
                    throw new ArgumentNullException(nameof(ImageEntity), "Request data cannot be null");

                // var json = JsonConvert.SerializeObject(ImageEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var ImageImageEntity = ConvertImageImageEntity(ImageEntity);

                var json = JsonConvert.SerializeObject(ImageEntity);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to create Image. Status: {response.StatusCode}");

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

        public async Task<bool> UpdateImageAsync(string ImageId, ImageEntity ImageEntity)
        {
            try
            {
                if (ImageEntity == null)
                    throw new ArgumentNullException(nameof(ImageEntity), "Request data cannot be null");
                var json = JsonConvert.SerializeObject(ImageEntity);
                // Tạo dữ liệu tối giản gửi lên server với tên property khớp yêu cầu
                // var ImageImageEntity = ConvertImageImageEntity(ImageEntity);

                // var json = JsonConvert.SerializeObject(ImageEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{BaseUrl}/{ImageId}", content);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to Update Image. Status: {response.StatusCode}");

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

        public async Task<bool> DeleteImageAsync(string ImageId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Image/{ImageId}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to delete Image. Status: {response.StatusCode}");

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
        // private object ConvertImageRequestData(ImageEntity ImageEntity)
        // {
        //     return new
        //     {
        //         name = ImageEntity.Name,
        //         Location = ImageEntity.Location ?? "",
        //         ListDevices = ImageEntity.DeviceEntities?
        //             .Where(d => d != null)
        //             .Select(d => new DeviceBasicDto(d.Id, d.Code))
        //             .ToList() ?? new List<DeviceBasicDto>(),
        //         ListModules = ImageEntity.ModuleEntities
        //             .Where(m => m != null)
        //             .Select(m => new ModuleBasicDto(m.Id, m.Name))
        //             .ToList() ?? new List<ModuleBasicDto>(),
        //         OutdoorImage = ImageEntity.OutdoorImageEntity != null
        //             ? new ImageBasicDto(ImageEntity.OutdoorImageEntity.Id, ImageEntity.OutdoorImageEntity.Name)
        //             : null,
        //         ListConnectionImages = ImageEntity.ConnectionImageEntities?
        //             .Where(i => i != null)
        //             .Select(i => new ImageBasicDto(i.Id, i.Name))
        //             .ToList() ?? new List<ImageBasicDto>()
        //     };
        // }
    }


}
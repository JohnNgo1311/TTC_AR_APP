using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.Interfaces;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;


namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly HttpClient _httpClient;


        public ImageRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // _httpClient.BaseAddress = new Uri(GlobalVariable.baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //! TRả về ImageEntity
        public async Task<ImageEntity> GetImageByIdAsync(int ImageId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/Images/{ImageId}/info");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get Image. Status: {response.StatusCode}");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // UnityEngine.Debug.Log(content);
                    return JsonConvert.DeserializeObject<ImageEntity>(content);

                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch Image", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }

        }

        //! Trả về List<ImageEntity>
        public async Task<List<ImageEntity>> GetListImageAsync(int grapperId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalVariable.baseUrl}/Grappers/{grapperId}/images");
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
                throw new ApplicationException("Failed to fetch Image list", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }


        public async Task<bool> CreateNewImageAsync(int grapperId, ImageEntity ImageEntity)
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

                var response = await _httpClient.PostAsync($"{GlobalVariable.baseUrl}/Images/grapperId?grapperId={grapperId}&fileName={ImageEntity.Name}", content);
                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(temp);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to create Image", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> DeleteImageAsync(int ImageId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalVariable.baseUrl}/Images/{ImageId}");
                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(temp);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to delete Image", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex); // Bao bọc lỗi khác
            }
        }

        public async Task<bool> UploadNewImageFromGallery(int grapperId, Texture2D texture, string fieldName, string fileName, string filePath, string mimeType)
        {
            // Lưu ảnh từ Texture2D thành file ảnh PNG            
            try
            {
                Debug.Log("Run Repository 1");

                File.WriteAllBytes(filePath, texture.EncodeToPNG());
                Debug.Log("Run Repository 2");
                byte[] fileData = File.ReadAllBytes(filePath);
                Debug.Log("Run Repository 3");
                WWWForm form = new WWWForm();
                Debug.Log("Run Repository 4");
                form.AddBinaryData(fieldName, fileData, fileName, mimeType);
                Debug.Log("Run Repository 5");
                using (UnityWebRequest request = UnityWebRequest.Post("https://imagekit.io/tools/free-image-hosting/", form))
                {
                    Debug.Log("Run Repository 6");
                    request.SendWebRequest();
                    while (!request.isDone)
                    {
                        await Task.Delay(50); // Đợi cho đến khi yêu cầu hoàn tất
                    }

                    if (request.result != UnityWebRequest.Result.Success)
                    {

                        // Debug.LogError($"❌ Lỗi upload ảnh: {request.error}");
                        throw new Exception($"Lỗi upload ảnh: {request.error}");
                    }
                    else
                    {
                        // Debug.Log($"✅ Upload thành công: {fileName}");
                        return true;

                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to upload image", ex); // Ném lỗi HTTP lên UseCase
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Lỗi upload ảnh: {ex.Message}", ex);
            }


        }
        public async Task<bool> UploadNewImageFromCamera(int grapperId, Texture2D texture, string fieldName, string fileName)
        {
            try
            { // Lưu ảnh từ Texture2D thành file ảnh PNG
              // Mã hóa thành PNG
                Debug.Log("Run Repository 1");

                byte[] imageData = texture.EncodeToPNG();

                Debug.Log("Run Repository 2");


                // Tạo form dữ liệu
                WWWForm form = new WWWForm();
                // Thêm dữ liệu ảnh vào form
                Debug.Log("Run Repository 3");

                form.AddBinaryData(fieldName, imageData, fileName, "image/png");
                // Gửi yêu cầu lên server
                Debug.Log("Run Repository 4");
                Debug.Log($"UploadNewImageFromCamera: {grapperId} {texture} {fieldName} {fileName}");
                using (UnityWebRequest request = UnityWebRequest.Post("https://imagekit.io/tools/free-image-hosting/", form))
                {
                    request.SendWebRequest();

                    while (!request.isDone)
                    {
                        await Task.Delay(50); // Đợi cho đến khi yêu cầu hoàn tất
                    }

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        // Debug.LogError($"❌ Lỗi upload ảnh: {request.error}");
                        throw new ApplicationException($"Lỗi upload ảnh: {request.error}");
                    }
                    else
                    {
                        // Debug.Log($"✅ Upload thành công: {fileName}");
                        return true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to upload image", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error during HTTP request", ex);
            }
        }


    }

    // public IEnumerator UploadNewImage()
    // {
    //     // Chụp ảnh màn hình
    //     string filePath = Path.Combine(Application.persistentDataPath, "screenshot.png");

    //     yield return new WaitForEndOfFrame();
    //     // Đọc dữ liệu ảnh
    //     byte[] imageBytes = File.ReadAllBytes(filePath);

    //     // Tạo DTO
    //     ImageDto imageDto = new ImageDto
    //     {
    //         FileName = "screenshot.png",
    //         ContentType = "image/png",
    //         Size = imageBytes.Length,
    //         Description = "Ảnh chụp từ Unity"
    //     };

    //     // Chuyển DTO thành JSON
    //     string jsonDto = JsonUtility.ToJson(imageDto);

    //     // Tạo form để gửi cả file và DTO
    //     WWWForm form = new WWWForm();
    //     form.AddBinaryData("file", imageBytes, "screenshot.png", "image/png");

    //     form.AddField("metadata", jsonDto);

    //     using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariable.baseUrl, form))
    //     {
    //         yield return www.SendWebRequest();

    //         if (www.result == UnityWebRequest.Result.Success)
    //         {
    //         }
    //         else
    //         {
    //         }
    //     }

    //     // Xóa file tạm
    //     File.Delete(filePath);
    // }
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class APIManagerOpenCV : MonoBehaviour
{
    public static APIManagerOpenCV Instance { get; private set; }

    private Dictionary<string, ModuleInformationModel> Dic_ModuleInformationModels = new Dictionary<string, ModuleInformationModel>();
    private Dictionary<string, List<Texture2D>> list_JBConnectionImagesFromModule = new Dictionary<string, List<Texture2D>>();
    private Dictionary<string, Texture2D> list_JBLocationImagesFromModule = new Dictionary<string, Texture2D>();
    private Dictionary<string, List<Texture2D>> list_AdditionalImagesFromDevice = new Dictionary<string, List<Texture2D>>();
    private List<JBInformationModel> temp_ListJBInformationModel = new List<JBInformationModel>();
    private List<string> temp_ListAdditionalImages = new List<string>();
    // private Dictionary<string, List<JBInformationModel>> temp_ListJBInformationModel = new Dictionary<string, List<JBInformationModel>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task<List<ModuleInformationModel>> GetListModule(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                // if (response.IsSuccessStatusCode)
                // {
                // string json = await response.Content.ReadAsStringAsync();
                // Debug.Log("json: " + response);

                var listModuleInformationModel = JsonConvert.DeserializeObject<List<ModuleInformationModel>>(response);

                // Debug.Log("listModuleInformationModel.Count: " + listModuleInformationModel.Count);

                if (listModuleInformationModel != null && listModuleInformationModel.Count > 0)
                {
                    StaticVariable.temp_ListModuleInformationModel = listModuleInformationModel;
                    Dic_ModuleInformationModels.Clear();

                    foreach (var Module in listModuleInformationModel)
                    {
                        Dic_ModuleInformationModels.TryAdd(Module.Name, Module);
                    }

                    StaticVariable.Dic_ModuleInformationModel = Dic_ModuleInformationModels;
                    // Debug.Log("Dic_ModuleInformationModels.Count: " + Dic_ModuleInformationModels.Count);
                }
                return listModuleInformationModel;
                // }
                // else
                // {
                //     HandleRequestError($"Failed to get JB information. Status code: {response.StatusCode}");
                //     return null;
                // }
            }
        }
        catch (Exception ex)
        {
            HandleRequestError($"Unexpected error: {ex.Message + " " + ex.StackTrace + " " + ex.InnerException}");
            return null;
        }
    }

    public async Task<ModuleInformationModel> GetModule(string url)
    {
        StaticVariable.ActiveCloseCanvasButton = false;
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    // Debug.Log("json: " + json);
                    var moduleInformationModel = JsonConvert.DeserializeObject<ModuleInformationModel>(json);

                    if (moduleInformationModel != null)
                    {
                        StaticVariable.ModuleId = moduleInformationModel.Id;
                        // StaticVariable.ModuleSpecificationId = moduleInformationModel.ModuleSpecificationModel.Id;
                        // StaticVariable.AdapterSpecificationId = moduleInformationModel.AdapterSpecificationModel.Id;

                        StaticVariable.temp_ListJBInformationModelFromModule.Clear();
                        StaticVariable.temp_ListDeviceInformationModelFromModule.Clear();

                        StaticVariable.temp_ModuleInformationModel = null;
                        StaticVariable.temp_ModuleSpecificationModel = null;
                        StaticVariable.temp_AdapterSpecificationModel = null;

                        StaticVariable.temp_ListJBInformationModelFromModule = moduleInformationModel.ListJBInformationModel;
                        StaticVariable.temp_ListDeviceInformationModelFromModule = moduleInformationModel.ListDeviceInformationModel;

                        StaticVariable.temp_ModuleSpecificationModel = moduleInformationModel.ModuleSpecificationModel;
                        StaticVariable.temp_AdapterSpecificationModel = moduleInformationModel.AdapterSpecificationModel;

                        // Debug.Log("temp_ListJBInformationModelFromModule.Count: " + StaticVariable.temp_ListJBInformationModelFromModule.Count);
                        // Debug.Log("temp_ListDeviceInformationModelFromModule.Count: " + StaticVariable.temp_ListDeviceInformationModelFromModule.Count);
                        return moduleInformationModel;
                    }
                    else
                    {
                        HandleRequestError("Failed to get JB information.");
                        return null;
                    }
                }
                else
                {
                    HandleRequestError($"Failed to get JB information. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (JsonException jsonEx)
        {
            HandleRequestError(jsonEx.Message);
            return null;

        }
        catch (Exception ex)
        {
            HandleRequestError(ex.Message);
            return null;

        }
    }

    private void HandleRequestError(string error)
    {
        Debug.LogError($"Request error: {error}");
        throw new Exception(error);
    }

    public async Task<List<DeviceInformationModel>> GetListDevice(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    // Debug.Log("json: " + json);
                    var deviceInformationModel = JsonConvert.DeserializeObject<List<DeviceInformationModel>>(json);

                    if (deviceInformationModel != null)
                    {

                        if (StaticVariable.ready_To_Reset_ListDevice)
                        {
                            StaticVariable.temp_ListDeviceInformationModelFromDeviceName.Clear();
                            StaticVariable.temp_ListAdditionalImageFromDevice.Clear();
                            // Debug.Log("Clear temp_ListDeviceInformationModelFromDeviceName");
                        }

                        foreach (var device in deviceInformationModel)
                        {
                            StaticVariable.temp_ListDeviceInformationModelFromDeviceName.Add(device.Code, device);

                            if (device.AdditionalConnectionImages.Count == 0)
                            {
                                continue;
                            }
                            StaticVariable.temp_ListAdditionalImageFromDevice.Add(device.Code, device.AdditionalConnectionImages.Select(image => image.url).ToList());
                            Debug.Log("device.Code: " + device.Code + " device.AdditionalConnectionImages.Count: " + device.AdditionalConnectionImages.Count);
                        }
                    }
                    return deviceInformationModel;
                }
                else
                {
                    HandleRequestError($"Failed to get JB information. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            HandleRequestError($"Unexpected error: {ex.Message}");
            return null;
        }
    }

    private async Task<bool> SendWebRequestAsync(UnityWebRequest request)
    {
        try
        {
            request.timeout = 20;
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            bool isSuccess = request.result == UnityWebRequest.Result.Success;
            StaticVariable.API_Status = isSuccess;
            return isSuccess;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during web request: {ex}");
            StaticVariable.API_Status = false;
            return false;
        }
    }


    private SemaphoreSlim _semaphore = new SemaphoreSlim(10);
    public async Task DownloadImagesAsync()
    {
        StaticVariable.ActiveCloseCanvasButton = false;
        try
        {

            temp_ListJBInformationModel = StaticVariable.temp_ListDeviceInformationModelFromDeviceName.SelectMany(jb => jb.Value.JBInformationModel).ToList();
            temp_ListAdditionalImages = StaticVariable.temp_ListAdditionalImageFromDevice.SelectMany(image => image.Value).ToList();

            if (temp_ListJBInformationModel == null || temp_ListJBInformationModel.Count == 0)
            {
                Debug.LogError("No JB information models available to download images.");
            }
            else
            {
                var downloadTasks = new List<Task<Texture2D>>();

                if (StaticVariable.temp_ListAdditionalImageFromDevice.ContainsKey(StaticVariable.device_Code))
                {
                    if (!list_AdditionalImagesFromDevice.ContainsKey(StaticVariable.device_Code))
                    {
                        list_AdditionalImagesFromDevice[StaticVariable.device_Code] = new List<Texture2D>();
                    }

                    foreach (var image in temp_ListAdditionalImages)
                    {
                        // if (!string.IsNullOrEmpty(image.url))
                        // {
                        //     downloadTasks.Add(DownloadImageAsync(image.url));
                        // }
                    }
                }

                foreach (var jb in temp_ListJBInformationModel)
                {

                    if (jb.Name == StaticVariable.jb_TSD_Name)
                    {
                        // Khởi tạo các danh sách nếu chưa tồn tại
                        if (!list_JBConnectionImagesFromModule.ContainsKey(jb.Name))
                        {
                            list_JBConnectionImagesFromModule[jb.Name] = new List<Texture2D>();
                        }

                        if (!list_JBLocationImagesFromModule.ContainsKey(jb.Name))
                        {
                            list_JBLocationImagesFromModule[jb.Name] = new Texture2D(2, 2);
                        }

                        //? Tải hình ảnh ngoài trời
                        if (!string.IsNullOrEmpty(jb.OutdoorImage.url))
                        {
                            downloadTasks.Add(DownloadImageAsync(jb.OutdoorImage.url));
                        }

                        //? Tải danh sách hình ảnh kết nối
                        foreach (var image in jb.ListConnectionImages)
                        {
                            if (!string.IsNullOrEmpty(image.url))
                            {
                                downloadTasks.Add(DownloadImageAsync(image.url));
                            }
                        }

                        //? Chờ tất cả hình ảnh kết nối hoàn tất
                        var downloadedTextures = await Task.WhenAll(downloadTasks);

                        //? Cập nhật danh sách hình ảnh kết nối trên Main Thread
                        UnityMainThreadDispatcher.Instance.Enqueue(() =>
                        {
                            if (!string.IsNullOrEmpty(jb.OutdoorImage.url))
                            {
                                list_JBLocationImagesFromModule[jb.Name] = downloadedTextures[0];
                                list_JBConnectionImagesFromModule[jb.Name].AddRange(downloadedTextures.Skip(1));
                            }
                            else
                            {
                                list_JBConnectionImagesFromModule[jb.Name].AddRange(downloadedTextures);
                            }
                        });
                        //? Dọn danh sách nhiệm vụ sau mỗi JB
                        downloadTasks.Clear();
                    }
                }

                // Cập nhật biến toàn cục trên Main Thread
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    StaticVariable.temp_listJBConnectionImageFromModule = new Dictionary<string, List<Texture2D>>(list_JBConnectionImagesFromModule);
                    StaticVariable.temp_listJBLocationImageFromModule = new Dictionary<string, Texture2D>(list_JBLocationImagesFromModule);
                    Debug.Log("All images downloaded and updated in global variables.");
                    list_JBConnectionImagesFromModule.Clear();
                    list_JBLocationImagesFromModule.Clear();
                });
                StaticVariable.ready_To_Update_Images_UI = true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error in DownloadImagesAsync: {ex.Message}");
            StaticVariable.ActiveCloseCanvasButton = false;
        }
    }

    private async Task<Texture2D> DownloadImageAsync(string url)
    {

        await _semaphore.WaitAsync();
        try
        {
            using (var request = UnityWebRequestTexture.GetTexture(url))
            {
                if (!await SendWebRequestAsync(request))
                {
                    Debug.LogError($"Request error from URL: {request.url}, Error: {request.error}");
                    return null;
                }

                var texture = DownloadHandlerTexture.GetContent(request);
                // Debug.Log($"Image downloaded from: {url}");
                return texture;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error while downloading image from {url}: {ex}");
            return null;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<JBInformationModel> GetJBInformation(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    // Debug.Log("json: " + json);
                    var jBInformationModel = JsonConvert.DeserializeObject<JBInformationModel>(json);

                    // Debug.Log("jBInformationModel: " + jBInformationModel);

                    if (StaticVariable.ready_To_Reset_ListJB)
                    {
                        StaticVariable.temp_ListJBInformationModel.Clear();
                    }

                    if (jBInformationModel != null)
                    {
                        // Debug.Log("jBInformationModel.Id: " + jBInformationModel.Id);

                        if (!StaticVariable.temp_ListJBInformationModel.Contains(jBInformationModel))
                        {
                            StaticVariable.temp_ListJBInformationModel.Add(jBInformationModel);
                        }
                        else
                        {
                            Debug.LogWarning("Device already exists in the list.");
                        }
                    }
                    return jBInformationModel;
                }
                else
                {
                    HandleRequestError($"Failed to get JB information. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            HandleRequestError($"Unexpected error: {ex.Message}");
            return null;
        }
    }
}

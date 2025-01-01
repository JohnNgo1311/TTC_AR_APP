using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance { get; private set; }

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

    public async Task GetListGrappers(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                HandleRequestError(webRequest.error);
                return;
            }

            try
            {
                var grapper_Models = JsonConvert.DeserializeObject<List<Grapper_General_Model>>(webRequest.downloadHandler.text);
                if (grapper_Models != null && grapper_Models.Count > 0)
                {
                    GlobalVariable.temp_List_Grapper_General_Models = grapper_Models;
                    var tempRackList = new List<Rack_General_Model>();
                    var tempModuleList = new List<Module_General_Non_Rack_Model>();

                    foreach (var grapper in grapper_Models)
                    {
                        tempRackList.AddRange(grapper.List_Rack_General_Model);
                        foreach (var rack in grapper.List_Rack_General_Model)
                        {
                            tempModuleList.AddRange(rack.List_Module_General_Non_Rack_Model);
                        }
                    }

                    GlobalVariable.temp_List_Rack_General_Models = tempRackList;
                    GlobalVariable.temp_List_Module_General_Non_Rack_Models = tempModuleList;
                }
            }
            catch (JsonException jsonEx)
            {
                HandleRequestError(jsonEx.Message);
                return;
            }
            catch (Exception ex)
            {
                HandleRequestError(ex.Message);
                return;
            }
        }
    }
    // Get list Devices by Grapper ==> Search Devices
    public async Task GetAllDevicesByGrapper(string url, string grapperId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                HandleRequestError(webRequest.error);
                return;
            }

            try
            {
                var list_Device_Information_Model = JsonConvert.DeserializeObject<List<Device_Information_Model>>(webRequest.downloadHandler.text);
                if (list_Device_Information_Model != null && list_Device_Information_Model.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_List_Device_Information_Model = list_Device_Information_Model;
                    GlobalVariable_Search_Devices.temp_List_Device_For_Fitler = FilterListDevicesForSearching(list_Device_Information_Model);
                }
            }
            catch (JsonException jsonEx)
            {
                HandleRequestError(jsonEx.Message);
                return;
            }
            catch (Exception ex)
            {
                HandleRequestError(ex.Message);
                return;
            }
        }
    }
    public async Task GetAllJBsByGrapper(string url, string grapperId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                HandleRequestError(webRequest.error);
                return;
            }

            try
            {
                var list_JB_Information_Model = JsonConvert.DeserializeObject<List<JB_Information_Model>>(webRequest.downloadHandler.text);
                if (list_JB_Information_Model != null && list_JB_Information_Model.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_List_JB_Information_Model = list_JB_Information_Model;
                }
            }
            catch (JsonException jsonEx)
            {
                HandleRequestError(jsonEx.Message);
                return;
            }
            catch (Exception ex)
            {
                HandleRequestError(ex.Message);
                return;
            }
        }
    }
    private List<string> FilterListDevicesForSearching(List<Device_Information_Model> Device_Information_Models)
    {
        var list_Devices_For_Filter = new HashSet<string>();
        foreach (var device in Device_Information_Models)
        {
            if (!string.IsNullOrWhiteSpace(device.Code)) list_Devices_For_Filter.Add(device.Code);
            if (!string.IsNullOrWhiteSpace(device.Function)) list_Devices_For_Filter.Add(device.Function);
        }
        return new List<string>(list_Devices_For_Filter);
    }
    private async Task LoadImageFromUrlAsync(string url, Image image)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                Debug.LogError($"Request error: {webRequest.error}");
                return;
            }
            try
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
                image.sprite = sprite;
            }
            catch (JsonException jsonEx)
            {
                Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
                return;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error: {ex}");
                return;
            }
        }
    }

    private async Task<bool> SendWebRequestAsync(UnityWebRequest webRequest)
    {
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield(); // Đợi cho đến khi request hoàn thành
        }
        return webRequest.result != UnityWebRequest.Result.ConnectionError && webRequest.result != UnityWebRequest.Result.ProtocolError;
    }

    private void HandleRequestError(string error)
    {
        Show_Dialog.Instance.ShowToast("failure", $"Request error: {error}");
        Debug.LogError($"Request error: {error}");
    }

    private async Task HandleSuccessfulPost(Device_Information_Model device, string sceneName)
    {
        Show_Dialog.Instance.ShowToast("success", $"Thêm thiết bị mới thành công: {device.Code}");
        // await GetAllDevicesByGrapper($"{GlobalVariable.baseUrl}{device.location[^1..]}", sceneName);
        await Task.Delay(0);

        Sence_Behaviour.Reload_Scene(sceneName);

    }
    // Create New Device
    /* public async Task CreateNewDevice(string url, Device_Information_Model device, string sceneName)
     {
         try
         {
             //PrepareDeviceData(device);
             string jsonData = JsonConvert.SerializeObject(device);

             if (string.IsNullOrEmpty(jsonData))
             {
                 Debug.LogError("Error serializing data.");
                 return;
             }

             using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
             {
                 webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
                 webRequest.downloadHandler = new DownloadHandlerBuffer();
                 webRequest.SetRequestHeader("Content-Type", "application/json");

                 if (!await SendWebRequestAsync(webRequest))
                 {
                     HandleRequestError(webRequest.error);
                     return;
                 }

                 await HandleSuccessfulPost(device, sceneName);
             }
         }
         catch (Exception ex)
         {
             Debug.LogError($"Unexpected error: {ex}");

         }

     }*/


    // public async Task Get_JB_TSD_Information(string url, string grapperName)
    // {
    //     await Task.Delay(1500);
    //     Show_Dialog.Instance.Set_Instance_Status_True();
    //     Show_Dialog.Instance.ShowToast("loading", $"Đang tải dữ liệu...");
    //     string jsonData = await FetchJsonData(url, grapperName);
    //     if (jsonData == null) return;
    //     try
    //     {
    //         var jbDataList = JsonConvert.DeserializeObject<List<JB_TSD_Data>>(jsonData);

    //         GlobalVariable.list_Name_and_Url_JB_Location_A = jbDataList[0].JB_TSD_Location;
    //         GlobalVariable.list_Name_and_Url_JB_Connection_A = jbDataList[0].JB_TSD_Wiring;

    //         await Task.WhenAll(
    //             LoadImagesAsync(GlobalVariable.list_Name_and_Url_JB_Location_A, "get_JB_Location"),
    //             LoadImagesAsync(GlobalVariable.list_Name_and_Url_JB_Connection_A, "get_JB_Connection")
    //         );
    //         Show_Dialog.Instance.ShowToast("success", $"Tải dữ liệu thành công");
    //         Debug.Log(GlobalVariable.list_Name_and_Url_JB_Location_A.Count);
    //         Debug.Log(GlobalVariable.list_Name_and_Url_JB_Connection_A.Count);
    //         await Task.Delay(1500);
    //         Show_Dialog.Instance.StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
    //     }
    //     catch (JsonException jsonEx)
    //     {
    //         Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
    //     }
    // }


    // private async Task LoadImagesAsync(Dictionary<string, List<string>> urlDictionary, string action)
    // {
    //     List<Task> loadTasks = new List<Task>();

    //     foreach (KeyValuePair<string, List<string>> kvp in urlDictionary)
    //     {
    //         var key = kvp.Key;
    //         var List_url = kvp.Value;

    //         if (action == "get_JB_Location" && !GlobalVariable.list_Name_And_Image_JB_Location_A.ContainsKey(key))
    //         {
    //             GlobalVariable.list_Key_JB_Location_A.Add(key);

    //         }
    //         if (action == "get_JB_Connection" && !GlobalVariable.list_Name_And_Image_JB_Connection_A.ContainsKey(key))
    //         {
    //             GlobalVariable.list_Key_JB_Connection_A.Add(key);
    //         }
    //         foreach (string link in List_url)
    //         {
    //             loadTasks.Add(LoadImageFromUrlAsync(link, key, action));
    //         }
    //     }
    //     await Task.WhenAll(loadTasks);
    //     Debug.Log("Lưu các Sprite thành công vào Dictionary");
    // }


    // private void LogJbTsdData(JB_TSD_Data jbData)
    // {
    //     Debug.Log("JB_TSD_Wiring:");
    //     foreach (var (key, urls) in jbData.JB_TSD_Wiring)
    //     {
    //         Debug.Log($"{key}:");
    //         foreach (var url in urls) Debug.Log($" - {url}");
    //     }

    //     Debug.Log("JB_TSD_Location:");
    //     foreach (var (key, urls) in jbData.JB_TSD_Location)
    //     {
    //         Debug.Log($"{key}:");
    //         foreach (var url in urls) Debug.Log($" - {url}");
    //     }
    // }
}

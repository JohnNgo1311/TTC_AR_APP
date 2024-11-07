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

    public async Task CreateNewDevice(string url, DeviceModel device, string sceneName)
    {

        try
        {
            PrepareDeviceData(device);
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

    }

    private void PrepareDeviceData(DeviceModel device)
    {
        if (string.IsNullOrEmpty(device.id))
        {
            device.id = (GlobalVariable_Search_Devices.all_Device_GrapperA.Count + 1).ToString();
        }

        if (device.listImageConnection == null || device.listImageConnection.Count == 0)
        {
            device.listImageConnection = GlobalVariable_Search_Devices.all_Device_GrapperA[1].listImageConnection;
        }
    }

    private async Task<bool> SendWebRequestAsync(UnityWebRequest webRequest)
    {
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
        return webRequest.result != UnityWebRequest.Result.ConnectionError && webRequest.result != UnityWebRequest.Result.ProtocolError;
    }

    private void HandleRequestError(string error)
    {
        Show_Dialog.Instance.ShowToast("failure", $"Request error: {error}");
        Debug.LogError($"Request error: {error}");
    }

    private async Task HandleSuccessfulPost(DeviceModel device, string sceneName)
    {
        Debug.Log("Post data successfully.");
        Show_Dialog.Instance.ShowToast("success", $"Thêm thiết bị mới thành công: {device.code}");

        GlobalVariable_Search_Devices.all_Device_GrapperA.Add(device);

        await Get_Devices_By_Grapper($"{GlobalVariable.baseUrl}{device.location[^1..]}", sceneName);
        Sence_Behaviour.Reload_Scene(sceneName);
    }

    public async Task Get_Devices_By_Grapper(string url, string grapperName)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                Debug.LogError($"Request error: {webRequest.error}");
                return;
            }

            try
            {
                var devices = JsonConvert.DeserializeObject<List<DeviceModel>>(webRequest.downloadHandler.text);
                if (devices != null && devices.Count > 0)
                {
                    GlobalVariable_Search_Devices.all_Device_GrapperA = devices;
                    GlobalVariable_Search_Devices.devices_Model_By_Grapper = devices;
                    ProcessAndSaveDevices(devices, grapperName);
                    GlobalVariable.ready_To_Nav_New_Scene = true;
                }
            }
            catch (JsonException jsonEx)
            {
                HandleRequestError(jsonEx.Message);
            }
            catch (Exception ex)
            {
                HandleRequestError(ex.Message);
            }
        }
    }

    private void ProcessAndSaveDevices(List<DeviceModel> devices, string grapperName)
    {
        List<string> filteredDevices = GetDeviceForFilter(devices);

        switch (grapperName)
        {
            case "A":
                GlobalVariable_Search_Devices.devices_Model_For_FilterA = filteredDevices;
                break;
            case "B":
                GlobalVariable_Search_Devices.devices_Model_For_FilterB = filteredDevices;
                break;
            case "C":
                GlobalVariable_Search_Devices.devices_Model_For_FilterC = filteredDevices;
                break;
            case "D":
                GlobalVariable_Search_Devices.devices_Model_For_FilterD = filteredDevices;
                break;
        }

        Debug.Log(filteredDevices.Count > 0 ? $"Data saved: {filteredDevices.Count} entries." : "No data saved.");
    }

    private List<string> GetDeviceForFilter(List<DeviceModel> deviceModels)
    {
        var devicesForFilter = new HashSet<string>();

        foreach (var device in deviceModels)
        {
            if (!string.IsNullOrWhiteSpace(device.code)) devicesForFilter.Add(device.code);
            if (!string.IsNullOrWhiteSpace(device.function)) devicesForFilter.Add(device.function);

        }

        return new List<string>(devicesForFilter);
    }

    public async Task Get_JB_TSD_Information(string url, string grapperName)
    {
        await Task.Delay(1500);
        Show_Dialog.Instance.StartCoroutine(Show_Dialog.Instance.Set_Instance_Status(true));
        Show_Dialog.Instance.ShowToast("loading", $"Đang tải dữ liệu...");
        string jsonData = await FetchJsonData(url, grapperName);
        if (jsonData == null) return;
        try
        {
            var jbDataList = JsonConvert.DeserializeObject<List<JB_TSD_Data>>(jsonData);

            GlobalVariable.list_Name_and_Url_JB_Location_A = jbDataList[0].JB_TSD_Location;
            GlobalVariable.list_Name_and_Url_JB_Connection_A = jbDataList[0].JB_TSD_Wiring;

            await Task.WhenAll(
                LoadImagesAsync(GlobalVariable.list_Name_and_Url_JB_Location_A, "get_JB_Location"),
                LoadImagesAsync(GlobalVariable.list_Name_and_Url_JB_Connection_A, "get_JB_Connection")
            );
            Show_Dialog.Instance.ShowToast("success", $"Tải dữ liệu thành công");
            Debug.Log(GlobalVariable.list_Name_and_Url_JB_Location_A.Count);
            Debug.Log(GlobalVariable.list_Name_and_Url_JB_Connection_A.Count);
            await Task.Delay(1500);
            Show_Dialog.Instance.StartCoroutine(Show_Dialog.Instance.Set_Instance_Status(false));
        }
        catch (JsonException jsonEx)
        {
            Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
        }
    }

    private async Task LoadImagesAsync(Dictionary<string, List<string>> urlDictionary, string action)
    {
        List<Task> loadTasks = new List<Task>();

        foreach (KeyValuePair<string, List<string>> kvp in urlDictionary)
        {
            var key = kvp.Key;
            var List_url = kvp.Value;

            if (action == "get_JB_Location" && !GlobalVariable.list_Name_And_Image_JB_Location_A.ContainsKey(key))
            {
                GlobalVariable.list_Key_JB_Location_A.Add(key);

            }
            if (action == "get_JB_Connection" && !GlobalVariable.list_Name_And_Image_JB_Connection_A.ContainsKey(key))
            {
                GlobalVariable.list_Key_JB_Connection_A.Add(key);
            }
            foreach (string link in List_url)
            {
                loadTasks.Add(LoadImageFromUrlAsync(link, key, action));
            }
        }
        await Task.WhenAll(loadTasks);
        Debug.Log("Lưu các Sprite thành công vào Dictionary");
    }

    private async Task LoadImageFromUrlAsync(string url, string key, string action)
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
                switch (action)
                {
                    case "get_JB_Location":
                        if (GlobalVariable.list_Name_And_Image_JB_Location_A.ContainsKey(key))
                        {
                            if (!GlobalVariable.list_Name_And_Image_JB_Location_A[key].Contains(sprite))
                            {
                                GlobalVariable.list_Name_And_Image_JB_Location_A[key].Add(sprite);
                            }
                        }
                        else
                        {
                            GlobalVariable.list_Name_And_Image_JB_Location_A.Add(key, new List<Sprite> { sprite });
                        }
                        Debug.Log(GlobalVariable.list_Name_And_Image_JB_Location_A.Count);
                        break;
                    case "get_JB_Connection":
                        if (GlobalVariable.list_Name_And_Image_JB_Connection_A.ContainsKey(key))
                        {
                            if (!GlobalVariable.list_Name_And_Image_JB_Connection_A[key].Contains(sprite))
                            {
                                GlobalVariable.list_Name_And_Image_JB_Connection_A[key].Add(sprite);
                            }
                        }
                        else
                        {
                            GlobalVariable.list_Name_And_Image_JB_Connection_A.Add(key, new List<Sprite> { sprite });
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error: {ex}");
            }
        }
    }

    private async Task<string> FetchJsonData(string url, string grapperName)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        if (!await SendWebRequestAsync(webRequest))
        {
            Debug.LogError($"Request error: {webRequest.error}");
            return null;
        }

        return webRequest.downloadHandler.text;
    }

    private void LogJbTsdData(JB_TSD_Data jbData)
    {
        Debug.Log("JB_TSD_Wiring:");
        foreach (var (key, urls) in jbData.JB_TSD_Wiring)
        {
            Debug.Log($"{key}:");
            foreach (var url in urls) Debug.Log($" - {url}");
        }

        Debug.Log("JB_TSD_Location:");
        foreach (var (key, urls) in jbData.JB_TSD_Location)
        {
            Debug.Log($"{key}:");
            foreach (var url in urls) Debug.Log($" - {url}");
        }
    }
}

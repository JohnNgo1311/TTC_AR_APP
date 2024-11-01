using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
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

            Debug.Log(jsonData);

            using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
            {
                byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");

                await SendWebRequestAsync(webRequest);

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    HandleRequestError(webRequest);
                }
                else
                {
                    await HandleSuccessfulPost(device, sceneName);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error: {ex.Message} + {ex.Data} + {ex.StackTrace}");
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
            device.listImageConnection = GlobalVariable_Search_Devices.all_Device_GrapperA[0].listImageConnection;
        }
    }

    private async Task SendWebRequestAsync(UnityWebRequest webRequest)
    {
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
    }

    private void HandleRequestError(UnityWebRequest webRequest)
    {
        Show_Dialog.Instance.ShowToast("failure", $"Request error: {webRequest.error}");
        Debug.LogError($"Request error: {webRequest.error}");
    }

    private async Task HandleSuccessfulPost(DeviceModel device, string sceneName)
    {
        Debug.Log("Post data successfully.");
        Show_Dialog.Instance.ShowToast("success", "Thêm thiết bị mới thành công: " + device.code);
        GlobalVariable_Search_Devices.all_Device_GrapperA.Add(device);
        await Get_Devices_By_Grapper($"{GlobalVariable.baseUrl}{device.location.Substring(device.location.Length - 1)}", sceneName);
        Sence_Behaviour.Reload_Scene(sceneName);
    }

    public async Task Get_Devices_By_Grapper(string url, string grapperName)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await SendWebRequestAsync(webRequest);

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Request error: {webRequest.error}");
                return;
            }

            try
            {
                string jsonData = webRequest.downloadHandler.text;
                var devices = JsonConvert.DeserializeObject<List<DeviceModel>>(jsonData);
                Debug.Log("Received data successfully.");

                if (devices.Count > 0)
                {
                    GlobalVariable_Search_Devices.all_Device_GrapperA = devices;
                    GlobalVariable_Search_Devices.devices_Model_By_Grapper = devices;
                    ProcessAndSaveDevices(devices, grapperName);
                    GlobalVariable.ready_To_Nav_New_Scene = true;
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error: {ex.Message}");
            }
        }
    }

    private void ProcessAndSaveDevices(List<DeviceModel> devices, string grapperName)
    {
        List<string> filteredDevices = GetDeviceForFilter(devices);
        Save_Data_To_Local.SaveStringList($"List_Device_For_Filter_{grapperName}", filteredDevices);

        List<string> savedList = Save_Data_To_Local.GetStringList($"List_Device_For_Filter_{grapperName}");
        GlobalVariable_Search_Devices.devices_Model_For_Filter = savedList;

        if (savedList != null && savedList.Count > 0)
        {
            Debug.Log($"Lượng data đã lưu: {savedList.Count} + {savedList[0]}");
        }
        else
        {
            Debug.LogError("Danh sách đã lưu có ít hơn 6 phần tử hoặc null");
        }
    }

    private List<string> GetDeviceForFilter(List<DeviceModel> deviceModels)
    {
        List<string> devicesForFilter = new List<string>();

        foreach (var device in deviceModels)
        {
            if (!string.IsNullOrWhiteSpace(device.code))
            {
                devicesForFilter.Add(device.code);
            }
            if (!string.IsNullOrWhiteSpace(device.function))
            {
                devicesForFilter.Add(device.function);
            }
        }

        return devicesForFilter;
    }
}

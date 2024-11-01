using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

//! Script này lấy data danh sách các devices
public class Get_Devices_By_Grapper : MonoBehaviour
{
    public string grapper;
    private string filePath;
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    public void Get_List_Device_By_Grapper()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, $"Device_Grapper{grapper}.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadJsonFromAndroid(filePath));
        }
        else
        {
            LoadJsonFromFile(filePath);
        }
    }

    private void LoadJsonFromFile(string file)
    {
        if (File.Exists(file))
        {
            try
            {
                string jsonData = File.ReadAllText(file);
                ProcessJsonData(jsonData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read JSON file: {e.Message}");
            }
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }

    private IEnumerator LoadJsonFromAndroid(string file)
    {
        string androidPath = File.Exists(file) && !string.IsNullOrWhiteSpace(File.ReadAllText(file)) ? file : $"jar:file://{Application.dataPath}!/assets/Device_Grapper{grapper}.json";
        using (UnityWebRequest www = UnityWebRequest.Get(androidPath))
        {
            www.timeout = 30;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load JSON file on Android: {www.error}");
            }
            else
            {
                try
                {
                    string jsonData = www.downloadHandler.text;
                    Debug.Log($"Loaded JSON data: {jsonData.Length} characters");
                    ProcessJsonData(jsonData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error processing JSON: {e.Message}");
                }
            }
        }
    }

    private void ProcessJsonData(string jsonData)
    {
        try
        {
            List<DeviceModel> devices = JsonConvert.DeserializeObject<List<DeviceModel>>(jsonData);


            if (devices != null && devices.Count > 0 && !string.IsNullOrWhiteSpace(devices[1].function))
            {
                GlobalVariable_Search_Devices.devices_Model_By_Grapper = devices;
                ProcessAndSaveDevices(devices);
            }
            else
            {
                Debug.LogError("List thiết bị null hoặc không có đủ dữ liệu hợp lệ.");
            }
        }
        catch (JsonException je)
        {
            Debug.LogError($"Failed to deserialize JSON data: {je.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Unexpected error during JSON processing: {e.Message}");
        }
    }

    private void ProcessAndSaveDevices(List<DeviceModel> devices)
    {
        List<string> filteredDevices = GetDeviceForFilter(devices);
        Save_Data_To_Local.SaveStringList($"List_Device_For_Filter_{grapper}", filteredDevices);

        List<string> savedList = Save_Data_To_Local.GetStringList($"List_Device_For_Filter_{grapper}");
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

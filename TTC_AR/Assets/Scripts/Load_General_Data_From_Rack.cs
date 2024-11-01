using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;
using UnityWebRequest = UnityEngine.Networking.UnityWebRequest;

public class Load_General_Data_From_Rack : MonoBehaviour
{
    public string grapper;
    [SerializeField]
    private string filePath;
    //! Scripts này sử dụng để tạo data cho 1 Rack, tương ứng hỗ trợ instantiate List JB Button, xác định Type Module, xác định type Adapter
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    private void Awake()
    {

        filePath = Path.Combine(Application.streamingAssetsPath, $"General_Data_Rack_Grapper{grapper}.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadJsonFromAndroid(filePath));
        }
        else
        {
            LoadJsonFromFile(filePath);
        }
    }

    private IEnumerator LoadJsonFromAndroid(string file)
    {
        string androidPath = file;
        if (!File.Exists(file) || string.IsNullOrWhiteSpace(File.ReadAllText(file)))
        {
            androidPath = $"jar:file://{Application.dataPath}!/assets/General_Data_Rack_Grapper{grapper}.json";
        }
        UnityWebRequest www = UnityWebRequest.Get(androidPath);
        www.timeout = 30;
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load JSON file on Android: {www.error}");
        }
        else
        {
            string jsonData = www.downloadHandler.text;
            try
            {
                if (jsonData != null) Debug.Log($"Loaded JSON data: {jsonData}");
                // Deserialize JSON data
                switch (grapper)
                {
                    case "A":
                        RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                        GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                        break;
                }
                /* RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                 GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                 Debug.Log($"Loaded JSON data 1 : {rackData_Grapper_General.Rack4[0].Module + rackData_Grapper_General.Rack4[0].JbConnection[0] + rackData_Grapper_General.Rack4[0].DeviceConnection[0]}");*/
            }
            catch (JsonException je)
            {
                Debug.LogError($"Failed to deserialize JSON data: {je.Message}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Unexpected error during JSON processing: {e.Message}");
            }
            Debug.Log($"Loaded JSON data lần 2: {jsonData}");
        }

    }
    private void LoadJsonFromFile(string file)
    {
        try
        {
            if (File.Exists(file))
            {
                string jsonData = File.ReadAllText(file);
                ProcessJsonData(jsonData);
            }
            else
            {
                Debug.LogError($"File not found: {filePath}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read JSON file: {e.Message}");
        }
    }
    private void ProcessJsonData(string jsonData)
    {
        Debug.Log($"Loaded JSON data:{jsonData.Length} + {jsonData}"); //! 8255 ký tự

        try
        {
            switch (grapper)
            {
                case "A":
                    RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                    GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                    Debug.Log($"Loaded JSON data 1 : {GlobalVariable.rackData_GrapperA.Rack_4[0].Module + GlobalVariable.rackData_GrapperA.Rack_4[0].JbConnection[0] + GlobalVariable.rackData_GrapperA.Rack_4[0].Type}");
                    break;
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
        // Filter devices
        List<string> filteredDevices = GetDeviceForFilter(devices);

        // Save filtered devices locally
        Save_Data_To_Local.SaveStringList($"List_Device_For_Filter_{grapper}", filteredDevices);

        // Retrieve and validate the saved list
        List<string> savedList = Save_Data_To_Local.GetStringList($"List_Device_For_Filter_{grapper}");
        GlobalVariable_Search_Devices.devices_Model_For_Filter = savedList;
        if (savedList != null && savedList.Count > 0)
        {
            Debug.Log($"Lượng data đã lưu: {savedList.Count} + {savedList[1]}");
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

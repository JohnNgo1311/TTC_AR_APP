using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using System;
public class Get_All_Device_By_Grapper : MonoBehaviour
{
    [SerializeField]
    private string grapper_Name = "A";
    [SerializeField]
    private List<DeviceModel> devices = new List<DeviceModel>();

    void Start()
    {
    }
    public async void On_Click_Get_List_Device_By_Grapper()
    {

        GlobalVariable.ready_To_Nav_New_Scene = false;
        if ((GlobalVariable_Search_Devices.all_Device_GrapperA == null || GlobalVariable_Search_Devices.all_Device_GrapperA.Count <= 0)
            && (GlobalVariable_Search_Devices.devices_Model_By_Grapper == null || GlobalVariable_Search_Devices.devices_Model_By_Grapper.Count <= 0)
            && (GlobalVariable_Search_Devices.devices_Model_For_Filter == null || GlobalVariable_Search_Devices.devices_Model_For_Filter.Count <= 0))
        {
            await APIManager.Instance.Get_Devices_By_Grapper($"{GlobalVariable.baseUrl}{grapper_Name}", grapper_Name);
            // StartCoroutine(Get_Devices_By_Grapper($"{GlobalVariable.baseUrl}{grapper_Name}"));
        }
        else
        {
            GlobalVariable.ready_To_Nav_New_Scene = true;
        }
    }
    //GET Request
    /*private IEnumerator Get_Devices_By_Grapper(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var operation = webRequest.SendWebRequest();
            yield return operation;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Request error: {webRequest.error}");
            }
            try
            {
                string jsonData = webRequest.downloadHandler.text;
                devices = JsonConvert.DeserializeObject<List<DeviceModel>>(jsonData);
                Debug.Log("Received data successfully.");
                if (devices.Count > 0)
                {
                    GlobalVariable_Search_Devices.all_Device_GrapperA = devices;
                    GlobalVariable_Search_Devices.devices_Model_By_Grapper = devices;
                    ProcessAndSaveDevices(devices);
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



    private void ProcessAndSaveDevices(List<DeviceModel> devices)
    {
        List<string> filteredDevices = GetDeviceForFilter(devices);
        Save_Data_To_Local.SaveStringList($"List_Device_For_Filter_{grapper_Name}", filteredDevices);

        List<string> savedList = Save_Data_To_Local.GetStringList($"List_Device_For_Filter_{grapper_Name}");
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




    //PUT Request
    IEnumerator putRequest(string url)
    {
        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes("Hello, This is a test");
        UnityWebRequest webRequest = UnityWebRequest.Put(url, dataToPut);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)

        {
            Debug.Log("Error While Sending: " + webRequest.error);
        }
        else
        {
            Debug.Log("Received: " + webRequest.downloadHandler.text);
        }
    }
    IEnumerator deleteRequest(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Delete(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + webRequest.error);
        }
        else
        {
            Debug.Log("Deleted");
        }
    }*/
}

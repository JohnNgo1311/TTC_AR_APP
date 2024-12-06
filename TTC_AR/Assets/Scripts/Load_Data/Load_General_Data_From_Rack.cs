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
    private bool isRunning = false;
    void Start()
    {
     
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
        isRunning = true;
        string androidPath = file;
        if (!File.Exists(file) || string.IsNullOrWhiteSpace(File.ReadAllText(file)))
        {
            Debug.Log("File không tồn tại hoặc rỗng");
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
            try
            {
                string jsonData = www.downloadHandler.text;
                if (jsonData != null) //Debug$"Loaded JSON data: {jsonData}");
                                      // Deserialize JSON data
                    switch (grapper)
                    {
                        case "A":
                            RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                            GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                            Debug.Log("Load data cho Rack A thành công");
                            break;
                            //thêm case B, C, D nếu có thêm Grapper
                    }
                /* RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                 GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                 //Debug$"Loaded JSON data 1 : {rackData_Grapper_General.Rack4[0].Module + rackData_Grapper_General.Rack4[0].JbConnection[0] + rackData_Grapper_General.Rack4[0].DeviceConnection[0]}");*/
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
        isRunning = false;
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
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read JSON file: {e.Message}");
        }
    }
    private void ProcessJsonData(string jsonData)
    {
        //Debug$"Loaded JSON data:{jsonData.Length} + {jsonData}"); //! 8255 ký tự
        try
        {
            switch (grapper)
            {
                case "A":
                    RackData_GrapperA rackData_Grapper_General = JsonConvert.DeserializeObject<RackData_GrapperA>(jsonData);
                    GlobalVariable.rackData_GrapperA = rackData_Grapper_General; //! Lưu danh sách các thiết bị trong 1 Grapper
                    Debug.Log("Load data cho Rack A thành công");
                    //Debug$"Loaded JSON data 1 : {GlobalVariable.rackData_GrapperA.Rack_4[0].Module + GlobalVariable.rackData_GrapperA.Rack_4[0].JbConnection[0] + GlobalVariable.rackData_GrapperA.Rack_4[0].Type}");
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

}
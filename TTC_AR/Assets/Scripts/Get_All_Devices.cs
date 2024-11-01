using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Get_All_Devices : MonoBehaviour
{
    private string filePath;
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    private void Awake()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "All_Devices.json");
        StartCoroutine(LoadJsonData());
    }

    private IEnumerator LoadJsonData()
    {
        string jsonData = null;

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            using (UnityWebRequest www = UnityWebRequest.Get(filePath))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    jsonData = www.downloadHandler.text;
                }
                else
                {
                    Debug.LogError($"Failed to load JSON data from {filePath}: {www.error}");
                    yield break;
                }
            }
        }
        else
        {
            if (File.Exists(filePath))
            {
                jsonData = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError($"File not found: {filePath}");
                yield break;
            }
        }

        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.LogError($"Loaded JSON data is null or empty: {filePath}");
            yield break;
        }

        try
        {
            GlobalVariable_Search_Devices.all_Device_Models = JsonConvert.DeserializeObject<List<DeviceModel>>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to deserialize JSON data: {e.Message}");
        }
    }
}

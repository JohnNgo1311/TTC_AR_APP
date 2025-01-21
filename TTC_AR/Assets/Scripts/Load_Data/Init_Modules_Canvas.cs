using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

public class Init_Modules_Canvas : MonoBehaviour
{
    [Header("Configuration")]
    public List<GameObject> imageTargets = new List<GameObject>();
    public List<GameObject> targetCanvas = new List<GameObject>();
    public GameObject moduleCanvasPrefab;
    public Transform parentTransformForInstantiate;
    public Get_List_Modules_By_Grapper getListModulesByGrapper;
    public bool isInstantiating = false;
    public Transform parentTransform;

    private const float TimeoutDuration = 30f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        StartCoroutine(InstantiateModuleCanvases());
    }

    private IEnumerator LoadGeneralDataWithTimeout()
    {
        float timer = 0f;
        getListModulesByGrapper.GetListModuleByGrapper();

        Debug.Log("Loading general data from rack...");

        while (GlobalVariable.temp_ListModuleInformationModel.Count == 0 && timer < TimeoutDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (GlobalVariable.temp_ListModuleInformationModel.Count > 0)
        {
            Debug.Log("Data successfully loaded.");
        }
        else
        {
            Debug.LogWarning("Timeout: Failed to load data within 30 seconds.");
        }
    }

    private IEnumerator InstantiateModuleCanvases()
    {
        yield return LoadGeneralDataWithTimeout();

        if (GlobalVariable.temp_ListModuleInformationModel.Count == 0)
        {
            Debug.LogError("No data available to instantiate module canvases.");
            yield break;
        }
        isInstantiating = false;
        foreach (var module in GlobalVariable.temp_ListModuleInformationModel)
        {
            GameObject newCanvas = Instantiate(moduleCanvasPrefab, parentTransform);
            string moduleName = module.Name;
            newCanvas.name = $"{moduleName}_Canvas";
            Transform closeButton = newCanvas.transform.Find("General_Panel/Close_Canvas_Btn");
            if (closeButton != null)
            {
                closeButton.gameObject.tag = $"{moduleName}_Btn";
            }
            targetCanvas.Add(newCanvas);
        }
        Destroy(moduleCanvasPrefab.gameObject);
        isInstantiating = true;
        Debug.Log("Module canvases instantiated successfully.");
    }

    private async Task<string> LoadJsonFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"File not found: {filePath}");
            return null;
        }

        try
        {
            return await Task.Run(() => File.ReadAllText(filePath));
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read file: {filePath}, Error: {e.Message}");
            return null;
        }
    }

    // private async Task<string> LoadJsonFromAndroidAsync(string filePath)
    // {
    //     UnityWebRequest request = UnityWebRequest.Get(filePath);
    //     request.timeout = 30;

    //     await request.SendWebRequest();

    //     if (request.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.LogError($"Failed to load JSON from Android: {request.error}");
    //         return null;
    //     }

    //     return request.downloadHandler.text;
    // }

    // private void ProcessJsonData(string jsonData)
    // {
    //     try
    //     {
    //         // Deserialize JSON and update GlobalVariable
    //         Debug.Log("Processing JSON data...");
    //     }
    //     catch (JsonException ex)
    //     {
    //         Debug.LogError($"JSON processing error: {ex.Message}");
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError($"Unexpected error: {ex.Message}");
    //     }
    // }
}

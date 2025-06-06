// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;
// using UnityEngine;
// using Newtonsoft.Json;
// using UnityEngine.Networking;
// using System.Collections;

// public class Init_Modules_Canvas : MonoBehaviour
// {
//     [Header("Configuration")]
//     public List<GameObject> targetCanvas = new List<GameObject>();
//     public GameObject moduleCanvasPrefab;
//     public GetListModuleFromGrapper getListModulesFromGrapper;
//     public bool isInstantiating = false;
//     public Transform parentTransform;

//     private const float TimeoutDuration = 30f;

//     private void Awake()
//     {
//         Initialize();
//     }

//     private void Initialize()
//     {
//         StartCoroutine(InstantiateModuleCanvases());
//     }

//     private IEnumerator LoadBasicDataWithTimeout()
//     {
//         float timer = 0f;
//         getListModulesFromGrapper.GetListModuleByGrapper();

//         // Debug.Log("Loading general data from rack...");

//         while (StaticVariable.temp_ListModuleInformationModel.Count == 0 && timer < TimeoutDuration)
//         {
//             timer += Time.deltaTime;
//             yield return null;
//         }

//         if (StaticVariable.temp_ListModuleInformationModel.Count > 0)
//         {
//             Debug.Log("Data successfully loaded.");
//         }
//         else
//         {
//             Debug.LogWarning("Timeout: Failed to load data within 30 seconds.");
//         }
//     }

//     private IEnumerator InstantiateModuleCanvases()
//     {
//         yield return LoadBasicDataWithTimeout();

//         if (StaticVariable.temp_ListModuleInformationModel.Count == 0)
//         {
//             Debug.LogError("No data available to instantiate module canvases.");
//             yield break;
//         }
//         isInstantiating = true;
//         foreach (var module in StaticVariable.temp_ListModuleInformationModel)
//         {
//             GameObject newCanvas = Instantiate(moduleCanvasPrefab, parentTransform);
//             string moduleName = module.Name;
//             newCanvas.name = $"{moduleName}_Canvas";
//             Transform closeButton = newCanvas.transform.Find("Basic_Panel/Close_Canvas_Btn");
//             if (closeButton != null)
//             {
//                 closeButton.gameObject.tag = $"{moduleName}_Btn";
//             }
//             targetCanvas.Add(newCanvas);
//         }
//         Destroy(moduleCanvasPrefab.gameObject);
//         isInstantiating = false;
//         Debug.Log("Module canvases instantiated successfully.");
//     }

//     private async Task<string> LoadJsonFromFileAsync(string filePath)
//     {
//         if (!File.Exists(filePath))
//         {
//             Debug.LogWarning($"File not found: {filePath}");
//             return null;
//         }

//         try
//         {
//             return await Task.Run(() => File.ReadAllText(filePath));
//         }
//         catch (Exception e)
//         {
//             Debug.LogError($"Failed to read file: {filePath}, Error: {e.Message}");
//             return null;
//         }
//     }
// }

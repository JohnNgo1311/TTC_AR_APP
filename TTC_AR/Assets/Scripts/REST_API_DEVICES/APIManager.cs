using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;
using System.Reflection;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance { get; private set; }

    public int GrapperId = 1;
    public int RackId = 1;
    public int ModuleId = 1;
    public int DeviceId = 1;
    public int JBId = 1;
    public int FieldDeviceId = 1;
    public int MCCId = 1;
    // Danh sách URL cần tải
    public List<Rack_General_Model> temp_List_Rack_General_Models;
    public List<Grapper_General_Model> temp_List_Grapper_General_Models;
    public List<ModuleInformationModel> temp_List_ModuleInformationModel = new List<ModuleInformationModel>();
    public ModuleInformationModel temp_ModuleInformationModel;
    public ModuleSpecificationGeneralModel temp_ModuleSpecificationGeneralModel;
    public ModuleSpecificationModel temp_ModuleSpecificationModel;
    public AdapterSpecificationModel temp_AdapterSpecificationModel;

    public List<MccModel> temp_ListMCCInformationModel = new List<MccModel>();
    public MccModel temp_MccInformationModel;
    public List<FieldDeviceInformationModel> temp_ListFieldDeviceInformationModel = new List<FieldDeviceInformationModel>();
    public FieldDeviceInformationModel temp_FieldDeviceInformationModel;
    public List<JBInformationModel> temp_ListJBInformationModel_From_Module = new List<JBInformationModel>();
    public List<DeviceInformationModel_FromModule> temp_ListDeviceInformationModel_FromModule = new List<DeviceInformationModel_FromModule>();
    public Dictionary<string, List<Texture2D>> list_JB_Connection_Images_From_Module = new Dictionary<string, List<Texture2D>>();
    public Dictionary<string, Texture2D> list_JB_Location_Images_From_Module = new Dictionary<string, Texture2D>();
    public List<string> imageUrls = new List<string>();
    public List<Texture2D> textures = new List<Texture2D>();


    public Dictionary<string, int> Dic_Grapper_General_Non_List_Rack_Models = new Dictionary<string, int>();
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
                Debug.Log("GetListGrappers:+ " + webRequest.downloadHandler.text);
                var grapper_Models = JsonConvert.DeserializeObject<List<Grapper_General_Model>>(webRequest.downloadHandler.text);
                if (grapper_Models != null && grapper_Models.Count > 0)
                {
                    temp_List_Grapper_General_Models = grapper_Models;
                    GlobalVariable.temp_List_Grapper_General_Models = temp_List_Grapper_General_Models;
                    Debug.Log(GlobalVariable.temp_List_Grapper_General_Models.Count);
                    Dic_Grapper_General_Non_List_Rack_Models.Clear();
                    foreach (var grapper in temp_List_Grapper_General_Models)
                    {
                        if (!Dic_Grapper_General_Non_List_Rack_Models.ContainsKey(grapper.Name))
                            Dic_Grapper_General_Non_List_Rack_Models.Add(grapper.Name, grapper.Id);
                    }

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
    public async Task GetListRacks(string url)
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
                Debug.Log("GetListRacks:+ " + webRequest.downloadHandler.text);
                var rack_models = JsonConvert.DeserializeObject<List<Rack_General_Model>>(webRequest.downloadHandler.text);
                if (rack_models != null && rack_models.Count > 0)
                {
                    temp_List_Rack_General_Models = rack_models;
                    GlobalVariable.temp_List_Rack_General_Models = rack_models;
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
    public async Task GetListMCCModels(string url)
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
                var List_MCC_Models = JsonConvert.DeserializeObject<List<MccModel>>(webRequest.downloadHandler.text);
                if (List_MCC_Models != null)
                {
                    GlobalVariable.temp_ListMCCInformationModel = new List<MccModel>();
                    temp_ListMCCInformationModel = List_MCC_Models;
                    GlobalVariable.temp_ListMCCInformationModel = temp_ListMCCInformationModel;
                    Debug.Log("List_MCC_Models.Count: " + List_MCC_Models.Count);
                }
                Debug.Log("success");
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
    public async Task GetMccInformation(string url)
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
                var mccModel = JsonConvert.DeserializeObject<MccModel>(webRequest.downloadHandler.text);

                if (mccModel != null)
                {
                    MCCId = mccModel.Id;
                    GlobalVariable.temp_MCCInformationModel = mccModel;
                    GlobalVariable.temp_FieldDeviceInformationModel = mccModel.FieldDeviceInformationModel[0];
                    GlobalVariable.FieldDeviceId = GlobalVariable.temp_FieldDeviceInformationModel.Id;
                    GlobalVariable.MCCId = MCCId;
                }
                Debug.Log("success");
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


    public async Task GetListModuleInformation(string url, int grapperId)
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
                var list_moduleInformationModel = JsonConvert.DeserializeObject<List<ModuleInformationModel>>(webRequest.downloadHandler.text);
                if (list_moduleInformationModel != null)
                {
                    temp_List_ModuleInformationModel = list_moduleInformationModel;

                }
                Debug.Log("success");
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
    public async Task GetModuleInformation(string url, int moduleId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            GlobalVariable.ActiveCloseCanvasButton = false;
            if (!await SendWebRequestAsync(webRequest))
            {
                HandleRequestError(webRequest.error);
                return;
            }

            try
            {
                var moduleInformationModel = JsonConvert.DeserializeObject<ModuleInformationModel>(webRequest.downloadHandler.text);
                if (moduleInformationModel != null)
                {
                    GlobalVariable.ModuleId = 1;
                    GlobalVariable.ModuleSpecificationId = 1;
                    GlobalVariable.temp_ListJBInformationModel_FromModule.Clear();
                    GlobalVariable.temp_ListDeviceInformationModel_FromModule.Clear();
                    GlobalVariable.temp_ModuleInformationModel = null;
                    GlobalVariable.temp_ModuleSpecificationGeneralModel = null;

                    temp_ModuleInformationModel = moduleInformationModel;
                    temp_ListJBInformationModel_From_Module = temp_ModuleInformationModel.ListJBInformationModel;
                    temp_ListDeviceInformationModel_FromModule = temp_ModuleInformationModel.ListDeviceInformationModel_FromModule;
                    ModuleId = temp_ModuleInformationModel.Id;

                    GlobalVariable.ModuleId = ModuleId;
                    GlobalVariable.ModuleSpecificationId = moduleInformationModel.ModuleSpecificationGeneralModel.Id;
                    GlobalVariable.temp_ListJBInformationModel_FromModule = temp_ListJBInformationModel_From_Module;
                    GlobalVariable.temp_ListDeviceInformationModel_FromModule = temp_ListDeviceInformationModel_FromModule;
                    GlobalVariable.temp_ModuleInformationModel = temp_ModuleInformationModel;
                    GlobalVariable.temp_ModuleSpecificationGeneralModel = moduleInformationModel.ModuleSpecificationGeneralModel;
                }

                Debug.Log("success");
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
    public async Task GetModuleSpecification(string url)
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
                var moduleSpecificationModel = JsonConvert.DeserializeObject<ModuleSpecificationModel>(webRequest.downloadHandler.text);
                if (moduleSpecificationModel != null)
                {
                    temp_ModuleSpecificationModel = moduleSpecificationModel;
                    temp_AdapterSpecificationModel = moduleSpecificationModel.Adapter;
                    GlobalVariable.temp_ModuleSpecificationModel = moduleSpecificationModel;
                    GlobalVariable.temp_AdapterSpecificationModel = moduleSpecificationModel.Adapter;
                }
                Debug.Log("success");
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

    private SemaphoreSlim _semaphore = new SemaphoreSlim(10);

    public async Task DownloadImagesAsync()
    {
        GlobalVariable.ActiveCloseCanvasButton = false;
        try
        {

            if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
            {
                if (temp_ListJBInformationModel_From_Module == null || temp_ListJBInformationModel_From_Module.Count == 0)
                {
                    Debug.LogWarning("No JB information models available to download images.");
                    return;
                }

                Debug.Log("temp_ListJBInformationModel_From_Module.Count: " + temp_ListJBInformationModel_From_Module.Count);

                var downloadTasks = new List<Task<Texture2D>>();

                foreach (var jb in temp_ListJBInformationModel_From_Module)
                {
                    // Khởi tạo các danh sách nếu chưa tồn tại
                    if (!list_JB_Connection_Images_From_Module.ContainsKey(jb.Name))
                    {
                        list_JB_Connection_Images_From_Module[jb.Name] = new List<Texture2D>();
                    }

                    if (!list_JB_Location_Images_From_Module.ContainsKey(jb.Name))
                    {
                        list_JB_Location_Images_From_Module[jb.Name] = new Texture2D(2, 2);
                    }

                    if (!string.IsNullOrEmpty(jb.OutdoorImage))
                    {
                        // Tải hình ảnh ngoài trời
                        var outdoorImageTask = DownloadImageAsync(jb.OutdoorImage);
                        list_JB_Location_Images_From_Module[jb.Name] = await outdoorImageTask;
                    }


                    // Tải danh sách hình ảnh kết nối
                    foreach (var url in jb.ListConnectionImages)
                    {
                        if (!string.IsNullOrEmpty(url))
                        {
                            downloadTasks.Add(DownloadImageAsync(url));
                        }
                    }

                    // Chờ tất cả hình ảnh kết nối hoàn tất
                    var downloadedTextures = await Task.WhenAll(downloadTasks);

                    // Cập nhật danh sách hình ảnh kết nối trên Main Thread
                    UnityMainThreadDispatcher.Instance.Enqueue(() =>
                    {
                        list_JB_Connection_Images_From_Module[jb.Name].AddRange(downloadedTextures);
                    });

                    // Dọn danh sách nhiệm vụ sau mỗi JB
                    downloadTasks.Clear();

                }

                // Cập nhật biến toàn cục trên Main Thread
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    GlobalVariable.temp_listJBConnectionImageFromModule = new Dictionary<string, List<Texture2D>>(list_JB_Connection_Images_From_Module);
                    GlobalVariable.temp_listJBLocationImageFromModule = new Dictionary<string, Texture2D>(list_JB_Location_Images_From_Module);
                    Debug.Log("All images downloaded and updated in global variables.");
                    list_JB_Connection_Images_From_Module.Clear();
                    list_JB_Location_Images_From_Module.Clear();
                });
            }
            else if (SceneManager.GetActiveScene().name == "FieldDevicesScene")
            {
                temp_FieldDeviceInformationModel = GlobalVariable.temp_FieldDeviceInformationModel;

                var downloadTasks = new List<Task<Texture2D>>();

                foreach (var image_url in temp_FieldDeviceInformationModel.ListConnectionImages)
                {
                    if (!string.IsNullOrEmpty(image_url))
                    {
                        downloadTasks.Add(DownloadImageAsync(image_url));
                    }
                }
                var downloadedTextures = await Task.WhenAll(downloadTasks);

                // Cập nhật danh sách hình ảnh kết nối trên Main Thread
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    GlobalVariable.temp_ListFieldDeviceConnectionImages = new List<Texture2D>(downloadedTextures);
                    Debug.Log("All field device connection images downloaded and updated.");
                });

                downloadTasks.Clear();
            }
            GlobalVariable.ActiveCloseCanvasButton = true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error in DownloadImagesAsync: {ex.Message}");
            GlobalVariable.ActiveCloseCanvasButton = false;
        }
    }

    private async Task<Texture2D> DownloadImageAsync(string url)
    {

        await _semaphore.WaitAsync();
        try
        {
            using (var request = UnityWebRequestTexture.GetTexture($"{GlobalVariable.baseUrl}files/{url}"))
            {
                if (!await SendWebRequestAsync(request))
                {
                    Debug.LogError($"Request error from URL: {request.url}, Error: {request.error}");
                    return null;
                }

                var texture = DownloadHandlerTexture.GetContent(request);
                Debug.Log($"Image downloaded from: {url}");
                return texture;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error while downloading image from {url}: {ex}");
            return null;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<bool> SendWebRequestAsync(UnityWebRequest request)
    {
        try
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to download image from URL: Error: {request.error}");
                return false;
            }
            return !request.result.HasFlag(UnityWebRequest.Result.ConnectionError | UnityWebRequest.Result.ProtocolError);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during web request: {ex}");
            return false;
        }
    }
    public async Task GetAllDevicesByGrapper(string url, int grapperId)
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
                var list_DeviceInformationModel = JsonConvert.DeserializeObject<List<DeviceInformationModel>>(webRequest.downloadHandler.text);
                if (list_DeviceInformationModel != null && list_DeviceInformationModel.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_ListDeviceInformationModel = list_DeviceInformationModel;
                    GlobalVariable_Search_Devices.temp_List_Device_For_Fitler = FilterListDevicesForSearching(list_DeviceInformationModel);
                }
                else
                {
                    Debug.Log("list_DeviceInformationModel is null");
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
    public async Task GetAllJBsByGrapper(string url, int grapperId)
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
                var list_JBInformationModel = JsonConvert.DeserializeObject<List<JBInformationModel>>(webRequest.downloadHandler.text);
                if (list_JBInformationModel != null && list_JBInformationModel.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_ListJBInformationModel = list_JBInformationModel;
                    Debug.Log("GlobalVariable_Search_Devices.temp_ListJBInformationModel_From_Module.Count: " + GlobalVariable_Search_Devices.temp_ListJBInformationModel.Count);
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
    private List<string> FilterListDevicesForSearching(List<DeviceInformationModel> DeviceInformationModels)
    {
        var list_Devices_For_Filter = new HashSet<string>();
        foreach (var device in DeviceInformationModels)
        {
            if (!string.IsNullOrWhiteSpace(device.Code)) list_Devices_For_Filter.Add(device.Code);
            if (!string.IsNullOrWhiteSpace(device.Function)) list_Devices_For_Filter.Add(device.Function);
        }
        Debug.Log("list_Devices_For_Filter.Count: " + list_Devices_For_Filter.Count);
        return new List<string>(list_Devices_For_Filter);
    }
    public async Task LoadImageFromUrlAsync(string url, Image image, bool convertToSprite = true)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            if (!await SendWebRequestAsync(webRequest))
            {
                Debug.LogError($"Không thành công: {webRequest.url}, Error: {webRequest.error}");
            }
            else
            {
                try
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                    if (convertToSprite)
                    {
                        Sprite sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
                        image.sprite = sprite;
                        image.gameObject.SetActive(true);
                    }
                }
                catch (JsonException jsonEx)
                {
                    Debug.LogError($"Error parsing JSON from URL: {webRequest.url}, Error: {jsonEx.Message}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Unexpected error from URL: {webRequest.url}, Error: {ex}");
                }
            }

        }
    }

    //operation.isDone: Đoạn mã này kiểm tra xem tác vụ tải ảnh có hoàn thành chưa.
    // Điều này là một công việc background — tải ảnh từ internet, không ảnh hưởng đến main thread.
    // await Task.Yield(): Khi bạn gọi await Task.Yield(), điều này có nghĩa là main thread tạm dừng công việc hiện tại của nó(kiểm tra operation.isDone) 
    //và nhường quyền điều khiển cho hệ thống để thực hiện các tác vụ khác.Main thread không bị block và có thể tiếp tục xử lý các tác vụ khác.
    //hệ thống sẽ trả quyền điều khiển lại cho main thread khi công việc background hoàn thành, 
    //hoặc khi có sự kiện mới yêu cầu thực thi trên main thread.

    private void HandleRequestError(string error)
    {

        Debug.LogError($"Request error: {error}");
    }

    // Create New Device
    /* public async Task CreateNewDevice(string url, DeviceInformationModel device, string sceneName)
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
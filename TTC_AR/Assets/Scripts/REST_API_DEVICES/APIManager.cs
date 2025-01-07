using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance { get; private set; }

    // Danh sách URL cần tải
    public List<Grapper_General_Model> temp_List_Grapper_General_Models = new List<Grapper_General_Model>();
    public List<Module_Information_Model> temp_List_Module_Information_Model = new List<Module_Information_Model>();
    public List<Field_Device_Information_Model> temp_List_Field_Device_Information_Model = new List<Field_Device_Information_Model>();
    public Field_Device_Information_Model temp_Field_Device_Information_Model;

    public List<JB_Information_Model> temp_List_JB_Information_Model_From_Module = new List<JB_Information_Model>();
    public List<Device_Information_Model> temp_List_Device_Information_Model_From_Module = new List<Device_Information_Model>();
    public Dictionary<string, List<Texture2D>> list_JB_Connection_Images_From_Module = new Dictionary<string, List<Texture2D>>();
    public Dictionary<string, Texture2D> list_JB_Location_Images_From_Module = new Dictionary<string, Texture2D>();
    public List<string> imageUrls = new List<string>();
    // Danh sách Texture2D
    public List<Texture2D> textures = new List<Texture2D>();
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

    public async Task GetList_Grappers(string url)
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
                var grapper_Models = JsonConvert.DeserializeObject<List<Grapper_General_Model>>(webRequest.downloadHandler.text);
                if (grapper_Models != null && grapper_Models.Count > 0)
                {
                    temp_List_Grapper_General_Models = grapper_Models;
                    GlobalVariable.temp_List_Grapper_General_Models = temp_List_Grapper_General_Models;
                    Debug.Log(GlobalVariable.temp_List_Grapper_General_Models.Count);
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
    public async Task GetListFieldDeviceInformation(string url, string grapperId)
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
                var list_Field_Device_Model = JsonConvert.DeserializeObject<List<Field_Device_Information_Model>>(webRequest.downloadHandler.text);
                if (list_Field_Device_Model != null)
                {
                    GlobalVariable.temp_List_Field_Device_Information_Model = new List<Field_Device_Information_Model>();
                    temp_List_Field_Device_Information_Model = list_Field_Device_Model;
                    GlobalVariable.temp_List_Field_Device_Information_Model = temp_List_Field_Device_Information_Model;
                    Debug.Log("list_Field_Device_Model.Count: " + list_Field_Device_Model.Count);
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
    public async Task GetFieldDeviceInformation(string url, string grapperId, string fieldDeviceId)
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
                var list_Field_Device_Model = JsonConvert.DeserializeObject<List<Field_Device_Information_Model>>(webRequest.downloadHandler.text);

                if (list_Field_Device_Model != null)
                {
                    GlobalVariable.temp_List_Field_Device_Information_Model = new List<Field_Device_Information_Model>();
                    GlobalVariable.temp_Field_Device_Information_Model = null;
                    temp_List_Field_Device_Information_Model = list_Field_Device_Model;
                    GlobalVariable.temp_List_Field_Device_Information_Model = temp_List_Field_Device_Information_Model;
                    GlobalVariable.temp_Field_Device_Information_Model = temp_List_Field_Device_Information_Model[0];
                    Debug.Log("list_Field_Device_Model.Count: " + list_Field_Device_Model.Count);
                    Debug.Log("GlobalVariable.temp_Field_Device_Information_Model: " + GlobalVariable.temp_Field_Device_Information_Model.Name);
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


    public async Task GetListModuleInformation(string url, string grapperId)
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
                var list_module_Information_Model = JsonConvert.DeserializeObject<List<Module_Information_Model>>(webRequest.downloadHandler.text);
                if (list_module_Information_Model != null)
                {
                    temp_List_Module_Information_Model = list_module_Information_Model;

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
    public async Task GetModuleInformation(string url, string grapperId, string rackId, string moduleId)
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
                var module_Information_Model = JsonConvert.DeserializeObject<List<Module_Information_Model>>(webRequest.downloadHandler.text);
                if (module_Information_Model != null)
                {
                    // temp_List_JB_Information_Model_From_Module.Clear();
                    // temp_List_Device_Information_Model_From_Module.Clear();
                    // temp_List_Module_Information_Model = null;

                    temp_List_Module_Information_Model = module_Information_Model;
                    temp_List_JB_Information_Model_From_Module = module_Information_Model[0].List_JB_Information_Model;
                    temp_List_Device_Information_Model_From_Module = module_Information_Model[0].List_Device_Information_Model;

                    GlobalVariable.temp_List_JB_Information_Model_From_Module = temp_List_JB_Information_Model_From_Module;

                    GlobalVariable.temp_List_Device_Information_Model_From_Module = temp_List_Device_Information_Model_From_Module;

                    GlobalVariable.temp_Module_Information_Model = temp_List_Module_Information_Model[0];

                    GlobalVariable.temp_Module_Specification_Model = module_Information_Model[0].Specification_Model;

                    GlobalVariable.temp_Adapter_Specification_Model = module_Information_Model[0].Specification_Model.Adapter;
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
        try
        {

            if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
            {
                if (temp_List_JB_Information_Model_From_Module == null || temp_List_JB_Information_Model_From_Module.Count == 0)
                {
                    Debug.LogWarning("No JB information models available to download images.");
                    return;
                }

                Debug.Log("temp_List_JB_Information_Model_From_Module.Count: " + temp_List_JB_Information_Model_From_Module.Count);

                var downloadTasks = new List<Task<Texture2D>>();

                foreach (var jb in temp_List_JB_Information_Model_From_Module)
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

                    // Tải hình ảnh ngoài trời
                    var outdoorImageTask = DownloadImageAsync(jb.Outdoor_Image);
                    list_JB_Location_Images_From_Module[jb.Name] = await outdoorImageTask;

                    // Tải danh sách hình ảnh kết nối
                    foreach (var url in jb.List_Connection_Images)
                    {
                        downloadTasks.Add(DownloadImageAsync(url));
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
                    GlobalVariable.temp_list_JB_Connection_Image_From_Module = new Dictionary<string, List<Texture2D>>(list_JB_Connection_Images_From_Module);
                    GlobalVariable.temp_list_JB_Location_Image_From_Module = new Dictionary<string, Texture2D>(list_JB_Location_Images_From_Module);
                    Debug.Log("All images downloaded and updated in global variables.");
                    list_JB_Connection_Images_From_Module.Clear();
                    list_JB_Location_Images_From_Module.Clear();
                });
            }
            else if (SceneManager.GetActiveScene().name == "FieldDevicesScene")
            {
                temp_Field_Device_Information_Model = GlobalVariable.temp_Field_Device_Information_Model;

                var downloadTasks = new List<Task<Texture2D>>();

                foreach (var image_url in temp_Field_Device_Information_Model.List_connection_Images)
                {
                    downloadTasks.Add(DownloadImageAsync(image_url));
                }

                var downloadedTextures = await Task.WhenAll(downloadTasks);

                // Cập nhật danh sách hình ảnh kết nối trên Main Thread
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    GlobalVariable.temp_List_Field_Device_Connection_Images = new List<Texture2D>(downloadedTextures);
                    Debug.Log("All field device connection images downloaded and updated.");
                });

                downloadTasks.Clear();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error in DownloadImagesAsync: {ex.Message}");
        }
    }

    private async Task<Texture2D> DownloadImageAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            Debug.LogWarning("URL is null or empty.");
            return null;
        }

        await _semaphore.WaitAsync();
        try
        {
            using (var request = UnityWebRequestTexture.GetTexture(url))
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

            return !request.result.HasFlag(UnityWebRequest.Result.ConnectionError | UnityWebRequest.Result.ProtocolError);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during web request: {ex}");
            return false;
        }
    }

    // private void LogImageResults()
    // {
    //     foreach (var kvp in list_JB_Connection_Images_From_Module)
    //     {
    //         Debug.Log($"JB: {kvp.Key}, Connection Images Count: {kvp.Value.Count}");
    //     }

    //     foreach (var kvp in list_JB_Location_Images_From_Module)
    //     {
    //         Debug.Log($"JB: {kvp.Key}, Location Image: {kvp.Value}");
    //     }
    // }


    // public IEnumerator Save_List_JB_Connection_To_Dictionary()
    // {
    //     // Danh sách các task để tải hình ảnh
    //     var tasks = new List<Task>();
    //     foreach (var jb in GlobalVariable.temp_List_JB_Information_Model_From_Modules_From_Module)
    //     {
    //         if (GlobalVariable.temp_list_JB_Connection_Image_From_Module.ContainsKey(jb.Name))
    //         {
    //             continue;
    //         }

    //         // background thread => không được sử dụng API của Unity
    //         var task = Task.Run(async () =>
    //         {
    //             await Task.Yield(); // Quay lại main thread

    //             var list_JB_Connection_Image = new List<Texture2D>();

    //             // Tải từng hình ảnh trong List_Connection_Images
    //             foreach (var imageUrl in jb.List_Connection_Images)
    //             {

    //                 Texture2D texture = await _LoadImageFromUrlAsync(imageUrl);
    //                 if (texture != null)
    //                 {
    //                     list_JB_Connection_Image.Add(texture);
    //                 }
    //             }
    //             //? Lưu kết quả vào Dictionary, lock vào phần cập nhật Dictionary để đảm bảo rằng chỉ một thread có thể truy cập và cập nhật từ Dictionary tại một thời điểm.
    //             lock (GlobalVariable.temp_list_JB_Connection_Image_From_Module)
    //             {
    //                 GlobalVariable.temp_list_JB_Connection_Image_From_Module[jb.Name] = list_JB_Connection_Image;
    //             }
    //         });
    //         tasks.Add(task);
    //     }

    //     // Đợi tất cả các task hoàn thành
    //     await Task.WhenAll(tasks);
    //     // Debug để kiểm tra
    //     Debug.Log("Loaded all images into dictionary.");
    //     Debug.Log("Save_List_JB_Connection_To_Dictionary\n GlobalVariable.temp_list_JB_Connection_Image_From_Module.Count: " + GlobalVariable.temp_list_JB_Connection_Image_From_Module.Count);
    // }



    // // Phương thức tải ảnh từ URL
    // public async Task<Texture2D> _LoadImageFromUrlAsync(string url)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
    //     {
    //         if (!await SendWebRequestAsync(webRequest))
    //         {
    //             Debug.LogError($"Request error: {webRequest.error}");
    //             return null;
    //         }
    //         try
    //         {
    //             return DownloadHandlerTexture.GetContent(webRequest);

    //         }
    //         catch (JsonException jsonEx)
    //         {
    //             Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
    //             return null;
    //         }
    //         catch (Exception ex)
    //         {
    //             Debug.LogError($"Unexpected error: {ex}");
    //             return null;
    //         }
    //     }
    // }
    public async Task GetAllDevicesByGrapper(string url, string grapperId)
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
                var list_Device_Information_Model = JsonConvert.DeserializeObject<List<Device_Information_Model>>(webRequest.downloadHandler.text);
                if (list_Device_Information_Model != null && list_Device_Information_Model.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_List_Device_Information_Model = list_Device_Information_Model;
                    GlobalVariable_Search_Devices.temp_List_Device_For_Fitler = FilterListDevicesForSearching(list_Device_Information_Model);
                }
                else
                {
                    Debug.Log("list_Device_Information_Model is null");
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
    public async Task GetAllJBsByGrapper(string url, string grapperId)
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
                var list_JB_Information_Model = JsonConvert.DeserializeObject<List<JB_Information_Model>>(webRequest.downloadHandler.text);
                if (list_JB_Information_Model != null && list_JB_Information_Model.Count > 0)
                {
                    GlobalVariable_Search_Devices.temp_List_JB_Information_Model = list_JB_Information_Model;
                    Debug.Log("GlobalVariable_Search_Devices.temp_List_JB_Information_Model_From_Module.Count: " + GlobalVariable_Search_Devices.temp_List_JB_Information_Model.Count);
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
    private List<string> FilterListDevicesForSearching(List<Device_Information_Model> Device_Information_Models)
    {
        var list_Devices_For_Filter = new HashSet<string>();
        foreach (var device in Device_Information_Models)
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
                Debug.LogError($"Request error from URL: {webRequest.url}, Error: {webRequest.error}");
                return;
            }
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
                return;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error from URL: {webRequest.url}, Error: {ex}");
                return;
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

    private async Task HandleSuccessfulPost(Device_Information_Model device, string sceneName)
    {
        Show_Dialog.Instance.ShowToast("success", $"Thêm thiết bị mới thành công: {device.Code}");
        // await GetAllDevicesByGrapper($"{GlobalVariable.baseUrl}{device.location[^1..]}", sceneName);
        await Task.Delay(0);

        Sence_Behaviour.Reload_Scene(sceneName);

    }
    // Create New Device
    /* public async Task CreateNewDevice(string url, Device_Information_Model device, string sceneName)
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

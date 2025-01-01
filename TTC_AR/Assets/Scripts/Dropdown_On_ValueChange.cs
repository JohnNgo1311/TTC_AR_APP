using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class Dropdown_On_ValueChange : MonoBehaviour
{
    public GameObject prefab_Device;
    private RectTransform contentTransform;
    private TMP_Text code_Value_Text, function_Value_Text, range_Value_Text, io_Value_Text, jb_Connection_Value_Text, jb_Connection_Location_Text;
    private Image module_Image, JB_Location_Image_Prefab, JB_Connection_Wiring_Image_Prefab;
    private GameObject JB_Connection_Group;
    private ScrollRect scrollRect;
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private List<Image> instantiatedImages = new List<Image>();
    private string currentLoadedDeviceCode;
    private bool loadDataSuccess = false;
    private Dictionary<string, Device_Information_Model> deviceDictionary;

    public SearchableDropDown searchableDropDown;

    [SerializeField] GameObject bottom_App_Bar;
    public Open_Detail_Image open_Detail_Image;
    [SerializeField] private EventPublisher eventPublisher;
    private string _jbName = "";
    private string _moduleName = "";


    private void Awake()
    {
        Debug.Log("Dropdown_On_ValueChange Awake");
        searchableDropDown = searchableDropDown.GetComponent<SearchableDropDown>();
    }
    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện
        if (eventPublisher != null)
        {
            eventPublisher.OnOrientationChanged += HandleOrientationChange;
        }
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị disable
        if (eventPublisher != null)
        {
            eventPublisher.OnOrientationChanged -= HandleOrientationChange;
        }
        ResetResources();
    }
    // Hàm xử lý khi sự kiện được kích hoạt
    private void HandleOrientationChange(ScreenOrientation newOrientation)
    {
        if (newOrientation == ScreenOrientation.Portrait)
        {
            bottom_App_Bar.SetActive(true);
        }
        else if (newOrientation == ScreenOrientation.LandscapeLeft || newOrientation == ScreenOrientation.LandscapeRight)
        {
            bottom_App_Bar.SetActive(false);
        }
    }

    private void SetToastInstanceStatusTrue()
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
    }
    private async void Start()
    {
        await LoadData();

        /*  if (Show_Dialog.Instance != null)
          {
              Invoke(nameof(SetToastInstanceStatusTrue), 1f); 
               sau 1s kể từ Start chạy thì hàm SetToastInstanceStatusTrue sẽ được gọi
               trước 1s thì hàm này không được gọi nhưng các dòng phía dưới sẽ được chạy
          }
          CacheUIElements();
          CacheDevices();
          Debug.Log("Dropdown_On_ValueChange Start");
          searchableDropDown.Initialize();
          searchableDropDown.inputField.onValueChanged.AddListener(OnInputValueChanged);*/

        if (string.IsNullOrEmpty(searchableDropDown.inputField.text))
        {
            loadDataSuccess = true;
            searchableDropDown.Set_Initial_Text_Field_Value();
        }
        /* if (!string.IsNullOrEmpty(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code))
         {
             OnInputValueChanged(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code);
         }*/
    }

    private async Task LoadData()
    {
        try
        {
            if (Show_Dialog.Instance != null)
            {
                SetToastInstanceStatusTrue();
            }
            Debug.Log(loadDataSuccess);
            CacheUIElements();
            CacheDevices();

            searchableDropDown.Initialize();
            searchableDropDown.inputField.onValueChanged.AddListener(OnInputValueChanged);
            Debug.Log(loadDataSuccess);
            await Task.Delay(1000);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    private void CacheUIElements()
    {
        var content = prefab_Device.transform.Find("Content");
        scrollRect = prefab_Device.GetComponent<ScrollRect>();
        contentTransform = content.GetComponent<RectTransform>();

        var deviceInfo = content.Find("Device_information");
        code_Value_Text = deviceInfo.Find("Code_group/Code_value").GetComponent<TMP_Text>();
        function_Value_Text = deviceInfo.Find("Function_group/Function_value").GetComponent<TMP_Text>();
        range_Value_Text = deviceInfo.Find("Range_group/Range_value").GetComponent<TMP_Text>();
        io_Value_Text = deviceInfo.Find("IO_group/IO_value").GetComponent<TMP_Text>();

        var jbConnectionGroup = content.Find("JB_Connection_group/JB_Connection_text_group");
        jb_Connection_Value_Text = jbConnectionGroup.Find("JB_Connection_value").GetComponent<TMP_Text>();
        jb_Connection_Location_Text = jbConnectionGroup.Find("JB_Connection_location").GetComponent<TMP_Text>();

        module_Image = content.Find("Module_group/Real_Module_Image").GetComponent<Image>();
        JB_Connection_Group = content.Find("JB_Connection_group").gameObject;
        JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        JB_Connection_Wiring_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();
    }
    private void CacheDevices()
    {
        deviceDictionary = GlobalVariable_Search_Devices.temp_List_Device_Information_Model.ToDictionary(d => d.Code);
        var functionDictionary = GlobalVariable_Search_Devices.temp_List_Device_Information_Model.ToDictionary(d => d.Function);
        foreach (var kvp in functionDictionary)
        {
            if (deviceDictionary.ContainsKey(kvp.Key))
            {
                deviceDictionary[kvp.Key] = kvp.Value;
            }
            else
            {
                deviceDictionary.Add(kvp.Key, kvp.Value);
            }
        }
    }

    private void OnInputValueChanged(string input)
    {
        if (input == currentLoadedDeviceCode)
        {
            return;
        }

        if (!deviceDictionary.TryGetValue(input, out var device))
        {
            ClearWiringGroupAndCache();
        }
        else
        {
            UpdateDeviceInformation(device);
            currentLoadedDeviceCode = input;
        }
    }

    private void ClearWiringGroupAndCache()
    {
        foreach (Transform child in JB_Connection_Group.transform)
        {
            if (child.gameObject.activeSelf && child.gameObject.name.Contains("JB_Connection_Wiring(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
        spriteCache.Clear();
    }

    private void UpdateDeviceInformation(Device_Information_Model device)
    {
        prefab_Device.name = $"Scroll_Area_{device.Code}";
        code_Value_Text.text = device.Code;
        function_Value_Text.text = device.Function;
        range_Value_Text.text = device.Range;
        io_Value_Text.text = device.IOAddress;

        var parts = device.JB_Information_Model.Name.Split('_');
        jb_Connection_Value_Text.text = $"{parts[0]}:";
        jb_Connection_Location_Text.text = parts.Length > 1 ? parts[1] : string.Empty;
        _jbName = parts[0];
        GlobalVariable_Search_Devices.jbName = _jbName;
        _moduleName = device.IOAddress.Substring(0, device.IOAddress.LastIndexOf('.'));
        GlobalVariable_Search_Devices.moduleName = _moduleName;

        if (!string.IsNullOrEmpty(GlobalVariable_Search_Devices.jbName))
        {
            LoadDeviceSprites(device.JB_Information_Model);
        }
    }

    private async void LoadDeviceSprites(JB_Information_Model jbInformationModel)
    {
        ClearWiringGroupAndCache();

        scrollRect.verticalNormalizedPosition = 1f;
        Debug.Log("scrollRect.verticalNormalizedPosition = 1f");
        if (loadDataSuccess)
        {
            Debug.Log("loadDataSuccess: " + loadDataSuccess);
            Show_Dialog.Instance.ShowToast("success", "Tải dữ liệu thành công");
            StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False()); // đợi 1 giây rồi đặt Instance_Status thành false
        }
        loadDataSuccess = false;

    }
    // private async Task LoadImageFromUrlAsync(string url, Image image)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
    //     {
    //         if (!await SendWebRequestAsync(webRequest))
    //         {
    //             Debug.LogError($"Request error: {webRequest.error}");
    //             return;
    //         }
    //         try
    //         {
    //             Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
    //             Sprite sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
    //             image.sprite = sprite;
    //         }
    //         catch (JsonException jsonEx)
    //         {
    //             Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
    //             return;
    //         }
    //         catch (Exception ex)
    //         {
    //             Debug.LogError($"Unexpected error: {ex}");
    //             return;
    //         }
    //     }
    // }
    private async Task Apply_Sprite_JB_Images(string outdoor_Image, List<string> list_Connection_Images)
    {
        ClearInstantiatedImages();
        JB_Location_Image_Prefab.gameObject.SetActive(true);

        JB_Location_Image_Prefab.gameObject.SetActive(false);
        foreach (string jb in GlobalVariable_Search_Devices.jbName.Split('-'))
        {
            CreateAndSetSprite($"{jb.Trim()}_Location");
        }

        // foreach (var spriteName in filteredList)
        // {
        //     if (spriteCache.TryGetValue(spriteName, out var jbConnectionSprite))
        //     {
        //         var newImage = Instantiate(JB_Connection_Wiring_Image_Prefab, JB_Connection_Group.transform);
        //         newImage.sprite = jbConnectionSprite;
        //         newImage.gameObject.GetComponent<Button>().onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(newImage));
        //         Debug.Log("Đã add sự kiện click vào newImage");
        //         newImage.gameObject.SetActive(true);
        //         StartCoroutine(
        //              Resize_Gameobject_Function.Set_NativeSize_For_GameObject(newImage));

        //     }
        // }

        JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(false);

        await Task.Yield();
    }




    private void SetSprite(Image imageComponent, string jb_name)
    {
        if (!spriteCache.TryGetValue(jb_name, out var jbSprite))
        {
            spriteCache.TryGetValue("JB_TSD_Location_Note", out jbSprite);
        }

        imageComponent.sprite = jbSprite;
        imageComponent.gameObject.GetComponent<Button>().onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(imageComponent));
        Debug.Log("Đã add sự kiện click vào imageComponent");
        StartCoroutine(

              Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent)
             );
    }

    private void CreateAndSetSprite(string jb_name)
    {
        var jbLocationImage = Instantiate(JB_Location_Image_Prefab, JB_Connection_Group.transform);
        jbLocationImage.transform.SetSiblingIndex(JB_Location_Image_Prefab.transform.GetSiblingIndex() + 1);
        jbLocationImage.gameObject.SetActive(true);
        SetSprite(jbLocationImage, jb_name);
        instantiatedImages.Add(jbLocationImage);
    }

    private void ClearInstantiatedImages()
    {
        foreach (var img in instantiatedImages)
        {
            if (img != null)
            {
                Destroy(img.gameObject);
            }
        }
        instantiatedImages.Clear();
    }
    private void ResetResources()
    {
        searchableDropDown.inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        spriteCache.Clear();
        currentLoadedDeviceCode = null;
    }
}
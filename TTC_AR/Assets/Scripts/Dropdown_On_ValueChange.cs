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
using EasyUI.Progress;

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
    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            InitilizeUIElements();
            Prepare_Device_Dictionary_For_Searching();
            searchableDropDown.Initialize();
            searchableDropDown.inputField.onValueChanged.AddListener(OnInputValueChanged);
            if (string.IsNullOrEmpty(searchableDropDown.inputField.text))
            {
                loadDataSuccess = true;
                searchableDropDown.Set_Initial_Text_Field_Value();
            }



        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    private void InitilizeUIElements()
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

        //   module_Image = content.Find("Module_group/Real_Module_Image").GetComponent<Image>();
        JB_Connection_Group = content.Find("JB_Connection_group").gameObject;
        JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        JB_Connection_Wiring_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();
    }
    private void Prepare_Device_Dictionary_For_Searching()
    {
        deviceDictionary = GlobalVariable_Search_Devices.temp_List_Device_Information_Model.ToDictionary(d => d.Code);
        var functionDictionary = GlobalVariable_Search_Devices.temp_List_Device_Information_Model.ToDictionary(d => d.Function);
        foreach (var keyValuePair in functionDictionary)
        {
            if (deviceDictionary.ContainsKey(keyValuePair.Key))
            {
                deviceDictionary[keyValuePair.Key] = keyValuePair.Value;
            }
            else
            {
                deviceDictionary.Add(keyValuePair.Key, keyValuePair.Value);
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
        // spriteCache.Clear();
    }

    private void UpdateDeviceInformation(Device_Information_Model device)
    {
        prefab_Device.name = $"Scroll_Area_{device.Code}";
        code_Value_Text.text = device.Code;
        function_Value_Text.text = device.Function;
        range_Value_Text.text = device.Range;
        io_Value_Text.text = device.IOAddress;

        jb_Connection_Value_Text.text = $"{device.JB_Information_Model.Name}:";
        jb_Connection_Location_Text.text = device.JB_Information_Model.Location;
        _jbName = jb_Connection_Value_Text.text;
        GlobalVariable_Search_Devices.jbName = _jbName;
        _moduleName = device.Module_General_Model.Name;
        GlobalVariable_Search_Devices.moduleName = _moduleName;

        if (!string.IsNullOrEmpty(_jbName))
        {
            ClearWiringGroupAndCache();
            LoadDeviceSprites(device.Additional_Connection_Images, jbInformationModel: device.JB_Information_Model);
        }
    }

    private async void LoadDeviceSprites(List<string> list_Additional_Images, JB_Information_Model jbInformationModel)
    {
        jbInformationModel.List_Connection_Images.AddRange(list_Additional_Images);
        await Apply_Sprite_JB_Images(outdoor_Image: jbInformationModel.Outdoor_Image, list_Connection_Images: jbInformationModel.List_Connection_Images);
        scrollRect.verticalNormalizedPosition = 1f;
        jbInformationModel.List_Connection_Images.RemoveRange(jbInformationModel.List_Connection_Images.Count - list_Additional_Images.Count, list_Additional_Images.Count);
    }

    private async Task Apply_Sprite_JB_Images(string outdoor_Image, List<string> list_Connection_Images)
    {
        JB_Location_Image_Prefab.gameObject.SetActive(false);
        JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(false);
        List<Task> tasks = new List<Task>();

        if (!string.IsNullOrEmpty(outdoor_Image))
        {
            JB_Location_Image_Prefab.gameObject.GetComponent<Button>().onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(JB_Location_Image_Prefab));
            tasks.Add(APIManager.Instance.LoadImageFromUrlAsync(outdoor_Image, JB_Location_Image_Prefab));
        }

        foreach (string connectionImage in list_Connection_Images)
        {
            var newImage = Instantiate(JB_Connection_Wiring_Image_Prefab, JB_Connection_Group.transform);
            newImage.gameObject.GetComponent<Button>().onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(newImage));
            tasks.Add(APIManager.Instance.LoadImageFromUrlAsync(connectionImage, newImage));
        }

        Progress.Show("Đang tra cứu...", ProgressColor.Blue, true);
        Progress.SetDetailsText("Dữ liệu đang được tải...");

        int totalTasks = tasks.Count;
        int completedTasks = 0;

        var wrappedTasks = tasks.Select(async task =>
        {
            try
            {
                await task;
            }
            finally
            {
                completedTasks++;
                Progress.SetProgressValue((float)completedTasks / totalTasks * 100f);
            }
        });

        await Task.WhenAll(wrappedTasks);
        Progress.SetProgressValue(100f);
        foreach (Image connectionImage in JB_Connection_Group.GetComponentsInChildren<Image>())
        {
            StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(connectionImage));
        }
        await Task.Delay(500);
        Progress.Hide();
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
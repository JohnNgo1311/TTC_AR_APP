using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EasyUI.Progress;

public class Dropdown_On_ValueChange : MonoBehaviour
{
    public GameObject prefab_Device;
    public SearchableDropDown searchableDropDown;
    public Open_Detail_Image open_Detail_Image;
    public EventPublisher eventPublisher;

    [SerializeField] private RectTransform contentTransform;
    [SerializeField] private TMP_Text code_Value_Text, function_Value_Text, range_Value_Text, io_Value_Text, jb_Connection_Value_Text, jb_Connection_Location_Text;
    [SerializeField] private Image JB_Location_Image_Prefab, JB_Connection_Wiring_Image_Prefab;
    [SerializeField] private GameObject JB_Connection_Group, bottom_App_Bar;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Dictionary<string, Sprite> spriteCache = new();
    [SerializeField] private List<Image> instantiatedImages = new();
    [SerializeField] private Transform deviceInfo;

    private string current_Search_Content = "";
    private Dictionary<string, DeviceInformationModel> deviceDictionary;
    private Dictionary<string, JBInformationModel> jBDictionary;

    private string _jbName = "";
    private string _moduleName = "";

    private void Awake()
    {
        searchableDropDown ??= GameObject.Find("Searchable").GetComponent<SearchableDropDown>();
    }

    private void OnEnable()
    {
        // if (eventPublisher != null)
        // {
        //     eventPublisher.OnOrientationChanged += HandleOrientationChange;
        // }
    }

    private void OnDisable()
    {
        // if (eventPublisher != null)
        // {
        //     eventPublisher.OnOrientationChanged -= HandleOrientationChange;
        // }
        // ResetResources();
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            InitUIElements();
            Prepare_Device_Dictionary_For_Searching();
            Prepare_JB_Dictionary_For_Searching();
            searchableDropDown.Initialize();
            searchableDropDown.inputField.onValueChanged.AddListener(OnInputValueChanged);
            searchableDropDown.SetInitialTextFieldValue();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.InnerException);
        }
    }

    private void InitUIElements()
    {
        var content = prefab_Device.transform.Find("Content");
        scrollRect ??= prefab_Device.GetComponent<ScrollRect>();
        contentTransform ??= content.GetComponent<RectTransform>();

        deviceInfo = content.Find("Device_information");
        code_Value_Text ??= deviceInfo.Find("Code_group/Code_value").GetComponent<TMP_Text>();
        function_Value_Text ??= deviceInfo.Find("Function_group/Function_value").GetComponent<TMP_Text>();
        range_Value_Text ??= deviceInfo.Find("Range_group/Range_value").GetComponent<TMP_Text>();
        io_Value_Text ??= deviceInfo.Find("IO_group/IO_value").GetComponent<TMP_Text>();

        var jbConnectionGroup = content.Find("JB_Connection_group/JB_Connection_text_group");
        jb_Connection_Value_Text ??= jbConnectionGroup.Find("JB_Connection_value").GetComponent<TMP_Text>();
        jb_Connection_Location_Text ??= jbConnectionGroup.Find("JB_Connection_location").GetComponent<TMP_Text>();

        JB_Connection_Group ??= content.Find("JB_Connection_group").gameObject;
        JB_Location_Image_Prefab ??= JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        JB_Connection_Wiring_Image_Prefab ??= JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();
    }
    private void Prepare_Device_Dictionary_For_Searching()
    {
        var tempListDevice = GlobalVariable_Search_Devices.temp_ListDeviceInformationModel;

        deviceDictionary = tempListDevice
            .GroupBy(device => device.Code.ToLower())
            .ToDictionary(g => g.Key, g => g.First());

        foreach (var device in tempListDevice)
        {
            var functionKey = device.Function.ToLower();
            if (!deviceDictionary.ContainsKey(functionKey))
            {
                deviceDictionary.Add(functionKey, device);
            }
        }
    }

    private void Prepare_JB_Dictionary_For_Searching()
    {
        var tempListJB = GlobalVariable_Search_Devices.temp_ListJBInformationModel;

        jBDictionary = tempListJB
            .GroupBy(jb => jb.Name.ToLower())
            .ToDictionary(g => g.Key, g => g.First());
    }
    // private void Prepare_Device_Dictionary_For_Searching()
    // {
    //     deviceDictionary = GlobalVariable_Search_Devices.temp_ListDeviceInformationModel.ToDictionary(d => d.Code);
    //     var functionDictionary = GlobalVariable_Search_Devices.temp_ListDeviceInformationModel.ToDictionary(d => d.Function);
    //     foreach (var keyValuePair in functionDictionary)
    //     {
    //         if (deviceDictionary.ContainsKey(keyValuePair.Key))
    //         {
    //             deviceDictionary[keyValuePair.Key] = keyValuePair.Value;
    //         }
    //         else
    //         {
    //             deviceDictionary.Add(keyValuePair.Key, keyValuePair.Value);
    //         }
    //     }
    // }
    private void OnInputValueChanged(string input)
    {
        if (input.ToLower() == current_Search_Content.ToLower())
        {
            return;
        }
        Debug.Log(searchableDropDown.filter_Type);
        if (searchableDropDown.filter_Type == "Device")
        {
            if (!deviceDictionary.TryGetValue(input.ToLower(), out var device))
            {
                ClearWiringGroupAndCache();
            }
            else
            {
                UpdateDeviceInformation(device);
                current_Search_Content = input.ToLower();

            }
        }
        if (searchableDropDown.filter_Type == "JB/TSD")
        {
            if (!jBDictionary.TryGetValue(input.ToLower(), out var jB))
            {
                ClearWiringGroupAndCache();
            }
            else
            {
                UpdateJBInformation(jB);
                current_Search_Content = input.ToLower();
            }
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
    }

    private void UpdateDeviceInformation(DeviceInformationModel device)
    {
        if (!deviceInfo.gameObject.activeSelf) deviceInfo.gameObject.SetActive(true);
        prefab_Device.name = $"Scroll_Area_{device.Code}";
        code_Value_Text.text = device.Code;
        function_Value_Text.text = device.Function;
        range_Value_Text.text = device.Range;
        io_Value_Text.text = device.IOAddress;

        jb_Connection_Value_Text.text = $"{device.JBInformationModel.Name}:";
        jb_Connection_Location_Text.text = device.JBInformationModel.Location;
        _jbName = jb_Connection_Value_Text.text;
        GlobalVariable_Search_Devices.jbName = _jbName;
        // _moduleName = device.ModuleBasicModel.Name;
        GlobalVariable_Search_Devices.moduleName = _moduleName;

        if (!string.IsNullOrEmpty(_jbName))
        {
            ClearWiringGroupAndCache();
            LoadDeviceSprites(device.AdditionalConnectionImages, jbInformationModel: device.JBInformationModel);
        }
    }

    private void UpdateJBInformation(JBInformationModel jB)
    {
        if (deviceInfo.gameObject.activeSelf) deviceInfo.gameObject.SetActive(false);
        prefab_Device.name = $"Scroll_Area_{jB.Name}";
        jb_Connection_Value_Text.text = $"{jB.Name}:";
        jb_Connection_Location_Text.text = jB.Location;
        _jbName = jb_Connection_Value_Text.text;
        GlobalVariable_Search_Devices.jbName = _jbName;
        if (!string.IsNullOrEmpty(_jbName))
        {
            ClearWiringGroupAndCache();
            LoadDeviceSprites(null, jbInformationModel: jB);
        }
    }

    private async void LoadDeviceSprites(List<ImageInformationModel> list_Additional_Images, JBInformationModel jbInformationModel)
    {
        if (list_Additional_Images != null) jbInformationModel.ListConnectionImages.AddRange(list_Additional_Images);
        await Apply_Sprite_JB_Images(jbInformationModel.OutdoorImage.url, jbInformationModel.ListConnectionImages);
        scrollRect.verticalNormalizedPosition = 1f;
        if (list_Additional_Images != null) jbInformationModel.ListConnectionImages.RemoveRange(jbInformationModel.ListConnectionImages.Count - list_Additional_Images.Count, list_Additional_Images.Count);

    }

    private async Task Apply_Sprite_JB_Images(string outdoorImage, List<ImageInformationModel> listConnectionImages)
    {
        JB_Location_Image_Prefab.gameObject.SetActive(false);
        JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(false);

        var tasks = new List<Task>();

        if (!string.IsNullOrEmpty(outdoorImage))
        {
            AddButtonListener(JB_Location_Image_Prefab, outdoorImage);
            tasks.Add(LoadImageAsync(outdoorImage, JB_Location_Image_Prefab));
        }

        foreach (var image in listConnectionImages)
        {
            var newImage = Instantiate(JB_Connection_Wiring_Image_Prefab, JB_Connection_Group.transform);
            AddButtonListener(newImage, image.Name);
            tasks.Add(LoadImageAsync(image.Name, newImage));
        }

        await ShowProgressBar("Đang tra cứu...", "Dữ liệu đang được tải...");
        await TrackProgress(tasks);
        ResizeImages();
        await HideProgressBar();
    }

    private void AddButtonListener(Image imagePrefab, string imageName)
    {
        var button = imagePrefab.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => open_Detail_Image.Open_Detail_Canvas(imagePrefab));
    }

    private Task LoadImageAsync(string imageName, Image imagePrefab)
    {
        string url = $"{GlobalVariable.baseUrl}files/{imageName}";
        return APIManager.Instance.LoadImageFromUrlAsync(url, imagePrefab);
    }

    private async Task ShowProgressBar(string title, string details)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        Progress.SetDetailsText(details);
        await Task.Delay(100);
    }

    private async Task TrackProgress(List<Task> tasks)
    {
        int totalTasks = tasks.Count;
        int completedTasks = 0;

        var wrappedTasks = tasks.Select(async task =>
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading image: {e.Message}");
            }
            finally
            {
                completedTasks++;
                Progress.SetProgressValue((float)completedTasks / totalTasks * 100f);
            }
        });
        await Task.WhenAll(wrappedTasks);
        Progress.SetProgressValue(100f);
    }

    private void ResizeImages()
    {
        StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(JB_Location_Image_Prefab));

        foreach (var connectionImage in JB_Connection_Group.GetComponentsInChildren<Image>())
        {
            StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(connectionImage));
        }
    }

    private async Task HideProgressBar()
    {
        await Task.Delay(200);
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
        StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent));
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
        current_Search_Content = null;
    }

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
}

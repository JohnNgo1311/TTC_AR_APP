using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Collections;

public class Dropdown_On_ValueChange : MonoBehaviour
{
    public GameObject prefab_Device;
    public TMP_InputField inputField;
    //  public GameObject overlay_Loading_Image;
    private RectTransform contentTransform;
    private TMP_Text code_Value_Text, function_Value_Text, range_Value_Text, io_Value_Text, jb_Connection_Value_Text, jb_Connection_Location_Text;
    private Image module_Image, JB_Location_Image_Prefab, JB_Connection_Wiring_Image_Prefab;
    private GameObject JB_Connection_Group;
    private ScrollRect scrollRect;
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private List<Image> instantiatedImages = new List<Image>();
    private string currentLoadedDeviceCode;
    private bool loadDataSuccess = false;
    private Dictionary<string, DeviceModel> deviceDictionary;


    private void Awake()
    {
        //  overlay_Loading_Image.SetActive(true);

        // Đặt Instance_Status thành true và chờ hoàn thành
        Show_Dialog.Instance.Set_Instance_Status_True();

        // Hiển thị thông báo "Đang tải dữ liệu..."
        Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");

        // Kiểm tra inputField
        if (inputField == null)
        {
            return;
        }

        // Cache các phần tử UI và dữ liệu thiết bị
        CacheUIElements();
        CacheDevices();
        // Thêm sự kiện cho inputField và gọi OnInputValueChanged
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        OnInputValueChanged(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code);
        // Kiểm tra nếu load dữ liệu thành công

        loadDataSuccess = true;
    }



    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    private void CacheUIElements()
    {
        scrollRect = prefab_Device.GetComponent<ScrollRect>();
        contentTransform = prefab_Device.transform.Find("Content").GetComponent<RectTransform>();
        code_Value_Text = contentTransform.Find("Device_information/Code_group/Code_value").GetComponent<TMP_Text>();
        function_Value_Text = contentTransform.Find("Device_information/Function_group/Function_value").GetComponent<TMP_Text>();
        range_Value_Text = contentTransform.Find("Device_information/Range_group/Range_value").GetComponent<TMP_Text>();
        io_Value_Text = contentTransform.Find("Device_information/IO_group/IO_value").GetComponent<TMP_Text>();
        jb_Connection_Value_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_value").GetComponent<TMP_Text>();
        jb_Connection_Location_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_location").GetComponent<TMP_Text>();
        module_Image = contentTransform.Find("Module_group/Real_Module_Image").GetComponent<Image>();
        JB_Connection_Group = contentTransform.Find("JB_Connection_group").gameObject;
        JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        JB_Connection_Wiring_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();
    }

    private void CacheDevices()
    {
        deviceDictionary = GlobalVariable_Search_Devices.devices_Model_By_Grapper.ToDictionary(d => d.code);
        var functionDictionary = GlobalVariable_Search_Devices.devices_Model_By_Grapper.ToDictionary(d => d.function);
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
        loadDataSuccess = false;
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

    private void UpdateDeviceInformation(DeviceModel device)
    {
        prefab_Device.name = $"Scroll_Area_{device.code}";
        code_Value_Text.text = device.code;
        function_Value_Text.text = device.function;
        range_Value_Text.text = device.rangeMeasurement;
        io_Value_Text.text = device.ioAddress;

        var parts = device.jbConnection.Split('_');
        jb_Connection_Value_Text.text = $"{parts[0]}:";
        jb_Connection_Location_Text.text = parts.Length > 1 ? parts[1] : string.Empty;

        GlobalVariable_Search_Devices.jbName = parts[0];
        GlobalVariable_Search_Devices.moduleName = device.ioAddress.Substring(0, device.ioAddress.LastIndexOf('.'));

        if (!string.IsNullOrEmpty(GlobalVariable_Search_Devices.jbName))
        {
            LoadDeviceSprites();
        }
    }

    private async void LoadDeviceSprites()
    {
        ClearWiringGroupAndCache();

        var addressableKeys = new List<string> { "Real_Outdoor_JB_TSD", $"{GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].location}_Connection_Wiring" };
        if (GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].location == "GrapperA")
        {
            for (int i = 1; i <= 6; i++)
            {
                addressableKeys.Add($"GrapperA_Module_Location_Rack{i}");
            }
        }

        var tasks = addressableKeys.Select(key => PreloadSpritesAsync(key)).ToList();
        await Task.WhenAll(tasks);

        var filteredList = spriteCache.Keys
            .Where(key => key.StartsWith($"{GlobalVariable_Search_Devices.jbName}_") && key.Split('_').Length > 1 && int.TryParse(key.Split('_')[1][0].ToString(), out _))
            .OrderBy(key => int.Parse(key.Split('_')[1][0].ToString()))
            .ToList();

        ApplyModuleLocationSprite();
        if (filteredList.Count > 0)
        {
            ApplySpritesToImages(filteredList);
        }

        scrollRect.verticalNormalizedPosition = 1f;

    }

    private async Task PreloadSpritesAsync(string addressableKey)
    {
        var handle = Addressables.LoadAssetsAsync<Sprite>(addressableKey, sprite =>
        {
            spriteCache[sprite.name] = sprite;
        });

        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            // Log error if needed
        }
    }

    private void ApplyModuleLocationSprite()
    {
        if (spriteCache.TryGetValue(GlobalVariable_Search_Devices.moduleName, out var moduleSprite))
        {
            module_Image.sprite = moduleSprite;
        }
    }

    private void ApplySpritesToImages(List<string> filteredList)
    {
        ClearInstantiatedImages();
        JB_Location_Image_Prefab.gameObject.SetActive(true);

        if (GlobalVariable_Search_Devices.jbName.Contains("-"))
        {
            JB_Location_Image_Prefab.gameObject.SetActive(false);
            foreach (string jb in GlobalVariable_Search_Devices.jbName.Split('-'))
            {
                CreateAndSetSprite($"{jb.Trim()}_Location");
            }
        }
        else
        {
            SetSprite(JB_Location_Image_Prefab, $"{GlobalVariable_Search_Devices.jbName}_Location");
        }

        foreach (var spriteName in filteredList)
        {
            if (spriteCache.TryGetValue(spriteName, out var jbConnectionSprite))
            {
                var newImage = Instantiate(JB_Connection_Wiring_Image_Prefab, JB_Connection_Group.transform);
                newImage.sprite = jbConnectionSprite;
                newImage.gameObject.SetActive(true);
                Resize_Gameobject_Function.Set_NativeSize_For_GameObject(newImage);
            }
        }

        JB_Connection_Wiring_Image_Prefab.gameObject.SetActive(false);

        if (loadDataSuccess)
        {
            Show_Dialog.Instance.ShowToast("success", "Tải dữ liệu thành công");
            StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
            loadDataSuccess = false;
        }
    }




    private void SetSprite(Image imageComponent, string jb_name)
    {
        if (!spriteCache.TryGetValue(jb_name, out var jbSprite))
        {
            spriteCache.TryGetValue("JB_TSD_Location_Note", out jbSprite);
        }

        imageComponent.sprite = jbSprite;
        Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
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

    private void OnDisable() => ResetResources();

    private void OnDestroy() => ResetResources();

    private void ResetResources()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        spriteCache.Clear();
        currentLoadedDeviceCode = null;
    }
}

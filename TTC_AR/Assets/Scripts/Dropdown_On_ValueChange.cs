using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class Dropdown_On_ValueChange : MonoBehaviour
{
    public GameObject prefab_Device;
    public TMP_InputField inputField;
    private RectTransform contentTransform;

    [Header("UI Elements")]
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private TMP_Text code_Value_Text;
    [SerializeField]

    private TMP_Text function_Value_Text;
    [SerializeField]

    private TMP_Text range_Value_Text;
    [SerializeField]

    private TMP_Text io_Value_Text;
    [SerializeField]

    private TMP_Text jb_Connection_Value_Text;
    [SerializeField]

    private TMP_Text jb_Connection_Location_Text;
    [SerializeField]

    private Image module_Image;
    [SerializeField]
    private Image JB_Location_Image_Prefab;
    [SerializeField]
    private Image JB_Connection_Wiring_Image_Prefab;
    [SerializeField]
    private GameObject JB_Connection_Group;

    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private int pendingSpriteLoads = 0;
    private List<Image> instantiatedImages = new List<Image>();
    private int add_InstantiatedImages_Count = 0;
    private void Awake()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField không được gán!");
            return;
        }
        CacheUIElements();
    }
    private async Task WaitForDataAndContinueAsync()
    {
        // Vòng lặp kiểm tra nếu dữ liệu đã sẵn sàng
        while (GlobalVariable_Search_Devices.devices_Model_By_Grapper == null || GlobalVariable_Search_Devices.devices_Model_By_Grapper.Count <= 0)
        {
            Debug.Log("Data đang tải...");
            // Chờ đợi một khoảng thời gian ngắn trước khi kiểm tra lại, để tránh khóa thread chính
            await Task.Yield();
        }

        // Khi dữ liệu đã sẵn sàng
        Debug.Log($"Check: {GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code}");

        // Thực hiện các hành động tiếp theo
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        OnInputValueChanged(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code);
    }
    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...", 3);
        Debug.Log($"Chạy Start");
        // inputField.text = GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code;
        //  inputField.onValueChanged.Invoke(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code); // Gọi sự kiện onValueChanged thủ công
        /* if (GlobalVariable_Search_Devices.devices_Model_By_Grapper.Count > 0)
         {
             UpdateDeviceInformation(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0]);
         }*/
    }

    private async void CacheUIElements()
    {
        if (prefab_Device == null)
        {
            Debug.LogError("Prefab_Device không được gán!");
            return;
        }
        if (scrollRect == null) scrollRect = prefab_Device.GetComponent<ScrollRect>();
        if (contentTransform == null) contentTransform = prefab_Device.transform.Find("Content").GetComponent<RectTransform>();
        if (code_Value_Text == null) code_Value_Text = contentTransform.Find("Device_information/Code_group/Code_value").GetComponent<TMP_Text>();
        if (function_Value_Text == null) function_Value_Text = contentTransform.Find("Device_information/Function_group/Function_value").GetComponent<TMP_Text>();
        if (range_Value_Text == null) range_Value_Text = contentTransform.Find("Device_information/Range_group/Range_value").GetComponent<TMP_Text>();
        if (io_Value_Text == null) io_Value_Text = contentTransform.Find("Device_information/IO_group/IO_value").GetComponent<TMP_Text>();
        if (jb_Connection_Value_Text == null) jb_Connection_Value_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_value").GetComponent<TMP_Text>();
        if (jb_Connection_Location_Text == null) jb_Connection_Location_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_location").GetComponent<TMP_Text>();
        if (module_Image == null) module_Image = contentTransform.Find("Module_group/Real_Module_Image").GetComponent<Image>();
        if (JB_Connection_Group == null) JB_Connection_Group = contentTransform.Find("JB_Connection_group").gameObject;
        if (JB_Location_Image_Prefab == null) JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        if (JB_Connection_Wiring_Image_Prefab == null) JB_Connection_Wiring_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();

        // code_Value_Text = contentTransform.Find("Device_information/Code_group/Code_value").GetComponent<TMP_Text>();
        // function_Value_Text = contentTransform.Find("Device_information/Function_group/Function_value").GetComponent<TMP_Text>();
        // range_Value_Text = contentTransform.Find("Device_information/Range_group/Range_value").GetComponent<TMP_Text>();
        // io_Value_Text = contentTransform.Find("Device_information/IO_group/IO_value").GetComponent<TMP_Text>();
        // jb_Connection_Value_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_value").GetComponent<TMP_Text>();
        // jb_Connection_Location_Text = contentTransform.Find("JB_Connection_group/JB_Connection_text_group/JB_Connection_location").GetComponent<TMP_Text>();
        // module_Image = contentTransform.Find("Module_group/Real_Module_Image").GetComponent<Image>();
        // JB_Connection_Group = contentTransform.Find("JB_Connection_group").gameObject;
        // JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        // JB_Connection_Wiring_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();

        await WaitForDataAndContinueAsync();
    }

    private void OnInputValueChanged(string input)
    {
        var device = GlobalVariable_Search_Devices.devices_Model_By_Grapper.FirstOrDefault(d => d.code == input || d.function == input);

        if (device == null)
        {
            ClearWiringGroupAndCache();
        }
        else
        {
            UpdateDeviceInformation(device);
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

    private void LoadDeviceSprites()
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
        pendingSpriteLoads = addressableKeys.Count;
        Debug.Log($"addressableyKey.Count: {addressableKeys.Count}");

        Debug.Log($"Pending sprite loads: {pendingSpriteLoads}");
        if (pendingSpriteLoads > 0)
        {
            foreach (var key in addressableKeys)
            {
            //  Debug.Log($"Test: {addressableKeys.Count}");
                PreloadSprites(key);
            }

        }
        else
        {
            Debug.LogError("No addressable keys to load.");
        }
        scrollRect.verticalNormalizedPosition = 1f;
    }

    private void PreloadSprites(string addressableKey)
    {
        Addressables.LoadAssetsAsync<Sprite>(addressableKey, sprite =>
        {
            spriteCache[sprite.name] = sprite;
        }).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                pendingSpriteLoads--;
                Debug.Log($"Pending sprite loads: {pendingSpriteLoads}");
                if (pendingSpriteLoads == 0)
                {
                    var filteredList = spriteCache.Keys
                        .Where(key => key.StartsWith($"{GlobalVariable_Search_Devices.jbName}_") && key.Split('_').Length > 1 && int.TryParse(key.Split('_')[1][0].ToString(), out _))
                        .OrderBy(key => int.Parse(key.Split('_')[1][0].ToString()))
                        .ToList();

                    ApplyModuleLocationSprite();
                    if (filteredList.Count > 0)
                    {
                        ApplySpritesToImages(filteredList);
                    }
                }
            }
            else
            {
                Debug.LogError($"Failed to load sprites: {handle.OperationException}");
            }
        };
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
        add_InstantiatedImages_Count++;
    }

    private void ClearInstantiatedImages()
    {
        foreach (var img in instantiatedImages)
        {
            if (img != null && add_InstantiatedImages_Count > 0)
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
    }
}

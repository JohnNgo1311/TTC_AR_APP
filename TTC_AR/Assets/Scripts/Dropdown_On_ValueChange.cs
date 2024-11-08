using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
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
    private Image JB_Connection_Image_Prefab;
    [SerializeField]
    private GameObject JB_Connection_Group;
    private readonly Dictionary<string, Sprite> spriteCache = new();
    private readonly List<Image> instantiatedImages_Location = new();
    private readonly List<Image> instantiatedImages_Connection = new();

    private int Instantiated_Images_Location_Count = 0;
    private int Instantiated_Images_Connection_Count = 0;

    private void Awake()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        if (inputField == null)
        {
            Debug.LogError("InputField không được gán!");
            return;
        }
        CacheUIElements();
    }

    private async Task WaitForDataAndContinueAsync()
    {

        // Vô hiệu hóa các thành phần UI
        // SetUIInteraction(false);

        while (GlobalVariable_Search_Devices.devices_Model_By_Grapper == null || GlobalVariable_Search_Devices.devices_Model_By_Grapper.Count <= 0)
        {
            await Task.Yield();
        }

        inputField.onValueChanged.AddListener(OnInputValueChanged);
        OnInputValueChanged(GlobalVariable_Search_Devices.devices_Model_By_Grapper[0].code);
        Debug.Log("Dữ liệu đã tải xong và gán GameObject thành công!");
        // Kích hoạt lại các thành phần UI khi dữ liệu đã tải xong
        //   SetUIInteraction(true);

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
        if (JB_Connection_Group == null)
        {
            JB_Connection_Group = contentTransform.Find("JB_Connection_group").gameObject;

        }
        if (JB_Location_Image_Prefab == null) JB_Location_Image_Prefab = JB_Connection_Group.transform.Find("JB_Location_Image").GetComponent<Image>();
        if (JB_Connection_Image_Prefab == null) JB_Connection_Image_Prefab = JB_Connection_Group.transform.Find("JB_Connection_Wiring").GetComponent<Image>();

        await WaitForDataAndContinueAsync().ConfigureAwait(false);
    }

    private void SetUIInteraction(bool isEnabled = false)
    {
        if (!isEnabled)
        {
            Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
            Debug.Log("Đang tải dữ liệu...");
        }
        else
        {
            Show_Dialog.Instance.ShowToast("success", "Dữ liệu đã tải xong!");
            Debug.Log("Dữ liệu đã tải xong!");
        }

        inputField.interactable = isEnabled;
        scrollRect.enabled = isEnabled;

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.interactable = isEnabled;
        }
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
            GlobalVariable.list_Temp_JB_Location_Image.Clear();
            GlobalVariable.list_Temp_JB_Connection_Image.Clear();
            UpdateDeviceInformation(device);
            Canvas.ForceUpdateCanvases();
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

    private async void UpdateDeviceInformation(DeviceModel device)
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
        GlobalVariable_Search_Devices.moduleName = device.ioAddress[..device.ioAddress.LastIndexOf('.')];

        if (!string.IsNullOrEmpty(GlobalVariable_Search_Devices.jbName))
        {
            await Task.WhenAll(
                ApplySpritesToImages("Location"),
                ApplySpritesToImages("Connection")
            );
        }
    }

    // Removed unused method ApplyModuleLocationSprite

    private async Task ApplySpritesToImages(string action)
    {
        string jb_Name = GlobalVariable_Search_Devices.jbName;
        switch (action)
        {
            case "Location":
                ClearInstantiatedImages(action);
                JB_Location_Image_Prefab.gameObject.SetActive(true);
                if (GlobalVariable.list_Name_And_Image_JB_Location_A.TryGetValue(jb_Name, out var LocationSprite))
                {
                    Debug.Log("LocationSprite.Count: " + LocationSprite.Count);
                    if (LocationSprite.Count > 1)
                    {
                        Debug.Log("LocationSprite.Count > 1: " + LocationSprite.Count);
                        JB_Location_Image_Prefab.gameObject.SetActive(false);
                        for (int i = 1; i <= LocationSprite.Count; i++)
                        {
                            Debug.Log("LocationSprite.Count: " + LocationSprite.Count);
                            CreateAndSetSprite(jb_Name, action);
                        }
                    }
                    else
                    {
                        Debug.Log("LocationSprite.Count <= 1: " + LocationSprite.Count);
                        await Set_Sprite(JB_Location_Image_Prefab, $"{jb_Name}", action);
                    }
                }
                break;
            case "Connection":
                ClearInstantiatedImages(action);
                JB_Connection_Image_Prefab.gameObject.SetActive(true);
                if (GlobalVariable.list_Name_And_Image_JB_Connection_A.TryGetValue(jb_Name, out var connectionSprite))
                {
                    Debug.Log("connectionSprite.Count: " + connectionSprite.Count);
                    if (connectionSprite.Count > 1)
                    {
                        Debug.Log("connectionSprite.Count > 1: " + connectionSprite.Count);
                        JB_Connection_Image_Prefab.gameObject.SetActive(false);
                        for (int i = 1; i <= connectionSprite.Count; i++)
                        {
                            Debug.Log("connectionSprite.Count: " + connectionSprite.Count);
                            CreateAndSetSprite(jb_Name, action);
                        }
                    }
                    else
                    {
                        Debug.Log("connectionSprite.Count <= 1: " + connectionSprite.Count);
                        await Set_Sprite(JB_Connection_Image_Prefab, $"{jb_Name}", action);
                    }
                }
                break;
        }
    }

    private async Task Set_Sprite(Image imageComponent, string jb_name, string action)
    {
        switch (action)
        {
            case "Location":
                if (!GlobalVariable.list_Name_And_Image_JB_Location_A.TryGetValue(jb_name, out var locationSprite))
                {
                    GlobalVariable.list_Name_And_Image_JB_Location_A.TryGetValue("JB_TSD_Location_Note", out locationSprite);
                }
                imageComponent.sprite = locationSprite[Instantiated_Images_Location_Count > 1 ? instantiatedImages_Location.Count - 1 : 0];
                Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
                GlobalVariable.list_Temp_JB_Location_Image.Add(imageComponent.sprite);
                await Task.Yield();
                break;
            case "Connection":
                if (!GlobalVariable.list_Name_And_Image_JB_Connection_A.TryGetValue(jb_name, out var connectionSprite))
                {
                    GlobalVariable.list_Name_And_Image_JB_Connection_A.TryGetValue("JB_TSD_Connection_Note", out connectionSprite);
                }
                imageComponent.sprite = connectionSprite[Instantiated_Images_Connection_Count > 1 ? instantiatedImages_Connection.Count - 1 : 0];
                Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
                GlobalVariable.list_Temp_JB_Connection_Image.Add(imageComponent.sprite);
                await Task.Yield();
                break;
        }
    }

    private async void CreateAndSetSprite(string jb_name, string action)
    {
        switch (action)
        {
            case "Location":
                Image jbLocationImage = Instantiate(JB_Location_Image_Prefab, JB_Connection_Group.transform);
                jbLocationImage.transform.SetSiblingIndex(JB_Location_Image_Prefab.transform.GetSiblingIndex() + 1);
                jbLocationImage.gameObject.SetActive(true);
                instantiatedImages_Location.Add(jbLocationImage);
                Instantiated_Images_Location_Count++;
                await Set_Sprite(jbLocationImage, jb_name, action);

                break;
            case "Connection":
                Image jbConnectionImage = Instantiate(JB_Connection_Image_Prefab, JB_Connection_Group.transform);
                jbConnectionImage.transform.SetSiblingIndex(JB_Connection_Image_Prefab.transform.GetSiblingIndex() + 1);
                jbConnectionImage.gameObject.SetActive(true);
                instantiatedImages_Connection.Add(jbConnectionImage);
                Instantiated_Images_Connection_Count++;
                await Set_Sprite(jbConnectionImage, jb_name, action);
                break;
        }

    }

    private void ClearInstantiatedImages(string action)
    {
        switch (action)
        {
            case "Location":
                foreach (var img in instantiatedImages_Location)
                {
                    if (img != null && Instantiated_Images_Location_Count > 0)
                    {
                        Destroy(img.gameObject);
                    }
                }
                Instantiated_Images_Location_Count = 0;
                instantiatedImages_Location.Clear();
                break;
            case "Connection":
                foreach (var img in instantiatedImages_Connection)
                {
                    if (img != null && Instantiated_Images_Connection_Count > 0)
                    {
                        Destroy(img.gameObject);
                    }
                }
                Instantiated_Images_Connection_Count = 0;
                instantiatedImages_Connection.Clear();
                break;
        }
    }

    private void OnDisable() => ResetResources();

    private void OnDestroy() => ResetResources();

    private void ResetResources()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        spriteCache.Clear();
    }
}

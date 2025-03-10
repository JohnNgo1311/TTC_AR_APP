using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Update_Device_Setting : MonoBehaviour
{
    // Interfaces and Models
    [Header("General")]
    // private IDeviceUseCase _deviceUseCase;
    [SerializeField] private DeviceGeneralModel _UpdateDeviceGeneralModel;
    public Initialize_Device_List_Option_Selection initialize_Device_List_Option_Selection;
    public DeviceInformationModel deviceInformationModel;

    // UI Components
    [Header("UI Elements")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform viewPortTransform;
    [SerializeField] private GameObject contentVerticalGroup;
    [SerializeField] private GameObject moduleObject;
    [SerializeField] private GameObject jbObject;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button addModuleIOButton;
    [SerializeField] private Button addJBButton;
    [SerializeField] private Button addAdditionalImageButton;


    // Input Fields
    [Header("Input Fields")]
    [SerializeField] private TMP_Text deviceCode_TextValue;
    [SerializeField] private TMP_InputField deviceFunction_TextField;
    [SerializeField] private TMP_InputField deviceRange_TextField;
    [SerializeField] private TMP_InputField deviceUnit_TextField;
    [SerializeField] private TMP_InputField deviceIOAddress_TextField;

    // Layout and Prefabs
    [Header("Layout and Prefabs")]
    [SerializeField] private GameObject additionalImageLayoutGroup;
    [SerializeField] private GameObject additionalImageItemPrefab;

    // Data Collections
    private readonly Dictionary<string, List<GameObject>> _selectedGameObjects = new()
    {
        ["AdditionalImage"] = new List<GameObject>()
    };

    private readonly Dictionary<string, int> _selectedCounts = new()
    {
        ["AdditionalImage"] = 0
    };

    // Constants for field names
    private const string MODULE_IO = "ModuleIO";
    private const string JB = "JB";
    private const string ADDITIONAL_IMAGE = "AdditionalImage";

    #region Initialization
    private void Awake()
    {
        // IDeviceRepository repository = new DeviceRepository();
        // _deviceUseCase = new DeviceUseCase(repository);
    }

    private void Start()
    {
        scrollRect.normalizedPosition = Vector2.up;
    }
    #endregion

    #region Enable/Disable

    private void OnEnable()
    {
        SetupButtonListeners();
        SetupSelectionOptionListeners();

        if (deviceInformationModel == null)
        {
            Debug.LogError("device Information Model is null");
        }
        else
        {
            deviceCode_TextValue.text = deviceInformationModel.Code;
            deviceFunction_TextField.text = deviceInformationModel.Function;
            deviceRange_TextField.text = deviceInformationModel.Range;
            deviceUnit_TextField.text = deviceInformationModel.Unit;
            deviceIOAddress_TextField.text = deviceInformationModel.IOAddress;

            if (deviceInformationModel.ModuleBasicModel != null)
            {
                moduleObject.SetActive(true);
                addModuleIOButton.gameObject.SetActive(false);
                moduleObject.GetComponentInChildren<TMP_Text>().text = deviceInformationModel.ModuleBasicModel.Name;
                AddButtonListener(GetButton(moduleObject), () => DeselectItem(moduleObject, MODULE_IO));
            }
            if (deviceInformationModel.JBInformationModel != null)
            {
                jbObject.SetActive(true);
                addJBButton.gameObject.SetActive(false);
                jbObject.GetComponentInChildren<TMP_Text>().text = deviceInformationModel.JBInformationModel.Name;
                AddButtonListener(GetButton(jbObject), () => DeselectItem(jbObject, JB));
            }
            if (deviceInformationModel.AdditionalConnectionImages != null && deviceInformationModel.AdditionalConnectionImages.Count > 0)
            {
                foreach (var image in deviceInformationModel.AdditionalConnectionImages)
                {
                    var item = Instantiate(additionalImageItemPrefab, additionalImageLayoutGroup.transform);
                    item.GetComponentInChildren<TMP_Text>().text = image.Name;
                    _selectedGameObjects[ADDITIONAL_IMAGE].Add(item);
                    _selectedCounts[ADDITIONAL_IMAGE]++;
                    var removeButton = GetButton(item);
                    AddButtonListener(removeButton, () => RemoveAdditionalImageItem(item));
                }
            }
        }

    }

    private void OnDisable()
    {
        ClearButtonListeners();
    }

    private void ClearButtonListeners()
    {
        ClearButtonListener(submitButton);

        ClearButtonListener(addModuleIOButton);
        ClearButtonListener(addAdditionalImageButton);
        ClearButtonListener(addJBButton);

        ClearButtonListener(GetButton(moduleObject));
        ClearButtonListener(GetButton(jbObject));

        ClearTransformButtons(initialize_Device_List_Option_Selection.ModuleIO_List_Selection_Option_Content_Transform);
        ClearTransformButtons(initialize_Device_List_Option_Selection.JB_List_Selection_Option_Content_Transform);
        ClearTransformButtons(initialize_Device_List_Option_Selection.Additional_Image_List_Selection_Option_Content_Transform);
    }

    private void ClearButtonListener(Button button)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void ClearTransformButtons(Transform parent)
    {
        if (parent == null) return;
        foreach (Transform child in parent)
        {
            ClearButtonListener(child.GetComponent<Button>());
        }
    }
    #endregion

    #region Button Setup
    private void SetupSelectionOptionListeners()
    {
        if (initialize_Device_List_Option_Selection == null)
        {
            Debug.LogError("initialize_Device_List_Option_Selection is not assigned in the Inspector!");
            return;
        }

        SetupOptionListeners(initialize_Device_List_Option_Selection.ModuleIO_List_Selection_Option_Content_Transform, moduleObject, MODULE_IO);
        SetupOptionListeners(initialize_Device_List_Option_Selection.JB_List_Selection_Option_Content_Transform, jbObject, JB);
        SetupAdditionalImageListeners();
    }

    private void SetupOptionListeners(Transform parent, GameObject itemObject, string field)
    {
        if (parent == null)
        {
            Debug.LogError($"Parent Transform for {field} is null! Check InitializeDeviceListOptionSelection assignments.");
            return;
        }
        if (itemObject == null)
        {
            Debug.LogError($"ItemObject for {field} is null!");
            return;
        }

        foreach (Transform child in parent)
        {
            var button = child.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogWarning($"Button component missing on child of {parent.name} for {field}");
                continue;
            }
            var text = child.GetComponentInChildren<TMP_Text>().text;
            AddButtonListener(button, () => SelectItem(text, itemObject));
        }
    }
    private void SetupButtonListeners()
    {
        AddButtonListener(submitButton, OnSubmitUpdateDevice);
        AddButtonListener(addModuleIOButton, () => OpenListSelection(MODULE_IO));
        AddButtonListener(addJBButton, () => OpenListSelection(JB));
        AddButtonListener(addAdditionalImageButton, () => OpenListSelection(ADDITIONAL_IMAGE));
        AddButtonListener(GetButton(moduleObject), () => DeselectItem(moduleObject, MODULE_IO));
        AddButtonListener(GetButton(jbObject), () => DeselectItem(jbObject, JB));
    }
    private void SetupAdditionalImageListeners()
    {
        if (initialize_Device_List_Option_Selection?.Additional_Image_List_Selection_Option_Content_Transform == null)
        {
            Debug.LogError("AdditionalImageListContent is null! Check InitializeDeviceListOptionSelection assignments.");
            return;
        }

        foreach (Transform option in initialize_Device_List_Option_Selection.Additional_Image_List_Selection_Option_Content_Transform)
        {
            var button = option.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogWarning($"Button component missing on child of AdditionalImageListContent");
                continue;
            }
            var text = option.GetComponentInChildren<TMP_Text>().text;
            AddButtonListener(button, () => AddAdditionalImageItem(text));
        }
    }
    private void AddButtonListener(Button button, Action action)
    {
        if (button == null) return;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action?.Invoke());
    }

    private Button GetButton(GameObject obj) => obj.GetComponentInChildren<Button>();
    #endregion

    #region Selection Handling
    private void SelectItem(string text, GameObject itemObject)
    {
        itemObject.GetComponentInChildren<TMP_Text>().text = text;
        itemObject.SetActive(true);
        CloseListSelection();
    }

    private void AddAdditionalImageItem(string text)
    {
        var item = Instantiate(additionalImageItemPrefab, additionalImageLayoutGroup.transform);
        item.GetComponentInChildren<TMP_Text>().text = text;
        _selectedGameObjects[ADDITIONAL_IMAGE].Add(item);
        _selectedCounts[ADDITIONAL_IMAGE]++;
        var removeButton = GetButton(item);
        AddButtonListener(removeButton, () => RemoveAdditionalImageItem(item));
        CloseListSelection();
    }

    private void RemoveAdditionalImageItem(GameObject item)
    {
        _selectedGameObjects[ADDITIONAL_IMAGE].Remove(item);
        _selectedCounts[ADDITIONAL_IMAGE]--;
        Destroy(item);
    }

    private void DeselectItem(GameObject itemObject, string field)
    {
        itemObject.SetActive(false);
        GetAddButton(field).SetActive(true);
    }
    #endregion

    #region List Selection
    private void OpenListSelection(string field)
    {
        var config = GetSelectionConfig(field);
        initialize_Device_List_Option_Selection.Selection_Option_Canvas.SetActive(true);
        config.Panel.SetActive(true);
        config.Content.SetActive(true);
        config.ItemObject?.SetActive(true);
        config.AddButton?.SetActive(false);
        DisableOtherPanels(field);
    }

    private void CloseListSelection()
    {
        initialize_Device_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
        initialize_Device_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
        initialize_Device_List_Option_Selection.selection_List_JB_Panel.SetActive(false);
        initialize_Device_List_Option_Selection.selection_List_Additional_Image_Panel.SetActive(false);
    }
    #endregion

    #region Helpers
    private GameObject GetAddButton(string field) => field switch
    {
        MODULE_IO => addModuleIOButton.gameObject,
        JB => addJBButton.gameObject,
        _ => null
    };

    private (GameObject Panel, GameObject Content, GameObject ItemObject, GameObject AddButton) GetSelectionConfig(string field) => field switch
    {
        MODULE_IO => (initialize_Device_List_Option_Selection.selection_List_ModuleIO_Panel, initialize_Device_List_Option_Selection.ModuleIO_List_Selection_Option_Content_Transform.gameObject, moduleObject, addModuleIOButton.gameObject),
        JB => (initialize_Device_List_Option_Selection.selection_List_JB_Panel, initialize_Device_List_Option_Selection.JB_List_Selection_Option_Content_Transform.gameObject, jbObject, addJBButton.gameObject),
        ADDITIONAL_IMAGE => (initialize_Device_List_Option_Selection.selection_List_Additional_Image_Panel, initialize_Device_List_Option_Selection.Additional_Image_List_Selection_Option_Content_Transform.gameObject, null, null),
        _ => throw new ArgumentException($"Invalid field: {field}")
    };

    private void DisableOtherPanels(string activeField)
    {
        foreach (var field in new[] { MODULE_IO, JB, ADDITIONAL_IMAGE })
        {
            if (field != activeField)
                GetSelectionConfig(field).Panel.SetActive(false);
        }
    }
    #endregion

    #region Input Handling
    private void Update()
    {
        if (ShouldCloseSelection())
        {
            CloseListSelection();

        }
    }

    private bool ShouldCloseSelection() =>
        Input.GetKeyDown(KeyCode.Escape) ||
        (Gamepad.current?.buttonEast.wasPressedThisFrame ?? false) ||
        (Keyboard.current?.escapeKey.wasPressedThisFrame ?? false);
    #endregion


    #region Submit Add New Device

    private async void OnSubmitUpdateDevice()
    {
        // _UpdateDeviceGeneralModel = new DeviceGeneralModel(
        //     deviceInformationModel.Id,
        //     deviceCode_TextValue.text,
        //     deviceFunction_TextField.text,
        //     deviceRange_TextField.text,
        //     deviceUnit_TextField.text,
        //     deviceIOAddress_TextField.text,
        //     null,
        //     null,
        //     new List<ImageBasicModel>()
        // );

        // var moduleName = moduleObject.GetComponentInChildren<TMP_Text>().text;
        // var jbName = jbObject.GetComponentInChildren<TMP_Text>().text;

        // _UpdateDeviceGeneralModel.ModuleBasicModel = GlobalVariable.temp_Dictionary_ModuleBasicModel.TryGetValue(moduleName, out var moduleBasicModel) ? moduleBasicModel : null;
        // _UpdateDeviceGeneralModel.JBBasicModel = GlobalVariable.temp_Dictionary_JBBasicModel.TryGetValue(jbName, out var jbBasicModel) ? jbBasicModel : null;

        // _UpdateDeviceGeneralModel.AdditionalImageModels.Clear();
        // foreach (var item in _selectedGameObjects[ADDITIONAL_IMAGE])
        // {
        //     var text = item.GetComponentInChildren<TMP_Text>().text;
        //     if (GlobalVariable.temp_Dictionary_ImageBasicModel.TryGetValue(text, out var imageBasicModel))
        //     {
        //         _UpdateDeviceGeneralModel.AdditionalImageModels.Add(imageBasicModel);
        //     }
        // }
        // try
        // {
        //     Debug.Log("Try to Add Device");
        //     bool success = await _deviceUseCase.UpdateDeviceModel(_UpdateDeviceGeneralModel);
        //     Debug.Log(success ? "Add Device Success" : "Add Device Fail");
        // }
        // catch (Exception ex)
        // {
        //     Debug.LogError($"Error: {ex.Message}");
        // }
    }
    #endregion
}


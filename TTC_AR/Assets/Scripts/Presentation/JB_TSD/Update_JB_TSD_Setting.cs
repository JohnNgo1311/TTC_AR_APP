using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Update_JB_TSD_Setting : MonoBehaviour
{
    public Initialize_JB_List_Option_Selection initialize_JB_List_Option_Selection;


    [SerializeField] private Button submitButton;
    private IJBUseCase _jbGeneralUseCase;

    [SerializeField]
    private JBGeneralModel UpdatedJBGeneralModel;

    [Header("Basic")]
    public JBInformationModel jBInformationModel;
    public GameObject parent_Content_Vertical_Group;
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public List<LayoutElement> layoutElements = new List<LayoutElement>();
    public TMP_Text JBName_TextValue;
    public TMP_InputField JBLocation_Textfield;

    [Header("LayOutGroup")]
    public GameObject Device_Parent_GridLayout_Group;
    public GameObject ModuleIO_Parent_GridLayout_Group;
    public GameObject Location_Image_Parent_VerticalLayout_Group;
    public GameObject Connection_Image_Parent_VerticalLayout_Group;

    [Header("Prefab")]
    public GameObject Device_Item_Prefab;
    public GameObject ModuleIO_Item_Prefab;
    public GameObject Location_Image_Item_Prefab;
    public GameObject Connection_Image_Item_Prefab;
    private Transform temp_Item_Transform;
    private Dictionary<string, DeviceBasicModel> temp_Dictionary_DeviceBasicModel = new Dictionary<string, DeviceBasicModel>();
    private Dictionary<string, ModuleBasicModel> temp_Dictionary_ModuleBasicModel = new Dictionary<string, ModuleBasicModel>();
    private Dictionary<string, ImageBasicModel> temp_Dictionary_ImageLocationBasicModel = new Dictionary<string, ImageBasicModel>();
    private Dictionary<string, ImageBasicModel> temp_Dictionary_ImageConnectionBasicModel = new Dictionary<string, ImageBasicModel>();

    private Dictionary<string, List<GameObject>> selectedGameObjects = new Dictionary<string, List<GameObject>>()

    {
        { "Device", new List<GameObject>() },
        { "ModuleIO", new List<GameObject>() },
        { "Location_Image", new List<GameObject>() },
        { "Connection_Image", new List<GameObject>() }
    };

    private Dictionary<string, int> selectedCounts = new Dictionary<string, int>()
    {
        { "Device", 0 },
        { "ModuleIO", 0 },
        { "Location_Image", 0 },
        { "Connection_Image", 0 }
    };

    private void Awake()
    {   // Khởi tạo dependency injection đơn giản
        IJBRepository repository = new JBRepository();
        _jbGeneralUseCase = new JBUseCase(repository);
    }
    private void Start()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        AdjustLayoutElements();
    }

    private async void OnSubmitUpdateJB()
    {
        if (!string.IsNullOrEmpty(JBLocation_Textfield.text))
            UpdatedJBGeneralModel.Location = JBLocation_Textfield.text;

        UpdatedJBGeneralModel = new JBGeneralModel(
           jBInformationModel.Id,
           JBName_TextValue.text,
           JBLocation_Textfield.text,
           temp_Dictionary_DeviceBasicModel.Values.ToList(),
           temp_Dictionary_ModuleBasicModel.Values.ToList(),
           temp_Dictionary_ImageLocationBasicModel.Values.FirstOrDefault(),
           temp_Dictionary_ImageConnectionBasicModel.Values.ToList()
           );

        try
        {
            bool success = await _jbGeneralUseCase.UpdateJBModel(UpdatedJBGeneralModel);
            Debug.Log(success ? "Data posted successfully" : "Failed to post data");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
        }
    }

    private void AdjustLayoutElements()
    {
        float viewPortHeight = viewPortTransform.rect.height;
        layoutElements[0].minHeight = viewPortHeight * 0.12f;
        layoutElements[1].minHeight = viewPortHeight * 0.12f;
        layoutElements[2].minHeight = viewPortHeight * 0.6f;
    }

    private void OnEnable()
    {
        submitButton.onClick.AddListener(OnSubmitUpdateJB);

        if (jBInformationModel == null)
        {
            Debug.LogError("JB Information Model is null");
        }
        else
        {
            JBName_TextValue.text = jBInformationModel.Name;

            JBLocation_Textfield.text = jBInformationModel.Location;

            if (jBInformationModel.ListDeviceInformation != null && jBInformationModel.ListDeviceInformation.Count > 0)
                PopulateItems(jBInformationModel.ListDeviceInformation, Device_Item_Prefab, Device_Parent_GridLayout_Group, "Device");

            if (jBInformationModel.ListModuleInformation != null && jBInformationModel.ListModuleInformation.Count > 0)
                PopulateItems(jBInformationModel.ListModuleInformation, ModuleIO_Item_Prefab, ModuleIO_Parent_GridLayout_Group, "ModuleIO");

            if (jBInformationModel.OutdoorImage != null)
                AddSingleItem(jBInformationModel.OutdoorImage.url, Location_Image_Item_Prefab, Location_Image_Parent_VerticalLayout_Group, "Location_Image");

            if (jBInformationModel.ListConnectionImages != null && jBInformationModel.ListConnectionImages.Count > 0)
                PopulateItems(jBInformationModel.ListConnectionImages, Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group, "Connection_Image");


        }
        AddButtonListeners(initialize_JB_List_Option_Selection.Device_List_Selection_Option_Content_Transform, "Device");
        AddButtonListeners(initialize_JB_List_Option_Selection.ModuleIO_List_Selection_Option_Content_Transform, "ModuleIO");
        AddButtonListeners(initialize_JB_List_Option_Selection.Location_Image_List_Selection_Option_Content_Transform, "Location_Image");
        AddButtonListeners(initialize_JB_List_Option_Selection.Connection_Image_List_Selection_Option_Content_Transform, "Connection_Image");
    }

    private void AddButtonListeners(Transform parentTransform, string field)
    {
        foreach (Transform optionChild in parentTransform)
        {
            AddButtonListener(optionChild.gameObject, () => SelectItem(optionChild.gameObject.GetComponentInChildren<TMP_Text>().text, field));
        }
    }

    private void OnDisable()
    {
        submitButton.onClick.RemoveAllListeners();
        JBName_TextValue.text = string.Empty;
        JBLocation_Textfield.text = string.Empty;
        ClearItems("Device");
        ClearItems("ModuleIO");
        ClearItems("Location_Image");
        ClearItems("Connection_Image");
    }
    private void ClearItems(string field)
    {
        foreach (var item in selectedGameObjects[field])
        {
            Destroy(item);
        }
        selectedGameObjects[field].Clear();
        selectedCounts[field] = 0;
    }
    private void PopulateItems<T>(List<T> listItems, GameObject itemPrefab, GameObject parentLayoutGroup, string field)
    {
        var parentTransform = parentLayoutGroup.transform;
        foreach (var item in listItems)
        {
            GameObject newItem = Instantiate(itemPrefab, parentTransform);
            SetItemTextValue(newItem.transform, item.ToString(), field);
            AddButtonListener(newItem, () => DeselectItem(newItem, field));
            selectedGameObjects[field].Add(newItem);
        }
    }

    private void AddSingleItem(string text, GameObject prefab, GameObject parentGroup, string field)
    {
        GameObject newItem = Instantiate(prefab, parentGroup.transform);
        SetItemTextValue(newItem.transform, text, field);
        AddButtonListener(newItem, () => DeselectItem(newItem, field));
        selectedGameObjects[field].Add(newItem);
    }


    private void AddButtonListener(GameObject option, UnityEngine.Events.UnityAction action)
    {
        Button button = option.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        if (!initialize_JB_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(true);
        GameObject newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(newItem, field));
        GetSelectionPanel(field).SetActive(true);
        //parentGroup.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenListDeviceSelection() => OpenListSelection("Device", Device_Item_Prefab, Device_Parent_GridLayout_Group);
    public void OpenListModuleSelection() => OpenListSelection("ModuleIO", ModuleIO_Item_Prefab, ModuleIO_Parent_GridLayout_Group);
    public void OpenListImageLocationSelection() => OpenListSelection("Location_Image", Location_Image_Item_Prefab, Location_Image_Parent_VerticalLayout_Group);
    public void OpenListImageConnectionSelection() => OpenListSelection("Connection_Image", Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group);

    private void SetItemTextValue(Transform temp_Item_Transform, string textValue, string field)
    {
        TMP_Text textComponent = temp_Item_Transform.GetComponentInChildren<TMP_Text>();

        if (textComponent != null)
        {
            textComponent.text = textValue;
            switch (field)
            {
                case "Device":
                    if (GlobalVariable.temp_Dictionary_DeviceInformationModel.TryGetValue(textValue, out DeviceInformationModel deviceInformationModel))
                    {
                        var deviceBasicModel = new DeviceBasicModel(deviceInformationModel.Id, deviceInformationModel.Code);
                        temp_Dictionary_DeviceBasicModel[textValue] = deviceBasicModel;
                    }
                    break;
                case "ModuleIO":
                    if (GlobalVariable.temp_Dictionary_ModuleInformationModel.TryGetValue(textValue, out ModuleInformationModel moduleInformationModel))
                    {
                        var moduleBasicModel = new ModuleBasicModel(moduleInformationModel.Id, moduleInformationModel.Name);
                        temp_Dictionary_ModuleBasicModel[textValue] = moduleBasicModel;
                    }
                    break;
                case "Location_Image":
                case "Connection_Image":
                    if (GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out ImageInformationModel imageInformationModel))
                    {
                        var imageBasicModel = new ImageBasicModel(imageInformationModel.Id, imageInformationModel.Name);
                        if (field == "Location_Image")
                        {
                            temp_Dictionary_ImageLocationBasicModel[textValue] = imageBasicModel;
                        }
                        else
                        {
                            temp_Dictionary_ImageConnectionBasicModel[textValue] = imageBasicModel;
                        }
                    }
                    break;
            }
        }
        Debug.Log("Text Value: " + textValue);
    }

    private void SelectItem(string textValue, string field)
    {
        SetItemTextValue(temp_Item_Transform, textValue, field);
        CloseListSelection();
        selectedCounts[field]++;
        if (!selectedGameObjects[field].Contains(temp_Item_Transform.gameObject))
        {
            selectedGameObjects[field].Add(temp_Item_Transform.gameObject);
        }
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--;
        selectedGameObjects[field].Remove(item);
        Destroy(item);
        CloseListSelection();
    }

    public void CloseListSelection()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        // Canvas.ForceUpdateCanvases();
        if (initialize_JB_List_Option_Selection.selection_List_Device_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(false);

    }
    public void CloseListSelectionFromBackButton()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        // Canvas.ForceUpdateCanvases();
        ClearInactiveChildren(Device_Parent_GridLayout_Group);
        ClearInactiveChildren(ModuleIO_Parent_GridLayout_Group);
        ClearInactiveChildren(Location_Image_Parent_VerticalLayout_Group);
        ClearInactiveChildren(Connection_Image_Parent_VerticalLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_Device_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_JB_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(false);

    }
    private GameObject GetSelectionPanel(string field)
    {
        return field switch
        {
            "Device" => initialize_JB_List_Option_Selection.selection_List_Device_Panel,
            "ModuleIO" => initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel,
            "Location_Image" => initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel,
            "Connection_Image" => initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field name")
        };
    }

    private void ClearInactiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            if (!child.gameObject.activeSelf && parentGroup.transform.childCount > 1)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (initialize_JB_List_Option_Selection.selection_List_Device_Panel.activeSelf || initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf || initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf || initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                CloseListSelectionFromBackButton();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateJBSettingView : MonoBehaviour, IJBView
{
    public Initialize_JB_List_Option_Selection initialize_JB_List_Option_Selection;
    private JBPresenter _presenter;

    [SerializeField] private JBInformationModel JBInformationModel;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField Name_TextField;
    [SerializeField] private TMP_InputField Location_TextField;

    [Header("Basic")]
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public GameObject parent_Content_Vertical_Group;
    private Transform temp_Item_Transform;

    [Header("LayOutGroup")]
    public GameObject List_Devices_Parent_GridLayout_Group;
    public GameObject List_Modules_Parent_GridLayout_Group;
    public GameObject List_Location_Image_Parent_Vertical_Group;
    public GameObject List_Connection_Image_Parent_Vertical_Group;

    [Header("Prefab")]
    public GameObject Device_Item_Prefab;
    public GameObject Module_Item_Prefab;
    public GameObject OutDoorImage_Item_Prefab;
    public GameObject ConnectionImage_Item_Prefab;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button backButtonDeviceListSelection;
    [SerializeField] private Button backButtonModuleListSelection;
    [SerializeField] private Button backButtonOutDoorImageListSelection;
    [SerializeField] private Button backButtonConnectionImageListSelection;
    [SerializeField] private GameObject addLocationImageItem;


    [Header("Toggle")]
    [SerializeField] private Toggle bis_Toggle;

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_JB_Canvas;
    public GameObject Add_New_JB_Canvas;
    public GameObject Update_JB_Canvas;

    private readonly Dictionary<string, DeviceInformationModel> temp_Dictionary_DeviceModel = new();
    private readonly Dictionary<string, ModuleInformationModel> temp_Dictionary_ModuleModel = new();
    private readonly Dictionary<string, ImageInformationModel> temp_Dictionary_OutDoorModel = new();
    private readonly Dictionary<string, ImageInformationModel> temp_Dictionary_ConnectionModel = new();

    private readonly Dictionary<string, List<GameObject>> selectedGameObjects = new()
    {
        { "Devices", new List<GameObject>() },
        { "Modules", new List<GameObject>() },
        { "Location_Image", new List<GameObject>() },
        { "Connection_Images", new List<GameObject>() }
    };

    private readonly Dictionary<string, int> selectedCounts = new()
    {
        { "Devices", 0 },
        { "Modules", 0 },
        { "Location_Image", 0 },
        { "Connection_Images", 0 }
    };

    // void Awake()
    // {
    //     var JBManager = FindObjectOfType<JBManager>();
    //     _presenter = new JBPresenter(this, JBManager._IJBService);
    // }

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new JBPresenter(this, ManagerLocator.Instance.JBManager._IJBService);
        // DeviceManager._IDeviceService
    }
    void OnEnable()
    {
        ResetAllInputFields();

        AddButtonListeners(initialize_JB_List_Option_Selection.Device_List_Selection_Option_Content_Transform, "Devices");
        AddButtonListeners(initialize_JB_List_Option_Selection.Module_List_Selection_Option_Content_Transform, "Modules");
        AddButtonListeners(initialize_JB_List_Option_Selection.Location_Image_List_Selection_Option_Content_Transform, "Location_Image");
        AddButtonListeners(initialize_JB_List_Option_Selection.Connection_Image_List_Selection_Option_Content_Transform, "Connection_Images");

        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();

        backButtonDeviceListSelection.onClick.RemoveAllListeners();
        backButtonModuleListSelection.onClick.RemoveAllListeners();
        backButtonOutDoorImageListSelection.onClick.RemoveAllListeners();
        backButtonConnectionImageListSelection.onClick.RemoveAllListeners();

        backButtonDeviceListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Devices"));
        backButtonModuleListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Modules"));
        backButtonOutDoorImageListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Location_Image"));
        backButtonConnectionImageListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Connection_Images"));

        backButton.onClick.AddListener(CloseAddCanvas);
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        scrollRect.verticalNormalizedPosition = 1;
    }

    void OnDisable()
    {
        RenewView();
    }

    private void OnSubmitButtonClick()
    {
        JBInformationModel = new JBInformationModel(
            name: bis_Toggle.isOn ? $"{Name_TextField.text.ToUpper()}_Bis" : Name_TextField.text.ToUpper(),
            location: Location_TextField.text,
            listDeviceInformation: temp_Dictionary_DeviceModel.Values.ToList(),
            listModuleInformation: temp_Dictionary_ModuleModel.Values.ToList(),
            outdoorImage: temp_Dictionary_OutDoorModel.Values.FirstOrDefault(),
            listConnectionImages: temp_Dictionary_ConnectionModel.Values.ToList()
        );

        if (string.IsNullOrEmpty(Name_TextField.text))
        {
            OpenErrorDialog("Vui lòng nhập mã tủ JB");
            return;
        }
        if (GlobalVariable.temp_Dictionary_JBInformationModel.ContainsKey(JBInformationModel.Name))
        {
            OpenErrorDialog("Mã tủ JB đã tồn tại", "Vui lòng nhập mã tủ JB khác");
            return;
        }
        else
        {
            _presenter.CreateNewJB(GlobalVariable.GrapperId, JBInformationModel);
        }
    }

    private void RenewView()
    {
        addLocationImageItem.SetActive(true);


        ClearActiveChildren(List_Devices_Parent_GridLayout_Group);
        ClearActiveChildren(List_Modules_Parent_GridLayout_Group);
        ClearActiveChildren(List_Location_Image_Parent_Vertical_Group);
        ClearActiveChildren(List_Connection_Image_Parent_Vertical_Group);

        ResetAllInputFields();

        temp_Dictionary_DeviceModel.Clear();
        selectedGameObjects["Devices"].Clear();
        selectedCounts["Devices"] = 0;

        temp_Dictionary_ModuleModel.Clear();
        selectedGameObjects["Modules"].Clear();
        selectedCounts["Modules"] = 0;

        temp_Dictionary_OutDoorModel.Clear();
        selectedGameObjects["Location_Image"].Clear();
        selectedCounts["Location_Image"] = 0;

        temp_Dictionary_ConnectionModel.Clear();
        selectedGameObjects["Connection_Images"].Clear();
        selectedCounts["Connection_Images"] = 0;
    }

    public void CloseAddCanvas()
    {
        Add_New_JB_Canvas.SetActive(false);
        Update_JB_Canvas.SetActive(false);
        List_JB_Canvas.SetActive(true);
    }

    private void ResetAllInputFields()
    {
        Name_TextField.text = "";
        Location_TextField.text = "";
        bis_Toggle.isOn = false;
    }

    private void AddButtonListeners(Transform contentTransform, string field)
    {
        foreach (Transform option in contentTransform)
        {
            AddButtonListener(option, () => SelectOption(option.gameObject.GetComponentInChildren<TMP_Text>().text, field));
        }
    }

    private void AddButtonListener(Transform option, UnityEngine.Events.UnityAction action)
    {
        var button = option.gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }

    private void SelectOption(string optionTextValue, string field)
    {
        SetItemTextValue(temp_Item_Transform, optionTextValue, field);
        CloseListSelection(field);

        selectedCounts[field]++;
        if (!selectedGameObjects[field].Contains(temp_Item_Transform.gameObject))
        {
            selectedGameObjects[field].Add(temp_Item_Transform.gameObject);
        }
        else
        {
            Destroy(temp_Item_Transform.gameObject);
        }
    }

    private void SetItemTextValue(Transform temp_Item_Transform, string textValue, string field)
    {
        var itemText = temp_Item_Transform.GetComponentInChildren<TMP_Text>();

        if (itemText == null) return;

        itemText.text = textValue;

        switch (field)
        {
            case "Devices":
                if (GlobalVariable.temp_Dictionary_DeviceInformationModel.TryGetValue(textValue, out var deviceInfo))
                {
                    if (!temp_Dictionary_DeviceModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_DeviceModel[textValue] = new DeviceInformationModel(deviceInfo.Id, deviceInfo.Code);
                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;

            case "Modules":
                if (GlobalVariable.temp_Dictionary_ModuleInformationModel.TryGetValue(textValue, out var moduleInfo))
                {
                    if (!temp_Dictionary_ModuleModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_ModuleModel[textValue] = new ModuleInformationModel(moduleInfo.Id, moduleInfo.Name);
                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;

            case "Location_Image":
                if (GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out var locationImageInfo))
                {
                    if (!temp_Dictionary_OutDoorModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_OutDoorModel[textValue] = new ImageInformationModel(locationImageInfo.Id, locationImageInfo.Name);
                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;

            case "Connection_Images":
                if (GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out var connectionImageInfo))
                {
                    if (!temp_Dictionary_ConnectionModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_ConnectionModel[textValue] = new ImageInformationModel(connectionImageInfo.Id, connectionImageInfo.Name);

                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;
        }
    }

    public void OpenListDeviceSelection() => OpenListSelection("Devices", Device_Item_Prefab, List_Devices_Parent_GridLayout_Group);
    public void OpenListModuleSelection() => OpenListSelection("Modules", Module_Item_Prefab, List_Modules_Parent_GridLayout_Group);
    public void OpenListLocationImageSelection() => OpenListSelection("Location_Image", OutDoorImage_Item_Prefab, List_Location_Image_Parent_Vertical_Group);
    public void OpenListConnectionImageSelection() => OpenListSelection("Connection_Images", ConnectionImage_Item_Prefab, List_Connection_Image_Parent_Vertical_Group);

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        // if (!initialize_JB_List_Option_Selection.Selection_Option_Canvas.activeSelf)
        //     initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(true);

        var newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        var button = newItem.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => DeselectItem(newItem, field));
        if (field == "Location_Image")
        {
            addLocationImageItem.SetActive(false);
        }
        GetSelectionPanel(field).SetActive(true);
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--;
        selectedGameObjects[field].Remove(item);
        temp_Dictionary_DeviceModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        temp_Dictionary_ModuleModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        temp_Dictionary_OutDoorModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        temp_Dictionary_ConnectionModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        Destroy(item);
        if (field == "Location_Image")
        {
            addLocationImageItem.SetActive(true);
        }
    }

    public void CloseListSelection(string field)
    {
        switch (field)
        {
            case "Devices":
                initialize_JB_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
                break;
            case "Modules":
                initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
                break;
            case "Location_Image":
                initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
                break;
            case "Connection_Images":
                initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
                break;
        }
        // initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    public void CloseListSelectionFromBackButton(string field)
    {
        ClearDeActiveChildren(List_Devices_Parent_GridLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        switch (field)
        {
            case "Devices":
                initialize_JB_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
                break;
            case "Modules":
                initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
                break;
            case "Location_Image":
                initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
                addLocationImageItem.SetActive(true);
                break;
            case "Connection_Images":
                initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
                break;
        }
        // initialize_JB_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    private void ClearDeActiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            if (!child.gameObject.activeSelf && parentGroup.transform.childCount > 1)
                Destroy(child.gameObject);
        }
    }

    private void ClearActiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private GameObject GetSelectionPanel(string field)
    {
        return field switch
        {
            "Devices" => initialize_JB_List_Option_Selection.selection_List_Device_Panel,
            "Modules" => initialize_JB_List_Option_Selection.selection_List_ModuleIO_Panel,
            "Location_Image" => initialize_JB_List_Option_Selection.selection_List_Location_Image_Panel,
            "Connection_Images" => initialize_JB_List_Option_Selection.selection_List_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field Name")
        };
    }

    private void OpenErrorDialog(string title = "Tạo tủ JB mới thất bại", string message = "Đã có lỗi xảy ra khi tạo tủ JB mới. Vui lòng thử lại sau.")
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message;
        DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });
    }

    private void OpenSuccessDialog(JBInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm tủ JB <b><color=#004C8A>{model.Name}</b></color> vào hệ thống";
        backgroundTransform.Find("Dialog_Title").GetComponent<TMP_Text>().text = "Thêm tủ JB mới thành công";

        var confirmButton = horizontalGroupTransform.Find("Confirm_Button").GetComponent<Button>();
        var backButton = horizontalGroupTransform.Find("Back_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");
        confirmButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Tiếp tục thêm mới";
        backButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Trở lại danh sách";

        confirmButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            ResetAllInputFields();
            DialogTwoButton.SetActive(false);
            scrollRect.verticalNormalizedPosition = 1;
            RenewView();
        });

        backButton.onClick.AddListener(() =>
        {
            ResetAllInputFields();
            DialogTwoButton.SetActive(false);
            RenewView();
            CloseAddCanvas();
        });
    }

    private void ShowProgressBar(string title, string details)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        Progress.SetDetailsText(details);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }

    public void ShowLoading(string title) => ShowProgressBar(title, "Đang tải dữ liệu...");
    public void HideLoading() => HideProgressBar();

    public void ShowError(string message)
    {
        if (GlobalVariable.APIRequestType.Contains("POST_JB"))
        {
            OpenErrorDialog(message: message);
        }
    }

    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("POST_JB"))
        {
            Show_Toast.Instance.ShowToast("success", "Thêm tủ JB mới thành công");
            OpenSuccessDialog(JBInformationModel);
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<JBInformationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
    public void DisplayDetail(JBInformationModel model) { }
}

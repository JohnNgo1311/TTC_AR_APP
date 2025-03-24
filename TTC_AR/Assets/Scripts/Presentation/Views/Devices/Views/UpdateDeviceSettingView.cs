using System;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpdateDeviceSettingView : MonoBehaviour, IDeviceView
{
    public Initialize_Device_List_Option_Selection initialize_Device_List_Option_Selection;
    private DevicePresenter _presenter;

    [SerializeField] private DeviceInformationModel DeviceInformationModel;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField deviceCode_TextField;
    [SerializeField] private TMP_InputField deviceFunction_TextField;
    [SerializeField] private TMP_InputField deviceRange_TextField;
    [SerializeField] private TMP_InputField deviceUnit_TextField;
    [SerializeField] private TMP_InputField deviceIOAddress_TextField;

    [Header("Basic")]
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public GameObject parent_Content_Vertical_Group;
    private Transform temp_Item_Transform;

    [Header("LayOutGroup")]
    public GameObject List_JB_Parent_GridLayout_Group;
    // public GameObject List_Module_Parent_GridLayout_Group;
    public GameObject List_Additional_Connection_Image_Parent_Vertical_Group;

    [Header("Prefab")]
    public GameObject JB_Item_Prefab;
    public GameObject Module_Item_Prefab;
    public GameObject ConnectionImage_Item_Prefab;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button backButtonJBListSelection;
    [SerializeField] private Button backButtonModuleListSelection;
    [SerializeField] private Button backButtonAdditionalConnectionImageListSelection;
    [SerializeField] private GameObject addModuleItem;

    [Header("Dialog Buttons")]
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;


    [Header("Canvas")]
    public GameObject List_Device_Canvas;
    public GameObject Add_New_Device_Canvas;
    public GameObject Update_Device_Canvas;



    private List<JBInformationModel> temp_JBModels = new List<JBInformationModel>();
    private ModuleInformationModel temp_ModuleModel = new ModuleInformationModel("", "");
    private Dictionary<string, ImageInformationModel> temp_Dictionary_Additional_ConnectionModel = new();
    private Dictionary<string, JBInformationModel> temp_Dictionary_JBInformationModel = new();
    private readonly Dictionary<string, List<GameObject>> selectedGameObjects = new()
    {
        { "Additional_Connection_Images", new List<GameObject>() },
        { "JBs", new List<GameObject>() },

    };

    private readonly Dictionary<string, int> selectedCounts = new()
    {
        { "Additional_Connection_Images", 0 },
        { "JBs", 0 },

    };

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new DevicePresenter(this, ManagerLocator.Instance.DeviceManager._IDeviceService);
        // DeviceManager._IDeviceService
    }
    private void ResetAllInputFields()
    {
        deviceCode_TextField.text = "";
        deviceFunction_TextField.text = "";
        deviceRange_TextField.text = "";
        deviceUnit_TextField.text = "";
        deviceIOAddress_TextField.text = "";
    }

    public void LoadDetailById()
    {
        RenewView();
        _presenter.LoadDetailById(GlobalVariable.DeviceId);
    }
    void OnEnable()
    {
        ResetAllInputFields();

        AddButtonListeners(initialize_Device_List_Option_Selection.JB_List_Selection_Option_Content_Transform, "JB");
        AddButtonListeners(initialize_Device_List_Option_Selection.Module_List_Selection_Option_Content_Transform, "Module");
        AddButtonListeners(initialize_Device_List_Option_Selection.Additional_Connection_Image_List_Selection_Option_Content_Transform, "Additional_Connection_Images");

        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
        backButtonJBListSelection.onClick.RemoveAllListeners();
        backButtonModuleListSelection.onClick.RemoveAllListeners();
        backButtonAdditionalConnectionImageListSelection.onClick.RemoveAllListeners();
        backButtonJBListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("JB"));
        backButtonModuleListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Module"));
        backButtonAdditionalConnectionImageListSelection.onClick.AddListener(() => CloseListSelectionFromBackButton("Additional_Connection_Images"));
        backButton.onClick.AddListener(CloseAddCanvas);

        submitButton.onClick.AddListener(OnSubmitButtonClick);
        _presenter.LoadDetailById(GlobalVariable.DeviceId);
        scrollRect.verticalNormalizedPosition = 1;
    }

    void OnDisable()
    {
        RenewView();
    }

    private void OnSubmitButtonClick()
    {

        if (string.IsNullOrEmpty(deviceCode_TextField.text))
        {
            OpenErrorDialog("Vui lòng nhập mã thiết bị");
            return;
        }
        else
        {
            Debug.Log("Module Name Value: " + temp_ModuleModel.Name);
            Debug.Log(addModuleItem.activeSelf);


            DeviceInformationModel = new DeviceInformationModel(
            code: string.IsNullOrEmpty(deviceCode_TextField.text) ? throw new ArgumentNullException(nameof(deviceCode_TextField.text)) : deviceCode_TextField.text,
            function: string.IsNullOrEmpty(deviceFunction_TextField.text) ? "Chưa cập nhật" : deviceFunction_TextField.text,
            range: string.IsNullOrEmpty(deviceRange_TextField.text) ? "Chưa cập nhật" : deviceRange_TextField.text,
            unit: string.IsNullOrEmpty(deviceUnit_TextField.text) ? "Chưa cập nhật" : deviceUnit_TextField.text,
            ioAddress: string.IsNullOrEmpty(deviceIOAddress_TextField.text) ? "Chưa cập nhật" : deviceIOAddress_TextField.text,
            jbInformationModels: temp_Dictionary_JBInformationModel.Any() ? temp_Dictionary_JBInformationModel.Values.ToList() : new List<JBInformationModel>(),
            moduleInformationModel: !addModuleItem.activeSelf ? temp_ModuleModel : null,
            additionalConnectionImages: temp_Dictionary_Additional_ConnectionModel.Any() ? temp_Dictionary_Additional_ConnectionModel.Values.ToList() : new List<ImageInformationModel>()
            );

            _presenter.UpdateDevice(GlobalVariable.GrapperId, DeviceInformationModel);
        }
    }

    private void RenewView()
    {
        // ClearActiveChildren(List_JB_Parent_GridLayout_Group);
        //  ClearActiveChildren(List_Module_Parent_GridLayout_Group);
        ClearActiveChildren(List_Additional_Connection_Image_Parent_Vertical_Group);
        JB_Item_Prefab.SetActive(false);
        Module_Item_Prefab.SetActive(false);
        addModuleItem.SetActive(true);
        temp_ModuleModel = new ModuleInformationModel("", "");
        temp_Dictionary_Additional_ConnectionModel.Clear();
        temp_JBModels.Clear();

        selectedGameObjects["Additional_Connection_Images"].Clear();
        selectedCounts["Additional_Connection_Images"] = 0;
        selectedCounts["JBs"] = 0;

    }

    public void CloseAddCanvas()
    {
        Add_New_Device_Canvas.SetActive(false);
        Update_Device_Canvas.SetActive(false);
        List_Device_Canvas.SetActive(true);
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

        if (field != "Module")
        {
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

    }

    private void SetItemTextValue(Transform temp_Item_Transform, string textValue, string field)
    {
        var itemText = temp_Item_Transform.GetComponentInChildren<TMP_Text>();
        if (itemText == null) return;
        itemText.text = textValue;

        switch (field)
        {
            case "JBs":
                if (GlobalVariable.temp_Dictionary_JBInformationModel.TryGetValue(textValue, out var jB))
                {
                    if (!temp_Dictionary_JBInformationModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_JBInformationModel[textValue] = new JBInformationModel(jB.Id, jB.Name);
                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;
            case "Module":
                if (GlobalVariable.temp_Dictionary_ModuleInformationModel.TryGetValue(textValue, out var module))
                {
                    if (temp_ModuleModel == null || temp_ModuleModel.Name != textValue)
                    {
                        temp_ModuleModel = new ModuleInformationModel(module.Id, module.Name);
                    }
                }
                break;
            case "Additional_Connection_Images":
                if (GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out var connectionImageInfo))
                {
                    if (!temp_Dictionary_Additional_ConnectionModel.ContainsKey(textValue))
                    {
                        temp_Dictionary_Additional_ConnectionModel[textValue] = new ImageInformationModel(connectionImageInfo.Id, connectionImageInfo.Name);
                    }
                    else
                    {
                        Destroy(temp_Item_Transform.gameObject);
                    }
                }
                break;
        }
    }

    public void OpenListJBSelection() => OpenListSelection("JB", JB_Item_Prefab, List_JB_Parent_GridLayout_Group);
    public void OpenListModuleSelection() => OpenListSelection("Module", Module_Item_Prefab, null);
    public void OpenListConnectionImageSelection() => OpenListSelection("Additional_Connection_Images", ConnectionImage_Item_Prefab, List_Additional_Connection_Image_Parent_Vertical_Group);

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        // if (!initialize_Device_List_Option_Selection.Selection_Option_Canvas.activeSelf)
        //     initialize_Device_List_Option_Selection.Selection_Option_Canvas.SetActive(true);
        if (field != "Module")
        {
            var newItem = Instantiate(itemPrefab, parentGroup.transform);
            temp_Item_Transform = newItem.transform;
            var button = newItem.GetComponentInChildren<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => DeselectItem(newItem, field));
        }
        else
        {
            temp_Item_Transform = itemPrefab.transform;
            itemPrefab.SetActive(true);
            var button = itemPrefab.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => DeselectItem(itemPrefab, field));
            addModuleItem.SetActive(false);
        }

        GetSelectionPanel(field).SetActive(true);
    }

    private void DeselectItem(GameObject item, string field)
    {
        if (field == "Additional_Connection_Images")
        {
            selectedCounts[field]--;
            selectedGameObjects[field].Remove(item);
            temp_Dictionary_Additional_ConnectionModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
            Destroy(item);
        }
        if (field == "JBs")
        {
            selectedCounts[field]--;
            selectedGameObjects[field].Remove(item);
            temp_Dictionary_JBInformationModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
            Destroy(item);
        }
        else
        {
            item.SetActive(false);
            addModuleItem.SetActive(true);
        }

    }

    public void CloseListSelection(string field)
    {
        GetSelectionPanel(field).SetActive(false);
        // initialize_Device_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    public void CloseListSelectionFromBackButton(string field)
    {
        //ClearDeActiveChildren(List_JB_Parent_GridLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        GetSelectionPanel(field).SetActive(false);
        if (field == "Module")
        {
            addModuleItem.SetActive(true);
        }
        // initialize_Device_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
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
            "JBs" => initialize_Device_List_Option_Selection.selection_List_JB_Panel,
            "Module" => initialize_Device_List_Option_Selection.selection_List_ModuleIO_Panel,
            "Additional_Connection_Images" => initialize_Device_List_Option_Selection.selection_List_Additional_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field Name")
        };
    }

    private void OpenErrorDialog(string title = "Cập nhật thiết bị thất bại", string message = "Đã có lỗi xảy ra khi cập nhật thiết bị. Vui lòng thử lại sau.")
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message;
        DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => DialogOneButton.SetActive(false));
    }
    private void OpenSuccessDialog(string title = "Cập nhật thiết bị thành công", string message = "Bạn đã cập nhật thiết bị thành công: ")
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");

        DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");

        DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message + DeviceInformationModel.Code;

        DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
            _presenter.LoadDetailById(GlobalVariable.DeviceId);
            scrollRect.verticalNormalizedPosition = 1;
        }
       );
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
        if (GlobalVariable.APIRequestType.Contains("PUT_Device"))
        {
            OpenErrorDialog();
        }
    }

    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("PUT_Device"))
        {
            Show_Toast.Instance.ShowToast("success", "Cập nhật thiết bị thành công");
            OpenSuccessDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_Device"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công");
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    private void SetInitialInputFields(DeviceInformationModel model)
    {
        Debug.Log(model.Code + model.Function + model.IOAddress);
        deviceCode_TextField.text = model.Code;
        deviceFunction_TextField.text = model.Function;
        deviceIOAddress_TextField.text = model.IOAddress;
        deviceRange_TextField.text = model.Range;
        deviceUnit_TextField.text = model.Unit;
    }
    private void PopulateItems<T>(List<T> listItems, GameObject itemPrefab, GameObject parentLayoutGroup, string field)
    {
        var parentTransform = parentLayoutGroup.transform;
        foreach (var item in listItems)
        {
            var newItem = Instantiate(itemPrefab, parentTransform);
            SetItemTextValue(newItem.transform, item.ToString(), field);
            AddButtonListener(newItem.transform.Find("Deselect_Button"), () => DeselectItem(newItem, field));
            selectedGameObjects[field].Add(newItem);
        }
    }

    public void DisplayList(List<DeviceInformationModel> models) { }
    public void DisplayUpdateResult(bool success)
    {

    }
    public void DisplayDeleteResult(bool success) { }

    public void DisplayCreateResult(bool success)
    {
    }

    public void DisplayDetail(DeviceInformationModel model)
    {
        SetInitialInputFields(model);

        if (model.AdditionalConnectionImages != null && model.AdditionalConnectionImages.Any())
        {
            var temp_Additional_ConnectionImageNames = model.AdditionalConnectionImages.Select(item => item.Name).ToList();
            PopulateItems(temp_Additional_ConnectionImageNames, ConnectionImage_Item_Prefab, List_Additional_Connection_Image_Parent_Vertical_Group, "Additional_Connection_Images");
        }
        if (model.JBInformationModels != null && model.JBInformationModels.Any())
        {
            var temp_JBNames = model.JBInformationModels.Select(item => item.Name).ToList();
            PopulateItems(temp_JBNames, JB_Item_Prefab, List_JB_Parent_GridLayout_Group, "JBs");
        }
        if (model.ModuleInformationModel != null)
        {
            Module_Item_Prefab.SetActive(true);
            addModuleItem.SetActive(false);
            SetItemTextValue(Module_Item_Prefab.transform, model.ModuleInformationModel.Name, "Module");
            AddButtonListener(Module_Item_Prefab.transform.Find("BackGround"), () => DeselectItem(Module_Item_Prefab, "Module"));
        }

    }
}

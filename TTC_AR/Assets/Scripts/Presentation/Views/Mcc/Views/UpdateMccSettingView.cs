using System;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpdateMccSettingView : MonoBehaviour, IMccView
{
    public Initialize_Mcc_List_Option_Selection initialize_Mcc_List_Option_Selection;
    private MccPresenter _presenter;

    [SerializeField] private MccInformationModel MccInformationModel;
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField CabinetCode_TextField;
    [SerializeField] private TMP_InputField Brand_TextField;
    [SerializeField] private TMP_InputField Note_TextField;

    [Header("Basic")]
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public GameObject parent_Content_Vertical_Group;
    private Transform temp_Item_Transform;

    [Header("LayOutGroup")]
    public GameObject FieldDevice_Parent_GridLayout_Group;

    [Header("Prefab")]
    public GameObject FieldDevice_Item_Prefab;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button backButtonListSelection;

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_Mcc_Canvas;
    public GameObject Add_New_Mcc_Canvas;
    public GameObject Update_Mcc_Canvas;

    private Dictionary<string, FieldDeviceInformationModel> temp_Dictionary_FieldDeviceConnectionModel = new Dictionary<string, FieldDeviceInformationModel>();
    private Dictionary<string, List<GameObject>> selectedGameObjects = new Dictionary<string, List<GameObject>>()
    {
        { "FieldDevices", new List<GameObject>() }
    };
    private Dictionary<string, int> selectedCounts = new Dictionary<string, int>()
    {
        { "FieldDevices", 0 }
    };

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new MccPresenter(this, ManagerLocator.Instance.MccManager._IMccService);
        // DeviceManager._IDeviceService
    }

    void OnEnable()
    {
        //! Clear all listeners
        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
        backButtonListSelection.onClick.RemoveAllListeners();

        //! Add listeners
        backButtonListSelection.onClick.AddListener(CloseListSelectionFromBackButton);
        backButton.onClick.AddListener(CloseUpdateCanvas);
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        //! Load detail by id
        _presenter.LoadDetailById(GlobalVariable.mccId);

        //! Reset scroll position
        scrollRect.verticalNormalizedPosition = 1;
    }

    void OnDisable()
    {
        RenewView();
    }

    private void OnSubmitButtonClick()
    {
        MccInformationModel = new MccInformationModel(
         cabinetCode: CabinetCode_TextField.text,
            brand: Brand_TextField.text,
            listFieldDeviceInformation: temp_Dictionary_FieldDeviceConnectionModel.Values.ToList(),
            note: Note_TextField.text
        );

        if (string.IsNullOrEmpty(CabinetCode_TextField.text))
        {
            OpenErrorDialog(title: "Cập nhật tủ Mcc thất bại", message: "Vui lòng nhập mã tủ Mcc");
            return;
        }

        _presenter.UpdateMcc(GlobalVariable.mccId, MccInformationModel);
    }

    private void RenewView()
    {
        ClearAllVerticalGroupChildren(FieldDevice_Parent_GridLayout_Group);

        temp_Dictionary_FieldDeviceConnectionModel.Clear();

        selectedGameObjects["FieldDevices"].Clear();

        selectedCounts["FieldDevices"] = 0;
    }

    public void CloseUpdateCanvas()
    {
        Add_New_Mcc_Canvas.SetActive(false);
        Update_Mcc_Canvas.SetActive(false);
        List_Mcc_Canvas.SetActive(true);
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

        CloseListSelection();

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

        if (itemText != null)
        {
            itemText.text = textValue;

            if (field == "FieldDevices" && GlobalVariable.temp_Dictionary_FieldDeviceInformationModel.TryGetValue(textValue, out var FieldDeviceInformationModel))
            {
                var FieldDeviceInfoModel = new FieldDeviceInformationModel(FieldDeviceInformationModel.Id, FieldDeviceInformationModel.Name);
                if (!temp_Dictionary_FieldDeviceConnectionModel.ContainsKey(textValue))
                {
                    temp_Dictionary_FieldDeviceConnectionModel[textValue] = FieldDeviceInfoModel;
                }
                else
                {
                    Destroy(temp_Item_Transform.gameObject);
                }
            }
        }
        Debug.Log("Temp Dictionary Count: " + temp_Dictionary_FieldDeviceConnectionModel.Count);
        Debug.Log("Text Value: " + textValue);
    }

    public void OpenListFieldDeviceConnectionSelection() => OpenListSelection("FieldDevices", FieldDevice_Item_Prefab, FieldDevice_Parent_GridLayout_Group);

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        if (!initialize_Mcc_List_Option_Selection.Selection_Option_Canvas.activeSelf)
            initialize_Mcc_List_Option_Selection.Selection_Option_Canvas.SetActive(true);

        var newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => DeselectItem(newItem.gameObject, field));
        GetSelectionPanel(field).SetActive(true);
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--;
        selectedGameObjects[field].Remove(item);
        temp_Dictionary_FieldDeviceConnectionModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        Destroy(item);
        Debug.Log("Temp Dictionary Count: " + temp_Dictionary_FieldDeviceConnectionModel.Count);

    }

    public void CloseListSelection()
    {
        initialize_Mcc_List_Option_Selection.selection_List_FieldDevices_Panel.SetActive(false);
        initialize_Mcc_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    public void CloseListSelectionFromBackButton()
    {
        ClearDeActiveChildren(FieldDevice_Parent_GridLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        initialize_Mcc_List_Option_Selection.selection_List_FieldDevices_Panel.SetActive(false);
        initialize_Mcc_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    private void ClearDeActiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            if (!child.gameObject.activeSelf && parentGroup.transform.childCount > 1)
                Destroy(child.gameObject);
        }
    }

    private void ClearAllVerticalGroupChildren(GameObject parentGroup)
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
            "FieldDevices" => initialize_Mcc_List_Option_Selection.selection_List_FieldDevices_Panel,
            _ => throw new ArgumentException("Invalid field name")
        };
    }

    private void Update()
    {
        if (initialize_Mcc_List_Option_Selection.selection_List_FieldDevices_Panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                CloseListSelectionFromBackButton();
            }
        }
    }

    private void OpenErrorDialog(string title = "Cập nhật tủ Mcc thất bại", string message = "Đã có lỗi xảy ra khi cập nhật tủ Mcc mới. Vui lòng thử lại sau.")
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
    private void OpenSuccessDialog(string title = "Cập nhật tủ Mcc thành công", string message = "Bạn đã cập nhật tủ Mcc thành công: ")
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");

        DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");

        DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message + $"<b><color =#004C8A>{MccInformationModel.CabinetCode}</b></color>";

        DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
            _presenter.LoadDetailById(GlobalVariable.mccId);
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

    private void SetInitialInputFields(MccInformationModel model)
    {
        CabinetCode_TextField.text = model.CabinetCode;
        Brand_TextField.text = model.Brand;
        Note_TextField.text = model.Note;
    }

    public void ShowLoading(string title) => ShowProgressBar(title, "Đang tải dữ liệu...");
    public void HideLoading() => HideProgressBar();

    public void ShowError(string message)
    {
        if (GlobalVariable.APIRequestType.Contains("PUT_Mcc"))
        {
            OpenErrorDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_Mcc"))
        {
            OpenErrorDialog(
                title: "Tải dữ liệu thất bại",
                message: "Đã có lỗi xảy ra khi tải dữ liệu. Vui lòng thử lại sau."
            );
        }

    }
    public void ReloadDetailById()
    {
        RenewView();
        _presenter.LoadDetailById(GlobalVariable.mccId);
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("PUT_Mcc"))
        {
            Show_Toast.Instance.ShowToast("success", "Cập nhật tủ Mcc mới thành công");
            OpenSuccessDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_Mcc"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công");
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<MccInformationModel> models) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }

    public void DisplayDetail(MccInformationModel model)
    {
        SetInitialInputFields(model);
        if (model.ListFieldDeviceInformation != null && model.ListFieldDeviceInformation.Any())
        {
            var temp_List_FieldDeviceNames = model.ListFieldDeviceInformation.Select(item => item.Name).ToList();
            PopulateItems(temp_List_FieldDeviceNames, FieldDevice_Item_Prefab, FieldDevice_Parent_GridLayout_Group, "FieldDevices");
        }
        AddButtonListeners(initialize_Mcc_List_Option_Selection.FieldDevices_List_Selection_Option_Content_Transform, "FieldDevices");
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

    public void DisplayCreateResult(bool success) { }

    public void DisplayFieldDeviceList(List<FieldDeviceInformationModel> models)
    {
    }
}

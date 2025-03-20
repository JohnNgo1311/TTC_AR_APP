using System;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpdateFieldDeviceSettingView : MonoBehaviour, IFieldDeviceView
{
    public Initialize_FieldDevice_List_Option_Selection initialize_FieldDevice_List_Option_Selection;
    private FieldDevicePresenter _presenter;

    [SerializeField] private FieldDeviceInformationModel fieldDeviceInformationModel;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField Name_TextField;
    [SerializeField] private TMP_InputField RatedPower_TextField;
    [SerializeField] private TMP_InputField RatedCurrent_TextField;
    [SerializeField] private TMP_InputField ActiveCurrent_TextField;
    [SerializeField] private TMP_InputField Note_TextField;

    [Header("Basic")]
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public GameObject parent_Content_Vertical_Group;
    private Transform temp_Item_Transform;

    [Header("LayOutGroup")]
    public GameObject Connection_Image_Parent_VerticalLayout_Group;

    [Header("Prefab")]
    public GameObject Connection_Image_Item_Prefab;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button backButtonListSelection;

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_FieldDevice_Canvas;
    public GameObject Add_New_FieldDevice_Canvas;
    public GameObject Update_FieldDevice_Canvas;

    private Dictionary<string, ImageInformationModel> temp_Dictionary_ImageConnectionModel = new Dictionary<string, ImageInformationModel>();
    private Dictionary<string, List<GameObject>> selectedGameObjects = new Dictionary<string, List<GameObject>>()
    {
        { "Connection_Image", new List<GameObject>() }
    };
    private Dictionary<string, int> selectedCounts = new Dictionary<string, int>()
    {
        { "Connection_Image", 0 }
    };

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new FieldDevicePresenter(this, ManagerLocator.Instance.FieldDeviceManager._IFieldDeviceService);
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
        backButton.onClick.AddListener(CloseAddCanvas);
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        //! Load detail by id
        _presenter.LoadDetailById(GlobalVariable.FieldDeviceId);

        //! Reset scroll position
        scrollRect.verticalNormalizedPosition = 1;
    }

    void OnDisable()
    {
        RenewView();
    }

    private void OnSubmitButtonClick()
    {
        fieldDeviceInformationModel = null;
        fieldDeviceInformationModel = new FieldDeviceInformationModel(
            name: Name_TextField.text,
            ratedPower: RatedPower_TextField.text,
            ratedCurrent: RatedCurrent_TextField.text,
            activeCurrent: ActiveCurrent_TextField.text,
            listConnectionImages: temp_Dictionary_ImageConnectionModel.Values.ToList(),
            note: Note_TextField.text
        );

        if (string.IsNullOrEmpty(fieldDeviceInformationModel.Name))
        {
            OpenErrorDialog(title: "Cập nhật thiết bị trường thất bại", message: "Vui lòng nhập tên thiết bị trường");
            return;
        }

        _presenter.UpdateFieldDevice(GlobalVariable.FieldDeviceId, fieldDeviceInformationModel);
    }

    private void RenewView()
    {
        ClearAllVerticalGroupChildren(Connection_Image_Parent_VerticalLayout_Group);

        temp_Dictionary_ImageConnectionModel.Clear();

        selectedGameObjects["Connection_Image"].Clear();

        selectedCounts["Connection_Image"] = 0;
    }

    public void CloseAddCanvas()
    {
        Add_New_FieldDevice_Canvas.SetActive(false);
        Update_FieldDevice_Canvas.SetActive(false);
        List_FieldDevice_Canvas.SetActive(true);
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

            if (field == "Connection_Image" && GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out var imageInformationModel))
            {
                var imageInfoModel = new ImageInformationModel(imageInformationModel.Id, imageInformationModel.Name);
                if (!temp_Dictionary_ImageConnectionModel.ContainsKey(textValue))
                {
                    temp_Dictionary_ImageConnectionModel[textValue] = imageInfoModel;
                }
                else
                {
                    Destroy(temp_Item_Transform.gameObject);
                }
            }
        }
        Debug.Log("Temp Dictionary Count: " + temp_Dictionary_ImageConnectionModel.Count);
        Debug.Log("Text Value: " + textValue);
    }

    public void OpenListImageConnectionSelection() => OpenListSelection("Connection_Image", Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group);

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        if (!initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.activeSelf)
            initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(true);

        var newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => DeselectItem(newItem.gameObject, field));
        GetSelectionPanel(field).SetActive(true);
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--;
        selectedGameObjects[field].Remove(item);
        temp_Dictionary_ImageConnectionModel.Remove(item.GetComponentInChildren<TMP_Text>().text);
        Destroy(item);
        Debug.Log("Temp Dictionary Count: " + temp_Dictionary_ImageConnectionModel.Count);

    }

    public void CloseListSelection()
    {
        initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }

    public void CloseListSelectionFromBackButton()
    {
        ClearDeActiveChildren(Connection_Image_Parent_VerticalLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
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
            "Connection_Image" => initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field name")
        };
    }

    private void Update()
    {
        if (initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                CloseListSelectionFromBackButton();
            }
        }
    }

    private void OpenErrorDialog(string title = "Cập nhật thiết bị trường thất bại", string message = "Đã có lỗi xảy ra khi cập nhật thiết bị trường mới. Vui lòng thử lại sau.")
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
    private void OpenSuccessDialog(string title = "Cập nhật thiết bị trường thành công", string message = "Bạn đã cập nhật thiết bị trường thành công: ")
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");

        DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");

        DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message + fieldDeviceInformationModel.Name;

        DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
            _presenter.LoadDetailById(GlobalVariable.FieldDeviceId);
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

    private void SetInitialInputFields(FieldDeviceInformationModel model)
    {
        Name_TextField.text = model.Name;
        RatedPower_TextField.text = model.RatedPower;
        RatedCurrent_TextField.text = model.RatedCurrent;
        ActiveCurrent_TextField.text = model.ActiveCurrent;
        Note_TextField.text = model.Note;
    }

    public void ShowLoading(string title) => ShowProgressBar(title, "Đang tải dữ liệu...");
    public void HideLoading() => HideProgressBar();

    public void ShowError(string message)
    {

        if (GlobalVariable.APIRequestType.Contains("PUT_FieldDevice"))
        {

            OpenErrorDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_FieldDevice"))
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
        _presenter.LoadDetailById(GlobalVariable.FieldDeviceId);
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("PUT_FieldDevice"))
        {

            Show_Toast.Instance.ShowToast("success", "Cập nhật thiết bị trường mới thành công");
            OpenSuccessDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_FieldDevice"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công");

        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<FieldDeviceInformationModel> models) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }

    public void DisplayDetail(FieldDeviceInformationModel model)
    {
        SetInitialInputFields(model);
        if (model.ListConnectionImages != null && model.ListConnectionImages.Any())
        {
            var temp_List_ConnectionImageNames = model.ListConnectionImages.Select(item => item.Name).ToList();
            PopulateItems(temp_List_ConnectionImageNames, Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group, "Connection_Image");
        }
        AddButtonListeners(initialize_FieldDevice_List_Option_Selection.Connection_Image_List_Selection_Option_Content_Transform, "Connection_Image");
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreateFieldDeviceSettingView : MonoBehaviour, IFieldDeviceView
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
    // public List<LayoutElement> layoutElements = new List<LayoutElement>();
    private Transform temp_Item_Transform;


    [Header("LayOutGroup")]
    public GameObject Connection_Image_Parent_VerticalLayout_Group;

    [Header("Prefab")]
    public GameObject Connection_Image_Item_Prefab;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
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
        FieldDeviceManager FieldDeviceManager = FindObjectOfType<FieldDeviceManager>();
        _presenter = new FieldDevicePresenter(this, FieldDeviceManager._IFieldDeviceService);
    }

    void OnEnable()
    {
        scrollRect.verticalNormalizedPosition = 1;

        ResetAllInputFields();

        AddButtonListeners(initialize_FieldDevice_List_Option_Selection.Connection_Image_List_Selection_Option_Content_Transform, "Connection_Image");

        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(CloseAddCanvas);

        submitButton.onClick.AddListener(() =>
        {
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
                OpenErrorCreateDialog("Vui lòng nhập tên thiết bị trường");
                return;
            }
            else
            {
                _presenter.CreateNewFieldDevice(GlobalVariable.GrapperId, fieldDeviceInformationModel);
            }
        }
           );
    }

    void OnDisable()
    {
        RenewView();
    }

    private void RenewView()
    {
        ClearActiveChildren(Connection_Image_Parent_VerticalLayout_Group);
        ResetAllInputFields();
        temp_Dictionary_ImageConnectionModel.Clear();
        selectedGameObjects["Connection_Image"].Clear();
        selectedGameObjects["Connection_Image"].Clear();
        selectedCounts["Connection_Image"] = 0;
    }

    private void Start()
    {
        // AdjustLayoutElements();
    }

    private void AdjustLayoutElements()
    {
        // float viewPortHeight = viewPortTransform.rect.height;
        // layoutElements[0].minHeight = viewPortHeight * 0.12f;
        // layoutElements[1].minHeight = viewPortHeight * 0.12f;
        // layoutElements[2].minHeight = viewPortHeight * 0.6f;
    }

    public void CloseAddCanvas()
    {
        if (Add_New_FieldDevice_Canvas.activeSelf) Add_New_FieldDevice_Canvas.SetActive(false);
        if (Update_FieldDevice_Canvas.activeSelf) Update_FieldDevice_Canvas.SetActive(false);
        if (!List_FieldDevice_Canvas.activeSelf) List_FieldDevice_Canvas.SetActive(true);
    }

    private void ResetAllInputFields()
    {
        Name_TextField.text = "";
        RatedPower_TextField.text = "";
        RatedCurrent_TextField.text = "";
        ActiveCurrent_TextField.text = "";
        Note_TextField.text = "";
    }

    private void AddButtonListeners(Transform contentTransform, string field)
    {
        foreach (Transform option in contentTransform)
        {
            AddButtonListener(option,
            () => SelectOption(option.gameObject.GetComponentInChildren<TMP_Text>().text, field));
        }
    }
    private void AddButtonListener(Transform option, UnityEngine.Events.UnityAction action)
    {
        Button button = option.gameObject.GetComponent<Button>();
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

        selectedCounts[field]++; //Tăng số lượng đã chọn

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
        TMP_Text itemText = temp_Item_Transform.GetComponentInChildren<TMP_Text>();

        if (itemText != null)
        {
            itemText.text = textValue;

            switch (field)
            {
                case "Connection_Image":
                    if (GlobalVariable.temp_Dictionary_ImageInformationModel.TryGetValue(textValue, out ImageInformationModel imageInformationModel))
                    {
                        ImageInformationModel ImageInformationModel = new ImageInformationModel(
                            id: imageInformationModel.Id,
                            name: imageInformationModel.Name
                        );
                        if (!temp_Dictionary_ImageConnectionModel.ContainsKey(textValue))
                        {
                            temp_Dictionary_ImageConnectionModel[textValue] = ImageInformationModel;
                        }
                        else
                        {
                            Destroy(temp_Item_Transform.gameObject);
                        }
                    }
                    break;
            }
        }
        Debug.Log("Text Value: " + textValue);
    }

    public void OpenListImageConnectionSelection() => OpenListSelection("Connection_Image", Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group);
    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        //! Bật ListOptionCanvas lên
        if (!initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(true);
        //! Khởi tạo item Prefab trong vertical Group
        GameObject newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => DeselectItem(newItem.gameObject, field));
        //! Tăt Panel, giữ canvas
        GetSelectionPanel(field).SetActive(true);
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--; //Giảm số lượng đã chọn
        selectedGameObjects[field].Remove(item); //Xóa khỏi danh sách đã chọn
        Destroy(item); //Xóa khỏi giao diện
    }

    public void CloseListSelection()
    {
        if (initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }
    public void CloseListSelectionFromBackButton()
    {
        ClearDeActiveChildren(Connection_Image_Parent_VerticalLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        if (initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_FieldDevice_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_FieldDevice_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }
    private void ClearDeActiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            if (!child.gameObject.activeSelf || parentGroup.transform.childCount > 1)
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


    private void OpenErrorCreateDialog(string message = "Đã có lỗi xảy ra khi tạo thiết bị trường mới. Vui lòng thử lại sau.")
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message;
        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Tạo thiết bị trường mới thất bại";
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            RenewView();
            DialogOneButton.SetActive(false);
        });
    }
    private void OpenSuccessCreateDialog(FieldDeviceInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm thiết bị trường <color=#FF0000><b>{model.Name}</b></color> vào hệ thống";
        backgroundTransform.Find("Dialog_Title").GetComponent<TMP_Text>().text = "Thêm thiết bị trường mới thành công";

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


    public void ShowLoading() => ShowProgressBar("Loading", "Dữ liệu đang được tải...");
    public void HideLoading() => HideProgressBar();
    public void ShowError(string message)
    {
        switch (GlobalVariable.APIRequestType)
        {
            case "POST_FieldDevice":
                OpenErrorCreateDialog();
                break;
        }
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        switch (GlobalVariable.APIRequestType)
        {
            case "POST_FieldDevice":
                Show_Toast.Instance.ShowToast("success", "Thêm thiết bị trường mới thành công");
                OpenSuccessCreateDialog(fieldDeviceInformationModel);
                break;
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<FieldDeviceInformationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }

    public void DisplayDetail(FieldDeviceInformationModel model)
    {
    }
}

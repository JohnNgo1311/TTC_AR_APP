using System.Collections.Generic;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateModuleSpecificationSettingView : MonoBehaviour, IModuleSpecificationView
{
    private ModuleSpecificationPresenter _presenter;

    // Input Fields
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField ModuleSpecificationCode_TextField;
    [SerializeField] private TMP_InputField Type_TextField;
    [SerializeField] private TMP_InputField NumOfIo_TextField;
    [SerializeField] private TMP_InputField SignalType_TextField;
    [SerializeField] private TMP_InputField CompatibleTBUs_TextField;
    [SerializeField] private TMP_InputField OperatingVoltage_TextField;

    [SerializeField] private TMP_InputField OperatingCurrent_TextField;
    [SerializeField] private TMP_InputField FlexbusCurrent_TextField;
    [SerializeField] private TMP_InputField Alarm_TextField;
    [SerializeField] private TMP_InputField Note_TextField;
    [SerializeField] private TMP_InputField PdfManual_TextField;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_ModuleSpecification_Canvas;
    public GameObject Add_New_ModuleSpecification_Canvas;
    public GameObject Update_ModuleSpecification_Canvas;

    public ScrollRect ScrollView;

    private ModuleSpecificationModel _ModuleSpecificationModel;

    void Awake()
    {
        ModuleSpecificationManager ModuleSpecificationManager = FindObjectOfType<ModuleSpecificationManager>();
        _presenter = new ModuleSpecificationPresenter(this, ModuleSpecificationManager._IModuleSpecificationService);
    }

    void OnEnable()
    {
        ResetAllInputFields();
        backButton.onClick.AddListener(CloseAddCanvas);

        submitButton.onClick.AddListener(() =>
        {
            _ModuleSpecificationModel = new ModuleSpecificationModel(
                code: ModuleSpecificationCode_TextField.text,
                type: Type_TextField.text,
                numOfIO: NumOfIo_TextField.text,
                signalType: SignalType_TextField.text,
                compatibleTBUs: CompatibleTBUs_TextField.text,
                operatingVoltage: OperatingVoltage_TextField.text,
                operatingCurrent: OperatingCurrent_TextField.text,
                flexbusCurrent: FlexbusCurrent_TextField.text,
                alarm: Alarm_TextField.text,
                note: Note_TextField.text,
                pdfManual: PdfManual_TextField.text
              );
            _presenter.CreateNewModuleSpecification(
                GlobalVariable.companyId, _ModuleSpecificationModel
           );
        });
    }
    void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
    }
    public void DisplayDetail(ModuleSpecificationModel model)
    {
        // detailText.text = model.DisplayText;
    }
    public void CloseAddCanvas()
    {
        if (Add_New_ModuleSpecification_Canvas.activeSelf) Add_New_ModuleSpecification_Canvas.SetActive(false);
        if (Update_ModuleSpecification_Canvas.activeSelf) Update_ModuleSpecification_Canvas.SetActive(false);
        if (!List_ModuleSpecification_Canvas.activeSelf) List_ModuleSpecification_Canvas.SetActive(true);
    }

    private void OpenErrorCreateDialog()
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi tạo loại Module mới. Vui lòng thử lại sau.";
        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Tạo Module mới thất bại";
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });
    }
    private void OpenSuccessCreateDialog(ModuleSpecificationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm loại Module <color=#FF0000><b>{model.Code}</b></color> vào hệ thống";
        backgroundTransform.Find("Dialog_Title").GetComponent<TMP_Text>().text = "Thêm loại Module mới thành công";

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
            ScrollView.verticalNormalizedPosition = 1;
        });

        backButton.onClick.AddListener(() =>
        {
            ResetAllInputFields();
            DialogTwoButton.SetActive(false);
            CloseAddCanvas();
        });
    }
    private void ResetAllInputFields()
    {
        ModuleSpecificationCode_TextField.text = "";
        Type_TextField.text = "";
        NumOfIo_TextField.text = "";
        SignalType_TextField.text = "";
        CompatibleTBUs_TextField.text = "";
        OperatingVoltage_TextField.text = "";
        OperatingCurrent_TextField.text = "";
        FlexbusCurrent_TextField.text = "";
        Alarm_TextField.text = "";
        Note_TextField.text = "";
        PdfManual_TextField.text = "";
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



    public void ShowLoading(string title) => ShowProgressBar(title, "Dữ liệu đang được tải...");
    public void HideLoading() => HideProgressBar();
    public void ShowError(string message)
    {
        if (GlobalVariable.APIRequestType.Contains("POST_ModuleSpecification"))
        {
            OpenErrorCreateDialog();
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("POST_ModuleSpecification"))
        {

            Show_Toast.Instance.ShowToast("success", "Thêm loại Module mới thành công");
            OpenSuccessCreateDialog(_ModuleSpecificationModel);
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }


    public void DisplayList(List<ModuleSpecificationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
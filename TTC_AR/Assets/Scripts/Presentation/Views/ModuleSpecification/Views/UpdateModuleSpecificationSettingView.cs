using System.Collections.Generic;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateModuleSpecificationSettingView : MonoBehaviour, IModuleSpecificationView
{
    private ModuleSpecificationPresenter _presenter;

    // Input Fields
    [Header("Input Fields")]
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
    public GameObject Add__ModuleSpecification_Canvas;
    public GameObject Update_ModuleSpecification_Canvas;

    public ScrollRect ScrollView;

    private ModuleSpecificationModel _ModuleSpecificationModel;

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new ModuleSpecificationPresenter(this, ManagerLocator.Instance.ModuleSpecificationManager._IModuleSpecificationService);
        // DeviceManager._IDeviceService
    }



    void OnEnable()
    {
        ResetAllInputFields();

        _presenter.LoadDetailById(GlobalVariable.ModuleSpecificationId);

        submitButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(CloseUpdateCanvas);

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }
    private void OnSubmitButtonClick()
    {

        if (string.IsNullOrEmpty(ModuleSpecificationCode_TextField.text))
        {
            OpenErrorDialog("loại Module thất bại", "Vui lòng nhập mã loại Module");
            return;
        }
        if (GlobalVariable.temp_Dictionary_DeviceInformationModel.ContainsKey(ModuleSpecificationCode_TextField.text))
        {
            OpenErrorDialog("loại Module thất bại", "Mã loại Module này đã tồn tại");
            return;
        }
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
        _presenter.UpdateModuleSpecification(
                 GlobalVariable.ModuleSpecificationId, _ModuleSpecificationModel
            );
    }



    void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
    }
    public void CloseUpdateCanvas()
    {
        if (Add__ModuleSpecification_Canvas.activeSelf) Add__ModuleSpecification_Canvas.SetActive(false);
        if (Update_ModuleSpecification_Canvas.activeSelf) Update_ModuleSpecification_Canvas.SetActive(false);
        if (!List_ModuleSpecification_Canvas.activeSelf) List_ModuleSpecification_Canvas.SetActive(true);
    }
    public void DisplayDetail(ModuleSpecificationModel model)
    {
        SetInitialInputFields(model);
    }
    private void OpenErrorDialog(string title = "Cập nhật loại Module thất bại", string content = "Đã có lỗi xảy ra khi cập nhật loại Module này. Vui lòng thử lại sau.")
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = content;
        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });
    }
    private void OpenSuccessDialog(ModuleSpecificationModel model)
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");
        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công cập nhật loại Module <b><color =#004C8A>{model.Code}</b></color>"; ;
        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Cập nhật loại Module thành công";
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
            ResetAllInputFields();
            CloseUpdateCanvas();
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
    private void SetInitialInputFields(ModuleSpecificationModel model)
    {
        ModuleSpecificationCode_TextField.text = model.Code;
        Type_TextField.text = model.Type;
        NumOfIo_TextField.text = model.NumOfIO;
        SignalType_TextField.text = model.SignalType;
        CompatibleTBUs_TextField.text = model.CompatibleTBUs;
        OperatingVoltage_TextField.text = model.OperatingVoltage;
        OperatingCurrent_TextField.text = model.OperatingCurrent;
        FlexbusCurrent_TextField.text = model.FlexbusCurrent;
        Alarm_TextField.text = model.Alarm;
        Note_TextField.text = model.Note;
        PdfManual_TextField.text = model.PdfManual;
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
        if (GlobalVariable.APIRequestType.Contains("PUT_ModuleSpecification"))
        {
            OpenErrorDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("GET_ModuleSpecification"))
        {
            OpenErrorDialog(title: "Tải dữ liệu thất bại", content: "Đã có lỗi xảy ra khi tải dữ liệu loại Module. Vui lòng thử lại sau");
        }
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("PUT_ModuleSpecification"))
        {

            Show_Toast.Instance.ShowToast("success", "Cập nhật dữ liệu thành công");
            OpenSuccessDialog(_ModuleSpecificationModel);
        }

        if (GlobalVariable.APIRequestType.Contains("GET_ModuleSpecification"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công");
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<ModuleSpecificationModel> models) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
    public void DisplayCreateResult(bool success)
    {
    }
}
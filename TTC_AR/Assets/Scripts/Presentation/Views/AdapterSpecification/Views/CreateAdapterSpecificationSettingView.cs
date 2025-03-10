using System.Collections.Generic;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAdapterSpecificationSettingView : MonoBehaviour, IAdapterSpecificationView
{
    private AdapterSpecificationPresenter _presenter;

    // Input Fields
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField AdapterSpecificationCode_TextField;
    [SerializeField] private TMP_InputField Type_TextField;
    [SerializeField] private TMP_InputField Communication_TextField;
    [SerializeField] private TMP_InputField NumOfAdapterAllowed_TextField;
    [SerializeField] private TMP_InputField CommSpeed_TextField;
    [SerializeField] private TMP_InputField InputSupply_TextField;

    [SerializeField] private TMP_InputField OutputSupply_TextField;
    [SerializeField] private TMP_InputField InrushCurrent_TextField;
    [SerializeField] private TMP_InputField Alarm_TextField;
    [SerializeField] private TMP_InputField Note_TextField;
    [SerializeField] private TMP_InputField PDFManual_TextField;

    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_AdapterSpecification_Canvas;
    public GameObject Add_New_AdapterSpecification_Canvas;
    public GameObject Update_AdapterSpecification_Canvas;

    public ScrollRect ScrollView;

    private AdapterSpecificationModel _adapterSpecificationModel;

    void Awake()
    {
        AdapterSpecificationManager AdapterSpecificationManager = FindObjectOfType<AdapterSpecificationManager>();
        _presenter = new AdapterSpecificationPresenter(this, AdapterSpecificationManager._IAdapterSpecificationService);
    }

    void OnEnable()
    {
        ResetAllInputFields();
        backButton.onClick.AddListener(CloseAddCanvas);

        submitButton.onClick.AddListener(() =>
        {
            _adapterSpecificationModel = new AdapterSpecificationModel(
                  AdapterSpecificationCode_TextField.text,
                  Type_TextField.text,
                  Communication_TextField.text,
                  NumOfAdapterAllowed_TextField.text,
                  CommSpeed_TextField.text,
                  InputSupply_TextField.text,
                  OutputSupply_TextField.text,
                  InrushCurrent_TextField.text,
                  Alarm_TextField.text,
                  Note_TextField.text,
                  PDFManual_TextField.text
              );
            _presenter.CreateNewAdapterSpecification(
                GlobalVariable.companyId, _adapterSpecificationModel
           );
        });
    }
    void OnDisable()
    {

        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
    }
    public void DisplayDetail(AdapterSpecificationModel model)
    {
        // detailText.text = model.DisplayText;
    }
    public void CloseAddCanvas()
    {
        if (Add_New_AdapterSpecification_Canvas.activeSelf) Add_New_AdapterSpecification_Canvas.SetActive(false);
        if (Update_AdapterSpecification_Canvas.activeSelf) Update_AdapterSpecification_Canvas.SetActive(false);
        if (!List_AdapterSpecification_Canvas.activeSelf) List_AdapterSpecification_Canvas.SetActive(true);
    }

    private void OpenErrorCreateDialog()
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();
        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");
        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");
        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi tạo loại Adapter mới. Vui lòng thử lại sau.";
        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Tạo Adapter mới thất bại";
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });
    }
    private void OpenSuccessCreateDialog(AdapterSpecificationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm loại Adapter <color=#FF0000><b>{model.Code}</b></color> vào hệ thống";
        backgroundTransform.Find("Dialog_Title").GetComponent<TMP_Text>().text = "Thêm loại Adapter mới thành công";

        var confirmButton = horizontalGroupTransform.Find("Confirm_Button").GetComponent<Button>();
        var backButton = horizontalGroupTransform.Find("Back_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");
        confirmButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Tiếp tục thêm mới";
        backButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Quay lại danh sách";

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
        AdapterSpecificationCode_TextField.text = "";
        Type_TextField.text = "";
        Communication_TextField.text = "";
        NumOfAdapterAllowed_TextField.text = "";
        CommSpeed_TextField.text = "";
        InputSupply_TextField.text = "";
        OutputSupply_TextField.text = "";
        InrushCurrent_TextField.text = "";
        Alarm_TextField.text = "";
        Note_TextField.text = "";
        PDFManual_TextField.text = "";
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



    public void ShowLoading() => ShowProgressBar("Loading", "Please wait...");
    public void HideLoading() => HideProgressBar();
    public void ShowError(string message)
    {
        switch (GlobalVariable.APIRequestType)
        {
            case "POST_AdapterSpecification":
                OpenErrorCreateDialog();
                break;
        }
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        switch (GlobalVariable.APIRequestType)
        {
            case "POST_AdapterSpecification":
                Show_Toast.Instance.ShowToast("success", "Thêm loại Adapter mới thành công");
                OpenSuccessCreateDialog(_adapterSpecificationModel);
                break;
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<AdapterSpecificationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
using System.Collections.Generic;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateImageSettingView : MonoBehaviour, IImageView
{
    private ImagePresenter _presenter;

    // Input Fields
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField Name_TextField;


    [Header("Buttons")]
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Canvas")]
    public GameObject List_Image_Canvas;
    public GameObject Add_New_Image_Canvas;
    public GameObject Update_Image_Canvas;

    public ScrollRect ScrollView;

    private ImageInformationModel _ImageInformationModel;

    void Awake()
    {
        ImageManager ImageManager = FindObjectOfType<ImageManager>();
        _presenter = new ImagePresenter(this, ImageManager._IImageService);
    }

    void OnEnable()
    {
        ResetAllInputFields();
        backButton.onClick.AddListener(CloseAddCanvas);

        submitButton.onClick.AddListener(() =>
        {
            // _ImageInformationModel = new ImageInformationModel(
            //     name ImageCode_TextField.text,
            //     type: Type_TextField.text,
            //     numOfIO: NumOfIo_TextField.text,
            //     signalType: SignalType_TextField.text,
            //     compatibleTBUs: CompatibleTBUs_TextField.text,
            //     operatingVoltage: OperatingVoltage_TextField.text,
            //     operatingCurrent: OperatingCurrent_TextField.text,
            //     flexbusCurrent: FlexbusCurrent_TextField.text,
            //     alarm: Alarm_TextField.text,
            //     note: Note_TextField.text,
            //     pdfManual: PdfManual_TextField.text
            //   );
            _presenter.CreateNewImage(
                GlobalVariable.companyId, _ImageInformationModel
           );
        });
    }
    void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();
    }
    public void DisplayDetail(ImageInformationModel model)
    {
        // detailText.text = model.DisplayText;
    }
    public void CloseAddCanvas()
    {
        if (Add_New_Image_Canvas.activeSelf) Add_New_Image_Canvas.SetActive(false);
        if (Update_Image_Canvas.activeSelf) Update_Image_Canvas.SetActive(false);
        if (!List_Image_Canvas.activeSelf) List_Image_Canvas.SetActive(true);
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
    private void OpenSuccessCreateDialog(ImageInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm loại Module <b><color =#004C8A>{model.Name}</b></color> vào hệ thống";
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
        Name_TextField.text = "";
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
        if (GlobalVariable.APIRequestType.Contains("POST_Image"))
            OpenErrorCreateDialog();


    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("POST_Image"))
        {
            Show_Toast.Instance.ShowToast("success", "Thêm loại Module mới thành công");
            OpenSuccessCreateDialog(_ImageInformationModel);
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<ImageInformationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
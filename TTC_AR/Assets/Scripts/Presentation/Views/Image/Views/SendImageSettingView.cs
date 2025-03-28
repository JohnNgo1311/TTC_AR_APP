using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SendImageSettingView : MonoBehaviour, IImageView
{
    public CreateImageSettingView createImageSettingView;
    private ImagePresenter _presenter;
    public WebCamPhotoCamera webCamPhotoCamera;
    public PickPhotoFromCamera pickPhotoFromCamera;
    public PickPhotoFromGallery pickPhotoFromGallery;

    [Header("Send Request")]
    public Button sendRequestButton;
    public TMP_Text imageNameText;
    public RawImage finalImage; //! Sẽ được cập nhật do PickPhotoFromCamera hoặc PickPhotoFromGallery

    private string imageObjectName = "";
    private string imageType = "";
    private string finalImageName = "";

    [Header("Canvas")]
    public GameObject List_Image_Canvas;
    public GameObject Add_NewImage_Canvas;

    public GameObject ConfirmRequestCanvas;

    [Header("Dialog Buttons")]
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    void Awake()
    {
        _presenter = new ImagePresenter(this,
        ManagerLocator.Instance.ImageManager._IImageService);
    }
    private void OnEnable()
    {
        SetImageName();

        sendRequestButton.onClick.RemoveAllListeners();

        if (GlobalVariable.PickPhotoFromCamera)

        {
            sendRequestButton.onClick.AddListener(() =>
            {
                SendImageRequestFromCamera();
            });
        }
        else
        {
            sendRequestButton.onClick.AddListener(() =>
            {
                SendImageRequestFromGallery();
            });
        }

        sendRequestButton.onClick.AddListener(() =>
        {

        });
    }

    void SetImageName()
    {
        switch (createImageSettingView.finalTypeImageText)
        {
            case
                "ConnectionImage":
                imageType = "Sơ đồ kết nối";
                break;
            case "OutdoorImage":
                imageType = "Hình ảnh tủ";
                break;
        }
        imageObjectName = createImageSettingView.finalObjectNameText;

        finalImageName = $"GrapperA_{imageType}_{imageObjectName}";
        if (GlobalVariable.temp_ListImage_Name.Any(imageName => imageName.Contains(finalImageName)))
        {
            var listImageSameName = GlobalVariable.temp_ListImage_Name
            .Where(x => x.Contains(finalImageName))
            .ToList();
            finalImageName += $"_{listImageSameName.Count + 1}";
        }
        else
        {
            finalImageName += "_1";
        }
        imageNameText.text = finalImageName;
    }


    public void SendImageRequestFromGallery()
    {

        _presenter.UploadImageFromGallery(
        GlobalVariable.GrapperId,
        (Texture2D)finalImage.texture,
        "image",
        imageNameText.text,
        pickPhotoFromGallery.imagePath

    );
    }
    public void SendImageRequestFromCamera()
    {
        _presenter.UploadImageFromCamera(
        GlobalVariable.GrapperId,
        (Texture2D)finalImage.texture,
        "image",
        imageNameText.text);
    }

    private void OpenErrorDialog(string title = "Thêm hình ảnh mới thất bại", string message = "Đã có lỗi xảy ra khi thêm hình ảnh mới. Vui lòng thử lại sau.")
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
            Add_NewImage_Canvas.SetActive(true);
            List_Image_Canvas.SetActive(false);
            ConfirmRequestCanvas.SetActive(false);
        });
    }

    private void OpenSuccessDialog()
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");
        var horizontalGroupTransform = backgroundTransform.Find("Horizontal_Group");

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Icon_For_Dialog");
        backgroundTransform.Find("Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn đã thành công thêm hình ảnh <b><color=#004C8A>{finalImageName}</b></color> vào hệ thống";
        backgroundTransform.Find("Dialog_Title").GetComponent<TMP_Text>().text = "thêm hình ảnh mới thành công";

        var confirmButton = horizontalGroupTransform.Find("Confirm_Button").GetComponent<Button>();
        var backButton = horizontalGroupTransform.Find("Back_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Success_Back_Button_Background");
        confirmButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Tiếp tục thêm mới";
        backButton.transform.Find("Text").GetComponent<TMP_Text>().text = "Trở lại danh sách";

        confirmButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
            Add_NewImage_Canvas.SetActive(true);
            List_Image_Canvas.SetActive(false);
            ConfirmRequestCanvas.SetActive(false);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
            Add_NewImage_Canvas.SetActive(false);
            List_Image_Canvas.SetActive(true);
            ConfirmRequestCanvas.SetActive(false);
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
        if (GlobalVariable.APIRequestType.Contains("POST_Image"))
        {
            OpenErrorDialog();
        }
    }

    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("POST_Image"))
        {
            Show_Toast.Instance.ShowToast("success", "Thêm hình ảnh mới thành công");
            OpenSuccessDialog();
        }
    }

    public void DisplayList(List<ImageInformationModel> models) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
    public void DisplayDetail(ImageInformationModel model) { }
}
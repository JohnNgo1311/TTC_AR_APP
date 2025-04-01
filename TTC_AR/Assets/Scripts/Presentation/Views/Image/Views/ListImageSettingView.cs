using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListImageSettingView : MonoBehaviour, IImageView
{
    private ImagePresenter _presenter;

    [Header("Canvas")]
    public GameObject List_Image_Canvas;
    public GameObject Add_New_Image_Canvas;

    [Header("Prefab & Layout")]
    public GameObject Image_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listImageItems = new List<GameObject>();

    [Header("Dialog")]
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    [Header("Preview")]
    public GameObject PreviewImageCanvas;
    public Button BackButtonFromPreviewCanvas;
    public Image PreviewImage;
    public TMP_Text PreviewImageName;

    void Awake()
    {
        _presenter = new ImagePresenter(this,
        ManagerLocator.Instance.ImageManager._IImageService);
    }
    void OnEnable()
    {
        LoadListImage();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        Image_Item_Prefab.SetActive(true);
        foreach (var item in listImageItems)
        {
            if (item != Image_Item_Prefab)
                Destroy(item);
        }
        listImageItems.Clear();
    }

    public void LoadListImage()
    {
        RefreshList();
        _presenter.LoadListImage(GlobalVariable.GrapperId);
    }
    public void DisplayList(List<ImageInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int ImageIndex = models.IndexOf(model);
                Debug.Log(ImageIndex);
                var newImageItem = Instantiate(Image_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newImageItemTransform = newImageItem.transform;
                var previewButton = newImageItem.GetComponent<Button>();
                previewButton.onClick.RemoveAllListeners();
                previewButton.onClick.AddListener(() => OpenPreviewImageCanvas(model));
                newImageItemTransform.Find("Preview_Image_Name").GetComponent<TMP_Text>().text = model.Name;
                newImageItemTransform.Find("Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleImageItem(newImageItem, model));
                listImageItems.Add(newImageItem);
            }
        }
        else
        {
            Debug.Log("No Images found");
        }
        Image_Item_Prefab.SetActive(false);
    }

    private async void OpenPreviewImageCanvas(ImageInformationModel model)
    {
        ShowProgressBar("Đang tải ảnh...", "...");
        PreviewImageCanvas.SetActive(true);
        BackButtonFromPreviewCanvas.onClick.RemoveAllListeners();
        BackButtonFromPreviewCanvas.onClick.AddListener(() => PreviewImageCanvas.SetActive(false));
        PreviewImageName.text = model.Name;

        await LoadImage.Instance.LoadImageFromUrlAsync(
                    imageName: "https://firebasestorage.googleapis.com/v0/b/ttc-project-81b04.appspot.com/o/JB_Outdoor_Location%2FJB101_Location.jpg?alt=media&token=98ea4fb2-7093-425a-802e-458579c12fbd",
                    image: PreviewImage,
                    true
                );
        StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(PreviewImage));

        await Task.Delay(1000);

        HideProgressBar();
    }
    private void DeleImageItem(GameObject ImageItem, ImageInformationModel model)
    {
        OpenDeleteWarningDialog(ImageItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_Image_Canvas.SetActive(true);
        List_Image_Canvas.SetActive(false);
    }

    private void OpenDeleteWarningDialog(GameObject ImageItem, ImageInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa hình ảnh <b><color=red>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa hình ảnh khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");

        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listImageItems.Remove(ImageItem);
            Debug.Log(model.Id);
            _presenter.DeleteImage(model.Id);
            DialogTwoButton.SetActive(false);
            Destroy(ImageItem);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }
    private void OpenErrorDialog(string title = "Xóa hình ảnh thất bại", string message = "Đã có lỗi xảy ra khi xóa hình ảnh khỏi hệ thống. Vui lòng thử lại sau")
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");

        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message;

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
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
        if (GlobalVariable.APIRequestType.Contains("GET_Image_List"))
        {
            OpenErrorDialog(title: "Tải danh sách thất bại", message: "Đã có lỗi xảy ra khi tải danh sách. Vui lòng thử lại sau");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Image"))
        {
            OpenErrorDialog();
        }
    }
    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("GET_Image_List"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Image"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa hình ảnh thành công");
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
    }

    // Không dùng trong ListView
    public void DisplayDetail(ImageInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
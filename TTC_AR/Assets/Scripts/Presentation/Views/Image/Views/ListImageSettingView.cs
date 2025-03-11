using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListImageSettingView : MonoBehaviour, IImageView
{
    [Header("Canvas")]
    public GameObject List_Image_Canvas;
    public GameObject Add_New_Image_Canvas;
    public GameObject Update_Image_Canvas;

    public GameObject Image_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listImageItems = new List<GameObject>();


    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private ImagePresenter _presenter;

    void Awake()
    {
        ImageManager ImageManager = FindObjectOfType<ImageManager>();
        _presenter = new ImagePresenter(this, ImageManager._IImageService);
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
        _presenter.LoadListImage(GlobalVariable.companyId);

    }
    public void DisplayList(List<ImageInformationModel> models)
    {
        if (models.Count > 0)
        {
            foreach (var model in models)
            {
                int ImageIndex = models.IndexOf(model);
                Debug.Log(ImageIndex);
                var newImageItem = Instantiate(Image_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newImageItemTransform = newImageItem.transform;
                Transform newImageItemPreviewInforGroup = newImageItemTransform.GetChild(0);
                newImageItemPreviewInforGroup.Find("Preview_Image_Code").GetComponent<TMP_Text>().text = model.Name;
                Transform newImageItemPreviewButtonGroup = newImageItemTransform.GetChild(1);
                listImageItems.Add(newImageItem);
                // newImageItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditImageItem(model.Id));
                newImageItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleImageItem(newImageItem, model));
            }
        }
        else
        {
            Debug.Log("No Images found");
        }
        Image_Item_Prefab.SetActive(false);

    }

    // private void EditImageItem(int id)
    // {

    //     GlobalVariable.ImageId = id;

    //     OpenUpdateCanvas();
    // }
    private void DeleImageItem(GameObject ImageItem, ImageInformationModel model)
    {
        OpenDeleteWarningDialog(ImageItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_Image_Canvas.SetActive(true);
        List_Image_Canvas.SetActive(false);
        Update_Image_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        Update_Image_Canvas.SetActive(true);
        List_Image_Canvas.SetActive(false);
        Add_New_Image_Canvas.SetActive(false);
    }

    private void OpenDeleteWarningDialog(GameObject ImageItem, ImageInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin hình ảnh <color=#FF0000><b>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa hình ảnh khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");


        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            Destroy(ImageItem);
            listImageItems.Remove(ImageItem);
            _presenter.DeleteImage(model.Id);
            DialogTwoButton.SetActive(false);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }


    private void OpenErrorDeletingDialog()
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");


        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi xóa hình ảnh khỏi hệ thống. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa hình ảnh thất bại";


        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });

    }

    private void OpenErrorCreateNewDialog()
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");

        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi thêm hình ảnh này khỏi hệ thống. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Thêm hình ảnh thất bại";


        backButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(() =>
        {
            DialogOneButton.SetActive(false);
        });
    }


    private void OpenErrorGetListDialog()
    {
        DialogOneButton.SetActive(true);
        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");

        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi tải danh sách. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Tải danh sách thất bại";


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



    public void ShowLoading() => ShowProgressBar("Loading", "Đang tải dữ liệu...");
    public void HideLoading() => HideProgressBar();
    public void ShowError(string message)
    {
        switch (GlobalVariable.APIRequestType)
        {
            case "GET_Image_List":
                OpenErrorGetListDialog();
                break;
            case "DELETE_Image":
                OpenErrorDeletingDialog();
                break;
        }
    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        switch (GlobalVariable.APIRequestType)
        {
            case "GET_Image_List":
                Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
                break;
            case "DELETE_Image":
                Show_Toast.Instance.ShowToast("success", "Xóa hình ảnh thành công");
                break;
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    // Không dùng trong ListView
    public void DisplayDetail(ImageInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
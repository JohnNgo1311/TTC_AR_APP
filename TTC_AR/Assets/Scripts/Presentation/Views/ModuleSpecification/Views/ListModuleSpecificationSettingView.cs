using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListModuleSpecificationSettingView : MonoBehaviour, IModuleSpecificationView
{
    [Header("Canvas")]
    public GameObject List_ModuleSpecification_Canvas;
    public GameObject Add_New_ModuleSpecification_Canvas;
    public GameObject Update_ModuleSpecification_Canvas;

    public GameObject ModuleSpecification_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listModuleSpecificationItems = new List<GameObject>();


    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private ModuleSpecificationPresenter _presenter;

    void Awake()
    {
        ModuleSpecificationManager ModuleSpecificationManager = FindObjectOfType<ModuleSpecificationManager>();
        _presenter = new ModuleSpecificationPresenter(this, ModuleSpecificationManager._IModuleSpecificationService);
    }

    void OnEnable()
    {
        LoadListModuleSpecification();

    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        ModuleSpecification_Item_Prefab.SetActive(true);
        foreach (var item in listModuleSpecificationItems)
        {
            if (item != ModuleSpecification_Item_Prefab)
                Destroy(item);
        }
        listModuleSpecificationItems.Clear();
    }

    public void LoadListModuleSpecification()
    {
        RefreshList();
        _presenter.LoadListModuleSpecification(GlobalVariable.companyId);

    }
    public void DisplayList(List<ModuleSpecificationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int ModuleSpecificationIndex = models.IndexOf(model);
                Debug.Log(ModuleSpecificationIndex);
                var newModuleSpecificationItem = Instantiate(ModuleSpecification_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newModuleSpecificationItemTransform = newModuleSpecificationItem.transform;
                Transform newModuleSpecificationItemPreviewInforGroup = newModuleSpecificationItemTransform.GetChild(0);
                newModuleSpecificationItemPreviewInforGroup.Find("Preview_ModuleSpecification_Code").GetComponent<TMP_Text>().text = model.Code;
                Transform newModuleSpecificationItemPreviewButtonGroup = newModuleSpecificationItemTransform.GetChild(1);
                listModuleSpecificationItems.Add(newModuleSpecificationItem);
                newModuleSpecificationItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditModuleSpecificationItem(model.Id));
                newModuleSpecificationItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleModuleSpecificationItem(newModuleSpecificationItem, model));
            }
        }
        else
        {
            Debug.Log("No ModuleSpecifications found");
        }
        ModuleSpecification_Item_Prefab.SetActive(false);

    }

    private void EditModuleSpecificationItem(string id)
    {

        GlobalVariable.ModuleSpecificationId = id;

        OpenUpdateCanvas();
    }
    private void DeleModuleSpecificationItem(GameObject ModuleSpecificationItem, ModuleSpecificationModel model)
    {
        OpenDeleteWarningDialog(ModuleSpecificationItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_ModuleSpecification_Canvas.SetActive(true);
        List_ModuleSpecification_Canvas.SetActive(false);
        Update_ModuleSpecification_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        Update_ModuleSpecification_Canvas.SetActive(true);
        List_ModuleSpecification_Canvas.SetActive(false);
        Add_New_ModuleSpecification_Canvas.SetActive(false);
    }

    private void OpenDeleteWarningDialog(GameObject ModuleSpecificationItem, ModuleSpecificationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin loại Module <b><color =#004C8A>{model.Code}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa loại Module khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");


        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            Destroy(ModuleSpecificationItem);
            listModuleSpecificationItems.Remove(ModuleSpecificationItem);
            _presenter.DeleteModuleSpecification(model.Id);
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


        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi xóa loại Module khỏi hệ thống. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa loại Module thất bại";


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

        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi thêm loại Module này khỏi hệ thống. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Thêm loại Module thất bại";


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



    public void ShowLoading(string title) => ShowProgressBar(title, "Đang tải dữ liệu...");
    public void HideLoading() => HideProgressBar();
    public void ShowError(string message)
    {

        if (GlobalVariable.APIRequestType.Contains("GET_ModuleSpecification_List"))
        {
            OpenErrorGetListDialog();
        }
        if (GlobalVariable.APIRequestType.Contains("DELETE_ModuleSpecification"))
        {
            OpenErrorDeletingDialog();
        }

    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();

        if (GlobalVariable.APIRequestType.Contains("GET_ModuleSpecification_List"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        if (GlobalVariable.APIRequestType.Contains("DELETE_ModuleSpecification"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa loại Module thành công");

        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    // Không dùng trong ListView
    public void DisplayDetail(ModuleSpecificationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
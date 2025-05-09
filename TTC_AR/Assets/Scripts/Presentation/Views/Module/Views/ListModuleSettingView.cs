using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListModuleSettingView : MonoBehaviour, IModuleView
{
    [Header("Canvas")]
    public GameObject List_Module_Canvas;
    public GameObject Add_New_Module_Canvas;
    public GameObject Update_Module_Canvas;

    public GameObject Module_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listModuleItems = new List<GameObject>();

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private ModulePresenter _presenter;

    void Awake()
    {
        // var ModuleManager = FindObjectOfType<ModuleManager>();
        _presenter = new ModulePresenter(this,
        ManagerLocator.Instance.ModuleManager._IModuleService);
        // ModuleManager._IModuleService
    }
    void OnEnable()
    {
        LoadListModule();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        Module_Item_Prefab.SetActive(true);
        foreach (var item in listModuleItems)
        {
            if (item != Module_Item_Prefab)
                Destroy(item);
        }
        listModuleItems.Clear();
    }

    public void LoadListModule()
    {
        RefreshList();
        _presenter.LoadListModule(GlobalVariable.GrapperId);
    }
    public void DisplayList(List<ModuleInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                // int ModuleIndex = models.IndexOf(model);
                // Debug.Log(ModuleIndex);
                var newModuleItem = Instantiate(Module_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newModuleItemTransform = newModuleItem.transform;
                Transform newModuleItemPreviewInforGroup = newModuleItemTransform.GetChild(0);
                newModuleItemPreviewInforGroup.Find("Preview_Module_Name").GetComponent<TMP_Text>().text = model.Name;
                Transform newModuleItemPreviewButtonGroup = newModuleItemTransform.GetChild(1);
                listModuleItems.Add(newModuleItem);
                newModuleItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditModuleItem(model.Id));
                newModuleItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleModuleItem(newModuleItem, model));
            }
        }
        else
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công nhưng danh sách trống");
            StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(2f));
        }
        Module_Item_Prefab.SetActive(false);
    }

    private void EditModuleItem(int id)
    {
        GlobalVariable.moduleId = id;
        OpenUpdateCanvas();
    }
    private void DeleModuleItem(GameObject ModuleItem, ModuleInformationModel model)
    {
        OpenDeleteWarningDialog(ModuleItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_Module_Canvas.SetActive(true);
        List_Module_Canvas.SetActive(false);
        Update_Module_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_Module_Canvas.SetActive(false);
        Add_New_Module_Canvas.SetActive(false);
        Update_Module_Canvas.SetActive(true);
    }

    private void OpenDeleteWarningDialog(GameObject ModuleItem, ModuleInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin Module IO <b><color =#004C8A>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa Module IO khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");

        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listModuleItems.Remove(ModuleItem);
            // Debug.Log(model.Id);
            _presenter.DeleteModule(model.Id);
            Destroy(ModuleItem);
            DialogTwoButton.SetActive(false);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }
    private void OpenErrorDialog(string title = "Xóa Module IO thất bại", string message = "Đã có lỗi xảy ra khi xóa Module IO khỏi hệ thống. Vui lòng thử lại sau")
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
        if (GlobalVariable.APIRequestType.Contains("GET_Module_List"))
        {
            OpenErrorDialog(title: "Tải danh sách thất bại", message: message);
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Module"))
        {
            OpenErrorDialog(title: "Xóa Module IO thất bại", message: message);
        }
    }
    public void ShowSuccess(string message)
    {
        if (GlobalVariable.APIRequestType.Contains("GET_Module_List"))
        {
            Show_Toast.Instance.ShowToast("success", message);
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Module"))
        {
            Show_Toast.Instance.ShowToast("success", message);
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
    }

    // Không dùng trong ListView
    public void DisplayDetail(ModuleInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
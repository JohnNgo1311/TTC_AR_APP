using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListJBSettingView : MonoBehaviour, IJBView
{
    [Header("Canvas")]
    public GameObject List_JB_Canvas;
    public GameObject Add_New_JB_Canvas;
    public GameObject Update_JB_Canvas;

    public GameObject JB_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listJBItems = new List<GameObject>();

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private JBPresenter _presenter;

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new JBPresenter(this, ManagerLocator.Instance.JBManager._IJBService);
        // DeviceManager._IDeviceService
    }
    void OnEnable()
    {
        LoadListJB();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        JB_Item_Prefab.SetActive(true);
        foreach (var item in listJBItems)
        {
            if (item != JB_Item_Prefab)
                Destroy(item);
        }
        listJBItems.Clear();
    }

    public void LoadListJB()
    {
        RefreshList();
        _presenter.LoadListJBGeneral(GlobalVariable.GrapperId);
    }
    public void DisplayList(List<JBInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int JBIndex = models.IndexOf(model);
                Debug.Log(JBIndex);
                var newJBItem = Instantiate(JB_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newJBItemTransform = newJBItem.transform;
                Transform newJBItemPreviewInforGroup = newJBItemTransform.GetChild(0);
                newJBItemPreviewInforGroup.Find("Preview_JB_Name").GetComponent<TMP_Text>().text = model.Name;
                Transform newJBItemPreviewButtonGroup = newJBItemTransform.GetChild(1);
                listJBItems.Add(newJBItem);
                newJBItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditJBItem(model.Id));
                newJBItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleJBItem(newJBItem, model));
            }
        }
        else
        {
            Debug.Log("No JBs found");
        }
        JB_Item_Prefab.SetActive(false);
    }

    private void EditJBItem(string id)
    {
        GlobalVariable.JBId = id;
        OpenUpdateCanvas();
    }
    private void DeleJBItem(GameObject JBItem, JBInformationModel model)
    {
        OpenDeleteWarningDialog(JBItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_JB_Canvas.SetActive(true);
        List_JB_Canvas.SetActive(false);
        Update_JB_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_JB_Canvas.SetActive(false);
        Add_New_JB_Canvas.SetActive(false);
        Update_JB_Canvas.SetActive(true);
    }

    private void OpenDeleteWarningDialog(GameObject JBItem, JBInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin tủ JB/TSD <b><color =#004C8A>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa tủ JB/TSD khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");

        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listJBItems.Remove(JBItem);
            Debug.Log(model.Id);
            _presenter.DeleteJB(model.Id);
            DialogTwoButton.SetActive(false);
            Destroy(JBItem);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }
    private void OpenErrorDialog(string title = "Xóa tủ JB/TSD thất bại", string message = "Đã có lỗi xảy ra khi xóa tủ JB/TSD khỏi hệ thống. Vui lòng thử lại sau")
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
        if (GlobalVariable.APIRequestType.Contains("GET_JB_List_General"))
        {
            OpenErrorDialog(title: "Tải danh sách thất bại", message: "Đã có lỗi xảy ra khi tải danh sách. Vui lòng thử lại sau");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_JB"))
        {
            OpenErrorDialog();
        }
    }
    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("GET_JB_List_General"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_JB"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa tủ JB/TSD thành công");
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
    }

    // Không dùng trong ListView
    public void DisplayDetail(JBInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListFieldDeviceSettingView : MonoBehaviour, IFieldDeviceView
{
    [Header("Canvas")]
    public GameObject List_FieldDevice_Canvas;
    public GameObject Add_New_FieldDevice_Canvas;
    public GameObject Update_FieldDevice_Canvas;

    public GameObject FieldDevice_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listFieldDeviceItems = new List<GameObject>();


    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private FieldDevicePresenter _presenter;

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new FieldDevicePresenter(this, ManagerLocator.Instance.FieldDeviceManager._IFieldDeviceService);
        // DeviceManager._IDeviceService
    }

    void OnEnable()
    {
        LoadListFieldDevice();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        FieldDevice_Item_Prefab.SetActive(true);
        foreach (var item in listFieldDeviceItems)
        {
            if (item != FieldDevice_Item_Prefab)
                Destroy(item);
        }
        listFieldDeviceItems.Clear();
    }

    public void LoadListFieldDevice()
    {
        RefreshList();
        _presenter.LoadListFieldDevice(GlobalVariable.GrapperId);

    }
    public void DisplayList(List<FieldDeviceInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int FieldDeviceIndex = models.IndexOf(model);
                Debug.Log(FieldDeviceIndex);
                var newFieldDeviceItem = Instantiate(FieldDevice_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newFieldDeviceItemTransform = newFieldDeviceItem.transform;
                Transform newFieldDeviceItemPreviewInforGroup = newFieldDeviceItemTransform.GetChild(0);
                newFieldDeviceItemPreviewInforGroup.Find("Preview_FieldDevice_Name").GetComponent<TMP_Text>().text = model.Name;
                Transform newFieldDeviceItemPreviewButtonGroup = newFieldDeviceItemTransform.GetChild(1);
                listFieldDeviceItems.Add(newFieldDeviceItem);
                newFieldDeviceItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditFieldDeviceItem(model.Id));
                newFieldDeviceItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleFieldDeviceItem(newFieldDeviceItem, model));
            }
        }
        else
        {
            Debug.Log("No FieldDevices found");
        }
        FieldDevice_Item_Prefab.SetActive(false);

    }

    private void EditFieldDeviceItem(string id)
    {
        GlobalVariable.FieldDeviceId = id;
        OpenUpdateCanvas();
    }
    private void DeleFieldDeviceItem(GameObject FieldDeviceItem, FieldDeviceInformationModel model)
    {
        OpenDeleteWarningDialog(FieldDeviceItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_FieldDevice_Canvas.SetActive(true);
        List_FieldDevice_Canvas.SetActive(false);
        Update_FieldDevice_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_FieldDevice_Canvas.SetActive(false);
        Add_New_FieldDevice_Canvas.SetActive(false);
        Update_FieldDevice_Canvas.SetActive(true);

    }

    private void OpenDeleteWarningDialog(GameObject FieldDeviceItem, FieldDeviceInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin thiết bi trường <b><color =#004C8A>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa thiết bi trường khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");


        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listFieldDeviceItems.Remove(FieldDeviceItem);
            Debug.Log(model.Id);
            _presenter.DeleteFieldDevice(model.Id);
            DialogTwoButton.SetActive(false);
            Destroy(FieldDeviceItem);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }


    private void OpenErrorDialog(string title = "Xóa thiết bi trường thất bại", string message = "Đã có lỗi xảy ra khi xóa thiết bi trường khỏi hệ thống. Vui lòng thử lại sau")
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
        if (GlobalVariable.APIRequestType.Contains("GET_FieldDevice_List"))
        {
            OpenErrorDialog(title: "Tải danh sách thiết bi trường thất bại", message: "Đã có lỗi xảy ra khi tải danh sách thiết bi trường. Vui lòng thử lại sau");
        }
        if (GlobalVariable.APIRequestType.Contains("DELETE_FieldDevice"))
        {
            OpenErrorDialog();
        }

    }
    public void ShowSuccess()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        if (GlobalVariable.APIRequestType.Contains("GET_FieldDevice_List"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        if (GlobalVariable.APIRequestType.Contains("DELETE_FieldDevice"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa thiết bi trường thành công");

        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    // Không dùng trong ListView
    public void DisplayDetail(FieldDeviceInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
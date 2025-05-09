using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListDeviceSettingView : MonoBehaviour, IDeviceView
{
    [Header("Canvas")]
    public GameObject List_Device_Canvas;
    public GameObject Add_New_Device_Canvas;
    public GameObject Update_Device_Canvas;

    public GameObject Device_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listDeviceItems = new List<GameObject>();

    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private DevicePresenter _presenter;

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new DevicePresenter(this,
        ManagerLocator.Instance.DeviceManager._IDeviceService);
        // DeviceManager._IDeviceService
    }
    void OnEnable()
    {
        LoadListDevice();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        Device_Item_Prefab.SetActive(true);

        foreach (var item in listDeviceItems)
        {
            if (item != Device_Item_Prefab)
                Destroy(item);
        }

        listDeviceItems.Clear();
    }

    public void LoadListDevice()
    {
        RefreshList();
        _presenter.LoadListDeviceGeneral(GlobalVariable.GrapperId);
    }
    public void DisplayList(List<DeviceInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int DeviceIndex = models.IndexOf(model);
                Debug.Log(DeviceIndex);
                var newDeviceItem = Instantiate(Device_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newDeviceItemTransform = newDeviceItem.transform;
                Transform newDeviceItemPreviewInforGroup = newDeviceItemTransform.GetChild(0);
                newDeviceItemPreviewInforGroup.Find("Preview_Device_Code").GetComponent<TMP_Text>().text = model.Code;
                Transform newDeviceItemPreviewButtonGroup = newDeviceItemTransform.GetChild(1);
                listDeviceItems.Add(newDeviceItem);
                newDeviceItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditDeviceItem(model.Id));
                newDeviceItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleDeviceItem(newDeviceItem, model));
            }
        }
        else
        {
            Debug.Log("No Devices found");
        }
        Device_Item_Prefab.SetActive(false);
    }

    private void EditDeviceItem(int id)
    {
        GlobalVariable.deviceId = id;
        OpenUpdateCanvas();
    }
    private void DeleDeviceItem(GameObject DeviceItem, DeviceInformationModel model)
    {
        OpenDeleteWarningDialog(DeviceItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_Device_Canvas.SetActive(true);
        List_Device_Canvas.SetActive(false);
        Update_Device_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_Device_Canvas.SetActive(false);
        Add_New_Device_Canvas.SetActive(false);
        Update_Device_Canvas.SetActive(true);
    }

    private void OpenDeleteWarningDialog(GameObject DeviceItem, DeviceInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin thiết bị <b><color =#004C8A>{model.Code}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa thiết bị khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");

        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listDeviceItems.Remove(DeviceItem);
            Debug.Log(model.Id);
            _presenter.DeleteDevice(model.Id);
            DialogTwoButton.SetActive(false);
            Destroy(DeviceItem);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }
    private void OpenErrorDialog(string title = "Xóa thiết bị thất bại", string message = "Đã có lỗi xảy ra khi xóa thiết bị khỏi hệ thống. Vui lòng thử lại sau")
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
        if (GlobalVariable.APIRequestType.Contains("GET_Device_List_General"))
        {
            OpenErrorDialog(title: "Tải danh sách thất bại", message: "Đã có lỗi xảy ra khi tải danh sách. Vui lòng thử lại sau");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Device"))
        {
            OpenErrorDialog();
        }
    }
    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("GET_Device_List_General"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Device"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa thiết bị thành công");
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
    }

    // Không dùng trong ListView
    public void DisplayDetail(DeviceInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }
}
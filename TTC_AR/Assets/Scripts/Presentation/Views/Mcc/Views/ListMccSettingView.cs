using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListMccSettingView : MonoBehaviour, IMccView
{
    [Header("Canvas")]
    public GameObject List_Mcc_Canvas;
    public GameObject Add_New_Mcc_Canvas;
    public GameObject Update_Mcc_Canvas;

    public GameObject Mcc_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    private List<GameObject> listMccItems = new List<GameObject>();


    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;
    private MccPresenter _presenter;

    void Awake()
    {
        // var DeviceManager = FindObjectOfType<DeviceManager>();
        _presenter = new MccPresenter(this, ManagerLocator.Instance.MccManager._IMccService);
        // DeviceManager._IDeviceService
    }
    void OnEnable()
    {
        LoadListMcc();
    }
    void OnDisable()
    {
    }

    private void RefreshList()
    {
        Mcc_Item_Prefab.SetActive(true);
        foreach (var item in listMccItems)
        {
            if (item != Mcc_Item_Prefab)
                Destroy(item);
        }
        listMccItems.Clear();
    }

    public void LoadListMcc()
    {
        RefreshList();
        _presenter.LoadListMcc(GlobalVariable.GrapperId);

    }
    public void DisplayList(List<MccInformationModel> models)
    {
        if (models.Any())
        {
            foreach (var model in models)
            {
                int MccIndex = models.IndexOf(model);
                Debug.Log(MccIndex);
                var newMccItem = Instantiate(Mcc_Item_Prefab, Parent_Vertical_Layout_Group.transform);
                Transform newMccItemTransform = newMccItem.transform;
                Transform newMccItemPreviewInforGroup = newMccItemTransform.GetChild(0);
                newMccItemPreviewInforGroup.Find("Preview_Mcc_CabinetCode").GetComponent<TMP_Text>().text = model.CabinetCode;
                Transform newMccItemPreviewButtonGroup = newMccItemTransform.GetChild(1);
                listMccItems.Add(newMccItem);
                newMccItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditMccItem(model.Id));
                newMccItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleMccItem(newMccItem, model));
            }
        }
        else
        {
            Debug.Log("No Mccs found");
        }
        Mcc_Item_Prefab.SetActive(false);

    }

    private void EditMccItem(int id)
    {
        GlobalVariable.mccId = id;
        OpenUpdateCanvas();
    }
    private void DeleMccItem(GameObject MccItem, MccInformationModel model)
    {
        OpenDeleteWarningDialog(MccItem, model);
    }

    public void OpenAddNewCanvas()
    {
        Add_New_Mcc_Canvas.SetActive(true);
        List_Mcc_Canvas.SetActive(false);
        Update_Mcc_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_Mcc_Canvas.SetActive(false);
        Add_New_Mcc_Canvas.SetActive(false);
        Update_Mcc_Canvas.SetActive(true);

    }

    private void OpenDeleteWarningDialog(GameObject MccItem, MccInformationModel model)
    {
        DialogTwoButton.SetActive(true);

        var backgroundTransform = DialogTwoButton.transform.Find("Background");

        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin tủ Mcc <b><color =#004C8A>{model.CabinetCode}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

        var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa tủ Mcc khỏi hệ thống?";

        backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

        confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");


        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

        confirmButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            listMccItems.Remove(MccItem);
            Debug.Log(model.Id);
            _presenter.DeleteMcc(model.Id);
            DialogTwoButton.SetActive(false);
            Destroy(MccItem);
        });
        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }


    private void OpenErrorDialog(string title = "Xóa tủ Mcc thất bại", string message = "Đã có lỗi xảy ra khi xóa tủ Mcc khỏi hệ thống. Vui lòng thử lại sau")
    {
        DialogOneButton.SetActive(true);

        var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

        backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

        var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");

        var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Đã có lỗi xảy ra khi xóa tủ Mcc khỏi hệ thống. Vui lòng thử lại sau";

        var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa tủ Mcc thất bại";


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
        if (GlobalVariable.APIRequestType.Contains("GET_Mcc_List"))
        {
            OpenErrorDialog(title: "Tải danh sách tủ Mcc thất bại", message: "Đã có lỗi xảy ra khi tải danh sách tủ Mcc. Vui lòng thử lại sau.");
        }
        else if (GlobalVariable.APIRequestType.Contains("DELETE_Mcc"))
        {
            OpenErrorDialog();
        }

    }
    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("GET_Mcc_List"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
        }
        if (GlobalVariable.APIRequestType.Contains("DELETE_Mcc"))
        {
            Show_Toast.Instance.ShowToast("success", "Xóa tủ Mcc thành công");
        }

        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    // Không dùng trong ListView
    public void DisplayDetail(MccInformationModel model) { }
    public void DisplayCreateResult(bool success) { }
    public void DisplayUpdateResult(bool success) { }
    public void DisplayDeleteResult(bool success) { }

    public void DisplayFieldDeviceList(List<FieldDeviceInformationModel> models)
    {
    }
}
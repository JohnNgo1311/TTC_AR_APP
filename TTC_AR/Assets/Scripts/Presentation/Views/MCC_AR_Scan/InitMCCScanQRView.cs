using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitMCCScanQRView : MonoBehaviour, IMccView
{
    [Header("Canvas")]
    public GameObject List_FieldDevices_Panel;
    public GameObject General_Panel;

    private MccPresenter _presenter;
    private int grapperId;

    void Awake()
    {
        _presenter = new MccPresenter(this, ManagerLocator.Instance.MccManager._IMccService);
    }
    void OnEnable()
    {
        grapperId = GlobalVariable.GrapperId;
        LoadListMCC();
    }
    void OnDisable()
    {
    }

    public void LoadListMCC()
    {
        _presenter.LoadListMcc(grapperId);
    }
    public void DisplayFieldDeviceList(List<FieldDeviceInformationModel> models)
    {
    }
    public void DisplayList(List<MccInformationModel> models)
    {
        GlobalVariable.temp_Dictionary_MCCInformationModel = models.ToDictionary(m => m.CabinetCode, m => m);
        Debug.Log("DisplayList: " + models.Count);
    }


    public void OpenGeneralPanel()
    {
        General_Panel.SetActive(true);
        List_FieldDevices_Panel.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        List_FieldDevices_Panel.SetActive(false);
        General_Panel.SetActive(false);

    }

    private void OpenDeleteWarningDialog(GameObject MccItem, MccInformationModel model)
    {

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


}
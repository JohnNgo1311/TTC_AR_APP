using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//! Script này sử dụng cho Dropdown trong Device Search trong mỗi module
//! Lọc theo GlobalVariable_Search_Devices.devices_Model_By_Grapper
public class SearchDeviceFromModule : MonoBehaviour
{
    public List<TMP_Text> deviceInformation = new List<TMP_Text>();
    private List<DeviceModel> listDeviceFromModule;
    private DeviceModel deviceInfor;
    public TMP_Dropdown dropdown;
    public GameObject contentPanel;

    public Button nav_JB_TSD_Detail_button;
    private string moduleName = "D1.0.I";
    private const string noDeviceMessage = "không có thiết bị kết nối";

    [SerializeField]
    private Canvas module_Canvas;
    private RectTransform list_Devices_Transform;
    private RectTransform jb_TSD_General_Transform;
    private RectTransform jb_TSD_Detail_Transform;
    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        list_Devices_Transform = module_Canvas.gameObject.transform.Find("List_Devices").GetComponent<RectTransform>();
        jb_TSD_General_Transform = module_Canvas.gameObject.transform.Find("JB_TSD_General_Panel").GetComponent<RectTransform>();
        jb_TSD_Detail_Transform = module_Canvas.gameObject.transform.Find("Detail_JB_TSD").GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        module_Canvas = GetComponentInParent<Canvas>();

        if (module_Canvas.gameObject.activeSelf)
        {
            // Tách moduleName một lần và lưu trữ
            moduleName = module_Canvas.name.Split('_')[0];
            listDeviceFromModule = Get_List_Device_By_Module(moduleName);

            // Xóa các tùy chọn trước đó
            dropdown.options.Clear();

            if (listDeviceFromModule.Count > 0)
            {
                // Chuyển đổi danh sách thiết bị thành danh sách tùy chọn cho dropdown
                foreach (var device in listDeviceFromModule)
                {
                    dropdown.options.Add(new TMP_Dropdown.OptionData(device.code));
                }

                // Đảm bảo rằng option1 luôn được chọn
                dropdown.value = 0;
                dropdown.RefreshShownValue();

                dropdown.onValueChanged.AddListener(OnValueChange);
                OnValueChange(0); // Gọi OnValueChange để cập nhật thông tin thiết bị đầu tiên

                /* deviceInfor = listDeviceFromModule[0];
                 UpdateDeviceInformation(deviceInfor);*

                 if (!contentPanel.activeSelf)
                 {
                     contentPanel.SetActive(true);
                 }*/

            }
            else
            {
                // Nếu không có thiết bị nào, thêm tùy chọn mặc định
                dropdown.options.Add(new TMP_Dropdown.OptionData(noDeviceMessage));
                dropdown.value = 0;
                dropdown.RefreshShownValue();

                // Ẩn contentPanel và xóa thông tin thiết bị
                contentPanel.SetActive(false);
                ClearDeviceInformation();
            }
        }
    }

    private void OnDisable()
    {
        // Xóa các tùy chọn trong dropdown
        dropdown.options.Clear();

        // Xóa thông tin hiển thị về thiết bị
        ClearDeviceInformation();

        // Ẩn contentPanel
        contentPanel.SetActive(false);

        // Gỡ sự kiện khi OnDisable được gọi
        dropdown.onValueChanged.RemoveListener(OnValueChange);
    }

    public void OnValueChange(int value)
    {
        if (value >= 0 && value < listDeviceFromModule.Count)
        {
            deviceInfor = listDeviceFromModule[value];
            UpdateDeviceInformation(deviceInfor);

            if (!contentPanel.activeSelf)
            {
                contentPanel.SetActive(true);
            }
        }
        else
        {
            // Nếu chọn tùy chọn mặc định "không có thiết bị kết nối"
            contentPanel.SetActive(false);
            ClearDeviceInformation();
        }
    }

    private List<DeviceModel> Get_List_Device_By_Module(string moduleName)
    {
        return GlobalVariable_Search_Devices.devices_Model_By_Grapper.FindAll(
            device => device.ioAddress.StartsWith(moduleName + ".")
        );

    }

    private void UpdateDeviceInformation(DeviceModel device)
    {
        deviceInformation[0].text = device.code;
        deviceInformation[1].text = device.function;
        deviceInformation[2].text = device.rangeMeasurement;
        deviceInformation[3].text = device.ioAddress;

        var jbParts = device.jbConnection.Split('_');
        deviceInformation[4].text = jbParts[0];
        deviceInformation[5].text = jbParts.Length > 1 ? jbParts[1] : string.Empty;
        // Đảm bảo không gán nhiều lần
        nav_JB_TSD_Detail_button.onClick.RemoveAllListeners();
        nav_JB_TSD_Detail_button.onClick.AddListener(() =>
        {
            GlobalVariable.navigate_from_List_Devices = true;
            GlobalVariable.navigate_from_JB_TSD_General = false;
            NavigateJBDetailScreen(device.jbConnection);
        });
    }

    private void ClearDeviceInformation()
    {
        foreach (var info in deviceInformation)
        {
            info.text = string.Empty;
        }
    }

    /* 
    private void NavigateJBDetailScreen(string jB_TSD_Name)
    {
        Debug.Log("Navigate to JB Detail Screen + " + jB_TSD_Name);
    }
    */

    public void NavigateJBDetailScreen(string jB_TSD_Connection)
    {
        GlobalVariable.jb_TSD_Title = jB_TSD_Connection; // Name_Location of JB
        var jobDetails = GlobalVariable.jb_TSD_Title.Split('_'); // EX: JB100_Hầm Cáp MCC
        GlobalVariable.jb_TSD_Name = jobDetails[0]; // jb_name: JB100
                                                    // Debug.Log("jb_name: " + jb_name);
        GlobalVariable.jb_TSD_Location = jobDetails.Length > 1 ? jobDetails[1] : string.Empty; // jb_location: Hầm Cáp MCC
                                                                                               // Debug.Log("jb_location: " + jb_location);
        if (GlobalVariable.navigate_from_JB_TSD_General)
        {
            jb_TSD_General_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            list_Devices_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
    }
    /*  public void NavigatePop()
      {

          if (GlobalVariable.navigate_from_JB_TSD_General)
          {
              jb_TSD_Detail_Transform.gameObject.SetActive(false);
              jb_TSD_General_Transform.gameObject.SetActive(true);
              GlobalVariable.navigate_from_JB_TSD_General = false;
          }
          if (GlobalVariable.navigate_from_List_Devices)
          {
              jb_TSD_Detail_Transform.gameObject.SetActive(false);
              list_Devices_Transform.gameObject.SetActive(true);
              GlobalVariable.navigate_from_List_Devices = false;
          }
      }*/
}

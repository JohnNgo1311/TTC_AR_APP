using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class SearchDeviceFromModule : MonoBehaviour
{
    public List<TMP_Text> deviceInformation = new List<TMP_Text>();
    private List<DeviceInformationModel> listDeviceFromModule;
    private DeviceInformationModel deviceInfor;
    public TMP_Dropdown dropdown;
    public GameObject contentPanel;
    private JBInformationModel jBInformationModel_ConnectingToDevice;
    public Button nav_JB_TSD_Detail_button;
    // private string moduleName = "D1.0.I";
    private const string noDeviceMessage = "không có thiết bị kết nối";

    [SerializeField]
    private Canvas module_Canvas;
    private RectTransform list_Devices_Transform;
    private RectTransform jb_TSD_Basic_Transform;
    private RectTransform jb_TSD_Detail_Transform;
    private void Awake()
    {
        list_Devices_Transform ??= module_Canvas.gameObject.transform.Find("list_Devices").GetComponent<RectTransform>();
        jb_TSD_Basic_Transform ??= module_Canvas.gameObject.transform.Find("JB_TSD_Basic_Panel").GetComponent<RectTransform>();
        jb_TSD_Detail_Transform ??= module_Canvas.gameObject.transform.Find("Detail_JB_TSD").GetComponent<RectTransform>();
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        if (!GlobalVariable.navigate_from_JB_TSD_Detail)
        {
            module_Canvas ??= GetComponentInParent<Canvas>();
            if (module_Canvas.gameObject.activeSelf)
            {
                // Tách moduleName một lần và lưu trữ
                //moduleName = module_Canvas.name.Split('_')[0];
                listDeviceFromModule = Get_List_Device_By_Module();
                // Xóa các tùy chọn trước đó
                dropdown.options.Clear();
                if (listDeviceFromModule.Any())
                {
                    // Chuyển đổi danh sách thiết bị thành danh sách tùy chọn cho dropdown
                    foreach (var device in listDeviceFromModule)
                    {
                        dropdown.options.Add(new TMP_Dropdown.OptionData(device.Code));
                    }

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
        GlobalVariable.navigate_from_JB_TSD_Detail = false;
    }

    private void OnDisable()
    {
        if (!GlobalVariable.navigate_from_JB_TSD_Detail)
        { // Xóa các tùy chọn trong dropdown
            dropdown.options.Clear();
            // Xóa thông tin hiển thị về thiết bị
            ClearDeviceInformation();
            // Ẩn contentPanel
            contentPanel.SetActive(false);
            // Gỡ sự kiện khi OnDisable được gọi
            dropdown.onValueChanged.RemoveListener(OnValueChange);
        }

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

    private List<DeviceInformationModel> Get_List_Device_By_Module()
    {
        return GlobalVariable.temp_ListDeviceInformationModel;
    }

    private void UpdateDeviceInformation(DeviceInformationModel device)
    {
        deviceInformation[0].text = device.Code;
        deviceInformation[1].text = device.Function;
        deviceInformation[2].text = device.Range;
        deviceInformation[3].text = device.IOAddress;

        jBInformationModel_ConnectingToDevice = GlobalVariable.temp_ListJBInformationModel_FromModule.Find(jB => jB.Id == device.JBInformationModels[0].Id);

        deviceInformation[4].text = jBInformationModel_ConnectingToDevice.Name;
        deviceInformation[5].text = jBInformationModel_ConnectingToDevice.Location;
        // Đảm bảo không gán nhiều lần
        nav_JB_TSD_Detail_button.onClick.RemoveAllListeners();
        nav_JB_TSD_Detail_button.onClick.AddListener(() =>
        {
            GlobalVariable.navigate_from_List_Devices = true;
            GlobalVariable.navigate_from_JB_TSD_Basic = false;
            NavigateJBDetailScreen(jB_Information_Model: jBInformationModel_ConnectingToDevice);
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
        //Debug"Navigate to JB Detail Screen + " + jB_TSD_Name);
    }
    */

    public void NavigateJBDetailScreen(JBInformationModel jB_Information_Model)
    {
        GlobalVariable.jb_TSD_Title = jB_Information_Model.Name; // Name_Location of JB

        GlobalVariable.jb_TSD_Name = jB_Information_Model.Name; // jb_name: JB100
                                                                // //Debug"jb_name: " + jb_name);
        GlobalVariable.jb_TSD_Location = jB_Information_Model.Location; // jb_location: Hầm Cáp MCC
                                                                        // //Debug"jb_location: " + jb_location);
        if (GlobalVariable.navigate_from_JB_TSD_Basic)
        {
            GlobalVariable.navigate_from_JB_TSD_Detail = true;
            jb_TSD_Basic_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            GlobalVariable.navigate_from_JB_TSD_Detail = true;
            list_Devices_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
    }
    /*  public void NavigatePop()
      {

          if (GlobalVariable.navigate_from_JB_TSD_Basic)
          {
              jb_TSD_Detail_Transform.gameObject.SetActive(false);
              jb_TSD_Basic_Transform.gameObject.SetActive(true);
              GlobalVariable.navigate_from_JB_TSD_Basic = false;
          }
          if (GlobalVariable.navigate_from_List_Devices)
          {
              jb_TSD_Detail_Transform.gameObject.SetActive(false);
              list_Devices_Transform.gameObject.SetActive(true);
              GlobalVariable.navigate_from_List_Devices = false;
          }
      }*/
}

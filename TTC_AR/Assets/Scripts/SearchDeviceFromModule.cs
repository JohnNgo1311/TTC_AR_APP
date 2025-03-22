using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;
using System;
using System.Linq;
public class SearchDeviceFromModule : MonoBehaviour
{
    public List<TMP_Text> deviceInformation = new List<TMP_Text>();
    private List<DeviceInformationModel> listDeviceFromModule;
    private DeviceInformationModel deviceInfor;
    public TMP_Dropdown dropdown;
    public GameObject contentPanel;
    public Transform jbConnectionParentTransform;
    private List<JBInformationModel> jBInformationModel_ConnectingToDevice_List;
    public GameObject nav_JB_TSD_Detail_button_Prefab;
    public Dictionary<string, JBInformationModel> dic_JBInformationModel = new Dictionary<string, JBInformationModel>();
    public Dictionary<string, GameObject> dic_JBInformationModel_Button = new Dictionary<string, GameObject>();

    private const string noDeviceMessage = "không có thiết bị kết nối";

    [SerializeField]
    private Canvas module_Canvas;
    private RectTransform list_Devices_Transform;
    private RectTransform jb_TSD_Basic_Transform;
    private RectTransform jb_TSD_Detail_Transform;
    private void Awake()
    {
        list_Devices_Transform ??= module_Canvas.gameObject.transform.Find("List_Devices").GetComponent<RectTransform>();
        jb_TSD_Basic_Transform ??= module_Canvas.gameObject.transform.Find("JB_TSD_General_Panel").GetComponent<RectTransform>();
        jb_TSD_Detail_Transform ??= module_Canvas.gameObject.transform.Find("Detail_JB_TSD").GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        // if (!StaticVariable.navigate_from_JB_TSD_Detail)
        // {
        module_Canvas ??= GetComponentInParent<Canvas>();
        StartCoroutine(UpdateUI());
        // }
        StaticVariable.navigate_from_JB_TSD_Detail = false;
    }

    private IEnumerator UpdateUI()
    {
        if (module_Canvas.gameObject.activeSelf)
        {
            // Nếu không có thiết bị nào, thêm tùy chọn mặc định
            dropdown.options.Add(new TMP_Dropdown.OptionData(noDeviceMessage));
            dropdown.value = 0;
            dropdown.RefreshShownValue();

            yield return new WaitUntil(() => StaticVariable.ready_To_Update_ListDevices_UI);

            listDeviceFromModule = Get_List_Device_By_Module();

            // Xóa các tùy chọn trước đó
            dropdown.options.Clear();

            // Debug.Log("listDeviceFromModule.Count: " + listDeviceFromModule.Count);

            if (listDeviceFromModule.Any())
            {
                // Chuyển đổi danh sách thiết bị thành danh sách tùy chọn cho dropdown
                foreach (var device in listDeviceFromModule)
                {
                    // Debug.Log("device.Code: " + device.Code);
                    dropdown.options.Add(new TMP_Dropdown.OptionData(device.Code));
                }

                OnValueChange(0);// Gọi OnValueChange để cập nhật thông tin thiết bị đầu tiên

                // Đảm bảo rằng option1 luôn được chọn
                dropdown.value = 0;
                dropdown.RefreshShownValue();

                dropdown.onValueChanged.AddListener(OnValueChange);
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
            StaticVariable.ready_To_Update_ListDevices_UI = false;
        }
    }

    private void OnDisable()
    {
        if (!StaticVariable.navigate_from_JB_TSD_Detail)
        {
            // Xóa các tùy chọn trong dropdown
            dropdown.options.Clear();
            // Xóa thông tin hiển thị về thiết bị
            ClearDeviceInformation();
            // Ẩn contentPanel
            contentPanel.SetActive(false);
            // Gỡ sự kiện khi OnDisable được gọi
            dropdown.onValueChanged.RemoveListener(OnValueChange);

            // Xóa danh sách thiết bị
            listDeviceFromModule.Clear();
        }
        DestroyAllInstancesExceptPrefab(jbConnectionParentTransform);
        dic_JBInformationModel.Clear();
        dic_JBInformationModel_Button.Clear();
    }

    private void DestroyAllInstancesExceptPrefab(Transform contentParent)
    {
        // Duyệt qua từng child của contentParent
        foreach (Transform child in contentParent)
        {
            // Kiểm tra nếu child đang bị inactive (tức là prefab gốc)
            if (child.gameObject == nav_JB_TSD_Detail_button_Prefab)
            {
                continue; // Bỏ qua không xóa
            }
            // Xóa các child khác
            Destroy(child.gameObject);
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
        // Debug.Log("Get_List_Device_By_Module");
        return StaticVariable.temp_ListDeviceInformationModelFromDeviceName.Values.ToList();
    }

    private void UpdateDeviceInformation(DeviceInformationModel device)
    {
        deviceInformation[0].text = device.Code;
        deviceInformation[1].text = device.Function;
        deviceInformation[2].text = device.Range;
        deviceInformation[3].text = device.Unit;
        deviceInformation[4].text = device.IOAddress;

        jBInformationModel_ConnectingToDevice_List = device.JBInformationModel;

        StaticVariable.device_Code = device.Code;

        // Disable các JB đã được tạo trước đó
        DisableAllItem(jbConnectionParentTransform);

        if (jBInformationModel_ConnectingToDevice_List.Count == 0)
        {
            nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>().NoDeviceMessage();
            return;
        }

        if (jBInformationModel_ConnectingToDevice_List.Count > 1)
        {
            foreach (var jB in jBInformationModel_ConnectingToDevice_List)
            {
                if (!dic_JBInformationModel.ContainsKey(jB.Name))
                {
                    var nav_Btn_Prefab = Instantiate(nav_JB_TSD_Detail_button_Prefab, jbConnectionParentTransform);
                    nav_Btn_Prefab.GetComponent<JBInfor>().SetJBInfor(jB);

                    // if (!dic_JBInformationModel_Button.ContainsKey(jB.Name))
                    // {
                    dic_JBInformationModel.Add(jB.Name, jB);
                    dic_JBInformationModel_Button.Add(jB.Name, nav_Btn_Prefab);
                    // }
                    // else
                    // {
                    //     dic_JBInformationModel[jB.Name] = jB;
                    //     dic_JBInformationModel_Button[jB.Name] = nav_Btn_Prefab;
                    // }

                    // Đảm bảo không gán nhiều lần
                    nav_Btn_Prefab.GetComponent<JBInfor>().jbButton.onClick.RemoveAllListeners();
                    nav_Btn_Prefab.GetComponent<JBInfor>().jbButton.onClick.AddListener(() =>
                    {
                        StaticVariable.navigate_from_List_Devices = true;
                        StaticVariable.navigate_from_JB_TSD_Basic = false;
                        NavigateJBDetailScreen(jB_Information_Model: jB);
                    });
                }
                dic_JBInformationModel_Button[jB.Name].SetActive(true);
            }
            nav_JB_TSD_Detail_button_Prefab.SetActive(false);
            return;
        }

        nav_JB_TSD_Detail_button_Prefab.SetActive(true);
        nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>().SetJBInfor(jBInformationModel_ConnectingToDevice_List[0]);

        // Đảm bảo không gán nhiều lần
        nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>().jbButton.onClick.RemoveAllListeners();
        nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>().jbButton.onClick.AddListener(() =>
        {
            StaticVariable.navigate_from_List_Devices = true;
            StaticVariable.navigate_from_JB_TSD_Basic = false;
            NavigateJBDetailScreen(jB_Information_Model: jBInformationModel_ConnectingToDevice_List[0]);
        });
    }

    private void DisableAllItem(Transform contentParent)
    {
        foreach (Transform child in contentParent)
        {
            if (!child.gameObject.activeSelf)
            {
                continue;
            }
            child.gameObject.SetActive(false);
            nav_JB_TSD_Detail_button_Prefab.SetActive(true);
        }
    }

    private void ClearDeviceInformation()
    {
        foreach (var info in deviceInformation)
        {
            info.text = string.Empty;
        }
    }

    public void NavigateJBDetailScreen(JBInformationModel jB_Information_Model)
    {
        StaticVariable.jb_TSD_Title = jB_Information_Model.Name; // Name_Location of JB

        StaticVariable.jb_TSD_Name = jB_Information_Model.Name; // jb_name: JB100

        StaticVariable.jb_TSD_Location = jB_Information_Model.Location; // jb_location: Hầm Cáp MCC

        if (StaticVariable.navigate_from_JB_TSD_Basic)
        {
            StaticVariable.navigate_from_JB_TSD_Detail = true;
            jb_TSD_Basic_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }

        if (StaticVariable.navigate_from_List_Devices)
        {
            StaticVariable.navigate_from_JB_TSD_Detail = true;
            list_Devices_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
    }
}

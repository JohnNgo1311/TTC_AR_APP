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
    private List<JBInformationModel> listJBInformation_FromDevice = new List<JBInformationModel>();
    public GameObject nav_JB_TSD_Detail_button_Prefab;
    // public List<GameObject> list_JB_TSD_Detail_Instantiates = new List<GameObject>();
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
        list_Devices_Transform ??= module_Canvas.gameObject.transform.Find("list_Devices").GetComponent<RectTransform>();
        jb_TSD_Basic_Transform ??= module_Canvas.gameObject.transform.Find("JB_TSD_Basic_Panel").GetComponent<RectTransform>();
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
            dropdown.options.Add(new TMP_Dropdown.OptionData(noDeviceMessage));
            dropdown.value = 0;
            dropdown.RefreshShownValue();

            yield return new WaitUntil(() => StaticVariable.ready_To_Update_ListDevices_UI);
            listDeviceFromModule = StaticVariable.temp_ListDeviceInformationModelFromDeviceName.Values.ToList();

            dropdown.options.Clear();

            // Debug.Log("listDeviceFromModule.Count: " + listDeviceFromModule.Count);

            if (listDeviceFromModule.Any())
            {
                // Chuyển đổi danh sách thiết bị thành danh sách tùy chọn cho dropdown
                foreach (var device in listDeviceFromModule)
                {
                    // Chuyển đổi danh sách thiết bị thành danh sách tùy chọn cho dropdown
                    foreach (var dv in listDeviceFromModule)
                    {
                        dropdown.options.Add(new TMP_Dropdown.OptionData(dv.Code));
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
                ClearDeviceInformation();

                contentPanel.SetActive(false);
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
        DestroyAllInstancesExceptPrefab(dic_JBInformationModel_Button.Values.ToList());
        // list_JB_TSD_Detail_Instantiates.Clear();
        dic_JBInformationModel.Clear();
        dic_JBInformationModel_Button.Clear();
    }

    private void DestroyAllInstancesExceptPrefab(List<GameObject> listInstances)
    {
        // Duyệt qua từng child của contentParent
        foreach (var instance in listInstances)
        {
            // Kiểm tra nếu child đang bị inactive (tức là prefab gốc)
            // if (instance != nav_JB_TSD_Detail_button_Prefab)
            // {
            // Xóa các child khác
            Destroy(instance);
            // }
        }
    }

    public void OnValueChange(int value)
    {
        if (value < listDeviceFromModule.Count)
        {
            deviceInfor = listDeviceFromModule[value];

            if (!contentPanel.activeSelf)
            {
                contentPanel.SetActive(true);
            }
            UpdateDeviceInformation(deviceInfor);

        }
        else
        {
            // Nếu chọn tùy chọn mặc định "không có thiết bị kết nối"
            ClearDeviceInformation();

            contentPanel.SetActive(false);
        }
    }


    private void UpdateDeviceInformation(DeviceInformationModel device)
    {

        // xóa các JB đã được tạo trước đó
        DestroyAllInstancesExceptPrefab(dic_JBInformationModel_Button.Values.ToList());

        deviceInformation[0].text = device.Code;
        deviceInformation[1].text = device.Function;
        deviceInformation[2].text = device.Range;
        deviceInformation[3].text = device.Unit;
        deviceInformation[4].text = device.IOAddress;

        listJBInformation_FromDevice = device.JBInformationModels;

        StaticVariable.device_Code = device.Code;

        StaticVariable.temp_DeviceInformationModel[device.Code] = device;

        dic_JBInformationModel.Clear();
        dic_JBInformationModel_Button.Clear();

        if (!listJBInformation_FromDevice.Any())
        {
            nav_JB_TSD_Detail_button_Prefab.SetActive(true);
            nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>().NoDeviceMessage();
            return;
        }
        else
        {
            if (listJBInformation_FromDevice.Count > 1)
            {
                foreach (var jB in listJBInformation_FromDevice)
                {
                    if (!dic_JBInformationModel.ContainsKey(jB.Name))
                    {
                        var new_Nav_Btn = Instantiate(nav_JB_TSD_Detail_button_Prefab, jbConnectionParentTransform);
                        var new_Nav_Btn_JBInfor = new_Nav_Btn.GetComponent<JBInfor>();
                        new_Nav_Btn_JBInfor.SetJBInfor(jB);

                        dic_JBInformationModel.Add(jB.Name, jB);
                        dic_JBInformationModel_Button.Add(jB.Name, new_Nav_Btn);

                        new_Nav_Btn_JBInfor.jbButton.onClick.RemoveAllListeners();
                        new_Nav_Btn_JBInfor.jbButton.onClick.AddListener(() =>
                        {
                            StaticVariable.navigate_from_List_Devices = true;
                            StaticVariable.navigate_from_JB_TSD_General = false;
                            NavigateJBDetailScreen(model: jB);
                        });
                    }
                    dic_JBInformationModel_Button[jB.Name].SetActive(true);
                }
                nav_JB_TSD_Detail_button_Prefab.SetActive(false);
                return;
            }
            else
            {

                nav_JB_TSD_Detail_button_Prefab.SetActive(true);
                var jbInfor = nav_JB_TSD_Detail_button_Prefab.GetComponent<JBInfor>();
                jbInfor.SetJBInfor(listJBInformation_FromDevice[0]);
                jbInfor.jbButton.onClick.RemoveAllListeners();
                jbInfor.jbButton.onClick.AddListener(() =>
                {
                    StaticVariable.navigate_from_List_Devices = true;
                    StaticVariable.navigate_from_JB_TSD_General = false;
                    NavigateJBDetailScreen(model: listJBInformation_FromDevice[0]);
                });
            }
        }


    }

    private void ClearDeviceInformation()
    {
        foreach (var info in deviceInformation)
        {
            info.text = string.Empty;
        }
    }

    public void NavigateJBDetailScreen(JBInformationModel model)
    {
        StaticVariable.temp_JBInformationModel = model;

        StaticVariable.jb_TSD_Title = model.Name; // Name_Location of JB

        StaticVariable.jb_TSD_Name = model.Name; // jb_name: JB100

        StaticVariable.jb_TSD_Location = model.Location; // jb_location: Hầm Cáp MCC

        if (StaticVariable.navigate_from_JB_TSD_General)
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

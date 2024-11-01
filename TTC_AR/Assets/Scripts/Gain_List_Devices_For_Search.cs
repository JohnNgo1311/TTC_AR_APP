using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gain_List_Devices_For_Search : MonoBehaviour
{
    public GameObject parent_Object;
    public GameObject prefab_Device;
    public TMP_Text title_TEST;

    private List<GameObject> deviceObjectsPool = new List<GameObject>();
    private int activeDeviceCount = 0;
   private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    private void Awake()
    {
        Debug.Log("Awake_Gain_List_Devices_For_Search");
        if (GlobalVariable_Search_Devices.devices_Model_For_Filter != null && GlobalVariable_Search_Devices.devices_Model_For_Filter.Count > 0)
        {
            // Chuẩn bị object pooling dựa trên số lượng thiết bị
            /*  PrepareObjectPooling(GlobalVariable_Search_Devices.devices_Model_For_Filter.Count);
              // Tạo đối tượng với tên dựa trên code và function
              foreach (var device in GlobalVariable_Search_Devices.devices_Model_By_Grapper)
              {
                  CreateOrReuseDeviceObject(device.code, device);
                  CreateOrReuseDeviceObject(device.function, device);
              }
              if (parent_Object.transform.childCount >= GlobalVariable_Search_Devices.devices_Model_For_Filter.Count)
              {
                  Destroy(prefab_Device);
              }*/
            //title_TEST.text = $"Total devices: {GlobalVariable_Search_Devices.devices_Model_For_Filter.Count}";

        }
        else
        {
            title_TEST.text = "Total devices: 0";
        }

    }

    private void PrepareObjectPooling(int requiredCount)
    {
        for (int i = deviceObjectsPool.Count; i < requiredCount; i++)
        {
            GameObject deviceObject = Instantiate(prefab_Device, parent_Object.transform);
            if (deviceObject != null)
            {
                deviceObject.SetActive(false);
                deviceObjectsPool.Add(deviceObject);
            }
            title_TEST.text = $"Total devices: {deviceObjectsPool.Count}";

        }

    }

    private void CreateOrReuseDeviceObject(string name, DeviceModel device)
    {
        GameObject deviceObject;
        if (activeDeviceCount < deviceObjectsPool.Count)
        {
            deviceObject = deviceObjectsPool[activeDeviceCount];
        }
        else
        {
            deviceObject = Instantiate(prefab_Device, parent_Object.transform);
            deviceObjectsPool.Add(deviceObject);
        }

        deviceObject.name = name;
        UpdateDeviceInformation(deviceObject, device);
        deviceObject.SetActive(false);
        activeDeviceCount++;
    }

    private void UpdateDeviceInformation(GameObject new_GameObject, DeviceModel device)
    {
        // Tối ưu hóa: Chỉ cập nhật nếu có thay đổi
        TMP_Text code = GetOrAddTextComponent(new_GameObject, "information/code_group/code", device.code);
        TMP_Text function = GetOrAddTextComponent(new_GameObject, "information/function_group/function", device.function);
        TMP_Text range_measurement = GetOrAddTextComponent(new_GameObject, "information/range_measurement_group/range_measurement", device.rangeMeasurement);
        TMP_Text I_O = GetOrAddTextComponent(new_GameObject, "information/I_O_group/I_O", device.ioAddress);

        var jb_infor = JB_SplitString(device.jbConnection);
        TMP_Text jb_name = GetOrAddTextComponent(new_GameObject, "jb_information_group/jb_name", $"{jb_infor[0]}:");
        TMP_Text jb_location = GetOrAddTextComponent(new_GameObject, "jb_information_group/jb_location", jb_infor[1]);
    }

    private TMP_Text GetOrAddTextComponent(GameObject gameObject, string path, string newText)
    {
        Transform transform = gameObject.transform.Find(path);
        if (transform != null)
        {
            TMP_Text textComponent = transform.GetComponent<TMP_Text>();
            if (textComponent != null && textComponent.text != newText)
            {
                textComponent.text = newText;
            }
            return textComponent;
        }
        Debug.LogError($"Component not found at path: {path}");
        return null;
    }

    private List<string> JB_SplitString(string jb_infor)
    {
        var parts = jb_infor.Split('_');
        if (parts.Length >= 2)
        {
            return new List<string> { parts[0], parts[1] };
        }
        else
        {
            Debug.LogError("Invalid JB information format.");
            return new List<string> { "Unknown", "Unknown" };
        }
    }
}

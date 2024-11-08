using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text;

public class Update_Device_By_Grapper : MonoBehaviour
{
    [SerializeField]
    private string grapperName = "A";

    [SerializeField]
    private DeviceModel device = new DeviceModel();

    public TMP_Text codeText;
    public string title = "Cập nhật thiết bị";
    public Button cancelButton;
    public Button confirmButton;
    public GameObject panelDialog;
    public List<TMP_InputField> inputFields = new List<TMP_InputField>();
    private string id_Of_Device_in_Globals;

    [Header("Set Interactive false To these objects")]

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Button editButton;

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private TMP_InputField inputField_Search;


    public GameObject Location_Image_Update_Group;
    public GameObject Connection_Image_Update_Group;


    private void Start()
    {
        panelDialog.SetActive(false);
    }

    public async void OpenPanelUpdateDevice()
    {
        if (GlobalVariable_Search_Devices.all_Device_GrapperA != null && GlobalVariable_Search_Devices.all_Device_GrapperA.Count > 0)
        {
            device = GlobalVariable_Search_Devices.all_Device_GrapperA.Find(d => d.code == codeText.text);
            if (device != null)
            {
                id_Of_Device_in_Globals = device.id;
                PopulateInputFields(device);
                await Task.WhenAll(
                Set_Up_JB_Location_Group_Image(device),
                Set_Up_JB_Connection_Group_Image(device));
                Canvas.ForceUpdateCanvases();
            }
            else
            {
                Debug.LogWarning("Device not found!");
                return;
            }
        }
        SetInteractable(false);
        ShowQuestionDialog(UpdateDeviceData, ClearInputFieldsAndListeners);
    }

    private void PopulateInputFields(DeviceModel device)
    {
        inputFields[0].text = device.code;
        inputFields[1].text = device.function;
        inputFields[2].text = device.rangeMeasurement;
        inputFields[3].text = device.ioAddress;

        var jbConnectionParts = device.jbConnection?.Split('_');
        if (jbConnectionParts != null && jbConnectionParts.Length == 2)
        {
            inputFields[4].text = jbConnectionParts[0];
            inputFields[5].text = jbConnectionParts[1];
        }
        else
        {
            Debug.LogWarning("Invalid jbConnection format.");
        }
    }
    private async Task Set_Up_JB_Location_Group_Image(DeviceModel device)
    {
        await Task.Yield();

        /* if (Location_Image_Update_Group.transform.childCount < GlobalVariable.list_Temp_JB_Location_Image.Count)
         {
             Instantiate(Location_Image_Update_Group.transform.GetChild(0).gameObject, Location_Image_Update_Group.transform);
         }
         else */
        if (Location_Image_Update_Group.transform.childCount > GlobalVariable.list_Temp_JB_Location_Image.Count)
        {
            int compare_Count = Location_Image_Update_Group.transform.childCount - GlobalVariable.list_Temp_JB_Location_Image.Count;
            for (int i = 1; i <= compare_Count; i++)
            {
                Location_Image_Update_Group.transform.GetChild(Location_Image_Update_Group.transform.childCount - i).gameObject.SetActive(false);
            }
        }

        var jbConnectionParts = device.jbConnection?.Split('_');
        if (jbConnectionParts != null && jbConnectionParts.Length == 2)
        {
            for (int i = 1; i <= GlobalVariable.list_Temp_JB_Location_Image.Count; i++)
            {
                Location_Image_Update_Group.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>().sprite = GlobalVariable.list_Temp_JB_Location_Image[i - 1];
            }
        }
        else
        {
            Debug.LogWarning("Invalid jbConnection format.");
        }
    }

    private async Task Set_Up_JB_Connection_Group_Image(DeviceModel device)
    {
        await Task.Yield();

        /*   if (Connection_Image_Update_Group.transform.childCount < GlobalVariable.list_Temp_JB_Connection_Image.Count)
           {
               Instantiate(Connection_Image_Update_Group.transform.GetChild(0).gameObject, Connection_Image_Update_Group.transform);
           }
           else */
        if (Connection_Image_Update_Group.transform.childCount > GlobalVariable.list_Temp_JB_Connection_Image.Count)
        {
            int compare_Count = Connection_Image_Update_Group.transform.childCount - GlobalVariable.list_Temp_JB_Connection_Image.Count;
            for (int i = 1; i <= compare_Count; i++)
            {
                Connection_Image_Update_Group.transform.GetChild(Connection_Image_Update_Group.transform.childCount - i).gameObject.SetActive(false);
            }
        }

        var jbConnectionParts = device.jbConnection?.Split('_');
        if (jbConnectionParts != null && jbConnectionParts.Length == 2)
        {
            for (int i = 1; i <= GlobalVariable.list_Temp_JB_Connection_Image.Count; i++)
            {
                Connection_Image_Update_Group.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>().sprite = GlobalVariable.list_Temp_JB_Connection_Image[i - 1];
            }
        }
        else
        {
            Debug.LogWarning("Invalid jbConnection format.");
        }
    }
    private void ShowQuestionDialog(Action confirmAction, Action cancelAction)
    {
        panelDialog.SetActive(true);
        cancelButton.onClick.AddListener(() => cancelAction());
        confirmButton.onClick.AddListener(() => confirmAction());
    }

    private void UpdateDeviceData()
    {
        DeviceModel tempDevice = new DeviceModel
        {
            id = id_Of_Device_in_Globals,
            location = $"Grapper{grapperName}",
            code = inputFields[0].text,
            function = inputFields[1].text,
            rangeMeasurement = inputFields[2].text,
            ioAddress = inputFields[3].text,
            jbConnection = $"{inputFields[4].text}_{inputFields[5].text}"
        };

        UpdateDevice(tempDevice);
        ClearInputFieldsAndListeners();
        SetInteractable(true);
    }

    private void ClearInputFieldsAndListeners()
    {
        panelDialog.SetActive(false);
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        foreach (var inputField in inputFields)
        {
            inputField.text = string.Empty;
        }
        SetInteractable(true);
    }

    private void SetInteractable(bool state)
    {
        backButton.interactable = state;
        scrollRect.vertical = state;
        editButton.interactable = state;
        inputField_Search.interactable = state;
    }

    public async void UpdateDevice(DeviceModel tempDevice)
    {
        panelDialog.SetActive(false);
        foreach (var inputField in inputFields)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                Debug.LogError("Input fields cannot be empty.");
                return;
            }
        }
        await UpdateDeviceData($"{GlobalVariable.baseUrl}{grapperName}", tempDevice).ConfigureAwait(false);
    }

    private async Task UpdateDeviceData(string url, DeviceModel device)
    {
        string jsonData = JsonConvert.SerializeObject(device);
        byte[] dataToByte = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest webRequest = new UnityWebRequest($"{url}/{device.id}", "PUT"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(dataToByte);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Request error: {webRequest.error}");
            }
            else
            {
                try
                {
                    Debug.Log("Post data successfully.");
                    GlobalVariable_Search_Devices.all_Device_GrapperA[int.Parse(id_Of_Device_in_Globals) - 1] = device;
                    ClearInputFieldsAndListeners();
                    Canvas.ForceUpdateCanvases();
                }
                catch (JsonException jsonEx)
                {
                    Debug.LogError($"Error parsing JSON: {jsonEx.Message}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}

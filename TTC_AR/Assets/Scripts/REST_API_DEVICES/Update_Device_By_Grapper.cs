using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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
    // public Show_Dialog showDialog;
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
    private void Start()
    {
        //    if (showDialog == null)
        //  {
        //      showDialog = Show_Dialog.Instance;
        //  }
        panelDialog.SetActive(false);
    }

    private void OnDestroy()
    {
        //   Destroy(showDialog);
    }

    public void OpenPanelUpdateDevice()
    {
        if (GlobalVariable_Search_Devices.all_Device_GrapperA != null && GlobalVariable_Search_Devices.all_Device_GrapperA.Count > 0)
        {
            device = GlobalVariable_Search_Devices.all_Device_GrapperA.Find(d => d.code == codeText.text);
            if (device != null)
            {
                id_Of_Device_in_Globals = device.id;
                PopulateInputFields(device);
            }
            else
            {
                Debug.LogWarning("Device not found!");
                return;
            }
        }
        backButton.interactable = false;
        scrollRect.vertical = false;
        editButton.interactable = false;
        inputField_Search.interactable = false;
        ShowQuestionDialog(
            confirmAction: UpdateDeviceData,
            cancelAction: ClearInputFieldsAndListeners
        );
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

    private void ShowQuestionDialog(Action confirmAction, Action cancelAction)
    {
        panelDialog.SetActive(true);
        cancelButton.onClick.AddListener(new UnityEngine.Events.UnityAction(cancelAction));
        confirmButton.onClick.AddListener(new UnityEngine.Events.UnityAction(confirmAction));
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
        backButton.interactable = true;
        scrollRect.vertical = true;
        editButton.interactable = true;
        inputField_Search.interactable = true;
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
        backButton.interactable = true;
        scrollRect.vertical = true;
        editButton.interactable = true;
        inputField_Search.interactable = true;
    }

    public async void UpdateDevice(DeviceModel tempDevice)
    {
        panelDialog.SetActive(false);
        foreach (var inputField in inputFields)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                Debug.LogError("Input fields cannot be empty.");
                //       showDialog.ShowToast("failure", "Hãy điền đầy đủ thông tin.");
                return;
            }
        }
        //  showDialog.ShowToast("loading", "Đang cập nhật dữ liệu ", 1);
        await UpdateDeviceData($"{GlobalVariable.baseUrl}{grapperName}", tempDevice).ConfigureAwait(false);
    }

    private async Task UpdateDeviceData(string url, DeviceModel device)
    {
        string jsonData = JsonConvert.SerializeObject(device);
        byte[] dataToByte = System.Text.Encoding.UTF8.GetBytes(jsonData);

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
                //   showDialog.ShowToast("failure", $"Request error: {webRequest.error}");
                Debug.LogError($"Request error: {webRequest.error}");
            }
            else
            {
                try
                {
                    Debug.Log("Post data successfully.");
                    //    showDialog.ShowToast("success", "Cập nhật thiết bị thành công: " + device.code);
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

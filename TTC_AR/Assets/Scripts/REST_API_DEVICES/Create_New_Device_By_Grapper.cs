using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;
using System.Collections;

public class Create_New_Device_By_Grapper : MonoBehaviour
{
    [SerializeField]
    private string grapperName = "A";

    [SerializeField]
    private DeviceModel device = new DeviceModel();
    public List<TMP_InputField> inputFields = new List<TMP_InputField>();
    public Button cancelButton;
    public Button confirmButton;
    public GameObject panelDialog;

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
        panelDialog.SetActive(false);
    }

    public void OpenPanelCreateDevice()
    {
        SetInteractable(false);
        ShowQuestionDialog(
            confirmAction: () => StartCoroutine(CreateDevicesByGrapper()),
            cancelAction: ClearInputFieldsAndListeners
        );
    }

    private void ShowQuestionDialog(Action confirmAction, Action cancelAction)
    {
        panelDialog.SetActive(true);
        cancelButton.onClick.AddListener(new UnityEngine.Events.UnityAction(cancelAction));
        confirmButton.onClick.AddListener(new UnityEngine.Events.UnityAction(confirmAction));
    }

    private void ClearInputFieldsAndListeners()
    {
        panelDialog.SetActive(false);
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        for (int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].text = "";
        }
        SetInteractable(true);
    }

    private IEnumerator CreateDevicesByGrapper()
    {
        device.location = $"Grapper{grapperName}";
        device.code = inputFields[0].text;
        device.function = inputFields[1].text;
        device.rangeMeasurement = inputFields[2].text;
        device.ioAddress = inputFields[3].text;
        device.jbConnection = $"{inputFields[4].text}_{inputFields[5].text}";
        device.listImageConnection = new List<string>();

        for (int i = 0; i < inputFields.Count; i++)
        {
            if (string.IsNullOrEmpty(inputFields[i].text))
            {
                Debug.LogError("Input fields cannot be empty.");
                yield break;
            }
        }

        yield return APIManager.Instance.CreateNewDevice($"{GlobalVariable.baseUrl}{grapperName}", device, SceneManager.GetActiveScene().name);

        SetInteractable(true);
    }

    private void SetInteractable(bool state)
    {
        backButton.interactable = state;
        scrollRect.vertical = state;
        editButton.interactable = state;
        inputField_Search.interactable = state;
    }
}

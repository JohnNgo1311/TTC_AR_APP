using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Set_Up_General_Panel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button nav_Back;
    [SerializeField] private GameObject listFieldDevicesPanel;
    [SerializeField] private GameObject generalPanel;

    private void OnEnable()
    {
        title.text = "Tủ " + StaticVariable.temp_FieldDeviceInformationModel.Mcc.CabinetCode;
        // Debug.Log("CabinetCode: " + StaticVariable.temp_FieldDeviceInformationModel.Mcc.CabinetCode);
        nav_Back.onClick.AddListener(() => NavigateBack());
    }

    private void NavigateBack()
    {
        generalPanel.SetActive(false);
        listFieldDevicesPanel.SetActive(true);
    }

    private void OnDisable()
    {
        nav_Back.onClick.RemoveAllListeners();
    }
}

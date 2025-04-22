using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FieldDeviceInfor : MonoBehaviour
{
    public TMP_Text fiedDeviceName;
    public Button nav_Btn;

    public void SetFieldDeviceName(FieldDeviceInformationModel fieldDeviceInformationModel)
    {
        fiedDeviceName.text = fieldDeviceInformationModel.Name;
    }
}

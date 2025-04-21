using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JBInfor : MonoBehaviour
{
    public Button jbButton;
    public TMP_Text Name;
    [SerializeField] private TMP_Text Location;

    private void OnEnable()
    {
        Name.gameObject.SetActive(true);
    }

    public void SetJBInfor(JBInformationModel jb)
    {
        jbButton.gameObject.SetActive(true);
        Name.text = jb.Name;
        if (jb.Location == null)
        {
            Location.text = "Chưa cập nhật JB/TSD kết nối";
        }
        else
        {
            Location.text = jb.Location;
        }
        // Location.alignment = TextAlignmentOptions.Left;
    }

    public void NoDeviceMessage()
    {
        jbButton.gameObject.SetActive(false);
        Location.text = "Chưa cập nhật JB/TSD kết nối";
        // Location.alignment = TextAlignmentOptions.Center;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JBInfor : MonoBehaviour
{
    public Button button;
    public TMP_Text value;
    public TMP_Text Location;

    private void OnEnable()
    {

    }

    public void SetJBInfor(JBInformationModel jb)
    {
        value.text = jb.Name;

        if (string.IsNullOrEmpty(jb.Location))
        {
            Location.text = "Vị trí tủ không được ghi chú";
        }
        else
        {
            Location.text = jb.Location;
        }
    }

    public void HandleEmptyList()
    {
        button.gameObject.SetActive(false);
        Location.text = "Không có JB kết nối";
        Location.alignment = TextAlignmentOptions.Center;
        Location.fontStyle = FontStyles.Bold;
        Location.color = Color.red;
    }
}

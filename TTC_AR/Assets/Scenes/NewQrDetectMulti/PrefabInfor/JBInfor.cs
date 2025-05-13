using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JBInfor : MonoBehaviour
{
    public Button button;
    public TMP_Text value;
    [SerializeField] private TMP_Text Location;

    private void OnEnable()
    {

    }

    public void SetJBInfor(JBInformationModel jb)
    {
        value.text = jb.Name;

        if (jb.Location == null)
        {
            Location.text = "Vị trí tủ được ghi chú trong sơ đồ";
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

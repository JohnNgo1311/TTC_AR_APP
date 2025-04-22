using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Update_Set_Up_Value_UI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> setup_Value_Texts;
    [SerializeField] private MccInformationModel MCCInformationModel;
    [SerializeField] private FieldDeviceInformationModel FieldDeviceInformationModel;
    // [SerializeField] private string cabinetCode;

    private void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        StartCoroutine(Update_UI());
    }


    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_UI()
    {
        FieldDeviceInformationModel = StaticVariable.temp_FieldDeviceInformationModel;
        MCCInformationModel = StaticVariable.temp_MccInformationModel;
        //!  FieldDeviceInformationModel = MCCInformationModel.FieldDeviceInformationModel[0];
        string[] values = {
            //!   MCCInformationModel.Type,
            "Biến tần",
            FieldDeviceInformationModel.Name,
            MCCInformationModel.CabinetCode,
            MCCInformationModel.Brand,
            FieldDeviceInformationModel.RatedPower,
            FieldDeviceInformationModel.RatedCurrent,
            FieldDeviceInformationModel.ActiveCurrent,
            MCCInformationModel.Note
        };

        for (int i = 0; i < setup_Value_Texts.Count; i++)
        {
            if (string.IsNullOrEmpty(values[i]))
            {
                setup_Value_Texts[i].text = "Chưa cập nhật";
                setup_Value_Texts[i].color = Color.red;
            }
            else
            {
                setup_Value_Texts[i].text = values[i];
                setup_Value_Texts[i].color = Color.black;
            }
            yield return null;
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Update_Set_Up_Value_UI : MonoBehaviour
{
    public List<TMP_Text> setup_Value_Texts;
    public Field_Device_Information_Model field_Device_Information_Model;
    public EventPublisher eventPublisher;

    private void OnEnable()
    {
        StartCoroutine(Update_Set_Up_Value_UI_Panel());
    }

    private IEnumerator Update_Set_Up_Value_UI_Panel()
    {
        yield return new WaitForSeconds(2f);

        eventPublisher.TriggerEvent_ButtonClicked();

        yield return StartCoroutine(Get_SetUp_Value());
        yield return StartCoroutine(Update_UI());

    }
    private IEnumerator Get_SetUp_Value()
    {
        while (GlobalVariable.temp_Field_Device_Information_Model == null)
        {
            Debug.Log("Waiting for GlobalVariable.temp_Field_Device_Information_Model to be assigned...");
            yield return null;
        }
        Debug.Log("All variables have been assigned!");
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_UI()
    {
        field_Device_Information_Model = GlobalVariable.temp_Field_Device_Information_Model;

        string[] values = {
            field_Device_Information_Model.Type,
            field_Device_Information_Model.Name,
            field_Device_Information_Model.Cabinet_Code,
            field_Device_Information_Model.Brand,
            field_Device_Information_Model.Rated_Power,
            field_Device_Information_Model.Output_Power,
            field_Device_Information_Model.Rated_Current,
            field_Device_Information_Model.Active_Current,
            field_Device_Information_Model.Active_Voltage,
            field_Device_Information_Model.Frequency,
            field_Device_Information_Model.Rotation_Speed,
            field_Device_Information_Model.Noted
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

    }
}

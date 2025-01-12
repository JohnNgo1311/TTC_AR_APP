using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Update_Set_Up_Value_UI : MonoBehaviour
{
    public List<TMP_Text> setup_Value_Texts;
    public FieldDeviceInformationModel field_DeviceInformationModel;
    public EventPublisher eventPublisher;

    private void OnEnable()
    {
        StartCoroutine(Update_Set_Up_Value_UI_Panel());
    }

    private IEnumerator Update_Set_Up_Value_UI_Panel()
    {
        yield return new WaitForSeconds(3f);

        eventPublisher.TriggerEvent_ButtonClicked();

        yield return StartCoroutine(Get_SetUp_Value());
        yield return StartCoroutine(Update_UI());

    }
    private IEnumerator Get_SetUp_Value()
    {
        while (GlobalVariable.temp_FieldDeviceInformationModel == null)
        {
            Debug.Log("Waiting for GlobalVariable.temp_FieldDeviceInformationModel to be assigned...");
            yield return null;
        }
        Debug.Log("All variables have been assigned!");
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_UI()
    {
        field_DeviceInformationModel = GlobalVariable.temp_FieldDeviceInformationModel;

        string[] values = {
            field_DeviceInformationModel.Type,
            field_DeviceInformationModel.Name,
            // field_DeviceInformationModel.CabinetCode,
            // field_DeviceInformationModel.Brand,
            field_DeviceInformationModel.RatedPower,
            field_DeviceInformationModel.RatedCurrent,
            field_DeviceInformationModel.ActiveCurrent,
            field_DeviceInformationModel.Note
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

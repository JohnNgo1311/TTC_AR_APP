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
    [SerializeField] private EventPublisher eventPublisher;

    private void OnEnable()
    {

        StartCoroutine(Update_Set_Up_Value_UI_Panel());
    }

    private IEnumerator Update_Set_Up_Value_UI_Panel()
    {
        eventPublisher.TriggerEvent_ButtonClicked();
        yield return StartCoroutine(Get_Module_Information());
        // Chạy song song hai coroutine
        yield return StartCoroutine(Update_UI());

    }
    private IEnumerator Get_Module_Information()
    {
        while (GlobalVariable.temp_Field_Device_Information_Model == null)
        {
            yield return null;
        }

        Debug.Log("All variables have been assigned!");
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_UI()
    {
        field_Device_Information_Model = GlobalVariable.temp_Field_Device_Information_Model;

        setup_Value_Texts[0].text = field_Device_Information_Model.Type;
        yield return null;
        setup_Value_Texts[1].text = field_Device_Information_Model.Name;
        yield return null;
        setup_Value_Texts[2].text = field_Device_Information_Model.Cabinet_Code;
        yield return null;
        setup_Value_Texts[3].text = field_Device_Information_Model.Brand;
        yield return null;
        setup_Value_Texts[4].text = field_Device_Information_Model.Rated_Power;
        yield return null;
        setup_Value_Texts[5].text = field_Device_Information_Model.Output_Power;
        yield return null;
        setup_Value_Texts[6].text = field_Device_Information_Model.Rated_Current;
        yield return null;
        setup_Value_Texts[7].text = field_Device_Information_Model.Active_Current;
        yield return null;
        setup_Value_Texts[8].text = field_Device_Information_Model.Active_Voltage;
        yield return null;
        setup_Value_Texts[9].text = field_Device_Information_Model.Frequency;
        yield return null;
        setup_Value_Texts[10].text = field_Device_Information_Model.Rotation_Speed;
        yield return null;
        setup_Value_Texts[11].text = field_Device_Information_Model.Noted;
    }


    private void OnDisable()
    {

    }
}

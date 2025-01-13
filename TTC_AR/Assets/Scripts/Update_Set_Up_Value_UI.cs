using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Update_Set_Up_Value_UI : MonoBehaviour
{
    public List<TMP_Text> setup_Value_Texts;
    public MccModel MCCInformationModel;
    public FieldDeviceInformationModel FieldDeviceInformationModel;
    public EventPublisher eventPublisher;
    private string cabinetCode;

    private void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        StartCoroutine(Update_Set_Up_Value_UI_Panel());
    }
    private IEnumerator Update_Set_Up_Value_UI_Panel()
    {
        cabinetCode = gameObject.transform.parent.gameObject.name.Split('_')[1];
        Debug.Log("Cabinet Code: " + cabinetCode);
        var cabinetId = GlobalVariable.temp_ListMCCInformationModel.Find(cabinet => cabinet.CabinetCode == cabinetCode).Id;
        GlobalVariable.MCCId = cabinetId;

        yield return new WaitForSeconds(2f);

        eventPublisher.TriggerEvent_ButtonClicked();
        yield return StartCoroutine(GetMCCInformationMopdel());
        yield return StartCoroutine(Update_UI());

    }
    private IEnumerator GetMCCInformationMopdel()
    {
        while (GlobalVariable.temp_MCCInformationModel == null)
        {
            Debug.Log("Waiting for GlobalVariable.temp_MCCInformationModel to be assigned...");
            yield return null;
        }
        Debug.Log("All variables have been assigned!");
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_UI()
    {
        MCCInformationModel = GlobalVariable.temp_MCCInformationModel;
        FieldDeviceInformationModel = MCCInformationModel.FieldDeviceInformationModel[0];
        string[] values = {
            MCCInformationModel.Type,
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
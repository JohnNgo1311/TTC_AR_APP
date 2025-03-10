using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;

public class Update_Module_Specification_Screen : MonoBehaviour
{
    private string moduleSpecificationCode;
    private string adapterSpecificationCode;
    public ModuleSpecificationModel module_Specification_Model;
    public AdapterSpecificationModel adapter_Specification_Model;
    public List<TMP_Text> module_Specification_Texts;
    public Button module_Specification_Button_PDF;
    public List<TMP_Text> adapter_Specification_Texts;
    public Button adapter_Specification_Button_PDF;

    public EventPublisher eventPublisher;

    private void OnEnable()
    {
        StartCoroutine(Initial());
    }

    IEnumerator Initial()
    {
        eventPublisher.TriggerEvent_SpecificationClicked();
        yield return WaitingDownload();
        yield return AssignVariables();
    }
    IEnumerator AssignVariables()
    {
        module_Specification_Model = GlobalVariable.temp_ModuleSpecificationModel;
        adapter_Specification_Model = GlobalVariable.temp_AdapterSpecificationModel;
        moduleSpecificationCode = module_Specification_Model.Code;
        adapterSpecificationCode = adapter_Specification_Model.Code;
        Debug.Log("Module Specification Code: " + moduleSpecificationCode);
        Debug.Log("Adapter Specification Code: " + adapterSpecificationCode);
        yield return null;
        Update_UI();
        module_Specification_Button_PDF.onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(module_Specification_Model.PdfManual));
        adapter_Specification_Button_PDF.onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(adapter_Specification_Model.PdfManual));
    }
    IEnumerator WaitingDownload()
    {
        while (GlobalVariable.temp_ModuleSpecificationModel == null || GlobalVariable.temp_AdapterSpecificationModel == null)
        {
            Debug.Log("Waiting for GlobalVariable.temp_ModuleSpecificationModel to be assigned...");
            yield return null;
        }
        Debug.Log("All variables have been assigned!");
    }

    private void Update_UI()
    {
        // Chạy song song hai coroutine
        StartCoroutine(Update_Module_Specification_UI());
        StartCoroutine(Update_Adapter_Specification_UI());
    }

    // Coroutine cập nhật UI cho Module và Adapter
    private IEnumerator Update_UI(TMP_Text[] texts, string[] values)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = values[i];
            texts[i].color = (texts[i].text == "Chưa cập nhật") ? Color.red : Color.black;
            yield return null; // Tạm dừng để tránh chặn frame
        }
    }

    private IEnumerator Update_Module_Specification_UI()
    {
        string[] values = new string[]
        {
            string.IsNullOrEmpty(module_Specification_Model.Code)? "Chưa cập nhật": module_Specification_Model.Code,
            string.IsNullOrEmpty(module_Specification_Model.Type)? "Chưa cập nhật": module_Specification_Model.Type,
            string.IsNullOrEmpty(module_Specification_Model.SignalType)? "Chưa cập nhật": module_Specification_Model.SignalType,
            string.IsNullOrEmpty(module_Specification_Model.NumOfIO)? "Chưa cập nhật": module_Specification_Model.NumOfIO,
            string.IsNullOrEmpty(module_Specification_Model.CompatibleTBUs)? "Chưa cập nhật": module_Specification_Model.CompatibleTBUs,
            string.IsNullOrEmpty(module_Specification_Model.OperatingVoltage)? "Chưa cập nhật": module_Specification_Model.OperatingVoltage,
            string.IsNullOrEmpty(module_Specification_Model.OperatingCurrent)? "Chưa cập nhật": module_Specification_Model.OperatingCurrent,
            string.IsNullOrEmpty(module_Specification_Model.FlexbusCurrent)? "Chưa cập nhật": module_Specification_Model.FlexbusCurrent,
            string.IsNullOrEmpty(module_Specification_Model.Alarm)? "Chưa cập nhật": module_Specification_Model.Alarm,
            string.IsNullOrEmpty(module_Specification_Model.Note)? "Chưa cập nhật": module_Specification_Model.Note
            // module_Specification_Model?.Type ?? "Chưa cập nhật",
            // module_Specification_Model?.Signal_Type ?? "Chưa cập nhật",
            // module_Specification_Model?.Num_Of_IO ?? "Chưa cập nhật",
            // module_Specification_Model?.Compatible_TBUs ?? "Chưa cập nhật",
            // module_Specification_Model?.Operating_Voltage ?? "Chưa cập nhật",
            // module_Specification_Model?.Operating_Current ?? "Chưa cập nhật",
            // module_Specification_Model?.Flexbus_Current ?? "Chưa cập nhật",
            // module_Specification_Model?.Alarm ?? "Chưa cập nhật",
            // module_Specification_Model?.Noted ?? "Chưa cập nhật"
        };
        yield return Update_UI(module_Specification_Texts.ToArray(), values);
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_Adapter_Specification_UI()
    {
        string[] values = new string[]
        {
            string.IsNullOrEmpty(adapter_Specification_Model.Code)? "Chưa cập nhật": adapter_Specification_Model.Code,
            string.IsNullOrEmpty(adapter_Specification_Model.Type)? "Chưa cập nhật": adapter_Specification_Model.Type,
            string.IsNullOrEmpty(adapter_Specification_Model.Communication)? "Chưa cập nhật": adapter_Specification_Model.Communication,
            string.IsNullOrEmpty(adapter_Specification_Model.NumOfModulesAllowed)? "Chưa cập nhật": adapter_Specification_Model.NumOfModulesAllowed,
            string.IsNullOrEmpty(adapter_Specification_Model.CommSpeed)? "Chưa cập nhật": adapter_Specification_Model.CommSpeed,
            string.IsNullOrEmpty(adapter_Specification_Model.InputSupply)? "Chưa cập nhật": adapter_Specification_Model.InputSupply,
            string.IsNullOrEmpty(adapter_Specification_Model.OutputSupply)? "Chưa cập nhật": adapter_Specification_Model.OutputSupply,
            string.IsNullOrEmpty(adapter_Specification_Model.InrushCurrent)? "Chưa cập nhật": adapter_Specification_Model.InrushCurrent,
            string.IsNullOrEmpty(adapter_Specification_Model.Alarm)? "Chưa cập nhật": adapter_Specification_Model.Alarm,
            string.IsNullOrEmpty(adapter_Specification_Model.Note)? "Chưa cập nhật": adapter_Specification_Model.Note

        };
        yield return Update_UI(adapter_Specification_Texts.ToArray(), values);
    }

    private void Open_Module_Adapter_Online_Catalog(string url)
    {
        Application.OpenURL(url);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        GlobalVariable.temp_ModuleSpecificationModel = null;
        GlobalVariable.temp_AdapterSpecificationModel = null;
        adapter_Specification_Button_PDF.onClick.RemoveAllListeners();
        module_Specification_Button_PDF.onClick.RemoveAllListeners();
        module_Specification_Model = null;
        adapter_Specification_Model = null;
        Debug.Log("All variables have been cleared!");
    }
}
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Update_Module_Specification_Screen : MonoBehaviour
{
    private string module_Type_Name;
    private string adapter_Type_Name;
    public Module_Specification_Model module_Specification_Model;
    public Adapter_Specification_Model adapter_Specification_Model;
    public List<TMP_Text> module_Specification_Texts;
    public Button module_Specification_Button_PDF;
    public List<TMP_Text> adapter_Specification_Texts;
    public Button adapter_Specification_Button_PDF;

    private void OnEnable()
    {
        module_Specification_Model = GlobalVariable.temp_Module_Specification_Model;
        adapter_Specification_Model = GlobalVariable.temp_Adapter_Specification_Model;
        module_Type_Name = GlobalVariable.module_Type_Name;
        adapter_Type_Name = GlobalVariable.apdapter_Type_Name;

        Update_Specification();

        module_Specification_Button_PDF.onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(module_Specification_Model.PDF_Turtorial));
        adapter_Specification_Button_PDF.onClick.AddListener(() => Open_Module_Adapter_Online_Catalog(adapter_Specification_Model.PDF_Turtorial));
    }

    private void Update_Specification()
    {
        // Chạy song song hai coroutine
        StartCoroutine(Update_Module_UI());
        StartCoroutine(Update_Adapter_UI());
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

    private IEnumerator Update_Module_UI()
    {
        string[] values = new string[]
        {
            string.IsNullOrEmpty(module_Specification_Model.Code)? "Chưa cập nhật": module_Specification_Model.Code,
            string.IsNullOrEmpty(module_Specification_Model.Type)? "Chưa cập nhật": module_Specification_Model.Type,
            string.IsNullOrEmpty(module_Specification_Model.Signal_Type)? "Chưa cập nhật": module_Specification_Model.Signal_Type,
            string.IsNullOrEmpty(module_Specification_Model.Num_Of_IO)? "Chưa cập nhật": module_Specification_Model.Num_Of_IO,
            string.IsNullOrEmpty(module_Specification_Model.Compatible_TBUs)? "Chưa cập nhật": module_Specification_Model.Compatible_TBUs,
            string.IsNullOrEmpty(module_Specification_Model.Operating_Voltage)? "Chưa cập nhật": module_Specification_Model.Operating_Voltage,
            string.IsNullOrEmpty(module_Specification_Model.Operating_Current)? "Chưa cập nhật": module_Specification_Model.Operating_Current,
            string.IsNullOrEmpty(module_Specification_Model.Flexbus_Current)? "Chưa cập nhật": module_Specification_Model.Flexbus_Current,
            string.IsNullOrEmpty(module_Specification_Model.Alarm)? "Chưa cập nhật": module_Specification_Model.Alarm,
            string.IsNullOrEmpty(module_Specification_Model.Noted)? "Chưa cập nhật": module_Specification_Model.Noted
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
    private IEnumerator Update_Adapter_UI()
    {
        string[] values = new string[]
        {
            string.IsNullOrEmpty(adapter_Specification_Model.Code)? "Chưa cập nhật": adapter_Specification_Model.Code,
            string.IsNullOrEmpty(adapter_Specification_Model.Type)? "Chưa cập nhật": adapter_Specification_Model.Type,
            string.IsNullOrEmpty(adapter_Specification_Model.Communication)? "Chưa cập nhật": adapter_Specification_Model.Communication,
            string.IsNullOrEmpty(adapter_Specification_Model.Num_Of_Module_Allowed)? "Chưa cập nhật": adapter_Specification_Model.Num_Of_Module_Allowed,
            string.IsNullOrEmpty(adapter_Specification_Model.Comm_Speed)? "Chưa cập nhật": adapter_Specification_Model.Comm_Speed,
            string.IsNullOrEmpty(adapter_Specification_Model.Input_Supply)? "Chưa cập nhật": adapter_Specification_Model.Input_Supply,
            string.IsNullOrEmpty(adapter_Specification_Model.Output_Supply)? "Chưa cập nhật": adapter_Specification_Model.Output_Supply,
            string.IsNullOrEmpty(adapter_Specification_Model.Inrush_Current)? "Chưa cập nhật": adapter_Specification_Model.Inrush_Current,
            string.IsNullOrEmpty(adapter_Specification_Model.Alarm)? "Chưa cập nhật": adapter_Specification_Model.Alarm,
            string.IsNullOrEmpty(adapter_Specification_Model.Noted)? "Chưa cập nhật": adapter_Specification_Model.Noted
            
            // adapter_Specification_Model?.Type ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Communication ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Num_Of_Module_Allowed ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Comm_Speed ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Input_Supply ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Output_Supply ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Inrush_Current  ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Alarm ?? "Chưa cập nhật",
            // adapter_Specification_Model?.Noted ?? "Chưa cập nhật"
        };
        yield return Update_UI(adapter_Specification_Texts.ToArray(), values);
    }

    private void Open_Module_Adapter_Online_Catalog(string url)
    {
        Application.OpenURL(url);
    }

    private void OnDisable()
    {
        adapter_Specification_Button_PDF.onClick.RemoveAllListeners();
        module_Specification_Button_PDF.onClick.RemoveAllListeners();
    }
}

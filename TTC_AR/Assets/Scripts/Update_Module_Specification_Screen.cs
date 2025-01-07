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

    // Coroutine cập nhật UI cho Module
    private IEnumerator Update_Module_UI()
    {
        module_Specification_Texts[0].text = module_Specification_Model.Code;
        yield return null; // Tạm dừng để tránh chặn frame
        module_Specification_Texts[1].text = module_Specification_Model.Type;
        yield return null;
        module_Specification_Texts[2].text = module_Specification_Model.Num_Of_IO;
        yield return null;
        module_Specification_Texts[3].text = module_Specification_Model.Signal_Type;
        yield return null;
        module_Specification_Texts[4].text = module_Specification_Model.Compatible_TBUs;
        yield return null;
        module_Specification_Texts[5].text = module_Specification_Model.Operating_Voltage;
        yield return null;
        module_Specification_Texts[6].text = module_Specification_Model.Operating_Current;
        yield return null;
        module_Specification_Texts[7].text = module_Specification_Model.Flexbus_Current;
        yield return null;
        module_Specification_Texts[8].text = module_Specification_Model.Alarm;
        yield return null;
        module_Specification_Texts[9].text = module_Specification_Model.Noted;
    }

    // Coroutine cập nhật UI cho Adapter
    private IEnumerator Update_Adapter_UI()
    {
        adapter_Specification_Texts[0].text = adapter_Specification_Model.Code;
        yield return null;
        adapter_Specification_Texts[1].text = adapter_Specification_Model.Type;
        yield return null;
        adapter_Specification_Texts[2].text = adapter_Specification_Model.Communication;
        yield return null;
        adapter_Specification_Texts[3].text = adapter_Specification_Model.Num_Of_Module_Allowed;
        yield return null;
        adapter_Specification_Texts[4].text = adapter_Specification_Model.Comm_Speed;
        yield return null;
        adapter_Specification_Texts[5].text = adapter_Specification_Model.Input_Supply;
        yield return null;
        adapter_Specification_Texts[6].text = adapter_Specification_Model.Output_Supply;
        yield return null;
        adapter_Specification_Texts[7].text = adapter_Specification_Model.Inrush_Current;
        yield return null;
        adapter_Specification_Texts[8].text = adapter_Specification_Model.Alarm;
        yield return null;
        adapter_Specification_Texts[9].text = adapter_Specification_Model.Noted;
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

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class Update_JB_TSD_General_UI : MonoBehaviour
{
    private string rack_Name = "Rack_1";
    private string module_Name = "D1.1.I";

    [SerializeField] private Canvas module_Canvas;
    private Module_Information_Model module_Information_Model;
    [SerializeField] private RectTransform list_Devices_Transform;
    [SerializeField] private RectTransform jb_TSD_General_Transform;
    [SerializeField] private RectTransform jb_TSD_Detail_Transform;
    [SerializeField] private RectTransform jb_TSD_Connection_Vertical_Group;
    [SerializeField] private RectTransform jb_TSD_Connection_Horizontal_Group;
    [SerializeField] private Button jb_TSD_Connection_Button_Prefab;
    [SerializeField] private TMP_Text jb_TSD_Connection_Name_Prefab;
    [SerializeField] private TMP_Text jb_TSD_Connection_Location_Prefab;
    public EventPublisher eventPublisher;
    public List<JB_Information_Model> jB_Information_Models = new List<JB_Information_Model>(); //List JB/TSD của Module

    private void OnEnable()
    {
        module_Canvas ??= GetComponentInParent<Canvas>();
        eventPublisher ??= GameObject.Find(nameof(EventPublisher)).GetComponent<EventPublisher>();
        StartCoroutine(InitializeUI());
    }

    private void OnDisable()
    {
        ClearCachedTransforms();
        GlobalVariable.module_Type_Name = "1794-IB32";
        GlobalVariable.apdapter_Type_Name = "1794-ACN15";
    }

    private IEnumerator InitializeUI()
    {
        eventPublisher.TriggerEvent_ButtonClicked();
        yield return StartCoroutine(Get_Module_Information());
        yield return StartCoroutine(InitionalizeUIElement());
        yield return StartCoroutine(Create_Module_General());
    }

    private IEnumerator Get_Module_Information()
    {
        while (GlobalVariable.temp_Module_Information_Model == null ||
               GlobalVariable.temp_List_JB_Information_Model_From_Module == null ||
               GlobalVariable.temp_List_Device_Information_Model_From_Module == null)
        {
            yield return null;
        }

        Debug.Log("All variables have been assigned!");
    }

    private IEnumerator Create_Module_General()
    {
        yield return StartCoroutine(Instantiate_JB_TSD_Connection_List());
    }

    private IEnumerator InitionalizeUIElement()
    {
        yield return null;
        list_Devices_Transform ??= FindRectTransform("List_Devices", module_Canvas.transform);
        yield return null;
        jb_TSD_Detail_Transform ??= FindRectTransform("Detail_JB_TSD", module_Canvas.transform);
        yield return null;
        jb_TSD_General_Transform ??= FindRectTransform("JB_TSD_General_Panel", module_Canvas.transform);
        yield return null;
        jb_TSD_Connection_Vertical_Group ??= FindRectTransform("Scroll_Area/Content/JB_TSD_Connection_Vertical_Group", jb_TSD_General_Transform);
        yield return null;
        jb_TSD_Connection_Horizontal_Group ??= FindRectTransform("JB_TSD_Connection_Horizontal_Group", jb_TSD_Connection_Vertical_Group);
        yield return null;
        jb_TSD_Connection_Button_Prefab ??= FindComponent<Button>("JB_TSD_Connection_Button", jb_TSD_Connection_Horizontal_Group);
        yield return null;
        jb_TSD_Connection_Name_Prefab ??= FindComponent<TMP_Text>("JB_TSD_Connection_Name", jb_TSD_Connection_Button_Prefab.transform);
        yield return null;
        jb_TSD_Connection_Location_Prefab ??= FindComponent<TMP_Text>("JB_TSD_Connection_Location", jb_TSD_Connection_Horizontal_Group);

        rack_Name = $"Rack_{module_Canvas.name.Substring(1, 1)}";
        module_Name = module_Canvas.gameObject.name.Split('_')[0];
        Debug.Log($"Rack Name: {rack_Name}, Module Name: {module_Name}");
        module_Information_Model = GlobalVariable.temp_Module_Information_Model;
        GlobalVariable.module_Type_Name = module_Information_Model.Specification_Model.Type;
        GlobalVariable.apdapter_Type_Name = module_Information_Model.Specification_Model.Adapter.Type;
    }

    private RectTransform FindRectTransform(string name, Transform parent)
    {
        return parent.Find(name).GetComponent<RectTransform>();
    }

    private T FindComponent<T>(string name, Transform parent) where T : Component
    {
        return parent.Find(name).GetComponent<T>();
    }

    private void ClearCachedTransforms()
    {
        StopAllCoroutines();
    }

    private void SetupJB_TSD_Connection()
    {
        if (module_Information_Model.List_JB_Information_Model == null || module_Information_Model.List_JB_Information_Model.Count == 0)
        {
            jb_TSD_Connection_Horizontal_Group.gameObject.SetActive(false);
        }
    }

    private IEnumerator Instantiate_JB_TSD_Connection_List()
    {
        jB_Information_Models = module_Information_Model.List_JB_Information_Model; //List JB/TSD của Module
        int connectionCount = jB_Information_Models.Count; // số lượng phần tử trong List JB/TSD của Module
        Debug.Log($"Connection Count: {connectionCount}");
        for (int i = 0; i < connectionCount; i++)
        {
            int currentIndex = i;
            RectTransform new_JB_TSD_Connection_Horizontal_Group = Instantiate(jb_TSD_Connection_Horizontal_Group, jb_TSD_Connection_Vertical_Group);

            var new_JB_TSD_Connection_Button = new_JB_TSD_Connection_Horizontal_Group.Find("JB_TSD_Connection_Button")?.GetComponent<Button>();
            if (new_JB_TSD_Connection_Button == null)
            {
                Debug.LogError("JB_TSD_Connection_Button not found or missing Button component.");
                continue;
            }

            var new_JB_TSD_Connection_Name = new_JB_TSD_Connection_Button.transform.Find("JB_TSD_Connection_Name")?.GetComponent<TMP_Text>();
            if (new_JB_TSD_Connection_Name == null)
            {
                Debug.LogError("JB_TSD_Connection_Name not found or missing TMP_Text component.");
                continue;
            }

            var new_JB_TSD_Connection_Location = new_JB_TSD_Connection_Horizontal_Group.Find("JB_TSD_Connection_Location")?.GetComponent<TMP_Text>();
            if (new_JB_TSD_Connection_Location == null)
            {
                Debug.LogError("JB_TSD_Connection_Location not found or missing TMP_Text component.");
                continue;
            }

            new_JB_TSD_Connection_Name.text = jB_Information_Models[i].Name;
            new_JB_TSD_Connection_Location.text = jB_Information_Models[i].Location;
            new_JB_TSD_Connection_Button.onClick.AddListener(() =>
            {
                GlobalVariable.navigate_from_List_Devices = false;
                GlobalVariable.navigate_from_JB_TSD_General = true;
                NavigateJBDetailScreen(jB_Information_Models[currentIndex]);
            });
        }
        if (jb_TSD_Connection_Horizontal_Group.gameObject.activeSelf) jb_TSD_Connection_Horizontal_Group.gameObject.SetActive(false);
        yield return null;

    }


    public void NavigateJBDetailScreen(JB_Information_Model jB_Information_Model)
    {
        GlobalVariable.jb_TSD_Title = jB_Information_Model.Name;
        GlobalVariable.jb_TSD_Name = jB_Information_Model.Name;
        GlobalVariable.jb_TSD_Location = jB_Information_Model.Location;
        if (GlobalVariable.navigate_from_JB_TSD_General)
        {
            jb_TSD_General_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            list_Devices_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
    }
}

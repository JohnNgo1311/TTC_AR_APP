using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class Update_JB_TSD_Basic_UI : MonoBehaviour
{
    private string rack_Name = "Rack_1";
    private string module_Name = "D1.1.I";

    [SerializeField] private Canvas module_Canvas;
    private ModuleInformationModel module_Information_Model;
    [SerializeField] private RectTransform list_Devices_Transform;
    [SerializeField] private RectTransform jb_TSD_Basic_Transform;
    [SerializeField] private RectTransform jb_TSD_Detail_Transform;
    [SerializeField] private RectTransform jb_TSD_Connection_Vertical_Group;
    [SerializeField] private RectTransform jb_TSD_Connection_Horizontal_Group;
    [SerializeField] private Button jb_TSD_Connection_Button_Prefab;
    [SerializeField] private TMP_Text jb_TSD_Connection_Name_Prefab;
    [SerializeField] private TMP_Text jb_TSD_Connection_Location_Prefab;
    public EventPublisher eventPublisher;
    public List<JBInformationModel> list_JBInformationModels = new List<JBInformationModel>(); //List JB/TSD của Module

    private void OnEnable()
    {
        module_Canvas ??= GetComponentInParent<Canvas>();
        eventPublisher ??= GameObject.Find(nameof(EventPublisher)).GetComponent<EventPublisher>();
        Initialize();
    }

    private void OnDisable()
    {
        ClearCachedTransforms();

    }
    private void Initialize() { StartCoroutine(InitializeUI()); }
    private IEnumerator InitializeUI()
    {
        eventPublisher.TriggerEvent_ButtonClicked();
        yield return new WaitForSeconds(2.5f);
        yield return Get_Module_Information();
        Debug.Log(GlobalVariable.temp_ModuleInformationModel.Name);
        yield return InitionalizeUIElement();
        yield return Create_Module_Basic();
    }

    private IEnumerator Get_Module_Information()
    {
        while (GlobalVariable.ActiveCloseCanvasButton == false)
        {
            yield return null;
            Debug.Log("Waiting ");

        }

        Debug.Log("All variables have been assigned!");
    }

    private IEnumerator Create_Module_Basic()
    {
        yield return Instantiate_JB_TSD_Connection_List();
    }

    private IEnumerator InitionalizeUIElement()
    {
        yield return null;
        list_Devices_Transform ??= FindRectTransform("list_Devices", module_Canvas.transform);
        yield return null;
        jb_TSD_Detail_Transform ??= FindRectTransform("Detail_JB_TSD", module_Canvas.transform);
        yield return null;
        jb_TSD_Basic_Transform ??= FindRectTransform("JB_TSD_Basic_Panel", module_Canvas.transform);
        yield return null;
        jb_TSD_Connection_Vertical_Group ??= FindRectTransform("Scroll_Area/Content/JB_TSD_Connection_Vertical_Group", jb_TSD_Basic_Transform);
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

        module_Information_Model = GlobalVariable.temp_ModuleInformationModel;
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
        if (module_Information_Model.ListJBInformationModel == null || module_Information_Model.ListJBInformationModel.Count == 0)
        {
            jb_TSD_Connection_Horizontal_Group.gameObject.SetActive(false);
        }
    }

    private IEnumerator Instantiate_JB_TSD_Connection_List()
    {
        list_JBInformationModels = module_Information_Model.ListJBInformationModel; //List JB/TSD của Module
        int connectionCount = list_JBInformationModels.Count; // số lượng phần tử trong List JB/TSD của Module
        Debug.Log($"Connection Count: {connectionCount}");
        if (connectionCount > 0)
        {
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

                new_JB_TSD_Connection_Name.text = list_JBInformationModels[currentIndex].Name;
                new_JB_TSD_Connection_Location.text = list_JBInformationModels[currentIndex].Location;
                new_JB_TSD_Connection_Button.onClick.AddListener(() =>
                {
                    GlobalVariable.navigate_from_List_Devices = false;
                    GlobalVariable.navigate_from_JB_TSD_Basic = true;
                    NavigateJBDetailScreen(list_JBInformationModels[currentIndex]);
                });
            }
        }
        yield return null;
        jb_TSD_Connection_Horizontal_Group.gameObject.SetActive(false);
    }


    public void NavigateJBDetailScreen(JBInformationModel jBInformationModel)
    {
        GlobalVariable.jb_TSD_Title = jBInformationModel.Name;
        Debug.Log
        (
            $"JB Name: {jBInformationModel.Name},\n JB Location: {jBInformationModel.Location}"
        );
        GlobalVariable.jb_TSD_Name = jBInformationModel.Name;
        GlobalVariable.jb_TSD_Location = jBInformationModel.Location;
        if (GlobalVariable.navigate_from_JB_TSD_Basic)
        {
            jb_TSD_Basic_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            list_Devices_Transform.gameObject.SetActive(false);
            jb_TSD_Detail_Transform.gameObject.SetActive(true);
        }
    }
}
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

    private void Start()
    {

    }
    private void OnEnable()
    {
        module_Canvas = GetComponentInParent<Canvas>();
        StartCoroutine(Create_Module_General());
        StartCoroutine(CacheTransforms());
    }

    private void OnDisable()
    {
        ClearCachedTransforms();
        module_Information_Model = null;
        GlobalVariable.module_Type_Name = "1794-IB32";
        GlobalVariable.apdapter_Type_Name = "1794-ACN15";
    }

    private IEnumerator Create_Module_General()
    {
        yield return null;
        rack_Name = $"Rack_{module_Canvas.name.Substring(1, 1)}"; //?{module_Canvas.name.Substring(1, 1)} = 1;
        module_Name = module_Canvas.gameObject.name.Split("_")[0]; //?module_Canvas.gameObject.name.Split("_")[0] = D1.1.I;
        Debug.Log($"Rack Name: {rack_Name}, Module Name: {module_Name}");
        module_Information_Model = GetModuleGeneralModel(rack_Name, module_Name);
        GlobalVariable.module_Type_Name = module_Information_Model.Specification_Model.Type; //?Module_Information_Model.Type = 1794-IB32;
        GlobalVariable.apdapter_Type_Name = module_Information_Model.Specification_Model.Adapter.Type; //?Module_Information_Model.Adapter = 1794-ACN15;
                                                                                                       // SetupJB_TSD_Connection();
        StartCoroutine(Instantiate_JB_TSD_Connection_List());
    }

    private Module_Information_Model GetModuleGeneralModel(string rackName, string moduleName)
    {

        //var rackData = GlobalVariable.rackData_GrapperA;
        // Debug.Log($"{rackData}: Lấy data của Rack A thành công");
        // List<Module_Information_Model> rackList = rackName switch
        // {
        //     "Rack_1" => rackData.Rack_1,
        //     "Rack_2" => rackData.Rack_2,
        //     "Rack_3" => rackData.Rack_3,
        //     "Rack_4" => rackData.Rack_4,
        //     "Rack_5" => rackData.Rack_5,
        //     "Rack_6" => rackData.Rack_6,
        //     _ => null
        // };
        // Debug.Log($"{rackList}: Lấy data của Rack A thành công");
        return null;
    }

    private IEnumerator CacheTransforms()
    {
        yield return null;
        list_Devices_Transform = FindRectTransform("List_Devices");
        jb_TSD_Detail_Transform = FindRectTransform("Detail_JB_TSD");
        jb_TSD_General_Transform = FindRectTransform("JB_TSD_General_Panel");

        jb_TSD_Connection_Vertical_Group = FindRectTransform("Scroll_Area/Content/JB_TSD_Connection_Vertical_Group", jb_TSD_General_Transform);
        jb_TSD_Connection_Horizontal_Group = FindRectTransform("JB_TSD_Connection_Horizontal_Group", jb_TSD_Connection_Vertical_Group);

        jb_TSD_Connection_Button_Prefab = FindComponent<Button>("JB_TSD_Connection_Button", jb_TSD_Connection_Horizontal_Group);
        jb_TSD_Connection_Name_Prefab = FindComponent<TMP_Text>("JB_TSD_Connection_Name", jb_TSD_Connection_Button_Prefab.transform);
        jb_TSD_Connection_Location_Prefab = FindComponent<TMP_Text>("JB_TSD_Connection_Location", jb_TSD_Connection_Horizontal_Group);
    }

    private RectTransform FindRectTransform(string name, Transform parent = null)
    {
        return (parent ?? module_Canvas.transform).Find(name).GetComponent<RectTransform>();
    }

    private T FindComponent<T>(string name, Transform parent) where T : Component
    {
        return parent.Find(name).GetComponent<T>();
    }

    private void ClearCachedTransforms()
    {
        module_Canvas = null;
        list_Devices_Transform = null;
        jb_TSD_Detail_Transform = null;
        jb_TSD_General_Transform = null;
        jb_TSD_Connection_Vertical_Group = null;
        jb_TSD_Connection_Horizontal_Group = null;
        jb_TSD_Connection_Button_Prefab = null;
        jb_TSD_Connection_Name_Prefab = null;
        jb_TSD_Connection_Location_Prefab = null;
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
        yield return new WaitForSeconds(0.1f);
        List<JB_Information_Model> jbConnections = module_Information_Model.List_JB_Information_Model; //List JB/TSD của Module
        int connectionCount = jbConnections.Count; // số lượng phần tử trong List JB/TSD của Module

        for (int i = 0; i < connectionCount; i++)
        {
            string jb_TSD_Connection = jbConnections[i].Name; // Lấy tên JB/TSD
            RectTransform new_JB_TSD_Connection_Horizontal_Group = Instantiate(jb_TSD_Connection_Horizontal_Group, jb_TSD_Connection_Vertical_Group);
            var new_JB_TSD_Connection_Button = new_JB_TSD_Connection_Horizontal_Group.Find("JB_TSD_Connection_Button").GetComponent<Button>();
            var new_JB_TSD_Connection_Name = new_JB_TSD_Connection_Button.transform.Find("JB_TSD_Connection_Name").GetComponent<TMP_Text>();
            var new_JB_TSD_Connection_Location = new_JB_TSD_Connection_Horizontal_Group.Find("JB_TSD_Connection_Location").GetComponent<TMP_Text>();

            var jbParts = jb_TSD_Connection.Split('_');
            new_JB_TSD_Connection_Name.text = jbParts[0];
            new_JB_TSD_Connection_Location.text = jbParts.Length > 1 ? jbParts[1] : string.Empty;

            new_JB_TSD_Connection_Button.onClick.AddListener(() =>
            {
                GlobalVariable.navigate_from_List_Devices = false;
                GlobalVariable.navigate_from_JB_TSD_General = true;
                NavigateJBDetailScreen(jb_TSD_Connection);
            });
        }
        if (jb_TSD_Connection_Horizontal_Group.gameObject.activeSelf) jb_TSD_Connection_Horizontal_Group.gameObject.SetActive(false);
    }

    public void NavigateJBDetailScreen(string jB_TSD_Connection)
    {
        GlobalVariable.jb_TSD_Title = jB_TSD_Connection;
        var jobDetails = GlobalVariable.jb_TSD_Title.Split('_');
        GlobalVariable.jb_TSD_Name = jobDetails[0];
        GlobalVariable.jb_TSD_Location = jobDetails.Length > 1 ? jobDetails[1] : string.Empty;

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

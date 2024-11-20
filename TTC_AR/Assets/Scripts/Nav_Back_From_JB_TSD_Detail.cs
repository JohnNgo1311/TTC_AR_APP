using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Nav_Back_From_JB_TSD_Detail : MonoBehaviour
{

    [SerializeField]
    private Canvas module_Canvas; private RectTransform list_Devices_Transform;
    private RectTransform jb_TSD_General_Transform;
    private RectTransform jb_TSD_Detail_Transform;
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        module_Canvas = GetComponentInParent<Canvas>();
        list_Devices_Transform = module_Canvas.gameObject.transform.Find("List_Devices").GetComponent<RectTransform>();
        jb_TSD_General_Transform = module_Canvas.gameObject.transform.Find("JB_TSD_General_Panel").GetComponent<RectTransform>();
        jb_TSD_Detail_Transform = module_Canvas.gameObject.transform.Find("Detail_JB_TSD").GetComponent<RectTransform>();
    }

    void Update()
    {
    }

    public void NavigatePop()
    {
        if (GlobalVariable.navigate_from_JB_TSD_General)
        {
            jb_TSD_Detail_Transform.gameObject.SetActive(false);
            jb_TSD_General_Transform.gameObject.SetActive(true);
            GlobalVariable.navigate_from_JB_TSD_General = false;
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            jb_TSD_Detail_Transform.gameObject.SetActive(false);
            list_Devices_Transform.gameObject.SetActive(true);
            GlobalVariable.navigate_from_List_Devices = false;
        }

    }
}

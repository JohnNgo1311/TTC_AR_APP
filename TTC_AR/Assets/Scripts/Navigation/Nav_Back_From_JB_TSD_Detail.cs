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
    private Canvas module_Canvas;
    private RectTransform list_Devices_Transform;
    private RectTransform jb_TSD_Basic_Transform;
    private RectTransform jb_TSD_Detail_Transform;
    void Start()
    {

        module_Canvas = GetComponentInParent<Canvas>();
        list_Devices_Transform = module_Canvas.gameObject.transform.Find("List_Devices").GetComponent<RectTransform>();
        jb_TSD_Basic_Transform = module_Canvas.gameObject.transform.Find("JB_TSD_General_Panel").GetComponent<RectTransform>();
        jb_TSD_Detail_Transform = module_Canvas.gameObject.transform.Find("Detail_JB_TSD").GetComponent<RectTransform>();
    }

    void Update()
    {
    }

    public void NavigatePop()
    {
        if (GlobalVariable.navigate_from_JB_TSD_Basic)
        {
            jb_TSD_Detail_Transform.gameObject.SetActive(false);
            jb_TSD_Basic_Transform.gameObject.SetActive(true);
            GlobalVariable.navigate_from_JB_TSD_Basic = false;
        }
        if (GlobalVariable.navigate_from_List_Devices)
        {
            jb_TSD_Detail_Transform.gameObject.SetActive(false);
            list_Devices_Transform.gameObject.SetActive(true);
            GlobalVariable.navigate_from_List_Devices = false;
        }

    }
}

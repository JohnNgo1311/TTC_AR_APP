using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;

//! Script này sử dụng cho JB_TSD_General_Panel trong mỗi module

public class Nav_Back_From_Module_Screen : MonoBehaviour
{
    [SerializeField] private Button close_Module_Screen_Button;
    private GameObject module_Screen;

    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    private void OnEnable()
    {
        module_Screen = transform.parent.gameObject;
        close_Module_Screen_Button = module_Screen.transform.Find("close_Module_Screen_Button").GetComponent<Button>();
        close_Module_Screen_Button.onClick.AddListener(NavigatePop);
    }

    private void NavigatePop()
    {
        GlobalVariable.generalPanel.SetActive(true);
        module_Screen.SetActive(false);
    }
    private void OnDisable()
    {
        close_Module_Screen_Button.onClick.RemoveAllListeners();
    }
}


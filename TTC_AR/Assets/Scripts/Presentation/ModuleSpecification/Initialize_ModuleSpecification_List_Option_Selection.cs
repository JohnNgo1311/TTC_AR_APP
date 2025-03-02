// using System;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;

// public class Initialize_Device_List_Option_Selection : MonoBehaviour
// {
//     [Header("Basic")]
//     public GameObject Selection_Option_Canvas;

//     [Header("List Selection Panels")]
//     public GameObject selection_List_JB_Panel;
//     public GameObject selection_List_ModuleIO_Panel;
//     public GameObject selection_List_Additional_Image_Panel;

//     [Header("List Selection Option Contents")]
//     public Transform JB_List_Selection_Option_Content_Transform;
//     public Transform ModuleIO_List_Selection_Option_Content_Transform;
//     public Transform Additional_Image_List_Selection_Option_Content_Transform;

//     public Dictionary<string, GameObject> initialSelectionOptions = new Dictionary<string, GameObject>();

//     private void Awake()
//     {
//         InitializeItemOptions();
//         PopulateListSelection();
//     }
//     private void Start()
//     {
//     }
//     private void InitializeItemOptions()
//     {
//         initialSelectionOptions["JB"] = JB_List_Selection_Option_Content_Transform.GetChild(0).gameObject;
//         initialSelectionOptions["ModuleIO"] = ModuleIO_List_Selection_Option_Content_Transform.GetChild(0).gameObject;
//         initialSelectionOptions["Additional_Image"] = Additional_Image_List_Selection_Option_Content_Transform.GetChild(0).gameObject;
//     }
//     private void PopulateListSelection()
//     {
//         PopulateSelectionPanel("JB", GlobalVariable.list_jBName, selection_List_JB_Panel, JB_List_Selection_Option_Content_Transform);
//         PopulateSelectionPanel("ModuleIO", GlobalVariable.list_ModuleIOName, selection_List_ModuleIO_Panel, ModuleIO_List_Selection_Option_Content_Transform);
//         PopulateSelectionPanel("Additional_Image", GlobalVariable.list_ImageName, selection_List_Additional_Image_Panel, Additional_Image_List_Selection_Option_Content_Transform);
//     }
//     private void PopulateSelectionPanel(string field, List<string> optionList, GameObject list_Option_Panel, Transform list_Option_Content_Transform)
//     {
//         if (optionList != null && optionList.Count > 0)
//         {
//             if (optionList.Count == 1)
//             {
//                 AddOption(initialSelectionOptions[field], optionList[0], field);
//                 //! Ví dụ: AddOption(initialSelectionOptions["JB"], "02LT002", "JB");
//                 //! initialSelectionOptions["JB"] => Initial Option
//             }
//             else
//             {
//                 foreach (var optionName in optionList)
//                 {
//                     GameObject newOption = Instantiate(initialSelectionOptions[field], list_Option_Content_Transform);
//                     AddOption(newOption, optionName, field);
//                 }
//                 initialSelectionOptions[field].SetActive(false); //! Tắt đi Option mặc định
//             }
//         }
//         var Back_Button = list_Option_Panel.transform.Find("Background/Appbar/Back_Button").gameObject.GetComponent<Button>();
//         Back_Button.onClick.RemoveAllListeners();
//         list_Option_Panel.SetActive(false);
//     }

//     private void AddOption(GameObject option, string text, string field)
//     {
//         SetOptionText(option, text);
//     }

//     private void SetOptionText(GameObject option, string text)
//     {
//         TMP_Text textComponent = option.GetComponentInChildren<TMP_Text>();
//         if (textComponent != null)
//         {
//             textComponent.text = text;
//         }
//     }
//     private void OnEnable()
//     {
//     }
//     private void OnDisable()
//     {
//     }






// }

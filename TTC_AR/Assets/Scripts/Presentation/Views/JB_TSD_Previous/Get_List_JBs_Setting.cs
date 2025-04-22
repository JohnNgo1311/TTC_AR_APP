// using System;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class Get_List_JBs_Setting : MonoBehaviour
// {
//     public Update_JB_TSD_Setting update_JB_TSD_Setting;
//     public GameObject List_JB_Canvas;
//     public GameObject Add_New_JB_Canvas;
//     public GameObject Update_JB_Canvas;
//     public GameObject DialogCanvas;
//     public GameObject JB_Item_Prefab;
//     public GameObject Parent_Vertical_Layout_Group;
//     public ScrollRect scrollView;
//     public GameObject DialogOneButton;
//     public GameObject DialogTwoButton;
//     // private IJBUseCase _jbInformationUseCase;
//     private List<string> ListJBsName = new List<string>();
//     private List<GameObject> listJBItems = new List<GameObject>();

//     private void Awake()
//     {   // Khởi tạo dependency injection đơn giản
//         // IJBRepository repository = new JBRepository();
//         // _jbInformationUseCase = new JBUseCase(repository);
//     }
//     private void Start()
//     {
//         if (GlobalVariable.list_jBName != null && GlobalVariable.list_jBName.Count > 0)
//         {
//             ListJBsName = GlobalVariable.list_jBName;
//             Initialize(ListJBsName);
//         }
//         scrollView.normalizedPosition = new Vector2(0, 1);
//     }

//     private void Initialize(List<string> listJBsName)
//     {
//         foreach (var jbName in listJBsName)
//         {
//             int jbIndex = ListJBsName.IndexOf(jbName);
//             Debug.Log(jbIndex);
//             GameObject newJBItem = Instantiate(JB_Item_Prefab, Parent_Vertical_Layout_Group.transform);
//             Transform newJBItemTransform = newJBItem.transform;
//             Transform newJBItemPreviewInforGroup = newJBItemTransform.GetChild(0);
//             Transform newJBItemPreviewButtonGroup = newJBItemTransform.GetChild(1);
//             newJBItemPreviewInforGroup.Find("Preview_JB_Name").GetComponent<TMP_Text>().text = jbName;
//             newJBItemPreviewInforGroup.Find("Preview_JB_GrapLocation").GetComponent<TMP_Text>().text = "GrapperA";

//             listJBItems.Add(newJBItem);

//             var jBInformationModel = GlobalVariable.temp_ListJBInformationModelFromModule.Find(x => x.Name == jbName);

//             newJBItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditJBItem(jBInformationModel));
//             newJBItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleJBItem(newJBItem, jbName));
//         }
//         JB_Item_Prefab.SetActive(false);
//     }

//     private void DeleJBItem(GameObject JBItem, string JbName)
//     {
//         OpenDeleteWarningPanel(JBItem, JbName);
//     }

//     private void EditJBItem(JBInformationModel jBInformationModel)
//     {
//         OpenUpdateCanvas(jBInformationModel);
//     }

//     public void OpenAddNewCanvas()
//     {
//         Add_New_JB_Canvas.SetActive(true);
//         List_JB_Canvas.SetActive(false);
//         Update_JB_Canvas.SetActive(false);
//     }
//     private void OpenUpdateCanvas(JBInformationModel jBInformationModel)
//     {
//         Update_JB_Canvas.SetActive(true);
//         List_JB_Canvas.SetActive(false);
//         Add_New_JB_Canvas.SetActive(false);
//     }
//     public void BackToListJb()
//     {
//         List_JB_Canvas.SetActive(true);
//         Update_JB_Canvas.SetActive(false);
//         Add_New_JB_Canvas.SetActive(false);
//     }


//     private void OpenDeleteWarningPanel(GameObject JBItem, string JbName)
//     {
//         DialogTwoButton.SetActive(true);
//         var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;
//         var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa tủ <color=#FF0000><b>{JbName}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";
//         var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa tủ JB/TSD khỏi hệ thống?";
//         var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();
//         var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();
//         var jbId = GlobalVariable.temp_ListJBInformationModelFromModule.Find(x => x.Name == JbName).Id;
//         confirmButton.onClick.RemoveAllListeners();
//         backButton.onClick.RemoveAllListeners();
//         confirmButton.onClick.AddListener(() =>
//         {
//             Destroy(JBItem);
//             listJBItems.Remove(JBItem);
//             OnSubmitDeleteJB(jbId);
//             DialogTwoButton.SetActive(false);
//         });

//         backButton.onClick.AddListener(() =>
//         {
//             DialogTwoButton.SetActive(false);
//         });
//     }
//     private async void OnSubmitDeleteJB(string JBId)
//     {
//         // try
//         // {
//         //     bool success = await _jbInformationUseCase.DeleteJBModel(JBId);
//         //     if (success)
//         //     {
//         //         Debug.Log("Delete JB success");
//         //     }
//         //     else
//         //     {
//         //         Debug.Log("Delete JB failed");
//         //     }
//         // }
//         // catch (Exception ex)
//         // {
//         //     Debug.LogError($"Error: {ex.Message}");
//         // }
//     }
// }

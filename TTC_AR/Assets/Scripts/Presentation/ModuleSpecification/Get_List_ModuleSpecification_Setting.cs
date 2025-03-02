// using System;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class Get_List_ModuleSpecification_Setting : MonoBehaviour
// {
//     public GameObject List_ModuleSpecification_Canvas;
//     public GameObject Add_New_ModuleSpecification_Canvas;
//     public GameObject Update_ModuleSpecification_Canvas;
//     public GameObject DialogCanvas;
//     public GameObject ModuleSpecification_Item_Prefab;
//     public GameObject Parent_Vertical_Layout_Group;
//     public ScrollRect scrollView;
//     public GameObject DialogOneButton;
//     public GameObject DialogTwoButton;
//     private IModuleSpecificationUseCase _ModuleSpecificationUseCase;
//     private List<string> ListModuleSpecificationCode = new List<string>();
//     private List<GameObject> listModuleSpecificationItems = new List<GameObject>();

//     private void Awake()
//     {   // Khởi tạo dependency injection đơn giản
//         IModuleSpecificationRepository repository = new ModuleSpecificationRepository();
//         _ModuleSpecificationUseCase = new ModuleSpecificationUseCase(repository);
//     }
//     private void Start()
//     {
//         if (GlobalVariable.list_ModuleSpecificationCode != null && GlobalVariable.list_ModuleSpecificationCode.Count > 0)
//         {
//             ListModuleSpecificationCode = GlobalVariable.list_ModuleSpecificationCode;
//             Initialize(ListModuleSpecificationCode);
//         }
//         scrollView.normalizedPosition = new Vector2(0, 1);
//     }

//     private void Initialize(List<string> listModuleSpecificationCode)
//     {
//         foreach (var ModuleSpecificationCode in listModuleSpecificationCode)
//         {
//             int ModuleSpecificationIndex = ListModuleSpecificationCode.IndexOf(ModuleSpecificationCode);

//             Debug.Log(ModuleSpecificationIndex);

//             GameObject newModuleSpecificationItem = Instantiate(ModuleSpecification_Item_Prefab, Parent_Vertical_Layout_Group.transform);

//             Transform newModuleSpecificationItemTransform = newModuleSpecificationItem.transform;

//             Transform newModuleSpecificationItemPreviewInforGroup = newModuleSpecificationItemTransform.GetChild(0);

//             Transform newModuleSpecificationItemPreviewButtonGroup = newModuleSpecificationItemTransform.GetChild(1);

//             newModuleSpecificationItemPreviewInforGroup.Find("Preview_ModuleSpecification_Code").GetComponent<TMP_Text>().text = ModuleSpecificationCode;

//             newModuleSpecificationItemPreviewInforGroup.Find("Preview_ModuleSpecification_GrapLocation").GetComponent<TMP_Text>().text = "GrapperA";

//             //  var Preview_ModuleSpecification_IOAddress = GlobalVariable.temp_Dictionary_ModuleSpecificationIOAddress[ModuleSpecificationCode];

//             // newModuleSpecificationItemPreviewInforGroup.Find("Preview_ModuleSpecification_IOAddress").GetComponent<TMP_Text>().text = Preview_ModuleSpecification_IOAddress;

//             newModuleSpecificationItemPreviewInforGroup.Find("Preview_ModuleSpecification_IOAddress").GetComponent<TMP_Text>().text = "A4.0.I";

//             listModuleSpecificationItems.Add(newModuleSpecificationItem);

//             newModuleSpecificationItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(EditModuleSpecificationItem);

//             newModuleSpecificationItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleModuleSpecificationItem(newModuleSpecificationItem, ModuleSpecificationCode));

//         }
//         ModuleSpecification_Item_Prefab.SetActive(false);
//     }
//     private void EditModuleSpecificationItem()
//     {
//         OpenUpdateCanvas();
//     }
//     private void DeleModuleSpecificationItem(GameObject ModuleSpecificationItem, string ModuleSpecificationCode)
//     {
//         OpenDeleteWarningPanel(ModuleSpecificationItem, ModuleSpecificationCode);
//     }

//     public void OpenAddNewCanvas()
//     {
//         Add_New_ModuleSpecification_Canvas.SetActive(true);
//         List_ModuleSpecification_Canvas.SetActive(false);
//         Update_ModuleSpecification_Canvas.SetActive(false);
//     }
//     private void OpenUpdateCanvas()
//     {
//         Update_ModuleSpecification_Canvas.SetActive(true);
//         List_ModuleSpecification_Canvas.SetActive(false);
//         Add_New_ModuleSpecification_Canvas.SetActive(false);
//     }
//     public void BackToListModuleSpecification()
//     {
//         List_ModuleSpecification_Canvas.SetActive(true);
//         Update_ModuleSpecification_Canvas.SetActive(false);
//         Add_New_ModuleSpecification_Canvas.SetActive(false);
//     }

//     private void OpenDeleteWarningPanel(GameObject ModuleSpecificationItem, string ModuleSpecificationCode)
//     {
//         DialogTwoButton.SetActive(true);
//         var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;
//         var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text =
//         $"Bạn có chắc chắn muốn thiết bị cảm biến <color=#FF0000><b>{ModuleSpecificationCode}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

//         var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa thiết bị cảm biến khỏi hệ thống?";

//         var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

//         var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

//         //var ModuleSpecificationId = GlobalVariable.temp_ListModuleSpecificationInformationModel.Find(x => x.Code == ModuleSpecificationCode).Id;

//         confirmButton.onClick.RemoveAllListeners();

//         backButton.onClick.RemoveAllListeners();

//         confirmButton.onClick.AddListener(() =>
//         {
//             Destroy(ModuleSpecificationItem);

//             listModuleSpecificationItems.Remove(ModuleSpecificationItem);

//             //OnSubmitDeleteModuleSpecification(ModuleSpecificationId);

//             DialogTwoButton.SetActive(false);

//         });

//         backButton.onClick.AddListener(() =>
//         {
//             DialogTwoButton.SetActive(false);
//         });
//     }
//     private async void OnSubmitDeleteModuleSpecification(int ModuleSpecificationId)
//     {
//         try
//         {
//             bool success = await _ModuleSpecificationUseCase.DeleteModuleSpecificationModel(ModuleSpecificationId);
//             if (success)
//             {
//                 Debug.Log("Delete ModuleSpecification success");
//             }
//             else
//             {
//                 Debug.Log("Delete ModuleSpecification failed");
//             }
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError($"Error: {ex.Message}");
//         }
//     }
// }

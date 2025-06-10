// using System;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class Get_List_Adapter_Setting : MonoBehaviour
// {
//     public GameObject List_Adapter_Canvas;
//     public GameObject Add_New_Adapter_Canvas;
//     public GameObject Update_Adapter_Canvas;
//     public GameObject DialogCanvas;
//     public GameObject Adapter_Item_Prefab;
//     public GameObject Parent_Vertical_Layout_Group;
//     public ScrollRect scrollView;
//     public GameObject DialogOneButton;
//     public GameObject DialogTwoButton;
//     private IAdapterUseCase _AdapterInformationUseCase;
//     private List<string> ListAdapterCode = new List<string>();
//     private List<GameObject> listAdapterItems = new List<GameObject>();

//     private void Awake()
//     {   // Khởi tạo dependency injection đơn giản
//         IAdapterRepository repository = new AdapterRepository();
//         _AdapterInformationUseCase = new AdapterUseCase(repository);
//     }
//     private void Start()
//     {
//         if (GlobalVariable.list_AdapterCode != null && GlobalVariable.list_AdapterCode.Count > 0)
//         {
//             ListAdapterCode = GlobalVariable.list_AdapterCode;
//             Initialize(ListAdapterCode);
//         }
//         scrollView.normalizedPosition = new Vector2(0, 1);
//     }

//     private void Initialize(List<string> listAdapterCode)
//     {
//         foreach (var AdapterCode in listAdapterCode)
//         {
//             int AdapterIndex = ListAdapterCode.IndexOf(AdapterCode);

//             Debug.Log(AdapterIndex);

//             GameObject newAdapterItem = Instantiate(Adapter_Item_Prefab, Parent_Vertical_Layout_Group.transform);

//             Transform newAdapterItemTransform = newAdapterItem.transform;

//             Transform newAdapterItemPreviewInforGroup = newAdapterItemTransform.GetChild(0);

//             Transform newAdapterItemPreviewButtonGroup = newAdapterItemTransform.GetChild(1);

//             newAdapterItemPreviewInforGroup.Find("Preview_Adapter_Code").GetComponent<TMP_Text>().text = AdapterCode;

//             newAdapterItemPreviewInforGroup.Find("Preview_Adapter_GrapLocation").GetComponent<TMP_Text>().text = "GrapperA";

//             //  var Preview_Adapter_IOAddress = GlobalVariable.temp_Dictionary_AdapterIOAddress[AdapterCode];

//             // newAdapterItemPreviewInforGroup.Find("Preview_Adapter_IOAddress").GetComponent<TMP_Text>().text = Preview_Adapter_IOAddress;

//             newAdapterItemPreviewInforGroup.Find("Preview_Adapter_IOAddress").GetComponent<TMP_Text>().text = "A4.0.I";

//             listAdapterItems.Add(newAdapterItem);

//             newAdapterItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(EditAdapterItem);

//             newAdapterItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleAdapterItem(newAdapterItem, AdapterCode));

//         }
//         Adapter_Item_Prefab.SetActive(false);
//     }
//     private void EditAdapterItem()
//     {
//         OpenUpdateCanvas();
//     }
//     private void DeleAdapterItem(GameObject AdapterItem, string AdapterCode)
//     {
//         OpenDeleteWarningPanel(AdapterItem, AdapterCode);
//     }

//     public void OpenAddNewCanvas()
//     {
//         Add_New_Adapter_Canvas.SetActive(true);
//         List_Adapter_Canvas.SetActive(false);
//         Update_Adapter_Canvas.SetActive(false);
//     }
//     private void OpenUpdateCanvas()
//     {
//         Update_Adapter_Canvas.SetActive(true);
//         List_Adapter_Canvas.SetActive(false);
//         Add_New_Adapter_Canvas.SetActive(false);
//     }
//     public void BackToListAdapter()
//     {
//         List_Adapter_Canvas.SetActive(true);
//         Update_Adapter_Canvas.SetActive(false);
//         Add_New_Adapter_Canvas.SetActive(false);
//     }

//     private void OpenDeleteWarningPanel(GameObject AdapterItem, string AdapterCode)
//     {
//         DialogTwoButton.SetActive(true);
//         var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;
//         var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text =
//         $"Bạn có chắc chắn muốn thiết bị cảm biến <color=#FF0000><b>{AdapterCode}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

//         var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa thiết bị cảm biến khỏi hệ thống?";

//         var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

//         var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

//         //var AdapterId = GlobalVariable.temp_ListAdapterInformationModel.Find(x => x.Code == AdapterCode).Id;

//         confirmButton.onClick.RemoveAllListeners();

//         backButton.onClick.RemoveAllListeners();

//         confirmButton.onClick.AddListener(() =>
//         {
//             Destroy(AdapterItem);

//             listAdapterItems.Remove(AdapterItem);

//             //OnSubmitDeleteAdapter(AdapterId);

//             DialogTwoButton.SetActive(false);

//         });

//         backButton.onClick.AddListener(() =>
//         {
//             DialogTwoButton.SetActive(false);
//         });
//     }
//     private async void OnSubmitDeleteAdapter(int AdapterId)
//     {
//         try
//         {
//             bool success = await _AdapterInformationUseCase.DeleteAdapterModel(AdapterId);
//             if (success)
//             {
//                 Debug.Log("Delete Adapter success");
//             }
//             else
//             {
//                 Debug.Log("Delete Adapter failed");
//             }
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError($"Error: {ex.Message}");
//         }
//     }
// }

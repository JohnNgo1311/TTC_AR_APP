// using System.Collections.Generic;
// using System.Linq;
// using EasyUI.Progress;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class ListRackSettingView : MonoBehaviour, IRackView
// {
//     [Header("Canvas")]
//     public GameObject List_Rack_Canvas;
//     public GameObject Add_New_Rack_Canvas;
//     public GameObject Update_Rack_Canvas;

//     public GameObject Rack_Item_Prefab;
//     public GameObject Parent_Vertical_Layout_Group;
//     public ScrollRect scrollView;
//     private List<GameObject> listRackItems = new List<GameObject>();

//     public GameObject DialogOneButton;
//     public GameObject DialogTwoButton;
//     private RackPresenter _presenter;

//     void Awake()
//     {
//         // var RackManager = FindObjectOfType<RackManager>();
//         _presenter = new RackPresenter(this,
//         ManagerLocator.Instance.RackManager._IRackService);
//         // RackManager._IRackService
//     }
//     void OnEnable()
//     {
//         LoadListRack();
//     }
//     void OnDisable()
//     {
//     }

//     private void RefreshList()
//     {
//         Rack_Item_Prefab.SetActive(true);
//         foreach (var item in listRackItems)
//         {
//             if (item != Rack_Item_Prefab)
//                 Destroy(item);
//         }
//         listRackItems.Clear();
//     }

//     public void LoadListRack()
//     {
//         RefreshList();
//         _presenter.LoadListRack(GlobalVariable.GrapperId);
//     }
//     public void DisplayList(List<RackInformationModel> models)
//     {
//         if (models.Any())
//         {
//             foreach (var model in models)
//             {
//                 int RackIndex = models.IndexOf(model);
//                 Debug.Log(RackIndex);
//                 var newRackItem = Instantiate(Rack_Item_Prefab, Parent_Vertical_Layout_Group.transform);
//                 Transform newRackItemTransform = newRackItem.transform;
//                 Transform newRackItemPreviewInforGroup = newRackItemTransform.GetChild(0);
//                 newRackItemPreviewInforGroup.Find("Preview_Rack_Name").GetComponent<TMP_Text>().text = model.Name;
//                 Transform newRackItemPreviewButtonGroup = newRackItemTransform.GetChild(1);
//                 listRackItems.Add(newRackItem);
//                 newRackItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(() => EditRackItem(model.Id));
//                 newRackItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleRackItem(newRackItem, model));
//             }
//         }
//         else
//         {
//             Debug.Log("No Racks found");
//         }
//         Rack_Item_Prefab.SetActive(false);
//     }

//     private void EditRackItem(string id)
//     {
//         GlobalVariable.RackId = id;
//         OpenUpdateCanvas();
//     }
//     private void DeleRackItem(GameObject RackItem, RackInformationModel model)
//     {
//         OpenDeleteWarningDialog(RackItem, model);
//     }

//     public void OpenAddNewCanvas()
//     {
//         Add_New_Rack_Canvas.SetActive(true);
//         List_Rack_Canvas.SetActive(false);
//         Update_Rack_Canvas.SetActive(false);
//     }
//     private void OpenUpdateCanvas()
//     {
//         List_Rack_Canvas.SetActive(false);
//         Add_New_Rack_Canvas.SetActive(false);
//         Update_Rack_Canvas.SetActive(true);
//     }

//     private void OpenDeleteWarningDialog(GameObject RackItem, RackInformationModel model)
//     {
//         DialogTwoButton.SetActive(true);

//         var backgroundTransform = DialogTwoButton.transform.Find("Background");

//         var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;

//         var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = $"Bạn có chắc chắn muốn xóa thông tin Rack IO <b><color =#004C8A>{model.Name}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";

//         var dialog_Title = DialogTwoButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = "Xóa Rack IO khỏi hệ thống?";

//         backgroundTransform.Find("Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Icon_For_Dialog");

//         var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();

//         confirmButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Warning_Back_Button_Background");

//         var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();

//         confirmButton.onClick.RemoveAllListeners();

//         backButton.onClick.RemoveAllListeners();

//         confirmButton.onClick.AddListener(() =>
//         {
//             listRackItems.Remove(RackItem);
//             Debug.Log(model.Id);
//             _presenter.DeleteRack(model.Id);
//             DialogTwoButton.SetActive(false);
//             Destroy(RackItem);
//         });
//         backButton.onClick.AddListener(() =>
//         {
//             DialogTwoButton.SetActive(false);
//         });
//     }
//     private void OpenErrorDialog(string title = "Xóa Rack IO thất bại", string message = "Đã có lỗi xảy ra khi xóa Rack IO khỏi hệ thống. Vui lòng thử lại sau")
//     {
//         DialogOneButton.SetActive(true);

//         var backButton = DialogOneButton.transform.Find("Background/Back_Button").GetComponent<Button>();

//         backButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Back_Button_Background");

//         var dialog_Icon = DialogOneButton.transform.Find("Background/Dialog_Status_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UIimages/Error_Icon_For_Dialog");

//         var dialog_Content = DialogOneButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text = message;

//         var dialog_Title = DialogOneButton.transform.Find("Background/Dialog_Title").GetComponent<TMP_Text>().text = title;

//         backButton.onClick.RemoveAllListeners();

//         backButton.onClick.AddListener(() =>
//         {
//             DialogOneButton.SetActive(false);
//         });

//     }
//     private void ShowProgressBar(string title, string details)
//     {
//         Progress.Show(title, ProgressColor.Blue, true);
//         Progress.SetDetailsText(details);
//     }
//     private void HideProgressBar()
//     {
//         Progress.Hide();
//     }
//     public void ShowLoading(string title) => ShowProgressBar(title, "Đang tải dữ liệu...");
//     public void HideLoading() => HideProgressBar();

//     public void ShowError(string message)
//     {
//         if (GlobalVariable.APIRequestType.Contains("GET_Rack_List"))
//         {
//             OpenErrorDialog(title: "Tải danh sách thất bại", message: "Đã có lỗi xảy ra khi tải danh sách. Vui lòng thử lại sau");
//         }
//         else if (GlobalVariable.APIRequestType.Contains("DELETE_Rack"))
//         {
//             OpenErrorDialog();
//         }
//     }
//     public void ShowSuccess()
//     {
//         if (GlobalVariable.APIRequestType.Contains("GET_Rack_List"))
//         {
//             Show_Toast.Instance.ShowToast("success", "Tải danh sách thành công");
//         }
//         else if (GlobalVariable.APIRequestType.Contains("DELETE_Rack"))
//         {
//             Show_Toast.Instance.ShowToast("success", "Xóa Rack IO thành công");
//         }

//         StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
//     }

//     // Không dùng trong ListView
//     public void DisplayDetail(RackInformationModel model) { }
//     public void DisplayCreateResult(bool success) { }
//     public void DisplayUpdateResult(bool success) { }
//     public void DisplayDeleteResult(bool success) { }
// }
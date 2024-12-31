// using System.Collections.Generic;
// using UnityEngine;
// using Newtonsoft.Json;
// using TMPro;
// using System;
// using UnityEngine.UI;
// using UnityEngine.Networking;
// using System.Text;
// using UnityEngine.SceneManagement;
// using System.Collections;
// using System.Threading.Tasks;

// public class Create_New_Device_By_Grapper : MonoBehaviour
// {
//     public GameObject Location_Image_Create_Group;
//     public GameObject Connection_Image_Create_Group;
//     [SerializeField]
//     private string grapperName = "A";

//     [SerializeField]
//     private Device_Information_Model device = new Device_Information_Model();
//     public List<TMP_InputField> inputFields = new List<TMP_InputField>();
//     public Button cancelButton;
//     public Button confirmButton;
//     public GameObject panelDialog;

//     [Header("Set Interactive false To these objects")]

//     [SerializeField]
//     private ScrollRect scrollRect;

//     [SerializeField]
//     private Button editButton;

//     [SerializeField]
//     private Button backButton;
//     [SerializeField]
//     private TMP_InputField inputField_Search;

//     private void Start()
//     {
//         panelDialog.SetActive(false);
//     }

//     public void OpenPanelCreateDevice()
//     {
//         SetInteractable(false);
//         ShowQuestionDialog(
//             confirmAction: () => StartCoroutine(CreateDevicesByGrapper()),
//             cancelAction: ClearInputFieldsAndListeners
//         );
//     }

//     private void ShowQuestionDialog(Action confirmAction, Action cancelAction)
//     {
//         panelDialog.SetActive(true);
//         cancelButton.onClick.AddListener(new UnityEngine.Events.UnityAction(cancelAction));
//         confirmButton.onClick.AddListener(new UnityEngine.Events.UnityAction(confirmAction));
//     }

//     private void ClearInputFieldsAndListeners()
//     {
//         panelDialog.SetActive(false);
//         confirmButton.onClick.RemoveAllListeners();
//         cancelButton.onClick.RemoveAllListeners();
//         for (int i = 0; i < inputFields.Count; i++)
//         {
//             inputFields[i].text = "";
//         }
//         SetInteractable(true);
//     }

//     private IEnumerator CreateDevicesByGrapper()
//     {
//         device.location = $"Grapper{grapperName}";
//         device.Code = inputFields[0].text;
//         device.Function = inputFields[1].text;
//         device.Range = inputFields[2].text;
//         device.IOAddress = inputFields[3].text;
//         device.JB.List_Connection_Images = $"{inputFields[4].text}_{inputFields[5].text}";
//         device.listImageConnection = new List<string>();

//         for (int i = 0; i < inputFields.Count; i++)
//         {
//             if (string.IsNullOrEmpty(inputFields[i].text))
//             {
//                 Debug.LogError("Input fields cannot be empty.");
//                 yield break;
//             }
//         }

//         yield return APIManager.Instance.CreateNewDevice($"{GlobalVariable.baseUrl}{grapperName}", device, SceneManager.GetActiveScene().name);

//         SetInteractable(true);
//     }

//     private void SetInteractable(bool state)
//     {
//         backButton.interactable = state;
//         scrollRect.vertical = state;
//         editButton.interactable = state;
//         inputField_Search.interactable = state;
//     }
//     private async Task Set_Up_JB_Location_Group_Image(Device_Information_Model device)
//     {
//         await Task.Yield();

//         /* if (Location_Image_Create_Group.transform.childCount < GlobalVariable.list_Temp_JB_Location_Image.Count)
//          {
//              Instantiate(Location_Image_Create_Group.transform.GetChild(0).gameObject, Location_Image_Create_Group.transform);
//          }
//          else */
//         if (Location_Image_Create_Group.transform.childCount > GlobalVariable.list_Temp_JB_Location_Image.Count)
//         {
//             int compare_Count = Location_Image_Create_Group.transform.childCount - GlobalVariable.list_Temp_JB_Location_Image.Count;
//             for (int i = 1; i <= compare_Count; i++)
//             {
//                 Location_Image_Create_Group.transform.GetChild(Location_Image_Create_Group.transform.childCount - i).gameObject.SetActive(false);
//             }
//         }

//         var jbConnectionParts = device.JB.List_Connection_Images?.Split('_');
//         if (jbConnectionParts != null && jbConnectionParts.Length == 2)
//         {
//             for (int i = 1; i <= GlobalVariable.list_Temp_JB_Location_Image.Count; i++)
//             {
//                 Location_Image_Create_Group.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>().sprite = GlobalVariable.list_Temp_JB_Location_Image[i - 1];
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Invalid jbConnection format.");
//         }
//     }

//     private async Task Set_Up_JB_Connection_Group_Image(Device_Information_Model device)
//     {
//         await Task.Yield();

//         /*   if (Connection_Image_Create_Group.transform.childCount < GlobalVariable.list_Temp_JB_Connection_Image.Count)
//            {
//                Instantiate(Connection_Image_Create_Group.transform.GetChild(0).gameObject, Connection_Image_Create_Group.transform);
//            }
//            else */
//         if (Connection_Image_Create_Group.transform.childCount > GlobalVariable.list_Temp_JB_Connection_Image.Count)
//         {
//             int compare_Count = Connection_Image_Create_Group.transform.childCount - GlobalVariable.list_Temp_JB_Connection_Image.Count;
//             for (int i = 1; i <= compare_Count; i++)
//             {
//                 Connection_Image_Create_Group.transform.GetChild(Connection_Image_Create_Group.transform.childCount - i).gameObject.SetActive(false);
//             }
//         }

//         var jbConnectionParts = device.JB.List_Connection_Images?.Split('_');
//         if (jbConnectionParts != null && jbConnectionParts.Length == 2)
//         {
//             for (int i = 1; i <= GlobalVariable.list_Temp_JB_Connection_Image.Count; i++)
//             {
//                 Connection_Image_Create_Group.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>().sprite = GlobalVariable.list_Temp_JB_Connection_Image[i - 1];
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Invalid jbConnection format.");
//         }
//     }

// }

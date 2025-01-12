// using System.Threading.Tasks;
// using UnityEngine;
// using UnityEngine.Networking;
// using Newtonsoft.Json;
// using TMPro;
// using UnityEngine.UI;
// using System;

// public class Delete_Device_By_Grapper : MonoBehaviour
// {
//     [SerializeField]
//     private string grapper_Name = "A";

//     [SerializeField]
//     private DeviceInformationModel device;

//     public Text code_text;
//     public string title = "Delete Device Data";
//     public Button cancelButton;
//     public Button confirmButton;
//     public GameObject panel_Dialog;
//     // public GameObject loadingIcon; // Biểu tượng loading

//     private void Start()
//     {
//         panel_Dialog.SetActive(false);
//     }

//     public void Open_Panel_Delete_Device()
//     {
//         device = GlobalVariable_Search_Devices.all_Device_GrapperA.Find(d => d.Code == code_text.text);
//         if (device != null)
//         {
//             //Debug"Device found!");
//         }
//         else
//         {
//             Debug.LogWarning("Device not found!");
//             return;
//         }

//         Show_Question_Dialog(
//             confirmAction: async () =>
//             {
//                 await DeleteDeviceData();
//             },
//             cancelAction: () =>
//             {
//                 panel_Dialog.SetActive(false);
//             }
//         );
//     }

//     private void Show_Question_Dialog(Action confirmAction, Action cancelAction)
//     {
//         panel_Dialog.SetActive(true);
//         cancelButton.onClick.RemoveAllListeners();
//         confirmButton.onClick.RemoveAllListeners();
//         cancelButton.onClick.AddListener(new UnityEngine.Events.UnityAction(cancelAction));
//         confirmButton.onClick.AddListener(new UnityEngine.Events.UnityAction(confirmAction));
//     }

//     private async Task DeleteDeviceData()
//     {
//         await Delete_device();
//     }

//     public async Task Delete_device()
//     {
//         panel_Dialog.SetActive(false);
//         // loadingIcon.SetActive(true); // Hiển thị biểu tượng loading
//         await Delete_device_Data($"{GlobalVariable.baseUrl}{grapper_Name}", device);
//         // loadingIcon.SetActive(false); // Ẩn biểu tượng loading khi hoàn thành
//     }

//     private async Task Delete_device_Data(string url, DeviceInformationModel device)
//     {
//         using (UnityWebRequest www = UnityWebRequest.Delete($"{url}/{device.Id}"))
//         {
//             var operation = www.SendWebRequest();

//             while (!operation.isDone)
//             {
//                 await Task.Yield(); // Chờ đợi trong khi vẫn duy trì hiệu suất
//             }

//             if (www.result != UnityWebRequest.Result.Success)
//             {
//                 Debug.LogError($"Error: {www.error}");
//             }
//             else
//             {
//                 //Debug"Delete complete!");
//             }
//         }
//     }
// }

// // Presentation/Controllers/JBController.cs
// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class JBController : MonoBehaviour
// {
//     [SerializeField] private Button submitButton;
//     private IJBUseCase _jbGeneralUseCase;

//     void Awake()
//     {
//         // Khởi tạo dependency injection đơn giản
//         IJBRepository repository = new JBRepository();
//         _jbGeneralUseCase = new JBUseCase(repository);
//     }

//     void Start()
//     {
//         submitButton.onClick.AddListener(OnSubmitButtonClicked);
//     }

//     private async void OnSubmitButtonClicked()
//     {
//         // Tạo sample data (có thể thay bằng dữ liệu từ UI)
//         var model = new JBGeneralModel(
//             id: 1,
//             name: "Test JB",
//             location: "Room 101",
//             listDevices: new List<DeviceBasicModel>(),
//             listModules: new List<ModuleBasicModel>(),
//             outdoorImage: new ImageBasicModel(1, "1"),
//     listConnectionImages: new List<ImageBasicModel>()
//         );

//         try
//         {
//             bool success = await _jbGeneralUseCase.UpdateJBModel(model);
//             Debug.Log(success ? "Data posted successfully" : "Failed to post data");
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError($"Error: {ex.Message}");
//         }
//     }

//     void OnDestroy()
//     {
//         submitButton.onClick.RemoveListener(OnSubmitButtonClicked);
//     }
// }
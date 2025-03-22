// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using EasyUI.Progress;
// using UnityEngine;

// public class GetListJBFromModule : MonoBehaviour
// {
//     private void OnEnable()
//     {
//         // if (!StaticVariable.navigate_from_JB_TSD_Detail)
//         // {
//         GetListJB();
//         // }
//         StaticVariable.navigate_from_JB_TSD_Detail = false;
//     }

//     public async void GetListJB()
//     {
//         {
//             try
//             {
//                 StaticVariable.ready_To_Nav_New_Scene = false;
//                 StaticVariable.ready_To_Reset_ListJB = true;
//                 StaticVariable.ready_To_Update_UI = false;

//                 foreach (var jb in StaticVariable.temp_ListJBInformationModelFromModule)
//                 {
//                     // Debug.Log("jb: " + jb.Name);

//                     if (jb == null)
//                     {
//                         Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin jb!");
//                         return;
//                     }

//                     await Move_On_Main_Thread.RunOnMainThread(() =>
//                               {
//                                   //   Show_Toast.Instance.Set_Instance_Status_True();
//                                   //   Show_Toast.Instance.ShowToast("loading", "Đang tải dữ liệu...");
//                                   ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
//                               });

//                     await APIManagerOpenCV.Instance.GetJBInformation(StaticVariable.GetJBUrl + "/" + jb.Id);

//                     StaticVariable.ready_To_Reset_ListJB = false;

//                 }

//                 await APIManagerOpenCV.Instance.DownloadImagesAsync();
//                 await Move_On_Main_Thread.RunOnMainThread(() =>
//                        {
//                            //    StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
//                            HideProgressBar();
//                        });

//                 StaticVariable.ready_To_Update_UI = true;
//                 StaticVariable.ready_To_Nav_New_Scene = true;
//                 StaticVariable.ready_To_Reset_ListJB = true;
//             }
//             catch (Exception e)
//             {
//                 Debug.LogWarning("Error: " + e.Message);
//                 StaticVariable.ready_To_Nav_New_Scene = false;
//                 // Xử lý lỗi và hiển thị thông báo
//                 await Move_On_Main_Thread.RunOnMainThread(() =>
//                  {
//                      Show_Toast.Instance.ShowToast("failure", "Đã có lỗi xảy ra");
//                  });
//                 await Task.Delay(2000);
//                 await Move_On_Main_Thread.RunOnMainThread(() =>
//                   {
//                       StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
//                   });
//             }
//         }
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
// }

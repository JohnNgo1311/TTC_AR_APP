using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyUI.Progress;
using UnityEngine;

public class GetJBInformation : MonoBehaviour
{
    private void OnEnable()
    {
        if (StaticVariable.navigate_from_JB_TSD_Basic)
        {
            // GetJB();
        }

        // StaticVariable.navigate_from_JB_TSD_Detail = false;
    }

    public async void GetJB()
    {
        {
            try
            {
                StaticVariable.ready_To_Nav_New_Scene = false;
                StaticVariable.ready_To_Reset_ListJB = true;
                StaticVariable.ready_To_Update_JB_UI = false;

                // foreach (var jb in StaticVariable.temp_ListJBInformationModelFromModule)
                // {
                // Debug.Log("jb: " + jb.Name);

                var jb = StaticVariable.temp_ListJBInformationModelFromModule.Find(jb => jb.Name == StaticVariable.jb_TSD_Name);

                if (jb == null)
                {
                    Debug.LogWarning("Không tìm thấy thông tin jb!");
                    Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin jb!");
                    return;
                }

                await Move_On_Main_Thread.RunOnMainThread(() =>
                          {
                              ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                          });

                await APIManagerOpenCV.Instance.GetJBInformation(StaticVariable.GetJBUrl + "/" + jb.Id);

                StaticVariable.ready_To_Reset_ListJB = false;

                // }

                // await APIManagerOpenCV.Instance.DownloadImagesAsync();
                await Move_On_Main_Thread.RunOnMainThread(() =>
                       {
                           HideProgressBar();
                       });

                StaticVariable.ready_To_Update_JB_UI = true;
                StaticVariable.ready_To_Nav_New_Scene = true;
                StaticVariable.ready_To_Reset_ListJB = true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
                StaticVariable.ready_To_Nav_New_Scene = false;
                // Xử lý lỗi và hiển thị thông báo
                await Move_On_Main_Thread.RunOnMainThread(() =>
                 {
                     Show_Toast.Instance.ShowToast("failure", "Đã có lỗi xảy ra");
                 });
                await Task.Delay(1000);
                await Move_On_Main_Thread.RunOnMainThread(() =>
                  {
                      StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
                  });
            }
        }
    }

    private void ShowProgressBar(string title, string details)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        Progress.SetDetailsText(details);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }
}

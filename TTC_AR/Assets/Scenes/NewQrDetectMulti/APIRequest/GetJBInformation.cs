using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyUI.Progress;
using UnityEngine;

public class GetJBInformation : MonoBehaviour
{
    public static GetJBInformation Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Destroy()
    {
        Instance = null;
    }

    public async Task GetJB(JBInformationModel jb)
    {
        {
            try
            {

                StaticVariable.ready_To_Nav_New_Scene = false;

                var jb_FromModule = StaticVariable.temp_ListJBInformationModelFromModule.Find(jb_FromModule => jb_FromModule.Name == jb.Name);

                if (jb_FromModule == null)
                {
                    Debug.LogWarning("Không tìm thấy thông tin jb!");
                    Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin jb!");
                    return;
                }

                await Move_On_Main_Thread.RunOnMainThread(() =>
                          {
                              ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                          });

                await APIManagerOpenCV.Instance.GetJBInformation(StaticVariable.GetJBUrl + "/" + jb_FromModule.Id);

                await Move_On_Main_Thread.RunOnMainThread(() =>
                       {
                           HideProgressBar();
                       });

                StaticVariable.ready_To_Nav_New_Scene = true;
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

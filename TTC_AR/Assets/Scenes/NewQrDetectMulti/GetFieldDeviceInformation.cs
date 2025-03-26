using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyUI.Progress;
using UnityEngine;

public class GetFieldDeviceInformation : MonoBehaviour
{
    public static GetFieldDeviceInformation Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Destroy()
    {
        Instance = null;
    }

    public async Task GetFieldDevice(FieldDeviceInformationModel fieldDevice)
    {
        {
            try
            {

                StaticVariable.ready_To_Nav_New_Scene = false;

                var fieldDevice_FromMCC = StaticVariable.temp_ListFieldDeviceModelFromMCC.Find(fieldDevice_FromMCC => fieldDevice_FromMCC.Name == fieldDevice.Name);

                if (fieldDevice_FromMCC == null)
                {
                    Debug.LogWarning("Không tìm thấy thông tin fieldDevice!");
                    Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin fieldDevice!");
                    return;
                }

                await Move_On_Main_Thread.RunOnMainThread(() =>
                          {
                              ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                          });

                await APIManagerOpenCV.Instance.GetFieldDeviceInformation(StaticVariable.GetFieldDevicesFromMCCUrl + "/" + fieldDevice_FromMCC.Id);

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

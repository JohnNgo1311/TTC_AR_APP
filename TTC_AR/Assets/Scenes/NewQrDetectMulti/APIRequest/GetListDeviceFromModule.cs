using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GetListDeviceFromModule : MonoBehaviour
{
    private void OnEnable()
    {
        GetListDevice();
    }

    public async void GetListDevice()
    {
        {
            try
            {
                StaticVariable.ready_To_Nav_New_Scene = false;

                foreach (var device in StaticVariable.temp_ListDeviceInformationModel)
                {
                    // Debug.Log("device: " + device.Code);

                    if (device == null)
                    {
                        Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin device!");
                        return;
                    }

                    await Move_On_Main_Thread.RunOnMainThread(() =>
                              {
                                  Show_Toast.Instance.Set_Instance_Status_True();
                                  Show_Toast.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                              });

                    await APIManagerOpenCV.Instance.GetDevice(StaticVariable.GetListDevicesFromModuleUrl + "/" + device.Id);
                    // await APIManagerOpenCV.Instance.DownloadImagesAsync();

                    await Move_On_Main_Thread.RunOnMainThread(() =>
                       {
                           StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
                       });
                    StaticVariable.ready_To_Nav_New_Scene = true;
                }
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
                await Task.Delay(2000);
                await Move_On_Main_Thread.RunOnMainThread(() =>
                  {
                      StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
                  });
            }
        }
    }
}

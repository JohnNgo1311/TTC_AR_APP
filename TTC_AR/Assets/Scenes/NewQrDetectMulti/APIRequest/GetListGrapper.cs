using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.Progress;
using UnityEngine;

public class GetListGrapper : MonoBehaviour
{
    private void OnEnable()
    {
        GetListGrapperByCompany();
    }

    private void Start()
    {

    }

    public async void GetListGrapperByCompany()
    {
        try
        {
            StaticVariable.ready_To_Nav_New_Scene = false;
            StaticVariable.ready_To_Update_Grapper_UI = false;

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                      });

            await APIManagerOpenCV.Instance.GetListGrapper(StaticVariable.GetListGrapperUrl);

            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   HideProgressBar();
               });

            StaticVariable.ready_To_Nav_New_Scene = true;
            StaticVariable.ready_To_Update_Grapper_UI = true;
        }
        catch (System.Exception)
        {
            StaticVariable.ready_To_Nav_New_Scene = false;
            // Xử lý lỗi và hiển thị thông báo
            await Move_On_Main_Thread.RunOnMainThread(() =>
             {
                 Show_Toast.Instance.ShowToast("failure", "Đã có lỗi xảy ra");
             });
            // await Task.Delay(2000);
            await Move_On_Main_Thread.RunOnMainThread(() =>
              {
                  StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
              });
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

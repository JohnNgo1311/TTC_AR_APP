using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyUI.Progress;
using UnityEngine;

public class GetListMCCsFromGrapper : MonoBehaviour
{
    private void Awake()
    {
        GetListMCCsByGrapper();
    }

    private void Start()
    {

    }

    public async void GetListMCCsByGrapper()
    {
        try
        {
            StaticVariable.ready_To_Nav_New_Scene = false;

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          ShowProgressBar("Đang tải dữ liệu...", "Vui lòng chờ...");
                      });

            await APIManagerOpenCV.Instance.GetListMCCs(StaticVariable.GetListMCCsUrl);

            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   HideProgressBar();
               });

            StaticVariable.ready_To_Nav_New_Scene = true;
        }
        catch (Exception)
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

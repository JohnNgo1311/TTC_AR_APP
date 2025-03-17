using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GetListModuleFromGrapper : MonoBehaviour
{
    private void Start()
    {
        GetListModuleByGrapper();
    }

    public async void GetListModuleByGrapper()
    {
        try
        {
            StaticVariable.ready_To_Nav_New_Scene = false;

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          Show_Toast.Instance.Set_Instance_Status_True();
                          Show_Toast.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                      });

            await APIManagerOpenCV.Instance.GetListModule(StaticVariable.GetListModuleUrl);

            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
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
            await Task.Delay(2000);
            await Move_On_Main_Thread.RunOnMainThread(() =>
              {
                  StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False());
              });
        }
    }
}


using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
public class Get_List_Modules_By_Grapper : MonoBehaviour
{
    private void Awake()
    {
    }
    private void OnEnable()
    {

    }

    void OnDisable()
    {

    }
    private void Start()
    {

    }
    public async void GetListModuleByGrapper()
    {
        try
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          Show_Dialog.Instance.Set_Instance_Status_True();
                          Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                      });

            await APIManager.Instance.GetListModuleInformation(
                url: GlobalVariable.baseUrl + $"Grappers/{GlobalVariable.GrapperId}/modules",
                grapperId: GlobalVariable.GrapperId
            );
            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
               });
            GlobalVariable.ready_To_Nav_New_Scene = true;
        }
        catch (Exception)
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;
            // Xử lý lỗi và hiển thị thông báo
            await Move_On_Main_Thread.RunOnMainThread(() =>
             {
                 Show_Dialog.Instance.ShowToast("failure", "Đã có lỗi xảy ra");
             });
            await Task.Delay(2000);
            await Move_On_Main_Thread.RunOnMainThread(() =>
              {
                  StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
              });
        }
    }
}

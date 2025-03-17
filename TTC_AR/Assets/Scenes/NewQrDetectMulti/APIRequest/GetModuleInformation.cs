using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GetModuleInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private EventPublisher eventPublisher;

    private void Awake()
    {
        eventPublisher.OnButtonClicked += GetModuleInformationModel;
    }

    private void Destroy()
    {
        eventPublisher.OnButtonClicked -= GetModuleInformationModel;
    }

    public async void GetModuleInformationModel()
    {
        try
        {
            StaticVariable.ready_To_Nav_New_Scene = false;

            var moduleName = title.text.Split(' ')[1];
            // Debug.Log("moduleName: " + moduleName);
            //? Module tương ứng    
            var module = StaticVariable.temp_ListModuleInformationModel.Find(module => module.Name == moduleName);

            if (module == null)
            {
                Show_Toast.Instance.ShowToast("error", "Không tìm thấy thông tin module!");
                return;
            }

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          Show_Toast.Instance.Set_Instance_Status_True();
                          Show_Toast.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                      });

            await APIManagerOpenCV.Instance.GetModule(StaticVariable.GetModuleUrl + "/" + module.Id);
            // await APIManagerOpenCV.Instance.DownloadImagesAsync();

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

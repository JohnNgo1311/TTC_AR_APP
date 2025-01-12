
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using PimDeWitte.UnityMainThreadDispatcher;
using System.Collections.Generic;
public class Get_Module_Information : MonoBehaviour
{
    public EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void Awake()
    {
    }

    private void OnEnable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked += Get_ModuleInformationModel; // Đăng ký sự kiện
        }
        else
        {
            Debug.Log("eventPublisher is null");
        }
    }


    void OnDisable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked -= Get_ModuleInformationModel; // Hủy đăng ký sự kiện
        }
        else
        {
            Debug.Log("eventPublisher is null");
        }
    }
    private void Start()
    {

    }
    public async void Get_ModuleInformationModel()
    {
        try
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            var moduleName = gameObject.name.Split('_')[0];
            var rackName = $"Rack_{gameObject.name.Substring(1, 1)}";
            //? Rack tương ứng
            var rack = GlobalVariable.temp_List_Rack_General_Models.Find(rack => rack.Name == rackName);
            //? Module tương ứng    
            var module = rack.List_Module_General_Non_Rack_Model.Find(module => module.Name == moduleName);

            if (rack == null || module == null)
            {
                Show_Dialog.Instance.ShowToast("error", "Không tìm thấy thông tin rack hoặc module!");
                return;
            }

            await Move_On_Main_Thread.RunOnMainThread(() =>
                      {
                          Show_Dialog.Instance.Set_Instance_Status_True();
                          Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                      });

            await APIManager.Instance.GetModuleInformation(
                url: $"{GlobalVariable.baseUrl}Modules/{module.Id}",
                moduleId: 1
            );
            await APIManager.Instance.DownloadImagesAsync();

            await Move_On_Main_Thread.RunOnMainThread(() =>
               {
                   StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
               });
            GlobalVariable.ready_To_Nav_New_Scene = true;

        }
        catch (Exception ex)
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            Debug.Log("Get_ModuleInformationModel + lỗi: " + ex.Message);
            // Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
        }
    }
}

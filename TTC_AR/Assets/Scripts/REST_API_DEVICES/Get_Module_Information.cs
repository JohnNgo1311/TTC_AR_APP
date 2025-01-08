
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using PimDeWitte.UnityMainThreadDispatcher;
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
            eventPublisher.OnButtonClicked += Get_Module_Information_Model; // Đăng ký sự kiện
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
            eventPublisher.OnButtonClicked -= Get_Module_Information_Model; // Hủy đăng ký sự kiện
        }
        else
        {
            Debug.Log("eventPublisher is null");
        }
    }
    private void Start()
    {

    }
    public async void Get_Module_Information_Model()
    {
        try
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            var moduleName = gameObject.name.Split('_')[0];
            var rackName = $"Rack_{gameObject.name.Substring(1, 1)}";
            var rack = GlobalVariable.temp_List_Rack_General_Models.Find(rack => rack.Name == rackName);
            var module = GlobalVariable.temp_List_Module_General_Non_Rack_Models.Find(module => module.Name == moduleName);

            if (rack == null || module == null)
            {
                Show_Dialog.Instance.ShowToast("error", "Không tìm thấy thông tin rack hoặc module!");
                return;
            }

            GlobalVariable.ready_To_Nav_New_Scene = false;
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
                     {
                         Show_Dialog.Instance.Set_Instance_Status_True();
                         Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
                     });

            await APIManager.Instance.GetModuleInformation(
                url: $"{GlobalVariable.baseUrl2}GetModuleInformation",
                grapperId: GlobalVariable.temp_Grapper_General_Model.Id,
                rackId: rack.Id,
                moduleId: module.Id

            );
            await APIManager.Instance.DownloadImagesAsync();
            GlobalVariable.ready_To_Nav_New_Scene = true;
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
              {
                  StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
              });
            GlobalVariable.ready_To_Nav_New_Scene = true;

        }
        catch (Exception ex)
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            Debug.Log("Get_Module_Information_Model + lỗi: " + ex.Message);
            // Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
        }
    }
}

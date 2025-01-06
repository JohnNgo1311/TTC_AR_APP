
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
public class Get_Module_Information : MonoBehaviour
{
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void Awake()
    {
        eventPublisher ??= FindObjectOfType<EventPublisher>();
    }

    private void OnEnable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked += Get_Module_Information_Model; // Đăng ký sự kiện
        }
    }


    void OnDisable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked -= Get_Module_Information_Model; // Hủy đăng ký sự kiện
        }
    }
    private void Start()
    {

    }
    public async void Get_Module_Information_Model()
    {
        try
        {
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
            Show_Dialog.Instance.Set_Instance_Status_True();
            Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");

            await APIManager.Instance.GetModuleInformation(
                url: $"{GlobalVariable.baseUrl2}GetModuleInformation",
                grapperId: GlobalVariable.temp_Grapper_General_Model.Id,
                rackId: rack.Id,
                moduleId: module.Id
            );
            await APIManager.Instance.DownloadImagesAsync();
            GlobalVariable.ready_To_Nav_New_Scene = true;
            StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
        }
        catch (Exception ex)
        {
            Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
        }
    }
}

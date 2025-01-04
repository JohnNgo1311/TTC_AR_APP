
using UnityEngine;
using System;
using System.Threading.Tasks;
public class Get_Module_Information : MonoBehaviour
{
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

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
        var moduleName = gameObject.name.Split('_')[0];
        var rackName = $"Rack_{gameObject.name.Substring(1, 1)}";
        var rackId = GlobalVariable.temp_List_Rack_General_Models.Find(rack => rack.Name == rackName).Id;
        var moduleId = GlobalVariable.temp_List_Module_General_Non_Rack_Models.Find(module => module.Name == moduleName).Id;
        GlobalVariable.ready_To_Nav_New_Scene = false;
        await APIManager.Instance.GetModuleInformation(url: $"{GlobalVariable.baseUrl2}GetModuleInformation", grapperId: GlobalVariable.temp_Grapper_General_Model.Id, rackId: rackId, moduleId: moduleId);
        await APIManager.Instance.DownloadImagesAsync();

        GlobalVariable.ready_To_Nav_New_Scene = true;
    }


}

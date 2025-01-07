
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
public class Get_Field_Device : MonoBehaviour
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
            eventPublisher.OnButtonClicked += Get_Field_Device_Model; // Đăng ký sự kiện
        }
    }


    void OnDisable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked -= Get_Field_Device_Model; // Hủy đăng ký sự kiện
        }
    }
    private void Start()
    {

    }
    public async void Get_Field_Device_Model()
    {
        try
        {
            var cabinet_Name = gameObject.name.Split('_')[1];
            var cabinet = GlobalVariable.temp_List_Field_Device_Information_Model.Find(cabinet => cabinet.Name == cabinet_Name);
            if (cabinet == null)
            {
                Show_Dialog.Instance.ShowToast("error", "Không tìm thấy thông tin tủ!");
                return;
            }

            GlobalVariable.ready_To_Nav_New_Scene = false;
            Show_Dialog.Instance.Set_Instance_Status_True();
            Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");

            await APIManager.Instance.GetFieldDeviceInformation(
                url: $"{GlobalVariable.baseUrl3}GetFieldDevice",
                grapperId: "GlobalVariable.temp_Grapper_General_Model.Id",//Update sau
                fieldDeviceId: cabinet.Id
            );
            await APIManager.Instance.DownloadImagesAsync();
            GlobalVariable.ready_To_Nav_New_Scene = true;
            StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
            Debug.Log("Get_Field_Device_Model");
        }
        catch (Exception ex)
        {
            Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
        }
    }
}

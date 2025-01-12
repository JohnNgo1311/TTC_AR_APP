using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using PimDeWitte.UnityMainThreadDispatcher;
public class Get_List_Field_Device : MonoBehaviour
{
    //  public EventPublisher eventPublisher; // Tham chiếu đến Publisher

    // private void Awake()
    // {
    //     eventPublisher ??= FindObjectOfType<EventPublisher>();
    // }

    // private void OnEnable()
    // {
    //     if (eventPublisher != null)
    //     {
    //         eventPublisher.OnButtonClicked += Get_List_Field_Device_Models; // Đăng ký sự kiện
    //     }
    // }


    // void OnDisable()
    // {
    //     if (eventPublisher != null)
    //     {
    //         eventPublisher.OnButtonClicked -= Get_List_Field_Device_Models; // Hủy đăng ký sự kiện
    //     }
    // }

    private void Awake()
    {
        // Khởi chạy phương thức khi script được kích hoạt
        Get_List_Field_Device_Models();
    }

    public async void Get_List_Field_Device_Models()
    {
        try
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;
            // Thực thi các tác vụ giao diện trên Main Thread
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
            {
                Show_Dialog.Instance.Set_Instance_Status_True();
                Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
            });

            // Chờ API hoàn thành
            await Task.WhenAll(
                APIManager.Instance.GetListFieldDeviceInformation(
                    url: $"{GlobalVariable.baseUrl}GetFieldDevice",1
                // GlobalVariable.temp_ListGrapperGeneralModels[0].Id
                )
            );

            // Đăng thông báo thành công
            Debug.Log("Get_List_Field_Device_Models completed.");

            // Cập nhật trạng thái giao diện
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
            {
                StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
            });
            GlobalVariable.ready_To_Nav_New_Scene = true;
        }
        catch (Exception ex)
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;

            // Xử lý lỗi và hiển thị thông báo
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
            {
                Debug.LogError("Error in Get_List_Field_Device_Models: " + ex.Message);
                Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
            });
        }
    }


}

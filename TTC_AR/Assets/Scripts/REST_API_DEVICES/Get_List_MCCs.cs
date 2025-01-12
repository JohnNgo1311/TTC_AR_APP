using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using PimDeWitte.UnityMainThreadDispatcher;
public class Get_List_MCCs : MonoBehaviour
{
    public string GrapperName = "";
    public int GrapperId = 1;
    private void Awake()
    {
    }

    public async void GetListMCCModels(int GrapperId)
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
                APIManager.Instance.GetListMCCModels(
                    url: $"{GlobalVariable.baseUrl}Grappers/{GrapperId}/mccs"
                // GlobalVariable.temp_ListGrapperGeneralModels[0].Id
                )
            );

            // Đăng thông báo thành công
            Debug.Log("GetListMCCModels completed.");

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
                Debug.LogError("Error in GetListMCCModels: " + ex.Message);
                Show_Dialog.Instance.ShowToast("failure", $"Lỗi: {ex.Message}");
            });
        }
    }


}

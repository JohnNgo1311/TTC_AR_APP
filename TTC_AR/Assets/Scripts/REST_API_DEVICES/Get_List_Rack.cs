
using UnityEngine;
using System;
using System.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
public class Get_List_Rack : MonoBehaviour
{
    public int grapperId = 1;
    private void Awake()
    {
        grapperId = GlobalVariable.GrapperId;
        Get_List_Rack_General_Models();
    }
    private void Start()
    {

    }
    public async void Get_List_Rack_General_Models()
    {
        try
        {
            await Move_On_Main_Thread.RunOnMainThread(() =>
             {
                 Show_Dialog.Instance.Set_Instance_Status_True();
                 Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
             });
            await Task.WhenAll(
            APIManager.Instance.GetListRacks(url: $"{GlobalVariable.baseUrl}Grappers/{grapperId}/racks")
            );
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
              {
                  StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());

              });
            Debug.Log("Get_List_Grapper_Models completed.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Get_List_Grapper_Models: {e.Message}");
        }

    }

}

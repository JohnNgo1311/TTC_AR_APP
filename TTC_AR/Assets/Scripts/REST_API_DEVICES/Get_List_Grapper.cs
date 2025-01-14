
using UnityEngine;
using System;
using System.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
public class Get_List_Grapper : MonoBehaviour
{
    private void Awake()
    {
        Get_List_Grapper_Models();
    }
    private void Start()
    {

    }

    public async void Get_List_Grapper_Models()
    {
        try
        {
            await Move_On_Main_Thread.RunOnMainThread(() =>
             {
                 Show_Dialog.Instance.Set_Instance_Status_True();
                 Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
             });
            await Task.WhenAll(
            APIManager.Instance.GetListGrappers(url: $"{GlobalVariable.baseUrl}grappers")
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

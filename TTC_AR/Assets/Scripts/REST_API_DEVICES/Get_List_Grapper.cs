
using UnityEngine;
using System;
using System.Threading.Tasks;
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
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("loading", "Đang tải dữ liệu...");
        await Task.WhenAll(
        //GlobalVariable.temp_Grapper_General_Model.Id
        APIManager.Instance.GetList_Grappers(url: $"{GlobalVariable.baseUrl2}GetListGrapper")
        );
        StartCoroutine(Show_Dialog.Instance.Set_Instance_Status_False());
    }


}

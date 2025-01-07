
using UnityEngine;
using System;
using System.Threading.Tasks;
public class Get_All_Data_By_Grapper_For_Searching : MonoBehaviour
{
    public EventPublisher eventPublisher; // Tham chiếu đến Publisher
    private void Awake()
    {
        if (GlobalVariable.ready_To_Nav_New_Scene) GlobalVariable.ready_To_Nav_New_Scene = false;

    }
    private void OnEnable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked += GetAllDataByGrapperForSearching; // Đăng ký sự kiện
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
            eventPublisher.OnButtonClicked -= GetAllDataByGrapperForSearching; // Hủy đăng ký sự kiện
        }
        else
        {
            Debug.Log("eventPublisher is null");
        }
    }
    private void Start()
    {

    }
    public async void GetAllDataByGrapperForSearching()
    {
        try
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;
            await Task.WhenAll(
            //GlobalVariable.temp_Grapper_General_Model.Id
            APIManager.Instance.GetAllDevicesByGrapper(url: $"{GlobalVariable.baseUrl1}GetListDevicesByGrapper", grapperId: GlobalVariable.temp_Grapper_General_Model.Id),
            APIManager.Instance.GetAllJBsByGrapper(url: $"{GlobalVariable.baseUrl1}GetListJBByGrapper", grapperId: GlobalVariable.temp_Grapper_General_Model.Id)
            );
            GlobalVariable.ready_To_Nav_New_Scene = true;
        }
        catch (Exception e)
        {
            GlobalVariable.ready_To_Nav_New_Scene = false;
            Debug.LogError($"GetAllDataByGrapperForSearching: {e.Message}");
        }

    }


}

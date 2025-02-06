
using UnityEngine;
using System;
using System.Threading.Tasks;
public class Get_All_Data_By_Grapper_For_Searching : MonoBehaviour
{
    public int grapperId;
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
            GlobalVariable.temp_List_Rack_Non_List_Module_Model = APIManager.Instance.temp_List_Grapper_General_Models.Find(grapper => grapper.Id == grapperId).List_Rack_Non_List_Module_Model;
            Debug.Log(GlobalVariable.temp_List_Rack_Non_List_Module_Model.Count);
            await Task.WhenAll(
            //GlobalVariable.temp_GrapperGeneralModel.Id
            APIManager.Instance.GetAllDevicesByGrapper(url: $"{GlobalVariable.baseUrl}Grappers/{grapperId}/devices", grapperId: grapperId),
            APIManager.Instance.GetAllJBsByGrapper(url: $"{GlobalVariable.baseUrl}Grappers/{grapperId}/jbs", grapperId: grapperId)
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

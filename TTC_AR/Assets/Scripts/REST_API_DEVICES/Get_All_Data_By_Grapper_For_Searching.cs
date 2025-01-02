
using UnityEngine;
using System;
using System.Threading.Tasks;
public class Get_All_Data_By_Grapper_For_Searching : MonoBehaviour
{
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void OnEnable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked += GetAllDataByGrapperForSearching; // Đăng ký sự kiện
        }
    }


    void OnDisable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked -= GetAllDataByGrapperForSearching; // Hủy đăng ký sự kiện
        }
    }
    private void Start()
    {

    }
    public async void GetAllDataByGrapperForSearching()
    {
        GlobalVariable.ready_To_Nav_New_Scene = false;
        await Task.WhenAll(
        //GlobalVariable.temp_Grapper_General_Model.Id
        APIManager.Instance.GetAllDevicesByGrapper(url: $"{GlobalVariable.baseUrl1}Device_GrapperA", grapperId: ""),
        APIManager.Instance.GetAllJBsByGrapper(url: $"{GlobalVariable.baseUrl1}JB_TSD_Information_GrapperA", grapperId: "")
        );
        GlobalVariable.ready_To_Nav_New_Scene = true;
    }


}

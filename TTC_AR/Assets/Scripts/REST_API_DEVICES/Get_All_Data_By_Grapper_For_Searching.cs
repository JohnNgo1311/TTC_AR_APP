
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

    private void OnDisable()
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
        APIManager.Instance.GetAllJBsByGrapper(url: GlobalVariable.baseUrl, grapperId: GlobalVariable.temp_Grapper_General_Model.Id),
        APIManager.Instance.GetAllJBsByGrapper(url: GlobalVariable.baseUrl, grapperId: GlobalVariable.temp_Grapper_General_Model.Id)
        );
        GlobalVariable.ready_To_Nav_New_Scene = true;
    }


}

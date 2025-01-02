
using UnityEngine;
using System;
using System.Threading.Tasks;
public class Get_Module_Information : MonoBehaviour
{
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void OnEnable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked += Get_Module_Information_Model; // Đăng ký sự kiện
        }
    }


    void OnDisable()
    {
        if (eventPublisher != null)
        {
            eventPublisher.OnButtonClicked -= Get_Module_Information_Model; // Hủy đăng ký sự kiện
        }
    }
    private void Start()
    {

    }
    public async void Get_Module_Information_Model()
    {
        GlobalVariable.ready_To_Nav_New_Scene = false;
        await Task.WhenAll(
        APIManager.Instance.GetModuleInformation(url: $"{GlobalVariable.baseUrl2}Get_Module_Information", grapperId: "", rackId: "", moduleId: "")
        );
        GlobalVariable.ready_To_Nav_New_Scene = true;
    }


}

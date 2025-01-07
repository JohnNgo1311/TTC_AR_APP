using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGrapperOnClick : MonoBehaviour
{
    public string grapperName;
    private Button onClickButton;
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void Awake()
    {
        onClickButton = gameObject.GetComponent<Button>();
    }
    private void OnEnable()
    {
        // Đăng ký chỉ SelectGrapperOnClick() cho nút
        onClickButton.onClick.AddListener(HandleButtonClick);
    }
    private void HandleButtonClick()
    {
        // Thực hiện SelectGrapperOnClick trước
        SelectGrapperOnClick();
        // Sau khi hoàn thành, gọi TriggerEvent()
        eventPublisher.TriggerEvent_ButtonClicked();
    }
    private void SelectGrapperOnClick()
    {   Grapper_General_Model grapper = GlobalVariable.temp_List_Grapper_General_Models.Find(grap=> grap.Name == grapperName);       
        GlobalVariable.temp_Grapper_General_Model = grapper;
        Filter_List_Rack_And_Module_From_Grapper(grapper.Id, GlobalVariable.temp_List_Grapper_General_Models);
    }

    private void OnDisable()
    {
        // Gỡ bỏ tất cả listener khi đối tượng bị disable
        onClickButton.onClick.RemoveAllListeners();
    }
    private void Filter_List_Rack_And_Module_From_Grapper(string grapperId, List<Grapper_General_Model> grapper_General_Models)
    {
        var grapper_General_Model = grapper_General_Models.Find(grapper => grapper.Id == grapperId);
        var tempRackList = new List<Rack_General_Model>();
        var tempModuleList = new List<Module_General_Non_Rack_Model>();

        tempRackList.AddRange(grapper_General_Model.List_Rack_General_Model);
        foreach (var rack in grapper_General_Model.List_Rack_General_Model)
        {
            tempModuleList.AddRange(rack.List_Module_General_Non_Rack_Model);
        }
        GlobalVariable.temp_List_Rack_General_Models = tempRackList;
        GlobalVariable.temp_List_Module_General_Non_Rack_Models = tempModuleList;
        Debug.Log("Filter_List_Rack_From_Grapper: " + GlobalVariable.temp_List_Rack_General_Models.Count + " \n" + "Filter_List_Module_From_Grapper: " + GlobalVariable.temp_List_Module_General_Non_Rack_Models.Count);
        // => Có được List<Rack> và List<Module> tương ứng với Grapper mà cần tra cứu
    }

}

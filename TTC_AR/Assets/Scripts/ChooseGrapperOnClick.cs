using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseGrapperOnClick : MonoBehaviour
{
    public string grapperName;
    public Button onClickButton;
    public Get_All_Data_By_Grapper_For_Searching getAllDataByGrapperForSearching;
    public EventPublisher eventPublisher;

    private void Awake()
    {
        onClickButton ??= gameObject.GetComponent<Button>();
        eventPublisher ??= FindObjectOfType<EventPublisher>();
    }

    private void Start()
    {
        onClickButton.onClick.AddListener(StartHandleButtonClick);
    }

    private void StartHandleButtonClick()
    {
        if (SceneManager.GetActiveScene().name == MyEnum.MenuScene.GetDescription() && grapperName != "GrapperA")
        {

        }
        else StartCoroutine(HandleButtonClick());
        // Vô hiệu hóa nút để không thể click lại trong quá trình xử lý
    }

    private IEnumerator HandleButtonClick()
    {// Vô hiệu hóa nút để không thể click lại trong quá trình xử lý
        APIManager.Instance.GrapperId = APIManager.Instance.Dic_Grapper_General_Non_List_Rack_Models[grapperName];
        Debug.Log($"Grapper ID: {APIManager.Instance.GrapperId}");
        getAllDataByGrapperForSearching.grapperId = APIManager.Instance.GrapperId;
        GlobalVariable.GrapperId = APIManager.Instance.GrapperId;
        yield return SelectGrapperOnClick();
        Debug.Log("Button Clicked!");
        eventPublisher.TriggerEvent_ButtonClicked();
        yield return new WaitForSeconds(2f);
        if (!onClickButton.interactable) onClickButton.interactable = true;
    }

    private IEnumerator SelectGrapperOnClick()
    {
        // Grapper_General_Model grapper = GlobalVariable.temp_ListGrapperGeneralModels.Find(grap => grap.Name == grapperName);
        //     if (grapper != null)
        //     {
        //         GlobalVariable.temp_GrapperGeneralModel = grapper;
        //         Filter_List_Rack_And_Module_From_Grapper(grapper.Id, GlobalVariable.temp_ListGrapperGeneralModels);
        //     }
        //     else
        //     {
        //         Debug.LogError($"Grapper not found: {grapperName}");
        //     }
        yield return new WaitForEndOfFrame();

    }

    private void OnDisable()
    {
        onClickButton.onClick.RemoveAllListeners();
    }

    // private void Filter_List_Rack_And_Module_From_Grapper(int grapperId, List<Grapper_General_Model> grapper_General_Models)
    // {
    //     var grapper_General_Model = grapper_General_Models.Find(grapper => grapper.Id == grapperId);
    //     if (grapper_General_Model == null)
    //     {
    //         Debug.LogError($"Grapper with ID {grapperId} not found.");
    //         return;
    //     }

    //     var tempRackList = new List<Rack_Non_List_Module_Model>(grapper_General_Model.List_Rack_General_Model);
    //     var tempModuleList = new List<Module_General_Non_Rack_Model>();

    //     foreach (var rack in grapper_General_Model.List_Rack_General_Model)
    //     {
    //         tempModuleList.AddRange(rack.List_Module_General_Non_Rack_Model);
    //     }

    //     GlobalVariable.temp_List_Rack_Non_List_Module_Model = tempRackList;
    //     GlobalVariable.temp_ListModuleGeneralNonRackModels = tempModuleList;

    //     Debug.Log($"Filter_List_Rack_From_Grapper: {GlobalVariable.temp_List_Rack_Non_List_Module_Model.Count} \nFilter_List_Module_From_Grapper: {GlobalVariable.temp_ListModuleGeneralNonRackModels.Count}");
    // }
}
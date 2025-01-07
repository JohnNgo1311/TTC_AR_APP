using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGrapperOnClick : MonoBehaviour
{
    public string grapperName;
    public Button onClickButton;
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
        StartCoroutine(HandleButtonClick());
        // Vô hiệu hóa nút để không thể click lại trong quá trình xử lý
    }

    private IEnumerator HandleButtonClick()
    {// Vô hiệu hóa nút để không thể click lại trong quá trình xử lý

        yield return SelectGrapperOnClick();
        Debug.Log("Button Clicked!");
        eventPublisher.TriggerEvent_ButtonClicked();
        yield return new WaitForSeconds(2f);
        if (!onClickButton.interactable) onClickButton.interactable = true;
    }

    private IEnumerator SelectGrapperOnClick()
    {
        Grapper_General_Model grapper = GlobalVariable.temp_List_Grapper_General_Models.Find(grap => grap.Name == grapperName);
        if (grapper != null)
        {
            GlobalVariable.temp_Grapper_General_Model = grapper;
            Filter_List_Rack_And_Module_From_Grapper(grapper.Id, GlobalVariable.temp_List_Grapper_General_Models);
        }
        else
        {
            Debug.LogError($"Grapper not found: {grapperName}");
        }
        yield return new WaitForEndOfFrame();

    }

    private void OnDisable()
    {
        onClickButton.onClick.RemoveAllListeners();
    }

    private void Filter_List_Rack_And_Module_From_Grapper(string grapperId, List<Grapper_General_Model> grapper_General_Models)
    {
        var grapper_General_Model = grapper_General_Models.Find(grapper => grapper.Id == grapperId);
        if (grapper_General_Model == null)
        {
            Debug.LogError($"Grapper with ID {grapperId} not found.");
            return;
        }

        var tempRackList = new List<Rack_General_Model>(grapper_General_Model.List_Rack_General_Model);
        var tempModuleList = new List<Module_General_Non_Rack_Model>();

        foreach (var rack in grapper_General_Model.List_Rack_General_Model)
        {
            tempModuleList.AddRange(rack.List_Module_General_Non_Rack_Model);
        }

        GlobalVariable.temp_List_Rack_General_Models = tempRackList;
        GlobalVariable.temp_List_Module_General_Non_Rack_Models = tempModuleList;

        Debug.Log($"Filter_List_Rack_From_Grapper: {GlobalVariable.temp_List_Rack_General_Models.Count} \nFilter_List_Module_From_Grapper: {GlobalVariable.temp_List_Module_General_Non_Rack_Models.Count}");
    }
}

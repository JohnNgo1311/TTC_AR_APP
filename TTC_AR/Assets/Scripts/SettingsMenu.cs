using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject scroll_Area;
    [SerializeField] private GameObject item;
    [SerializeField] private EventPublisher eventPublisher;
    public GameObject mainButton;
    private GameObject FirstBtn = null;
    private GrapperInfor grapperInforBtn;

    void Start()
    {
        // Debug.Log("SettingsMenu Start");
        StartCoroutine(InitializeMenuItems());
        SetupMainButton();
    }

    private IEnumerator InitializeMenuItems()
    {
        // scroll_Area = scroll_Area ?? gameObject.transform.Find("Scroll_Area").gameObject;

        yield return new WaitUntil(() => StaticVariable.ready_To_Update_Grapper_UI == true);
        // Debug.Log("temp_ListGrapperInformationModel.Count: " + StaticVariable.temp_ListGrapperInformationModel.Count);

        if (SceneManager.GetActiveScene().name == "FieldDeviceScene")
        {
            if (StaticVariable.temp_ListGrapperInformationModel.Any())
            {
                item.SetActive(true);
                foreach (var grapper in StaticVariable.temp_ListGrapperInformationModel)
                {
                    var newObject = Instantiate(item, content);
                    var InstantiateGrapperInfor = newObject.GetComponent<GrapperInfor>();
                    InstantiateGrapperInfor.SetGrapperName(grapper);
                    InstantiateGrapperInfor.grapperButton.onClick.AddListener(() => OnItemClick(grapper));
                    if (FirstBtn == null)
                    {
                        FirstBtn = newObject;
                        OnItemClick(grapper);
                    }
                }
                item.SetActive(false);
            }
            else
            {
                Debug.LogError("No Grapper found");
            }
        }
        else
        {
            Debug.LogError("Not in FieldDeviceScene");
        }
        scroll_Area.SetActive(false);
    }

    private void SetupMainButton()
    {
        grapperInforBtn = mainButton.GetComponent<GrapperInfor>();
        grapperInforBtn.grapperButton.onClick.AddListener(() => ToggleMenu());
    }

    private void ToggleMenu()
    {
        scroll_Area.gameObject.SetActive(!scroll_Area.activeSelf);
    }

    public void OnItemClick(GrapperInformationModel grapper)
    {
        StaticVariable.temp_GrapperInformationModel = grapper;
        eventPublisher.TriggerEvent_GrapperClicked();
        grapperInforBtn.SetGrapperName(grapper);
        ToggleMenu();
    }

    void OnDestroy()
    {
        grapperInforBtn.grapperButton.onClick.RemoveAllListeners();
    }
}
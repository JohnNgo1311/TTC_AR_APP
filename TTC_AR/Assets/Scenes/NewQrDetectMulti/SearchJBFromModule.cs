using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchJBFromModule : MonoBehaviour
{
    [SerializeField] private GameObject jbPrefab;
    [SerializeField] private Transform jbParent;
    [SerializeField] private List<JBInformationModel> jbInformationList;
    [SerializeField] private GameObject detail_JB_TSD_Panel;

    private void OnEnable()
    {
        StaticVariable.is_JB_TSD_Basic_Canvas_Active = true;
        // if (!StaticVariable.navigate_from_JB_TSD_Detail)
        // {
        // Debug.Log("Get_List_JB_By_Module");
        UpdateUI();
        // }
        StaticVariable.navigate_from_JB_TSD_Detail = false;
    }

    private void UpdateUI()
    {
        jbInformationList = StaticVariable.temp_ListJBInformationModelFromModule;
        // Debug.Log("jbInformationList.Count: " + jbInformationList.Count);

        foreach (var jb in jbInformationList)
        {
            var jbObject = Instantiate(jbPrefab, jbParent);
            UpdateJBInformation(jbObject, jb);
            jbObject.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                StaticVariable.navigate_from_List_Devices = false;
                StaticVariable.navigate_from_JB_TSD_General = true;
                NavigateJBDetailScreen(jb);
            });
        }
    }


    private async void NavigateJBDetailScreen(JBInformationModel jb)
    {
        // StaticVariable.temp_JBInformationModel = jb;

        await GetJBInformation.Instance.GetJB(jb);

        StaticVariable.jb_TSD_Title = StaticVariable.temp_JBInformationModel.Name;
        StaticVariable.jb_TSD_Name = StaticVariable.temp_JBInformationModel.Name;
        StaticVariable.jb_TSD_Location = StaticVariable.temp_JBInformationModel.Location;

        if (StaticVariable.navigate_from_JB_TSD_General)
        {
            StaticVariable.navigate_from_JB_TSD_Detail = true;
            gameObject.gameObject.SetActive(false);
            detail_JB_TSD_Panel.gameObject.SetActive(true);
        }
    }

    private void UpdateJBInformation(GameObject jbObject, JBInformationModel jb)
    {
        jbObject.GetComponent<JBInfor>().SetJBInfor(jb);
    }

    private void OnDisable()
    {
        foreach (Transform child in jbParent)
        {
            Destroy(child.gameObject);
        }
        StaticVariable.is_JB_TSD_Basic_Canvas_Active = false;
    }
}

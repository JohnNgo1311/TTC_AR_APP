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
        // yield return new WaitUntil(() => StaticVariable.ready_To_Update_UI);

        jbInformationList = Get_List_JB_By_Module();
        // Debug.Log("jbInformationList.Count: " + jbInformationList.Count);

        foreach (var jb in jbInformationList)
        {
            var jbObject = Instantiate(jbPrefab, jbParent);
            UpdateJBInformation(jbObject, jb);
            jbObject.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                StaticVariable.navigate_from_List_Devices = false;
                StaticVariable.navigate_from_JB_TSD_Basic = true;
                NavigateJBDetailScreen(jb);
            });
        }

        StaticVariable.ready_To_Update_JB_UI = false;
    }

    private List<JBInformationModel> Get_List_JB_By_Module()
    {
        return StaticVariable.temp_ListJBInformationModelFromModule;
    }


    private void NavigateJBDetailScreen(JBInformationModel jb)
    {
        StaticVariable.jb_TSD_Title = jb.Name;

        StaticVariable.jb_TSD_Name = jb.Name;

        StaticVariable.jb_TSD_Location = jb.Location;

        if (StaticVariable.navigate_from_JB_TSD_Basic)
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

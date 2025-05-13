using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyUI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIListJBPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject listJBPanel;
    [SerializeField] private GameObject JBDetailPanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject jbPrefab;
    [SerializeField] private List<JBInformationModel> jbInformationList = new List<JBInformationModel>();
    [SerializeField] private List<GameObject> instantiatedJBObjects = new List<GameObject>();


    void Awake()
    {
    }
    private void OnEnable()
    {
        RefreshJBListPanel();
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => CloseListJBPanel());
        StartCoroutine(UpdateListPanelUI());
    }
    private void RefreshJBListPanel()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    private void CloseListJBPanel()
    {
        generalPanel.SetActive(true);
        listJBPanel.SetActive(false);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        instantiatedJBObjects.Clear();
        StopAllCoroutines();
    }
    private IEnumerator UpdateListPanelUI()
    {
        instantiatedJBObjects.Clear();
        yield return null;
        if (GlobalVariable.temp_ListJBInformationModel_FromModule != null)
        {
            jbInformationList = GlobalVariable.temp_ListJBInformationModel_FromModule;
        }
        if (jbInformationList.Any())
        {
            foreach (var model in jbInformationList)
            {
                var newJBItem = Instantiate(jbPrefab, content);
                newJBItem.SetActive(true);
                var jbInfor = newJBItem.GetComponent<JBInfor>();
                jbInfor.button.onClick.RemoveAllListeners();
                jbInfor.button.onClick.AddListener(() =>
                {
                    GlobalVariable.navigate_from_List_Devices = false;
                    GlobalVariable.navigate_from_list_JBs = true;
                    OpenJBDetailPanel(model);
                });
                instantiatedJBObjects.Add(newJBItem);
            }
        }
        else
        {
            var newJBItem = Instantiate(jbPrefab, content);
            newJBItem.SetActive(true);
            newJBItem.GetComponent<JBInfor>().HandleEmptyList();
        }


    }
    private void OpenJBDetailPanel(JBInformationModel model)
    {
        GlobalVariable.temp_JBInformationModel = model;
        GlobalVariable.JBId = model.Id;
        GlobalVariable.navigate_from_list_JBs = true;
        GlobalVariable.navigate_from_List_Devices = false;
        GlobalVariable.navigate_from_JB_TSD_Detail = true;
        listJBPanel.SetActive(false);
        JBDetailPanel.SetActive(true);

    }




}

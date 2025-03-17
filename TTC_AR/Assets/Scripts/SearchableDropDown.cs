using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchableDropDown : MonoBehaviour
{
    public GameObject arrowButtonUp;
    public GameObject arrowButtonDown;
    public GameObject filterDropdownButton;
    public GameObject itemPrefab;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    public GameObject content;
    public event Action<string> OnValueChangedEvt;
    public GameObject filterContent;
    public TMP_Text filterText;
    public string filter_Type = "Device";
    private RectTransform contentRect;
    [SerializeField] private List<GameObject> itemGameObjects = new List<GameObject>();
    [SerializeField] private List<DeviceInformationModel> tempDeviceInfo = new List<DeviceInformationModel>();
    [SerializeField] private List<JBInformationModel> tempJBInfo = new List<JBInformationModel>();
    [SerializeField] private List<string> availableOptions = new List<string>();
    private bool isFilterContentActive = false;
    private Vector2 scrollRectInitialSize;

    private void Awake()
    {
        contentRect = content.GetComponent<RectTransform>();
        scrollRectInitialSize = scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta;
        Debug.Log("SearchableDropDown awake");
        filter_Type = "Device";
    }

    private void Start()
    {
        //Invoke(nameof(SetInitialTextFieldValue), 2f);
    }

    public void SetInitialTextFieldValue()
    {
        tempDeviceInfo = GlobalVariable_Search_Devices.temp_ListDeviceInformationModel;
        tempJBInfo = GlobalVariable_Search_Devices.temp_ListJBInformationModel;
        if (!string.IsNullOrEmpty(tempDeviceInfo[0].Code) && !string.IsNullOrEmpty(tempJBInfo[0].Name))
        {
            inputField.text = tempDeviceInfo[0].Code;
            //    OnValueChangedEvt?.Invoke(inputField.text);
        }
        else
        {
            Debug.Log("Debug Log: tempDeviceInfo[0].Code or tempJBInfo[0].Name is null or empty");
            return;
        }
    }

    public void Initialize()
    {
        availableOptions = GlobalVariable_Search_Devices.temp_List_Device_For_Fitler;
        if (scrollRect == null || inputField == null || content == null || itemPrefab == null)
        {
            Debug.LogError("Cannot find necessary components for SearchableDropDown");
            return;
        }
        else
        {
            if (availableOptions.Count == 0)
            {
                Debug.Log("availableOptions.Count: " + availableOptions.Count);
                return;
            }
            else
            {
                Debug.Log(availableOptions.Count);
                PopulateDropdown(availableOptions);
                UpdateUI();
                int onValueChangedListenerCount = inputField.onValueChanged.GetPersistentEventCount();
                inputField.onValueChanged.AddListener(OnInputValueChange);
                filterDropdownButton.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
            }

        }
    }

    public void OnDeviceFilterClicked()
    {
        filter_Type = "Device";
        var deviceOptions = GlobalVariable_Search_Devices.temp_List_Device_For_Fitler;
        UpdateDropdownOptions(deviceOptions);
        ToggleDropdown();
        filterText.text = filter_Type;
        inputField.text = deviceOptions[0];
    }

    public void OnJBFilterClicked()
    {
        filter_Type = "JB/TSD";
        var jbOptions = GlobalVariable_Search_Devices.temp_ListJBInformationModel.Select(jb => jb.Name).ToList();
        UpdateDropdownOptions(jbOptions);
        ToggleDropdown();
        filterText.text = filter_Type;
        inputField.text = jbOptions[0];
    }

    private void UpdateDropdownOptions(List<string> options)
    {
        availableOptions = options;
        if (scrollRect == null || inputField == null || content == null || itemPrefab == null)
        {
            Debug.LogError("Cannot find necessary components for SearchableDropDown");
            return;
        }

        inputField.onValueChanged.RemoveListener(OnInputValueChange);
        PopulateDropdown(availableOptions);
        UpdateUI();

        if (inputField.onValueChanged.GetPersistentEventCount() > 0)
        {
            Debug.Log("inputField.onValueChanged.GetPersistentEventCount(): " + inputField.onValueChanged.GetPersistentEventCount());
            inputField.onValueChanged.AddListener(OnInputValueChange);
        }
    }

    private void ClearChildren(Transform parentObjectTransform)
    {
        if (parentObjectTransform == null)
        {
            Debug.LogError("parentObjectTransform is null");
            return;
        }
        else
        {
            foreach (Transform child in parentObjectTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void PopulateDropdown(List<string> options)
    {
        ClearChildren(content.transform);
        itemGameObjects.Clear();
        foreach (var option in options)
        {
            var itemObject = Instantiate(itemPrefab, content.transform);
            itemObject.name = option;
            var textComponent = itemObject.GetComponentInChildren<TMP_Text>();
            textComponent.text = option;
            itemObject.GetComponent<Button>().onClick.AddListener(() => OnItemSelected(option));
            itemGameObjects.Add(itemObject);
        }
        Debug.Log("itemGameObjects.Count: " + itemGameObjects.Count);
    }

    private void UpdateUI()
    {
        int optionsCount = availableOptions.Count;
        for (int i = 0; i < itemGameObjects.Count; i++)
        {
            var item = itemGameObjects[i];
            if (i < optionsCount)
            {
                Debug.Log("item Index: " + i);
                var optionText = availableOptions[i];
                var textComponent = item.GetComponentInChildren<TMP_Text>();
                textComponent.text = optionText;
                item.name = optionText;
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    private void ToggleDropdown()
    {
        isFilterContentActive = !isFilterContentActive;
        filterContent.gameObject.SetActive(isFilterContentActive);
        arrowButtonDown.SetActive(isFilterContentActive);
        arrowButtonUp.SetActive(!isFilterContentActive);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    private void SetContentActive(bool isActive)
    {
        arrowButtonDown.SetActive(isActive);
        arrowButtonUp.SetActive(!isActive);
    }

    private void OnInputValueChange(string input)
    {
        FilterDropdown(input.ToLower());
    }

    private void FilterDropdown(string input)
    {
        if (!scrollRect.gameObject.activeSelf)
            scrollRect.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(input))
        {
            foreach (var item in itemGameObjects)
            {
                bool shouldActivate = item.name.ToLower().Contains(input);
                if (item.activeSelf != shouldActivate)
                {
                    item.SetActive(shouldActivate);
                }
            }
        }
        else
        {
            foreach (var item in itemGameObjects)
            {
                if (!item.activeSelf)
                {
                    item.SetActive(true);
                }
            }
        }
        Debug.Log("OnInputValueChange");
        ResizeContent();
        Debug.Log("ResizeContent");
    }

    private void ResizeContent()
    {
        scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = scrollRectInitialSize;
        int activeItemCount = itemGameObjects.Count(item => item.activeSelf);
        if (activeItemCount > 0)
        {
            Debug.Log("activeItemCount: " + activeItemCount);
            RectTransform itemRect = itemGameObjects.FirstOrDefault(item => item.activeSelf).gameObject.GetComponent<RectTransform>();
            float newHeight = itemRect.sizeDelta.y * activeItemCount * 1.05f;
            Debug.Log("newHeight: " + newHeight);
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);
            scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = (activeItemCount == 1)
                ? new Vector2(scrollRectInitialSize.x, newHeight * 1.05f)
                : scrollRectInitialSize;
        }
        else
        {
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 0);
            scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
        Canvas.ForceUpdateCanvases();

    }

    private void OnItemSelected(string selectedItem)
    {
        inputField.text = selectedItem;
        OnValueChangedEvt?.Invoke(inputField.text);
        scrollRect.gameObject.SetActive(false);
        arrowButtonDown.SetActive(false);
        arrowButtonUp.SetActive(true);
    }

    public void ResetDropdown()
    {
        inputField.text = string.Empty;
        ResizeContent();
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChange);
        filterDropdownButton.GetComponent<Button>().onClick.RemoveListener(ToggleDropdown);
    }
}

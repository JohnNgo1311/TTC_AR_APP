using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchableDropDown : MonoBehaviour
{
    public GameObject combobox;
    public GameObject arrowButtonUp;
    public GameObject arrowButtonDown;
    public GameObject itemPrefab;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    public GameObject content;
    private RectTransform contentRect;
    private List<string> availableOptions = new List<string>();
    private List<GameObject> itemGameObjects = new List<GameObject>();
    private bool contentActive = false;

    public event Action<string> OnValueChangedEvt;

    private void Awake()
    {
        availableOptions = GlobalVariable_Search_Devices.devices_Model_For_Filter;
        Debug.Log(availableOptions[5].ToString());
        contentRect = content.GetComponent<RectTransform>();
        Initialize();
    }

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputValueChange);
        arrowButtonDown.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
        arrowButtonUp.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
        UpdateUI();
    }

    private void Initialize()
    {
        if (combobox == null || scrollRect == null || inputField == null || content == null || itemPrefab == null)
        {
            Debug.LogError("Không thể tìm thấy các thành phần cần thiết trong combobox!");
            return;
        }
        PopulateDropdown(availableOptions);
    }

    private void PopulateDropdown(List<string> options)
    {
        foreach (var option in options)
        {
            var itemObject = Instantiate(itemPrefab, content.transform);
            itemObject.name = option;
            var textComponent = itemObject.GetComponentInChildren<TMP_Text>();
            textComponent.text = option;
            itemGameObjects.Add(itemObject);
            itemObject.GetComponent<Button>().onClick.AddListener(() => OnItemSelected(option));
        }

        ResizeContent();
        scrollRect.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (availableOptions == null || availableOptions.Count == 0)
        {
            availableOptions = Save_Data_To_Local.GetStringList("List_Device_For_Fitler_A");
        }

        for (int i = 0; i < itemGameObjects.Count; i++)
        {
            var item = itemGameObjects[i];
            if (i < availableOptions.Count)
            {
                var optionText = availableOptions[i];
                var textComponent = item.GetComponentInChildren<TMP_Text>();
                textComponent.text = optionText;
                item.name = optionText;
            }
            else
            {
                item.SetActive(false);
            }
        }

        ResizeContent();
    }

    private void ToggleDropdown()
    {
        //   contentActive = !contentActive;
        SetContentActive(false);
    }

    private void SetContentActive(bool isActive)
    {
        scrollRect.gameObject.SetActive(isActive);
        arrowButtonDown.SetActive(!isActive);
        arrowButtonUp.SetActive(isActive);
        ResizeContent();
    }

    private void OnInputValueChange(string input)
    {
        FilterDropdown(input);
    }

    private void FilterDropdown(string input)
    {
        bool hasActiveItems = false;
        foreach (var item in itemGameObjects)
        {
            bool shouldActivate = item.name.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0;
            item.SetActive(shouldActivate);
            if (shouldActivate) hasActiveItems = true;
        }
        SetContentActive(hasActiveItems);
        ResizeContent();
    }

    private void ResizeContent()
    {
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        int activeItemCount = itemGameObjects.Count(item => item.activeSelf);
        RectTransform itemRect = itemGameObjects.FirstOrDefault()?.GetComponent<RectTransform>();
        if (itemRect != null)
        {
            float newHeight = itemRect.sizeDelta.y * activeItemCount * 1.2f;
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);
        }
    }

    private void OnItemSelected(string selectedItem)
    {
        inputField.text = selectedItem;
        OnValueChangedEvt?.Invoke(selectedItem);
        ToggleDropdown();
    }

    public void ResetDropDown()
    {
        inputField.text = string.Empty;
    }
}
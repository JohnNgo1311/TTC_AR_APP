using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchableDropDown : MonoBehaviour
{
    public GameObject combobox;
    public Button arrowButtonUp;
    public Button arrowButtonDown;
    public GameObject itemPrefab;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    public GameObject content;
    private RectTransform contentRect;
    private List<string> availableOptions = new List<string>();
    private List<GameObject> itemGameObjects = new List<GameObject>();
    private bool contentActive = false;
    private Vector2 scrollRectInitialSize;

    public event Action<string> OnValueChangedEvt;

    private void Awake()
    {
        availableOptions = GlobalVariable_Search_Devices.devices_Model_For_FilterA;
        contentRect = content.GetComponent<RectTransform>();
        scrollRectInitialSize = scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta;
        Initialize();
    }

    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        PopulateDropdown(availableOptions);
        UpdateUI();
        inputField.onValueChanged.AddListener(OnInputValueChange);
        arrowButtonDown.onClick.AddListener(ToggleDropdown);
        arrowButtonUp.onClick.AddListener(ToggleDropdown);
    }

    private void Initialize()
    {
        if (combobox == null || scrollRect == null || inputField == null || content == null || itemPrefab == null)
        {
            Debug.LogError("Không thể tìm thấy các thành phần cần thiết trong combobox!");
            return;
        }
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
            availableOptions = GlobalVariable_Search_Devices.devices_Model_For_FilterA;
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
                item.SetActive(true);
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
        contentActive = !contentActive;
        SetContentActive(contentActive);
    }

    private void SetContentActive(bool isActive)
    {
        ResizeContent();
        scrollRect.gameObject.SetActive(isActive);
        arrowButtonDown.gameObject.SetActive(isActive);
        arrowButtonUp.gameObject.SetActive(!isActive);
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
        int activeItemCount = itemGameObjects.Count(item => item.activeSelf);
        if (activeItemCount > 0)
        {
            RectTransform itemRect = itemGameObjects.FirstOrDefault()?.GetComponent<RectTransform>();
            if (itemRect != null)
            {
                float newHeight = itemRect.sizeDelta.y * activeItemCount * 1.05f;
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);
                scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollRectInitialSize.x, newHeight);
            }
        }
        else
        {
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 0);
            scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        }
    }

    private void OnItemSelected(string selectedItem)
    {
        inputField.text = selectedItem;
        OnValueChangedEvt?.Invoke(inputField.text);
        scrollRect.gameObject.SetActive(false);
        arrowButtonDown.gameObject.SetActive(false);
        arrowButtonUp.gameObject.SetActive(true);
        ResizeContent();
    }

    public void ResetDropDown()
    {
        inputField.text = string.Empty;
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChange);
        arrowButtonDown.GetComponent<Button>().onClick.RemoveListener(ToggleDropdown);
        arrowButtonUp.GetComponent<Button>().onClick.RemoveListener(ToggleDropdown);
    }
}

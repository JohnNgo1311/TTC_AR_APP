using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//! Searchable cho trang PLC Box
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
    private Vector2 scrollRectInitialSize;

    public event Action<string> OnValueChangedEvt;

    //! Script này sử dụng cho trang search nhanh 
    private void Awake()
    {
        availableOptions = GlobalVariable_Search_Devices.devices_Model_For_Filter;
        // Debug.Log(availableOptions[5].ToString());
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
        int onValueChangedListenerCount = inputField.onValueChanged.GetPersistentEventCount();
        if (onValueChangedListenerCount > 0)
        {
            inputField.onValueChanged.AddListener(OnInputValueChange);
        }
        arrowButtonDown.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
        arrowButtonUp.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
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
            availableOptions = GlobalVariable_Search_Devices.devices_Model_For_Filter;
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
        contentActive = !contentActive;
        SetContentActive(contentActive);
    }

    private void SetContentActive(bool isActive)
    {
        ResizeContent();
        scrollRect.gameObject.SetActive(isActive);
        arrowButtonDown.SetActive(isActive);
        arrowButtonUp.SetActive(!isActive);
    }

    private void OnInputValueChange(string input)
    {
        FilterDropdown(input);
    }

    private void FilterDropdown(string input)
    {
        bool hasActiveItems = false;
        //itemGameObjects chứa danh sách các item (là các GameObject) trong dropdown
        foreach (var item in itemGameObjects)
        {
            bool shouldActivate = item.name.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0;
            //StringComparison.OrdinalIgnoreCase: Là cách so sánh chuỗi không phân biệt chữ hoa chữ thường.
            //IndexOf: Trả về chỉ số của chuỗi con đầu tiên được tìm thấy trong chuỗi hiện tại. Nếu không tìm thấy, trả về -1, nếu có thì trả về 0
            item.SetActive(shouldActivate);
            if (shouldActivate) hasActiveItems = true;
        }
        SetContentActive(hasActiveItems);
        ResizeContent();
    }

    private void ResizeContent()
    {
        scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = scrollRectInitialSize;
        int activeItemCount = itemGameObjects.Count(item => item.activeSelf);
        RectTransform itemRect = itemGameObjects.FirstOrDefault()?.GetComponent<RectTransform>();
        if (itemRect != null)
        {
            float newHeight = itemRect.sizeDelta.y * activeItemCount * 1.05f;
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);
            if (activeItemCount == 1)
            {
                scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollRectInitialSize.x, (float)newHeight * 1.05f);
            }
            if (activeItemCount == 0)
            {
                scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            }
        }
    }

    private void OnItemSelected(string selectedItem)
    {
        inputField.text = selectedItem;
        OnValueChangedEvt?.Invoke(inputField.text);
        scrollRect.gameObject.SetActive(false);
        arrowButtonDown.SetActive(false);
        arrowButtonUp.SetActive(true);
        ResizeContent();
        //ToggleDropdown();
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

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

//! Searchable cho trang PLC Box
public class SearchableDropDown : MonoBehaviour
{
    public GameObject arrowButtonUp;
    public GameObject arrowButtonDown;
    public GameObject itemPrefab;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    public GameObject content;
    private RectTransform contentRect;
    private List<string> available_Options_For_Dropdown = new List<string>();
    private List<GameObject> itemGameObjects = new List<GameObject>();
    private bool contentActive = false;
    private Vector2 scrollRectInitialSize;


    public event Action<string> OnValueChangedEvt;

    private void Awake()
    {
        contentRect = content.GetComponent<RectTransform>();
        scrollRectInitialSize = scrollRect.gameObject.GetComponent<RectTransform>().sizeDelta;
        Debug.Log("SearchableDropDown awake");
    }

    private void Start()
    {
        //Invoke(nameof(Set_Initial_Text_Field_Value), 2f);
    }
    public void Set_Initial_Text_Field_Value()
    {
        if (!string.IsNullOrEmpty(GlobalVariable_Search_Devices.temp_List_Device_Information_Model[0].Code))
        { inputField.text = GlobalVariable_Search_Devices.temp_List_Device_Information_Model[0].Code; };
    }
    public void Initialize()
    {
        available_Options_For_Dropdown = GlobalVariable_Search_Devices.temp_List_Device_For_Fitler;

        if (scrollRect == null || inputField == null || content == null || itemPrefab == null)
        {
            Debug.LogError("Không thể tìm thấy các thành phần cần thiết cho SearchableDropDown");
            return;
        }
        else
        {
            Debug.Log(available_Options_For_Dropdown.Count);
            PopulateDropdown(available_Options_For_Dropdown);
            UpdateUI();
            //Đếm số lượng sự kiện được gán cố định cho inputField
            int onValueChangedListenerCount = inputField.onValueChanged.GetPersistentEventCount();
            if (onValueChangedListenerCount > 0)
            {
                inputField.onValueChanged.AddListener(OnInputValueChange);
            }
            arrowButtonDown.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
            arrowButtonUp.GetComponent<Button>().onClick.AddListener(ToggleDropdown);
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
            itemObject.GetComponent<Button>().onClick.AddListener(() => OnItemSelected(option));
            itemGameObjects.Add(itemObject);
        }
        Debug.Log(itemGameObjects.Count);
    }

    private void UpdateUI()
    {
        int optionsCount = available_Options_For_Dropdown.Count;
        for (int i = 0; i < itemGameObjects.Count; i++)
        {
            var item = itemGameObjects[i];
            if (i < optionsCount)
            {
                var optionText = available_Options_For_Dropdown[i];
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
        contentActive = !contentActive;
        scrollRect.gameObject.SetActive(contentActive);
        arrowButtonDown.SetActive(contentActive);
        arrowButtonUp.SetActive(!contentActive);
    }

    private void SetContentActive(bool isActive)
    {
        scrollRect.gameObject.SetActive(isActive);
        arrowButtonDown.SetActive(isActive);
        arrowButtonUp.SetActive(!isActive);
        ResizeContent();
    }

    private void OnInputValueChange(string input)
    {
        FilterDropdown(input);
    }

    private void FilterDropdown(string input)
    {
        //itemGameObjects chứa danh sách các item (là các GameObject) trong dropdown
        foreach (var item in itemGameObjects)
        {
            bool shouldActivate = item.name.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0;
            //StringComparison.OrdinalIgnoreCase: Là cách so sánh chuỗi không phân biệt chữ hoa chữ thường.
            //IndexOf: Trả về chỉ số của chuỗi con đầu tiên được tìm thấy trong chuỗi hiện tại. Nếu không tìm thấy, trả về -1, nếu có thì trả về 0
            item.SetActive(shouldActivate);
        }
        SetContentActive(true);
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
        //ToggleDropdown();
    }

    public void ResetDropDown()
    {
        inputField.text = string.Empty;
        ResizeContent();
    }
    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChange);
        arrowButtonDown.GetComponent<Button>().onClick.RemoveListener(ToggleDropdown);
        arrowButtonUp.GetComponent<Button>().onClick.RemoveListener(ToggleDropdown);
    }
}
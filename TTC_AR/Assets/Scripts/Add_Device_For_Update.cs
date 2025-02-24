using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Add_Device_For_Update : MonoBehaviour
{
    [Header("General")]
    public GameObject parent_Content_Vertical_Group;
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public List<LayoutElement> layoutElements = new List<LayoutElement>();

    [Header("List Selection Panels")]
    public GameObject selection_List_Device_Panel;
    public GameObject selection_List_ModuleIO_Panel;
    public GameObject selection_List_Location_Image_Panel;
    public GameObject selection_List_Connection_Image_Panel;

    public Transform Device_List_Selection_Option_Transform;
    public Transform ModuleIO_List_Selection_Option_Transform;
    public Transform Location_Image_List_Selection_Option_Transform;
    public Transform Connection_Image_List_Selection_Option_Transform;
    private Dictionary<string, GameObject> initialSelectionOptions = new Dictionary<string, GameObject>();

    [Header("Selection Items")]
    public GameObject Device_Parent_GridLayout_Group;
    public GameObject Device_Item_Prefab;
    public GameObject ModuleIO_Parent_GridLayout_Group;
    public GameObject ModuleIO_Item_Prefab;
    public GameObject Location_Image_Parent_VerticalLayout_Group;
    public GameObject Location_Image_Item_Prefab;
    public GameObject Connection_Image_Parent_VerticalLayout_Group;
    public GameObject Connection_Image_Item_Prefab;

    private Dictionary<string, List<GameObject>> selectedGameObjects = new Dictionary<string, List<GameObject>>()
    {
        { "Device", new List<GameObject>() },
        { "ModuleIO", new List<GameObject>() },
        { "Location_Image", new List<GameObject>() },
        { "Connection_Image", new List<GameObject>() }
    };

    private Dictionary<string, int> selectedCounts = new Dictionary<string, int>()
    {
        { "Device", 0 },
        { "ModuleIO", 0 },
        { "Location_Image", 0 },
        { "Connection_Image", 0 }
    };

    private Transform temp_Item_Transform;

    private void Start()
    {
        InitializeItemOptions();
        PopulateListSelection();
        scrollRect.normalizedPosition = new Vector2(0, 1);
        AdjustLayoutElements();
    }

    private void InitializeItemOptions()
    {
        initialSelectionOptions["Device"] = Device_List_Selection_Option_Transform.GetChild(0).gameObject;
        initialSelectionOptions["ModuleIO"] = ModuleIO_List_Selection_Option_Transform.GetChild(0).gameObject;
        initialSelectionOptions["Location_Image"] = Location_Image_List_Selection_Option_Transform.GetChild(0).gameObject;
        initialSelectionOptions["Connection_Image"] = Connection_Image_List_Selection_Option_Transform.GetChild(0).gameObject;
    }

    private void AdjustLayoutElements()
    {
        float viewPortHeight = viewPortTransform.rect.height;
        layoutElements[0].minHeight = viewPortHeight * 0.12f;
        layoutElements[1].minHeight = viewPortHeight * 0.12f;
        layoutElements[2].minHeight = viewPortHeight * 0.6f;
    }

    private void PopulateListSelection()
    {
        PopulateSelectionPanel("Device", GlobalVariable.list_DeviceName, selection_List_Device_Panel, Device_List_Selection_Option_Transform);
        PopulateSelectionPanel("ModuleIO", GlobalVariable.list_ModuleIOName, selection_List_ModuleIO_Panel, ModuleIO_List_Selection_Option_Transform);
        PopulateSelectionPanel("Location_Image", GlobalVariable.list_ImageName, selection_List_Location_Image_Panel, Location_Image_List_Selection_Option_Transform);
        PopulateSelectionPanel("Connection_Image", GlobalVariable.list_ImageName, selection_List_Connection_Image_Panel, Connection_Image_List_Selection_Option_Transform);
    }

    private void PopulateSelectionPanel(string field, List<string> itemList, GameObject panel, Transform optionTransform)
    {
        if (itemList != null && itemList.Count > 0)
        {
            if (itemList.Count == 1)
            {
                AddItemOption(initialSelectionOptions[field], itemList[0], field);
            }
            else
            {
                foreach (var itemName in itemList)
                {
                    GameObject newOption = Instantiate(initialSelectionOptions[field], optionTransform);
                    AddItemOption(newOption, itemName, field);
                }
                initialSelectionOptions[field].SetActive(false);
            }
        }
        panel.SetActive(false);
    }

    private void AddItemOption(GameObject item, string text, string field)
    {
        SetItemOptionText(item, text);
        AddButtonListener(item, () => SelectItem(text, field));
    }

    private void SetItemOptionText(GameObject item, string text)
    {
        TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }

    private void AddButtonListener(GameObject item, UnityEngine.Events.UnityAction action)
    {
        Button button = item.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        GameObject newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(newItem, field));
        GetSelectionPanel(field).SetActive(true);
        //parentGroup.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenListDeviceSelection() => OpenListSelection("Device", Device_Item_Prefab, Device_Parent_GridLayout_Group);
    public void OpenListModuleSelection() => OpenListSelection("ModuleIO", ModuleIO_Item_Prefab, ModuleIO_Parent_GridLayout_Group);
    public void OpenListImageLocationSelection() => OpenListSelection("Location_Image", Location_Image_Item_Prefab, Location_Image_Parent_VerticalLayout_Group);
    public void OpenListImageConnectionSelection() => OpenListSelection("Connection_Image", Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group);

    private void SetItemTextValue(Transform temp_Item_Transform, string textValue)
    {
        TMP_Text textComponent = temp_Item_Transform.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = textValue;
        }
        Debug.Log("Text Value: " + textValue);
    }

    private void SelectItem(string textValue, string field)
    {
        SetItemTextValue(temp_Item_Transform, textValue);
        CloseListSelection();
        selectedCounts[field]++;
        if (!selectedGameObjects[field].Contains(temp_Item_Transform.gameObject))
        {
            selectedGameObjects[field].Add(temp_Item_Transform.gameObject);
        }
    }

    private void DeselectItem(GameObject item, string field)
    {
        selectedCounts[field]--;
        selectedGameObjects[field].Remove(item);
        Destroy(item);
        CloseListSelection();
    }

    public void CloseListSelection()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        // Canvas.ForceUpdateCanvases();
        if (selection_List_Device_Panel.activeSelf) selection_List_Device_Panel.SetActive(false);
        if (selection_List_ModuleIO_Panel.activeSelf) selection_List_ModuleIO_Panel.SetActive(false);
        if (selection_List_Location_Image_Panel.activeSelf) selection_List_Location_Image_Panel.SetActive(false);
        if (selection_List_Connection_Image_Panel.activeSelf) selection_List_Connection_Image_Panel.SetActive(false);
    }
    public void CloseListSelectionFromBackButton()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        // Canvas.ForceUpdateCanvases();
        Destroy(temp_Item_Transform.gameObject);
        if (selection_List_Device_Panel.activeSelf) selection_List_Device_Panel.SetActive(false);
        if (selection_List_ModuleIO_Panel.activeSelf) selection_List_ModuleIO_Panel.SetActive(false);
        if (selection_List_Location_Image_Panel.activeSelf) selection_List_Location_Image_Panel.SetActive(false);
        if (selection_List_Connection_Image_Panel.activeSelf) selection_List_Connection_Image_Panel.SetActive(false);
    }
    private GameObject GetSelectionPanel(string field)
    {
        return field switch
        {
            "Device" => selection_List_Device_Panel,
            "ModuleIO" => selection_List_ModuleIO_Panel,
            "Location_Image" => selection_List_Location_Image_Panel,
            "Connection_Image" => selection_List_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field name")
        };
    }
    private void Update()
    {
        if (selection_List_Device_Panel.activeSelf || selection_List_ModuleIO_Panel.activeSelf || selection_List_Location_Image_Panel.activeSelf || selection_List_Connection_Image_Panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                CloseListSelectionFromBackButton();
            }
        }

    }
}

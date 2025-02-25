using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Add_JB_TSD_Setting : MonoBehaviour
{
    public Initialize_List_Option_Selection initialize_List_Option_Selection;

    [Header("Basic")]
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public GameObject parent_Content_Vertical_Group;
    public List<LayoutElement> layoutElements = new List<LayoutElement>();


    [Header("LayOutGroup")]
    public GameObject Device_Parent_GridLayout_Group;
    public GameObject ModuleIO_Parent_GridLayout_Group;
    public GameObject Location_Image_Parent_VerticalLayout_Group;
    public GameObject Connection_Image_Parent_VerticalLayout_Group;


    [Header("Prefab")]
    public GameObject Device_Item_Prefab;
    public GameObject ModuleIO_Item_Prefab;
    public GameObject Location_Image_Item_Prefab;
    public GameObject Connection_Image_Item_Prefab;
    private Transform temp_Item_Transform;

    [SerializeField]
    private JBInformationModel NewJBInformationModel;

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


    private void Start()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        AdjustLayoutElements();
    }
    private void AdjustLayoutElements()
    {
        float viewPortHeight = viewPortTransform.rect.height;
        layoutElements[0].minHeight = viewPortHeight * 0.12f;
        layoutElements[1].minHeight = viewPortHeight * 0.12f;
        layoutElements[2].minHeight = viewPortHeight * 0.6f;
    }

    private void OnEnable()
    {
        AddButtonListeners(initialize_List_Option_Selection.Device_List_Selection_Option_Content_Transform, "Device");
        AddButtonListeners(initialize_List_Option_Selection.ModuleIO_List_Selection_Option_Content_Transform, "ModuleIO");
        AddButtonListeners(initialize_List_Option_Selection.Location_Image_List_Selection_Option_Content_Transform, "Location_Image");
        AddButtonListeners(initialize_List_Option_Selection.Connection_Image_List_Selection_Option_Content_Transform, "Connection_Image");
    }

    private void AddButtonListeners(Transform parentTransform, string field)
    {
        foreach (Transform optionChild in parentTransform)
        {
            AddButtonListener(optionChild.gameObject, () => SelectItem(optionChild.gameObject.GetComponentInChildren<TMP_Text>().text, field));
        }
    }


    private void AddButtonListener(GameObject item, UnityEngine.Events.UnityAction action)
    {
        Button button = item.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
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

    private void SetItemTextValue(Transform temp_Item_Transform, string textValue)
    {
        TMP_Text textComponent = temp_Item_Transform.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = textValue;
        }
        Debug.Log("Text Value: " + textValue);
    }


    public void OpenListDeviceSelection() => OpenListSelection("Device", Device_Item_Prefab, Device_Parent_GridLayout_Group);
    public void OpenListModuleSelection() => OpenListSelection("ModuleIO", ModuleIO_Item_Prefab, ModuleIO_Parent_GridLayout_Group);
    public void OpenListImageLocationSelection() => OpenListSelection("Location_Image", Location_Image_Item_Prefab, Location_Image_Parent_VerticalLayout_Group);
    public void OpenListImageConnectionSelection() => OpenListSelection("Connection_Image", Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group);

    public void OpenListSelection(string field, GameObject itemPrefab, GameObject parentGroup)
    {
        if (!initialize_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_List_Option_Selection.Selection_Option_Canvas.SetActive(true);
        GameObject newItem = Instantiate(itemPrefab, parentGroup.transform);
        temp_Item_Transform = newItem.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(temp_Item_Transform.gameObject, field));
        GetSelectionPanel(field).SetActive(true);
        //parentGroup.transform.GetChild(0).gameObject.SetActive(false);
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
        if (initialize_List_Option_Selection.selection_List_Device_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf) initialize_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }
    public void CloseListSelectionFromBackButton()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        // Canvas.ForceUpdateCanvases();
        ClearInactiveChildren(Device_Parent_GridLayout_Group);
        ClearInactiveChildren(ModuleIO_Parent_GridLayout_Group);
        ClearInactiveChildren(Location_Image_Parent_VerticalLayout_Group);
        ClearInactiveChildren(Connection_Image_Parent_VerticalLayout_Group);
        temp_Item_Transform.gameObject.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_Device_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Device_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf) initialize_List_Option_Selection.selection_List_ModuleIO_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Location_Image_Panel.SetActive(false);
        if (initialize_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf) initialize_List_Option_Selection.selection_List_Connection_Image_Panel.SetActive(false);
        if (initialize_List_Option_Selection.Selection_Option_Canvas.activeSelf) initialize_List_Option_Selection.Selection_Option_Canvas.SetActive(false);
    }
    private void ClearInactiveChildren(GameObject parentGroup)
    {
        foreach (Transform child in parentGroup.transform)
        {
            if (!child.gameObject.activeSelf && parentGroup.transform.childCount > 1)
            {
                Destroy(child.gameObject);
            }
        }
    }
    private GameObject GetSelectionPanel(string field)
    {
        return field switch
        {
            "Device" => initialize_List_Option_Selection.selection_List_Device_Panel,
            "ModuleIO" => initialize_List_Option_Selection.selection_List_ModuleIO_Panel,
            "Location_Image" => initialize_List_Option_Selection.selection_List_Location_Image_Panel,
            "Connection_Image" => initialize_List_Option_Selection.selection_List_Connection_Image_Panel,
            _ => throw new ArgumentException("Invalid field name")
        };
    }
    private void Update()
    {
        if (initialize_List_Option_Selection.selection_List_Device_Panel.activeSelf || initialize_List_Option_Selection.selection_List_ModuleIO_Panel.activeSelf || initialize_List_Option_Selection.selection_List_Location_Image_Panel.activeSelf || initialize_List_Option_Selection.selection_List_Connection_Image_Panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
            {
                CloseListSelectionFromBackButton();
            }
        }

    }
}

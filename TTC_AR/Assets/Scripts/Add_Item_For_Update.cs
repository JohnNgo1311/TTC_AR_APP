using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Add_Item_For_Update : MonoBehaviour
{
    [Header("General")]
    public GameObject parent_Content_Vertical_Group;
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public List<LayoutElement> layoutElements = new List<LayoutElement>();


    [Header("List Selection Panel")]
    public GameObject selection_List_Device_Panel;
    public GameObject selection_List_ModuleIO_Panel;
    public GameObject selection_List_Location_Image_Panel;
    public GameObject selection_List_Connection_Image_Panel;

    public Transform Device_List_Selection_Option_Transform;
    public Transform ModuleIO_List_Selection_Option_Transform;
    public Transform Location_Image_List_Selection_Option_Transform;
    public Transform Connection_Image_List_Selection_Option_Transform;

    private GameObject Device_Selection_Option_Initial;
    private GameObject ModuleIO_Selection_Option_Initial;
    private GameObject Location_Image_Selection_Option_Initial;
    private GameObject Connection_Image_Selection_Option_Initial;


    [Header("Device Selection Item")]
    public GameObject Device_Parent_GridLayout_Group;
    public GameObject Device_Item_Prefab;
    private List<GameObject> Device_selected_GameObjects = new List<GameObject>();
    private int Device_Selected_Count = 0;

    [Header("ModuleIO Selection Item")]
    public GameObject ModuleIO_Parent_GridLayout_Group;
    public GameObject ModuleIO_Item_Prefab;
    private List<GameObject> ModuleIO_selected_GameObjects = new List<GameObject>();
    private int ModuleIO_Selected_Count = 0;


    [Header("Location_Image Selection Item")]
    public GameObject Location_Image_Parent_VerticalLayout_Group;
    public GameObject Location_Image_Item_Prefab;
    private List<GameObject> Location_Image_selected_GameObjects = new List<GameObject>();
    private int Location_Image_Selected_Count = 0;

    [Header("Connection_Image Selection Item")]
    public GameObject Connection_Image_Parent_VerticalLayout_Group;
    public GameObject Connection_Image_Item_Prefab;
    private List<GameObject> Connection_Image_selected_GameObjects = new List<GameObject>();
    private int Connection_Image_Selected_Count = 0;

    private Transform temp_Item_Transform;
    // private Dictionary<int, string> item_Text_Value_Dictionary = new Dictionary<int, string>();

    private void Start()
    {
        InitializeItemOption();
        Populate_List_Selection_JB_TSD();
        scrollRect.normalizedPosition = new Vector2(0, 1);
        AdjustLayoutElements();
    }

    private void InitializeItemOption()
    {
        Device_Selection_Option_Initial = Device_List_Selection_Option_Transform.GetChild(0).gameObject;
        ModuleIO_Selection_Option_Initial = ModuleIO_List_Selection_Option_Transform.GetChild(0).gameObject;
        Location_Image_Selection_Option_Initial = Location_Image_List_Selection_Option_Transform.GetChild(0).gameObject;
        Connection_Image_Selection_Option_Initial = Connection_Image_List_Selection_Option_Transform.GetChild(0).gameObject;

    }
    private void AdjustLayoutElements()
    {
        float viewPortHeight = viewPortTransform.rect.height;
        layoutElements[0].minHeight = viewPortHeight * 0.12f;
        layoutElements[1].minHeight = viewPortHeight * 0.12f;
        layoutElements[2].minHeight = viewPortHeight * 0.6f;
    }

    private void Populate_List_Selection_JB_TSD()
    {
        if (!selection_List_Device_Panel.activeSelf) selection_List_Device_Panel.SetActive(true);
        if (!selection_List_ModuleIO_Panel.activeSelf) selection_List_ModuleIO_Panel.SetActive(true);
        if (!selection_List_Location_Image_Panel.activeSelf) selection_List_Location_Image_Panel.SetActive(true);
        if (!selection_List_Connection_Image_Panel.activeSelf) selection_List_Connection_Image_Panel.SetActive(true);


        List<string> List_DeviceName = GlobalVariable.list_DeviceName;
        List<string> List_ModuleIOName = GlobalVariable.list_ModuleIOName;

        List<string> List_LocationImageName = GlobalVariable.list_ImageName;
        List<string> List_ConnectionImageName = GlobalVariable.list_ImageName;

        if (List_DeviceName != null && List_DeviceName.Count > 0)
        {
            // item_Text_Value_Dictionary.Clear();
            if (List_DeviceName.Count == 1)
            {
                AddItemOption(Device_Selection_Option_Initial, List_DeviceName[0], "Device");
            }
            else
            {
                foreach (var DeviceName in List_DeviceName)
                {
                    GameObject new_Device_Option = Instantiate(Device_Selection_Option_Initial, Device_List_Selection_Option_Transform);
                    AddItemOption(new_Device_Option, DeviceName, "Device");
                }
                Device_Selection_Option_Initial.SetActive(false);
            }
        }
        selection_List_Device_Panel.SetActive(false);

        if (List_ModuleIOName != null && List_ModuleIOName.Count > 0)
        {
            // item_Text_Value_Dictionary.Clear();
            if (List_ModuleIOName.Count == 1)
            {
                AddItemOption(ModuleIO_Selection_Option_Initial, List_ModuleIOName[0], "ModuleIO");
            }
            else
            {
                foreach (var ModuleIOName in List_ModuleIOName)
                {
                    GameObject new_ModuleIO_Option = Instantiate(ModuleIO_Selection_Option_Initial, ModuleIO_List_Selection_Option_Transform);
                    AddItemOption(new_ModuleIO_Option, ModuleIOName, "ModuleIO");
                }
                ModuleIO_Selection_Option_Initial.SetActive(false);
            }
        }
        selection_List_ModuleIO_Panel.SetActive(false);

        if (List_LocationImageName != null && List_LocationImageName.Count > 0)
        {
            // item_Text_Value_Dictionary.Clear();
            if (List_LocationImageName.Count == 1)
            {
                AddItemOption(Location_Image_Selection_Option_Initial, List_LocationImageName[0], "Location_Image");
            }
            else
            {
                foreach (var ImageName in List_LocationImageName)
                {
                    GameObject new_Image_Option = Instantiate(Location_Image_Selection_Option_Initial, Location_Image_List_Selection_Option_Transform);
                    AddItemOption(new_Image_Option, ImageName, "Location_Image");
                }
                Location_Image_Selection_Option_Initial.SetActive(false);
            }
        }
        selection_List_Location_Image_Panel.SetActive(false);

        if (List_ConnectionImageName != null && List_ConnectionImageName.Count > 0)
        {
            // item_Text_Value_Dictionary.Clear();
            if (List_ConnectionImageName.Count == 1)
            {
                AddItemOption(Connection_Image_Selection_Option_Initial, List_ConnectionImageName[0], "Connection_Image");
            }
            else
            {
                foreach (var ImageName in List_ConnectionImageName)
                {
                    GameObject new_Image_Option = Instantiate(Connection_Image_Selection_Option_Initial, Connection_Image_List_Selection_Option_Transform);
                    AddItemOption(new_Image_Option, ImageName, "Connection_Image");
                }
                Connection_Image_Selection_Option_Initial.SetActive(false);
            }
        }
        selection_List_Connection_Image_Panel.SetActive(false);
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

    public void OpenListDeviceSelection()
    {
        GameObject newDeviceItem = Instantiate(Device_Item_Prefab, Device_Parent_GridLayout_Group.transform);
        temp_Item_Transform = newDeviceItem.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(newDeviceItem, "Device"));
        selection_List_Device_Panel.SetActive(true);
        Device_Parent_GridLayout_Group.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OpenListModuleSelection()
    {
        GameObject newModuleItem = Instantiate(ModuleIO_Item_Prefab, ModuleIO_Parent_GridLayout_Group.transform);
        temp_Item_Transform = newModuleItem.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(newModuleItem, "ModuleIO"));
        selection_List_ModuleIO_Panel.SetActive(true);
        ModuleIO_Parent_GridLayout_Group.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OpenListImageLocationSelection()
    {
        GameObject new_Location_Image_Item = Instantiate(Location_Image_Item_Prefab, Location_Image_Parent_VerticalLayout_Group.transform);
        temp_Item_Transform = new_Location_Image_Item.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(new_Location_Image_Item, "Location_Image"));
        selection_List_Location_Image_Panel.SetActive(true);
    }
    public void OpenListImageConnectionSelection()
    {
        GameObject new_Connection_Image_Item = Instantiate(Connection_Image_Item_Prefab, Connection_Image_Parent_VerticalLayout_Group.transform);
        temp_Item_Transform = new_Connection_Image_Item.transform;
        temp_Item_Transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(new_Connection_Image_Item, "Connection_Image"));
        selection_List_Connection_Image_Panel.SetActive(true);
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

    private void SelectItem(string textValue, string Field)
    {
        SetItemTextValue(temp_Item_Transform, textValue);
        // Blue_Item_Selected_Count++;
        // Debug.Log("Select Item" + Blue_Item_Selected_Count);
        CloseListSelection();
        switch (Field)
        {
            case "Device":
                Device_Selected_Count++;
                if (!Device_selected_GameObjects.Contains(temp_Item_Transform.gameObject))
                {
                    Device_selected_GameObjects.Add(temp_Item_Transform.gameObject);
                }
                break;
            case "ModuleIO":
                ModuleIO_Selected_Count++;
                if (!ModuleIO_selected_GameObjects.Contains(temp_Item_Transform.gameObject))
                {
                    ModuleIO_selected_GameObjects.Add(temp_Item_Transform.gameObject);
                }
                break;
            case "Location_Image":
                Location_Image_Selected_Count++;
                if (!Location_Image_selected_GameObjects
                    .Contains(temp_Item_Transform.gameObject))
                {
                    Location_Image_selected_GameObjects.Add(temp_Item_Transform.gameObject);
                }
                break;
            case "Connection_Image":
                Connection_Image_Selected_Count++;
                if (!Connection_Image_selected_GameObjects
                    .Contains(temp_Item_Transform.gameObject))
                {
                    Connection_Image_selected_GameObjects.Add(temp_Item_Transform.gameObject);
                }
                break;
        }

    }

    private void DeselectItem(GameObject item, string field)
    {
        // if (Device_selected_GameObjects.Contains(item))
        // {
        //     Device_selected_GameObjects.Remove(item);
        //     Destroy(item);
        //     Debug.Log("Deselect Item - Count: " + Blue_Item_Selected_Count);
        //     CloseListSelection();
        // }
        switch (field)
        {
            case "Device":
                Device_Selected_Count--;
                Device_selected_GameObjects.Remove(item);
                Destroy(item);
                break;
            case "ModuleIO":
                ModuleIO_Selected_Count--;
                ModuleIO_selected_GameObjects.Remove(item);
                Destroy(item);
                break;
            case "Location_Image":
                Location_Image_Selected_Count--;
                Location_Image_selected_GameObjects.Remove(item);
                Destroy(item);
                break;
            case "Connection_Image":
                Connection_Image_Selected_Count--;
                Connection_Image_selected_GameObjects.Remove(item);
                Destroy(item);
                break;
        }
        CloseListSelection();
    }

    public void CloseListSelection()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(parent_Content_Vertical_Group.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        if (selection_List_Device_Panel.activeSelf) selection_List_Device_Panel.SetActive(false);
        if (selection_List_ModuleIO_Panel.activeSelf) selection_List_ModuleIO_Panel.SetActive(false);
        if (selection_List_Location_Image_Panel.activeSelf) selection_List_Location_Image_Panel.SetActive(false);
        if (selection_List_Connection_Image_Panel.activeSelf) selection_List_Connection_Image_Panel.SetActive(false);
    }
}

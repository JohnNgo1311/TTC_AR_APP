using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Add_Item_For_Update_Optional : MonoBehaviour
{
    public GameObject parentVerticalObject;
    public GameObject selectionList;
    public GameObject horizontal_Item_Selection_Prefab;
    public GameObject horizontal_Item_Selection_Available;
    public Transform Content_List_Selection_Transform;
    private GameObject Item_Option_Initial;
    private List<GameObject> selectedGameObjects = new List<GameObject>();
    [SerializeField]
    private int itemSelectedCount = 0;
    [SerializeField]
    private int horizontalItemCount;
    private Sprite blue_Add_Remove_Item_Background;
    private Transform itemTransform;
    private string item_Text_Value;
    private Dictionary<int, string> item_Text_Value_Dictionary = new Dictionary<int, string>();

    private void Start()
    {
        LoadResources();
        InitializeItemOption();
        InitializeHorizontalItemCount();
        PopulateItemList();
    }

    private void LoadResources()
    {
        blue_Add_Remove_Item_Background = Resources.Load<Sprite>("images/UIimages/Blue_item_add_or_remove_Background");
        Debug.Log(blue_Add_Remove_Item_Background == null ? "Blue_Add_Remove_Item_Background_Temp is null" : "Blue_Add_Remove_Item_Background_Temp is not null");
    }

    private void InitializeItemOption()
    {
        Item_Option_Initial = Content_List_Selection_Transform.GetChild(0).gameObject;
    }

    private void InitializeHorizontalItemCount()
    {
        horizontalItemCount = parentVerticalObject.transform.childCount - 1; // Exclude HorizontalItemSelectionAvailable
        Debug.Log("Horizontal_Item_Count: " + horizontalItemCount);
    }

    private void PopulateItemList()
    {
        selectionList.SetActive(true);
        List<string> list_JBName = GlobalVariable.list_jBName;
        if (list_JBName != null && list_JBName.Any())
        {
            item_Text_Value_Dictionary.Clear();

            if (list_JBName.Count == 1)
            {
                item_Text_Value_Dictionary.Add(0, list_JBName[0]);
                SetItemOptionText(Item_Option_Initial, list_JBName[0]);
                AddButtonListener(Item_Option_Initial, () => SelectItem(list_JBName[0]));
            }
            else
            {
                foreach (var JB_Name in list_JBName)
                {
                    GameObject new_Item = Instantiate(Item_Option_Initial, Content_List_Selection_Transform);
                    SetItemOptionText(new_Item, JB_Name);
                    AddButtonListener(new_Item, () => SelectItem(JB_Name));
                }
                Item_Option_Initial.SetActive(false);
            }
        }
        selectionList.SetActive(false);
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

    public void OpenListSelection()
    {
        Debug.Log("Item_Selected_Count " + itemSelectedCount);
        if (itemSelectedCount == 0)
        {
            itemTransform = horizontal_Item_Selection_Available.transform.GetChild(0);
        }
        else if (itemSelectedCount < 4)
        {
            itemTransform = horizontal_Item_Selection_Available.transform.GetChild(itemSelectedCount);
        }
        else if (itemSelectedCount % 4 == 0)
        {
            GameObject newHorizontalItemSelection = Instantiate(horizontal_Item_Selection_Prefab, parentVerticalObject.transform);
            itemTransform = newHorizontalItemSelection.transform.GetChild(0);
            horizontalItemCount++;
            Debug.Log("Horizontal_Item_Count: " + horizontalItemCount);
        }
        else
        {
            itemTransform = parentVerticalObject.transform.GetChild(horizontalItemCount).GetChild(itemSelectedCount % 4);
        }
        SetItemBackground(itemTransform, blue_Add_Remove_Item_Background);
        var tempItemTransform = itemTransform;
        itemTransform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => DeselectItem(tempItemTransform.gameObject));
        selectionList.SetActive(true);
    }

    private void SetItemBackground(Transform itemTransform, Sprite background)
    {
        Image imageComponent = itemTransform.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = background;
        }
    }

    private void SetItemTextValue(Transform itemTransform, string textValue)
    {
        TMP_Text textComponent = itemTransform.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = textValue;
        }
        Debug.Log("Text Value: " + textValue);
    }

    private void SelectItem(string textValue)
    {
        SetItemTextValue(itemTransform, textValue);
        itemSelectedCount++;
        Debug.Log("Select Item" + itemSelectedCount);
        CloseListSelection();
        if (!selectedGameObjects.Contains(itemTransform.gameObject)) selectedGameObjects.Add(itemTransform.gameObject);

    }

    private void DeselectItem(GameObject item)
    {
        if (selectedGameObjects.Contains(item))
        {
            selectedGameObjects.Remove(item);
            itemSelectedCount = Mathf.Max(0, itemSelectedCount - 1);

            TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = "";
            }

            item.SetActive(false);
            Debug.Log("Deselect Item - Count: " + itemSelectedCount);
        }
    }



    public void CloseListSelection()
    {
        Resize_Gameobject_Function.Resize_Parent_GameObject(parentVerticalObject.GetComponent<RectTransform>());
        selectionList.SetActive(false);
    }
}
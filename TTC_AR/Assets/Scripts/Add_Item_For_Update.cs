using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Add_Item_For_Update : MonoBehaviour
{
    public GameObject ParentVerticalObject;
    public GameObject SelectionList;
    public GameObject Horizontal_Item_Selection_Prefab;
    public GameObject Horizontal_Item_Selection_Available;

    private List<GameObject> List_Selected_GameObject = new List<GameObject>();
    [SerializeField]
    private int Item_Selected_Count = 0;  //! Tổng số Item đã được chọn, chỉ tăng sau khi Item được gán giá trị
    [SerializeField]
    private int List_Horizontal_Item_Count; //! Tổng số horinzontal Group đã được tạo
    private Sprite Blue_Add_Remove_Item_Background_Temp;
    //  private Sprite Light_Blue_Add_Remove_Item_Background_Temp;
    private int index;

    private void Start()
    {
        Blue_Add_Remove_Item_Background_Temp = Resources.Load<Sprite>("images/UIimages/Blue_item_add_or_remove_Background");
        if (Blue_Add_Remove_Item_Background_Temp == null)
        {
            Debug.LogError("Blue_Add_Remove_Item_Background_Temp is null");
        }
        else
        {
            Debug.Log("Blue_Add_Remove_Item_Background_Temp is not null");
        }
        // Light_Blue_Add_Remove_Item_Background_Temp = Resources.Load<Sprite>("images/UIimages/light_Blue_Add_Remove_Item_Background.png");
        List_Horizontal_Item_Count = ParentVerticalObject.transform.childCount - 1;
        Debug.Log("List_Horizontal_Item_Count: " + List_Horizontal_Item_Count);
    }

    public void OpenListSelection()
    {
        Debug.Log("Item_Selected_Count " + Item_Selected_Count);
        if (Item_Selected_Count == 0)
        {
            SetItemBackground(Horizontal_Item_Selection_Available.transform.GetChild(0), Blue_Add_Remove_Item_Background_Temp);
        }
        else if (Item_Selected_Count < 4)
        {
            SetItemBackground(Horizontal_Item_Selection_Available.transform.GetChild(Item_Selected_Count), Blue_Add_Remove_Item_Background_Temp);
        }
        else if (Item_Selected_Count % 4 == 0)
        {
            GameObject new_Horizontal_Item_Selection = Instantiate(Horizontal_Item_Selection_Prefab, ParentVerticalObject.transform);
            SetItemBackground(new_Horizontal_Item_Selection.transform.GetChild(0), Blue_Add_Remove_Item_Background_Temp);
            List_Horizontal_Item_Count++;
            Debug.Log("List_Horizontal_Item_Count: " + List_Horizontal_Item_Count);
        }
        else
        {
            SetItemBackground(ParentVerticalObject.transform.GetChild(List_Horizontal_Item_Count).GetChild(Item_Selected_Count % 4), Blue_Add_Remove_Item_Background_Temp);
        }
        SelectionList.SetActive(true);
    }

    private void SetItemBackground(Transform itemTransform, Sprite background)
    {
        itemTransform.GetComponent<Image>().sprite = background;
    }

    private void SelectItem()
    {
        Item_Selected_Count++;
        Debug.Log("Select Item" + Item_Selected_Count);
    }

    private void DeselectItem()
    {
        Item_Selected_Count--;
        Debug.Log("Deselect Item" + Item_Selected_Count);
    }

    public void CloseListSelection()
    {
        SelectItem();
        SelectionList.SetActive(false);
    }
}
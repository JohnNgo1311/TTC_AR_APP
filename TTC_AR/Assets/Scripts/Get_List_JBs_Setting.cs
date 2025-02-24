using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Get_List_JBs_Setting : MonoBehaviour
{
    public GameObject List_JB_Canvas;
    public GameObject Add_New_JB_Canvas;
    public GameObject Update_JB_Canvas;
    public GameObject DialogCanvas;
    public GameObject JB_Item_Prefab;
    public GameObject Parent_Vertical_Layout_Group;
    public ScrollRect scrollView;
    public GameObject DialogOneButton;
    public GameObject DialogTwoButton;

    private List<string> ListJBsName = new List<string>();
    private List<GameObject> listJBItems = new List<GameObject>();


    private void Start()
    {
        if (GlobalVariable.list_jBName != null && GlobalVariable.list_jBName.Count > 0)
        {
            ListJBsName = GlobalVariable.list_jBName;
            Initialize(ListJBsName);
        }
        scrollView.normalizedPosition = new Vector2(0, 1);
    }

    private void Initialize(List<string> listJBsName)
    {
        foreach (var jbName in listJBsName)
        {
            int jbIndex = ListJBsName.IndexOf(jbName);
            Debug.Log(jbIndex);
            GameObject newJBItem = Instantiate(JB_Item_Prefab, Parent_Vertical_Layout_Group.transform);
            Transform newJBItemTransform = newJBItem.transform;
            Transform newJBItemPreviewInforGroup = newJBItemTransform.GetChild(0);
            Transform newJBItemPreviewButtonGroup = newJBItemTransform.GetChild(1);
            newJBItemPreviewInforGroup.Find("Preview_JB_Name").GetComponent<TMP_Text>().text = jbName;
            newJBItemPreviewInforGroup.Find("Preview_JB_GrapLocation").GetComponent<TMP_Text>().text = "GrapperA";

            listJBItems.Add(newJBItem);

            newJBItemPreviewButtonGroup.Find("Group/Edit_Button").GetComponent<Button>().onClick.AddListener(EditJBItem);
            newJBItemPreviewButtonGroup.Find("Group/Delete_Button").GetComponent<Button>().onClick.AddListener(() => DeleJBItem(newJBItem, jbName, 1));
        }
        JB_Item_Prefab.SetActive(false);
    }

    private void DeleJBItem(GameObject JBItem, string JbName, int ItemId = 1)
    {
        OpenDeleteWarningPanel(JBItem, JbName, ItemId);
    }

    private void EditJBItem()
    {
        OpenUpdateCanvas();
    }

    public void OpenAddNewCanvas()
    {
        Add_New_JB_Canvas.SetActive(true);
        List_JB_Canvas.SetActive(false);
        Update_JB_Canvas.SetActive(false);
    }
    private void OpenUpdateCanvas()
    {
        Update_JB_Canvas.SetActive(true);
        List_JB_Canvas.SetActive(false);
        Add_New_JB_Canvas.SetActive(false);
    }
    public void BackToListJb()
    {
        List_JB_Canvas.SetActive(true);
        Update_JB_Canvas.SetActive(false);
        Add_New_JB_Canvas.SetActive(false);
    }


    private void OpenDeleteWarningPanel(GameObject JBItem, string JbName, int ItemId = 1)
    {
        DialogTwoButton.SetActive(true);
        var Horizontal_Group = DialogTwoButton.transform.Find("Background/Horizontal_Group").gameObject.transform;
        var dialog_Content = DialogTwoButton.transform.Find("Background/Dialog_Content").GetComponent<TMP_Text>().text =
        $"Bạn có chắc chắn muốn xóa tủ <color=#FF0000><b>{JbName}</b></color> khỏi hệ thống? Hãy kiểm tra kĩ trước khi nhấn nút xác nhận phía dưới";
        var confirmButton = Horizontal_Group.transform.Find("Confirm_Button").GetComponent<Button>();
        var backButton = Horizontal_Group.transform.Find("Back_Button").GetComponent<Button>();
        confirmButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            Destroy(JBItem);
            listJBItems.Remove(JBItem);
            DialogTwoButton.SetActive(false);
        });

        backButton.onClick.AddListener(() =>
        {
            DialogTwoButton.SetActive(false);
        });
    }
}

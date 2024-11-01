using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public string grapper;
    public List<GameObject> imageTargets;
    public GameObject content;
    private GameObject scroll_Area;
    [Header("space between menu items")]
    //  [SerializeField] private Vector2 spacing;

    [Header("Main button rotation")]
    public Button mainButton;
    private List<SettingsMenuItem> menuItems;

    private bool isExpanded = false;
    // private Vector2 mainButtonPosition;

    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        InitializeMenuItems();
        SetupMainButton();
        // mainButtonPosition = mainButton.GetComponent<RectTransform>().anchoredPosition;
    }

    private void InitializeMenuItems()
    {
        scroll_Area = gameObject.transform.Find("Scroll_Area").gameObject;
        // scrollRect = gameObject.GetComponent<ScrollRect>();
        if (grapper == "A" && GlobalVariable.rackData_GrapperA != null)
        {
            for (int i = 1; i < 6; i++)
            {
                var newObject = Instantiate(content.transform.GetChild(0), content.transform);
                newObject.name = $"Rack_{i + 1}";
                newObject.GetComponentInChildren<TMP_Text>().text = $"Rack {i + 1}";
                newObject.gameObject.SetActive(false);
            }
            content.gameObject.SetActive(false);
            scroll_Area.SetActive(false);

        }

        int itemsCount = content.transform.childCount;
        menuItems = new List<SettingsMenuItem>(itemsCount);

        for (int i = 0; i < itemsCount; i++)
        {
            menuItems.Add(content.transform.GetChild(i).GetComponent<SettingsMenuItem>());
        }
        OnItemClick("Rack_1");
    }

    private void SetupMainButton()
    {
        mainButton = mainButton.GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
    }

    private void ToggleMenu()
    {
        isExpanded = !isExpanded;
        Debug.Log($"ToggleMenu: {isExpanded}");
        menuItems.ForEach(item => item.gameObject.SetActive(isExpanded));
        content.SetActive(isExpanded);
        // Resize_Gameobject_Function.Resize_Parent_GameObject(content.GetComponent<RectTransform>());
        //  Resize_Gameobject_Function.Resize_Parent_GameObject(scroll_Area.GetComponent<RectTransform>());
        scroll_Area.SetActive(isExpanded);

    }

    public void OnItemClick(string itemName)
    {
        string[] splitString = itemName.Split("_");
        char rackIdentifier = splitString[1][0];
        GlobalVariable.activated_iamgeTargets.Clear();
        foreach (var imageTarget in imageTargets)
        {
            bool isActive = imageTarget.name.Split("_")[1][1] == rackIdentifier;
            imageTarget.SetActive(isActive);

            if (isActive)
            {
                mainButton.GetComponentInChildren<TMP_Text>().text = $"Rack {rackIdentifier}";
                GlobalVariable.activated_iamgeTargets.Add(imageTarget);
            }
        }
        ToggleMenu();

    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}

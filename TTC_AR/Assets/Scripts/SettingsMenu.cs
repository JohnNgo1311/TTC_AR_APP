using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public List<GameObject> imageTargets;
    public GameObject content;

    [SerializeField]
    private GameObject scroll_Area;
    public Button mainButton;
    private List<SettingsMenuItem> menuItems;
    private bool isExpanded = true;

    public Get_List_MCCs get_List_MCCs;
    // private Vector2 mainButtonPosition;

    void Start()
    {
        InitializeMenuItems();
        SetupMainButton();
        // mainButtonPosition = mainButton.GetComponent<RectTransform>().anchoredPosition;
    }

    private void InitializeMenuItems()
    {
        scroll_Area = scroll_Area ?? gameObject.transform.Find("Scroll_Area").gameObject;
        // scrollRect = gameObject.GetComponent<ScrollRect>();
        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
        {
            if (GlobalVariable.temp_ListRackBasicModels.Any())
            {
                for (int i = 1; i < GlobalVariable.temp_ListRackBasicModels.Count; i++)
                {
                    var newObject = Instantiate(content.transform.GetChild(0), content.transform);
                    newObject.name = $"Rack_{i + 1}";
                    newObject.GetComponentInChildren<TMP_Text>().text = $"Rack {i + 1}";
                    newObject.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("No rack found");
            }
        }
        else if (SceneManager.GetActiveScene().name == MyEnum.FieldDevicesScene.GetDescription())
        {
            if (GlobalVariable.temp_ListGrapperBasicModels.Any())
            {
                for (int i = 1; i < GlobalVariable.temp_ListGrapperBasicModels.Count; i++)
                {
                    var newObject = Instantiate(content.transform.GetChild(0), content.transform);
                    newObject.name = GlobalVariable.temp_ListGrapperBasicModels[i].Name;
                    newObject.GetComponentInChildren<TMP_Text>().text = newObject.name;
                    newObject.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("No Grapper found");
            }
        }

        content.SetActive(false);
        scroll_Area.SetActive(false);

        int itemsCount = content.transform.childCount;
        menuItems = new List<SettingsMenuItem>(itemsCount);

        for (int i = 0; i < itemsCount; i++)
        {
            menuItems.Add(content.transform.GetChild(i).GetComponent<SettingsMenuItem>());
        }
        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
        {
            OnItemClick("Rack_1");
        }
        else if (SceneManager.GetActiveScene().name == MyEnum.FieldDevicesScene.GetDescription())
        {
            OnItemClick("GrapperA");
        }
    }

    private void SetupMainButton()
    {
        mainButton = mainButton.GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
    }

    private void ToggleMenu()
    {
        isExpanded = !isExpanded;
        //Debug$"ToggleMenu: {isExpanded}");
        menuItems.ForEach(item => item.gameObject.SetActive(isExpanded));
        content.SetActive(isExpanded);
        // Resize_GameObject_Function.Resize_Parent_GameObject(content.GetComponent<RectTransform>());
        //  Resize_GameObject_Function.Resize_Parent_GameObject(scroll_Area.GetComponent<RectTransform>());
        scroll_Area.SetActive(isExpanded);

    }

    public void OnItemClick(string itemName)
    {
        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
        {
            string[] splitString = itemName.Split("_");
            var rackIdentifier = splitString[1][0];
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
        }
        else if (SceneManager.GetActiveScene().name == MyEnum.FieldDevicesScene.GetDescription())
        {
            var grapperId = GlobalVariable.temp_ListGrapperBasicModels.Find(g => g.Name == itemName).Id;
            get_List_MCCs.GrapperId = grapperId;
            get_List_MCCs.GrapperName = itemName;
            get_List_MCCs.GetListMCCModels(grapperId);

            foreach (var imageTarget in imageTargets)
            {
                if (imageTarget.gameObject.name.Contains(itemName))
                {
                    imageTarget.SetActive(true);
                    GlobalVariable.activated_iamgeTargets.Add(imageTarget);
                }
                else
                {
                    imageTarget.SetActive(false);
                }
            }
            mainButton.GetComponentInChildren<TMP_Text>().text = itemName;

        }
        ToggleMenu();

    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
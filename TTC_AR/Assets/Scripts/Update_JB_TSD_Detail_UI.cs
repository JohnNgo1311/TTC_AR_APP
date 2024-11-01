using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;

public class Update_JB_TSD_Detail_UI : MonoBehaviour
{
    public string grapper;
    [SerializeField] private GameObject jB_TSD_Detail_Panel_Prefab;
    [SerializeField] private Canvas canvas_Parent;
    [SerializeField] private TMP_Text jB_TSD_Title;
    [SerializeField] private TMP_Text jb_location_value;
    [SerializeField] private GameObject jb_location_imagePrefab_hortizontal_group;
    [SerializeField] private Image jb_location_imagePrefab;
    [SerializeField] private Image jb_connection_imagePrefab;
    [SerializeField] private GameObject jb_connection_imagePrefab_group;
    [SerializeField] private GameObject jb_Infor_Item_Prefab;
    [SerializeField] private GameObject scroll_Area_Content;
    [SerializeField] private ScrollRect scroll_Area;
    private List<string> jb_location_bis_and_non_bis_filtered = new List<string>();


    private List<GameObject> instantiatedImages = new List<GameObject>();
    private string jb_name;
    private string jb_location;
    private Dictionary<string, Sprite> connectionSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> locationSprites = new Dictionary<string, Sprite>();
    private int pendingSpriteLoad1 = 0;
    private int pendingSpriteLoad2 = 0;
    private List<string> jb_Location_bis_and_non_bis = new List<string>();
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    private void OnEnable()
    {
        // SetJobDetails();
        if (canvas_Parent == null) canvas_Parent = GetComponentInParent<Canvas>();
        if (jB_TSD_Detail_Panel_Prefab == null)
            jB_TSD_Detail_Panel_Prefab = canvas_Parent?.transform.Find("Detail_JB_TSD")?.gameObject;
        scroll_Area = jB_TSD_Detail_Panel_Prefab?.transform.Find("Scroll_Area")?.GetComponent<ScrollRect>();
        scroll_Area.verticalNormalizedPosition = 1f;
        InitializeReferences();
        UpdateTitle();
        LoadLocationSprites(); // hàm này chạy bất đông bộ
        LoadConnectionSprites(); // hàm này chạy bất đông bộ    }
    }



    private void OnDisable()
    {
        ClearSpritesAndObjects();
        Clear_Instantiated_Image_Objects();
    }
    /*  private void SetJobDetails()
      {
          var jobDetails = GlobalVariable.jb_TSD_Title.Split('_'); // EX: JB100_Hầm Cáp MCC
          jb_name = jobDetails[0]; // jb_name: JB100
          // Debug.Log("jb_name: " + jb_name);
          jb_location = jobDetails.Length > 1 ? jobDetails[1] : string.Empty; // jb_location: Hầm Cáp MCC
          // Debug.Log("jb_location: " + jb_location);
      }*/

    private void InitializeReferences()
    {

        jB_TSD_Title = jB_TSD_Detail_Panel_Prefab?.transform.Find("Horizontal_JB_TSD_Title/JB_TSD_Title")?.GetComponent<TMP_Text>();
        scroll_Area_Content = jB_TSD_Detail_Panel_Prefab?.transform.Find("Scroll_Area/Content")?.gameObject;
        jb_location_imagePrefab_hortizontal_group = scroll_Area_Content?.transform.Find("JB_location_Horizontal_Group").gameObject;
        jb_Infor_Item_Prefab = jb_location_imagePrefab_hortizontal_group?.transform.Find("JB_Infor")?.gameObject;
        jb_location_value = jb_Infor_Item_Prefab?.transform.Find("Text_JB_location_group/Text_Jb_Location_Value")?.GetComponent<TMP_Text>();
        jb_location_imagePrefab = jb_Infor_Item_Prefab?.transform.Find("JB_location_imagePrefab")?.GetComponent<Image>();
        jb_connection_imagePrefab = scroll_Area_Content?.transform.Find("JB_TSD_connection_imagePrefab")?.GetComponent<Image>();
    }

    private void UpdateTitle()
    {
        if (!string.IsNullOrEmpty(GlobalVariable.jb_TSD_Name) && !string.IsNullOrEmpty(GlobalVariable.jb_TSD_Location))
        {
            jB_TSD_Title.text = GlobalVariable.jb_TSD_Name;
            jb_location_value.text = GlobalVariable.jb_TSD_Location;
        }
    }

    private void LoadLocationSprites()
    {
        jb_name = GlobalVariable.jb_TSD_Name;
        jb_location = GlobalVariable.jb_TSD_Location;
        pendingSpriteLoad1++;
        //! Addressables khiến cho hàm LoadLocationSprites() chạy bất đồng bộ
        Addressables.LoadAssetsAsync<Sprite>("Real_Outdoor_JB_TSD", sprite =>
        {
            locationSprites[sprite.name] = sprite;
            jb_Location_bis_and_non_bis.Add(sprite.name);
            Debug.Log("Location sprite loaded: " + sprite.name);
        }).Completed += handle =>
        {
            //! Sự kiện này chạy khi đã tải xong toàn bộ Sprite có cùng key là "Real_Outdoor_JB_TSD"
            Debug.Log("pendingSpriteLoads: " + pendingSpriteLoad1);
            pendingSpriteLoad1--;
            if (handle.Status == AsyncOperationStatus.Succeeded && pendingSpriteLoad1 == 0)
            {
                Debug.Log("All location sprites loaded successfully.");
                ApplyLocationSprite();
            }
            else
            {
                Debug.LogError("Failed to load location sprites");
            }
        };
    }

    private void ApplyLocationSprite()
    {
        //  Clear_Instantiated_Image_Objects();
        jb_location_bis_and_non_bis_filtered = new List<string>();
        jb_location_bis_and_non_bis_filtered.AddRange(jb_Location_bis_and_non_bis.Where(x => x.Contains($"{jb_name}_Location")).ToList());

        if (jb_location_bis_and_non_bis_filtered.Count < 2)
        {

            if (jb_name.Contains("-")) //? Ex: JB100-JB101
            {
                string[] jb_name_split = jb_name.Split("-");
                for (int i = 0; i < jb_name_split.Length; i++)
                {

                    Create_And_Set_Sprite_For_JB_Location($"{jb_name_split[i]}_Location");
                }
                jb_Infor_Item_Prefab.gameObject.SetActive(false);
            }

            else
            {
                string jb_location_name = jb_Location_bis_and_non_bis.Find(x => x.Contains($"{jb_name}_Location"));
                if (jb_location_name != null)
                {
                    jb_Infor_Item_Prefab.gameObject.SetActive(true);
                    SetSpriteLocation(jb_location_imagePrefab, jb_location_name, locationSprites);
                }
                else
                {
                    jb_Infor_Item_Prefab.gameObject.SetActive(false);
                    jb_location_imagePrefab_hortizontal_group.SetActive(false);
                }

            }

            jb_location_bis_and_non_bis_filtered.RemoveAll(x => x.Contains($"{jb_name}_Location"));
            jb_location_bis_and_non_bis_filtered.Clear();
        }
        else if (jb_location_bis_and_non_bis_filtered.Count >= 2)
        {
            Debug.Log($"pt1: {jb_location_bis_and_non_bis_filtered[0]}");
            Debug.Log($"pt2: {jb_location_bis_and_non_bis_filtered[1]}");
            Debug.Log($"Count {jb_location_bis_and_non_bis_filtered.Count}");
            jb_Infor_Item_Prefab.gameObject.SetActive(false);
            for (int i = 0; i < jb_location_bis_and_non_bis_filtered.Count; i++)
            {
                Create_And_Set_Sprite_For_JB_Location(jb_location_bis_and_non_bis_filtered[i]);
            }

        }

    }

    private void Create_And_Set_Sprite_For_JB_Location(string spriteName)
    {
        GameObject imageObject = Instantiate(jb_Infor_Item_Prefab.gameObject, jb_location_imagePrefab_hortizontal_group.transform);
        if (imageObject != null)
        {
            imageObject.SetActive(true);
            Image new_Location_Image = imageObject.GetComponentInChildren<Image>();
            new_Location_Image.gameObject.SetActive(true);
            new_Location_Image.gameObject.name = spriteName;
            instantiatedImages.Add(imageObject);
            SetSpriteLocation(new_Location_Image, spriteName, locationSprites);
            Debug.Log($"Đã gain xong phần tử 1 {spriteName}");
        }
    }
    private void LoadConnectionSprites()
    {
        jb_name = GlobalVariable.jb_TSD_Name;
        jb_location = GlobalVariable.jb_TSD_Location;
        pendingSpriteLoad2++;
        //! Addressables khiến cho hàm LoadConnectionSprites() chạy bất đồng bộ
        Addressables.LoadAssetsAsync<Sprite>($"Grapper{grapper}_Connection_Wiring", sprite =>
        {
            connectionSprites[sprite.name] = sprite;
            Debug.Log("sprite.name: " + sprite.name);
        }).Completed += handle =>
        {
            pendingSpriteLoad2--;
            if (handle.Status == AsyncOperationStatus.Succeeded && pendingSpriteLoad2 == 0)
            {
                Debug.Log("All location Load Connection Sprites successfully.");
                ApplyConnectionSprites();
            }
            else
            {
                Debug.LogError("Failed to LoadC onnection Sprites ");
            }

        };
    }


    private void ApplyConnectionSprites()
    {
        var filteredList = connectionSprites.Keys
            .Where(key => key.StartsWith($"{jb_name}_") && key.Split('_').Length > 1 && int.TryParse(key.Split('_')[1][0].ToString(), out _))
            .OrderBy(key => int.Parse(key.Split('_')[1][0].ToString()))
            .ToList();

        if (filteredList.Count != 0 && filteredList != null)
        {
            foreach (var imageName in filteredList)
            {
                var imageObject = Instantiate(jb_connection_imagePrefab, scroll_Area_Content.transform);
                SetSprite(imageObject.GetComponent<Image>(), imageName, connectionSprites);
                imageObject.gameObject.SetActive(true);
                Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageObject.GetComponent<Image>());

            }
        }
        jb_connection_imagePrefab.gameObject.SetActive(false);
        //  Resize_Gameobject_Function.Resize_Parent_GameObject(jb_connection_imagePrefab_group.GetComponent<RectTransform>());
        //  Resize_Gameobject_Function.Resize_Parent_GameObject(scroll_Area_Content.GetComponent<RectTransform>());
    }

    private void SetSpriteLocation(Image imageComponent, string spriteName, Dictionary<string, Sprite> spriteDictionary)
    {
        if (!spriteDictionary.TryGetValue(spriteName, out var sprite))
        {
            imageComponent.gameObject.transform.parent.gameObject.SetActive(false);
            if (instantiatedImages.Count <= 1)
            {
                if (jb_location_imagePrefab_hortizontal_group.activeSelf)
                {
                    jb_location_imagePrefab_hortizontal_group.SetActive(false);
                }
            }
        }
        else
        {
            if (instantiatedImages.Count != 1)
            {
                if (!jb_location_imagePrefab_hortizontal_group.activeSelf)
                {
                    jb_location_imagePrefab_hortizontal_group.SetActive(true);
                }
                if (instantiatedImages.Count == 0)
                {
                    jb_Infor_Item_Prefab.gameObject.SetActive(true);
                    spriteDictionary.TryGetValue(spriteName, out var _sprite);
                    imageComponent.sprite = _sprite;
                    //Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
                }
                if (instantiatedImages.Count > 1)
                {
                    spriteDictionary.TryGetValue(spriteName, out var _sprite);
                    Debug.Log("spriteName khi gain: " + spriteName);
                    imageComponent.sprite = _sprite;
                    GameObject gameObject = imageComponent.gameObject.transform.parent.gameObject;
                    if (gameObject != jb_Infor_Item_Prefab)
                    {
                        gameObject.SetActive(true);
                    }
                    imageComponent.gameObject.SetActive(true);
                    //Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
                }
            }
            else
            {
                if (jb_location_bis_and_non_bis_filtered.Count >= 2)
                {
                    spriteDictionary.TryGetValue(spriteName, out var _sprite);
                    Debug.Log("spriteName khi gain: " + spriteName);
                    imageComponent.sprite = _sprite;
                    GameObject gameObject = imageComponent.gameObject.transform.parent.gameObject;
                    if (gameObject != jb_Infor_Item_Prefab)
                    {
                        gameObject.SetActive(true);
                    }
                    imageComponent.gameObject.SetActive(true);
                    //Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
                }
            }
        }

    }
    private void SetSprite(Image imageComponent, string spriteName, Dictionary<string, Sprite> spriteDictionary)
    {
        spriteDictionary.TryGetValue(spriteName, out var sprite);
        imageComponent.sprite = sprite;
        //Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
    }
    private void Clear_Instantiated_Image_Objects()
    {
        instantiatedImages.Clear();
        foreach (Transform newObject in jb_location_imagePrefab_hortizontal_group.transform)
        {
            if (newObject != jb_Infor_Item_Prefab.transform)
            {
                Destroy(newObject.gameObject);
            }
        }
        jb_location_imagePrefab_hortizontal_group.SetActive(true);
    }
    private void ClearSpritesAndObjects()
    {
        foreach (Transform child in scroll_Area_Content.transform)
        {
            if (child.gameObject != jb_connection_imagePrefab.gameObject && child.gameObject != jb_location_imagePrefab_hortizontal_group)
            {
                Destroy(child.gameObject);
            }
        }
        locationSprites.Clear();
        connectionSprites.Clear();
        jb_Location_bis_and_non_bis.Clear();
    }

}

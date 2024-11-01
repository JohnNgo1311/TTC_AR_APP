using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Update_Module_Screen : MonoBehaviour
{
    private Image module_Datasheet_Image;
    private Image adapter_Datasheet_Image;
    private Image module_Mounts_On_Terminal_Base_Image;
    private GameObject module_Screen;
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private string module_Type_Name = "1794-IE8";
    private string adapter_Type_Name = "1794-ACN15";
    private List<string> addressableKeys = new List<string>() { "Module_Specification", "Adapter_Specification", "Module_Mounts_On_Terminal_Base" };
    private int pendingSpriteLoads;

    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    private void OnEnable()
    {
        module_Type_Name = GlobalVariable.module_Type_Name;
        adapter_Type_Name = GlobalVariable.apdapter_Type_Name;
        module_Screen = transform.parent.gameObject;
        module_Datasheet_Image = module_Screen.transform.Find("General_Backgound/Scroll_Area/Content/Module_Datasheet_Image").GetComponent<Image>();
        adapter_Datasheet_Image = module_Screen.transform.Find("General_Backgound/Scroll_Area/Content/Adapter_Datasheet_Image").GetComponent<Image>();
        module_Mounts_On_Terminal_Base_Image = module_Screen.transform.Find("General_Backgound/Scroll_Area/Content/Module_Mounts_On_Terminal_Base_Image").GetComponent<Image>();
        LoadSprites();
    }

    private void OnDisable()
    {
        spriteCache.Clear();
    }

    private void LoadSprites()
    {
        pendingSpriteLoads = addressableKeys.Count;

        foreach (var key in addressableKeys)
        {
            if (!spriteCache.ContainsKey(key)) // Tránh tải lại sprite đã tải
            {
                Addressables.LoadAssetsAsync<Sprite>(key, sprite =>
                {
                    spriteCache[sprite.name] = sprite;
                }).Completed += OnSpriteLoadComplete;
            }
            else
            {
                pendingSpriteLoads--; // Giảm số lượng sprite cần tải nếu đã có trong cache
            }
        }

        // Áp dụng sprite ngay lập tức nếu đã có trong cache
        if (pendingSpriteLoads == 0)
        {
            ApplySprites();
        }
    }

    private void OnSpriteLoadComplete(AsyncOperationHandle<IList<Sprite>> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to load sprites: " + handle.OperationException);
            return;
        }

        pendingSpriteLoads--;
        if (pendingSpriteLoads == 0)
        {
            ApplySprites();
        }
    }

    private void ApplySprites()
    {
        ApplySpriteToImage(module_Type_Name, module_Datasheet_Image, "Module Datasheet");
        ApplySpriteToImage(adapter_Type_Name, adapter_Datasheet_Image, "Adapter Datasheet");
        ApplySpriteToImage("Module_Mounts_on_Terminal_Base", module_Mounts_On_Terminal_Base_Image, "Module Mounts on Terminal Base");
    }

    private void ApplySpriteToImage(string spriteName, Image targetImage, string logName)
    {
        if (spriteCache.TryGetValue(spriteName, out var sprite))
        {
            targetImage.sprite = sprite;
            Debug.Log($"{logName} Sprite: {sprite.name}");
        }
        else
        {
            Debug.LogWarning($"{logName} Sprite not found for {spriteName}");
        }
    }
}

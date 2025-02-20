
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Update_Field_Device_Connection_Screen : MonoBehaviour
{
    [SerializeField] private GameObject field_Device_Connection_Panel_Prefab;
    [SerializeField] private Canvas canvas_Parent;
    [SerializeField] private TMP_Text MCC_Title;
    [SerializeField] private Image connection_ImagePrefab;
    [SerializeField] private GameObject scroll_Area_Content;
    [SerializeField] private ScrollRect scroll_Area;
    private string fieldDevice_Name;
    private string cabinet_Type;
    private List<GameObject> instantiatedImages = new List<GameObject>();

    private void OnEnable()
    {
        canvas_Parent ??= GetComponentInParent<Canvas>();
        scroll_Area.verticalNormalizedPosition = 1f;

        InitializeReferences();
        UpdateTitle();

        // Chạy hai hàm song song bằng coroutines
        StartCoroutine(RunApplyFunctions());
    }

    private void OnDisable()
    {
        ClearInstantiatedImageObjects();
    }

    private void InitializeReferences()
    {
        MCC_Title ??= field_Device_Connection_Panel_Prefab.transform.Find("Title").GetComponent<TMP_Text>();
        scroll_Area ??= field_Device_Connection_Panel_Prefab.transform.Find("Scroll_Area").GetComponent<ScrollRect>();
        scroll_Area_Content ??= field_Device_Connection_Panel_Prefab.transform.Find("Scroll_Area/Content").gameObject;
        connection_ImagePrefab ??= scroll_Area_Content.transform.Find("Field_Device_Connection_Image_Prefab").gameObject.GetComponent<Image>();
    }
    private void UpdateTitle()
    {
        if (!string.IsNullOrEmpty(GlobalVariable.temp_MCCInformationModel.Type) && !string.IsNullOrEmpty(GlobalVariable.temp_FieldDeviceInformationModel.Name))
        {
            fieldDevice_Name = GlobalVariable.temp_FieldDeviceInformationModel.Name;
            cabinet_Type = GlobalVariable.temp_MCCInformationModel.Type;
            if (cabinet_Type.ToLower() == "biến tần")
            {
                MCC_Title.text = $"Sơ đồ đấu dây tủ {cabinet_Type.ToLower()} {fieldDevice_Name}";
            }
            else
            {
                MCC_Title.text = $"Sơ đồ đấu dây tủ động lực + {fieldDevice_Name}";
            }
        }
    }

    private IEnumerator RunApplyFunctions()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        Show_Toast.Instance.ShowToast("loading", "Đang tải hình ảnh...");
        yield return ApplyConnectionSprites();
        yield return Show_Toast.Instance.Set_Instance_Status_False();
    }



    private IEnumerator ApplyConnectionSprites()
    {
        if (GlobalVariable.temp_ListFieldDeviceConnectionImages != null)
        {
            var list_Texture = GlobalVariable.temp_ListFieldDeviceConnectionImages;
            if (list_Texture.Count > 0)
            {
                if (list_Texture.Count == 1)
                {
                    connection_ImagePrefab.sprite = Texture_To_Sprite.ConvertTextureToSprite(list_Texture[0]);
                    connection_ImagePrefab.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("list_Texture.Count: " + list_Texture.Count);
                    foreach (var texture in list_Texture)
                    {
                        var imageObject = Instantiate(connection_ImagePrefab, scroll_Area_Content.transform);
                        instantiatedImages.Add(imageObject.gameObject);
                        imageObject.sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
                        imageObject.gameObject.SetActive(true);
                        yield return StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageObject));

                    }
                    connection_ImagePrefab.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("No connection images found");
        }
    }

    private void ClearInstantiatedImageObjects()
    {
        foreach (var imageObject in instantiatedImages)
        {
            Destroy(imageObject);
        }
        instantiatedImages.Clear();
    }
}

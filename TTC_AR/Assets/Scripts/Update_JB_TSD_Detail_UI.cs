using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using EasyUI.Progress;

public class Update_JB_TSD_Detail_UI : MonoBehaviour
{
    [SerializeField] private GameObject jB_TSD_Detail_Panel_Prefab;
    [SerializeField] private Canvas canvas_Parent;
    [SerializeField] private TMP_Text jB_TSD_Title;
    [SerializeField] private TMP_Text jb_location_value;
    [SerializeField] private TMP_Text jb_location_title;
    [SerializeField] private Image jb_connection_imagePrefab;

    [SerializeField] private Image jb_location_imagePrefab;
    [SerializeField] private GameObject jb_Infor_Item_Prefab;
    [SerializeField] private GameObject scroll_Area_Content;
    [SerializeField] private ScrollRect scroll_Area;

    private List<GameObject> instantiatedImages = new List<GameObject>();
    private string jb_name;

    private void OnEnable()
    {
        canvas_Parent ??= GetComponentInParent<Canvas>();
        jB_TSD_Detail_Panel_Prefab ??= canvas_Parent.transform.Find("Detail_JB_TSD").gameObject;
        scroll_Area ??= jB_TSD_Detail_Panel_Prefab.transform.Find("Scroll_Area").GetComponent<ScrollRect>();
        scroll_Area.verticalNormalizedPosition = 1f;

        InitializeReferences();
        UpdateTitle();

        // Chạy hai hàm song song bằng coroutines
        StartCoroutine(RunApplyFunctions());
        // Debug.Log("Update_JB_TSD_Detail_UI is enabled");
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        {
            jb_Infor_Item_Prefab.GetComponent<ContentSizeFitter>().enabled = false;
            scroll_Area_Content.GetComponent<ContentSizeFitter>().enabled = false;
        }
        ClearInstantiatedImageObjects();
    }

    private void InitializeReferences()
    {
        jB_TSD_Title = jB_TSD_Detail_Panel_Prefab.transform.Find("Horizontal_JB_TSD_Title/JB_TSD_Title").GetComponent<TMP_Text>();
        scroll_Area_Content = jB_TSD_Detail_Panel_Prefab.transform.Find("Scroll_Area/Content").gameObject;
        jb_Infor_Item_Prefab = scroll_Area_Content.transform.Find("JB_Infor").gameObject;
        jb_location_title = jb_Infor_Item_Prefab.transform.Find("Text_JB_location_group/Text_Jb_Location_Title").GetComponent<TMP_Text>();
        jb_location_value = jb_Infor_Item_Prefab.transform.Find("Text_JB_location_group/Text_Jb_Location_Value").GetComponent<TMP_Text>();
        jb_location_imagePrefab = jb_Infor_Item_Prefab.transform.Find("JB_location_imagePrefab").GetComponent<Image>();
        jb_connection_imagePrefab = scroll_Area_Content.transform.Find("JB_TSD_connection_imagePrefab").GetComponent<Image>();
    }

    private void UpdateTitle()
    {
        if (!string.IsNullOrEmpty(StaticVariable.jb_TSD_Name))
        {
            jb_name = jB_TSD_Title.text = StaticVariable.jb_TSD_Name;
            jb_location_value.text = StaticVariable.jb_TSD_Location;
        }
    }

    private IEnumerator RunApplyFunctions()
    {
        // Chờ cả hai coroutines kết thúc
        // Show_Toast.Instance.Set_Instance_Status_True();
        // Show_Toast.Instance.ShowToast("loading", "Đang tải hình ảnh...");
        ShowProgressBar("Đang tải hình ảnh...", "Vui lòng chờ...");

        yield return new WaitUntil(() => StaticVariable.ready_To_Update_Images_UI);
        StaticVariable.ready_To_Update_Images_UI = false;

        // Chạy song song ApplyLocationSprite và ApplyConnectionSprites
        yield return ApplyConnectionSprites();
        yield return new WaitForSeconds(2.2f);
        yield return ApplyLocationSprite();
        if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        {
            jb_Infor_Item_Prefab.GetComponent<ContentSizeFitter>().gameObject.SetActive(true);
            jb_Infor_Item_Prefab.GetComponent<ContentSizeFitter>().enabled = true;
            yield return null;
            scroll_Area_Content.GetComponent<ContentSizeFitter>().gameObject.SetActive(true);
            scroll_Area_Content.GetComponent<ContentSizeFitter>().enabled = true;
        }
        // yield return Show_Toast.Instance.Set_Instance_Status_False();
        HideProgressBar();
    }

    private IEnumerator ApplyLocationSprite()
    {
        if (StaticVariable.temp_listJBLocationImageFromModule != null &&
            StaticVariable.temp_listJBLocationImageFromModule.TryGetValue(jb_name, out var texture))
        {
            if (texture != null)
            {
                if (texture.width == 2 && texture.height == 2)
                {
                    jb_location_title.gameObject.SetActive(false);
                    jb_location_value.gameObject.SetActive(false);
                    jb_location_imagePrefab.gameObject.SetActive(false);
                    jb_Infor_Item_Prefab.SetActive(false);
                }
                else
                {
                    if (!jb_Infor_Item_Prefab.gameObject.activeSelf) jb_Infor_Item_Prefab.SetActive(true);
                    if (!jb_location_title.gameObject.activeSelf) jb_location_title.gameObject.SetActive(true);
                    if (!jb_location_value.gameObject.activeSelf) jb_location_value.gameObject.SetActive(true);
                    if (!jb_location_imagePrefab.gameObject.activeSelf) jb_location_imagePrefab.gameObject.SetActive(true);
                    jb_location_imagePrefab.sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
                    yield return Resize_Gameobject_Function.Set_NativeSize_For_GameObject(jb_location_imagePrefab);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                Debug.Log("texture is null");
            }
        }
    }

    private IEnumerator ApplyConnectionSprites()
    {
        if (StaticVariable.temp_listJBConnectionImageFromModule != null &&
            StaticVariable.temp_listJBConnectionImageFromModule.TryGetValue(jb_name, out var list_Texture))
        {
            if (list_Texture.Any())
            {
                if (list_Texture.Count == 1)
                {
                    jb_connection_imagePrefab.sprite = Texture_To_Sprite.ConvertTextureToSprite(list_Texture[0]);
                    jb_connection_imagePrefab.gameObject.SetActive(true);
                    yield return Resize_Gameobject_Function.Set_NativeSize_For_GameObject(jb_connection_imagePrefab);
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    // Debug.Log("list_Texture.Count: " + list_Texture.Count);
                    foreach (var texture in list_Texture)
                    {
                        var imageObject = Instantiate(jb_connection_imagePrefab, scroll_Area_Content.transform);
                        instantiatedImages.Add(imageObject.gameObject);
                        imageObject.sprite = Texture_To_Sprite.ConvertTextureToSprite(texture);
                        imageObject.gameObject.SetActive(true);

                        yield return Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageObject);
                        yield return new WaitForSeconds(0.2f);
                    }
                    jb_connection_imagePrefab.gameObject.SetActive(false);
                }
            }
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

    private void ShowProgressBar(string title, string details)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        Progress.SetDetailsText(details);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }
}
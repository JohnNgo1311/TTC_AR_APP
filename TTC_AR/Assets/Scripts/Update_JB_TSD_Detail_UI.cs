using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using EasyUI.Progress;
using System.Threading.Tasks;
using OpenCVForUnity.VideoModule;

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

    private void Awake()
    {
        canvas_Parent ??= GetComponentInParent<Canvas>();
        jB_TSD_Detail_Panel_Prefab ??= canvas_Parent.transform.Find("Detail_JB_TSD").gameObject;
        scroll_Area ??= jB_TSD_Detail_Panel_Prefab.transform.Find("Scroll_Area").GetComponent<ScrollRect>();
        InitializeReferences();
    }

    private void OnEnable()
    {
        UpdateTitle();
        // Chạy hai hàm song song bằng coroutines
        RunApplyFunctions();
        scroll_Area.verticalNormalizedPosition = 1f;

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

    private async void RunApplyFunctions()
    {
        ShowProgressBar("Đang tải hình ảnh...", "Vui lòng chờ...");

        //Hàm khác
        var tasks = new List<Task>
        {
            LoadImage.Instance.LoadImageFromUrlAsync(StaticVariable.temp_JBInformationModel.OutdoorImage.url, jb_location_imagePrefab)
        };

        jb_connection_imagePrefab.gameObject.SetActive(true);
        foreach (var image in StaticVariable.temp_JBInformationModel.ListConnectionImages)
        {
            // Debug.Log("image.url: " + image.url);
            var new_jb_connection_image = Instantiate(jb_connection_imagePrefab, scroll_Area_Content.transform);
            instantiatedImages.Add(new_jb_connection_image.gameObject);
            tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(image.url, new_jb_connection_image));
        }

        if (!StaticVariable.temp_DeviceInformationModel.TryGetValue(StaticVariable.device_Code, out var device))
        {
            Debug.Log("device is null");
        }
        else
        {
            // jb_connection_imagePrefab.gameObject.SetActive(true);
            foreach (var image in device.AdditionalConnectionImages)
            {
                var new_Additional_Image = Instantiate(jb_connection_imagePrefab, scroll_Area_Content.transform);
                instantiatedImages.Add(new_Additional_Image.gameObject);
                tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(image.url, new_Additional_Image));
            }
        }
        jb_connection_imagePrefab.gameObject.SetActive(false);

        await Task.WhenAll(tasks);

        //Resize hình ảnh
        StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(jb_location_imagePrefab));
        foreach (var image in instantiatedImages)
        {
            StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(image.GetComponent<Image>()));
        }

        // if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        // {
        //     jb_Infor_Item_Prefab.GetComponent<ContentSizeFitter>().gameObject.SetActive(true);
        //     jb_Infor_Item_Prefab.GetComponent<ContentSizeFitter>().enabled = true;
        //     // yield return null;
        //     scroll_Area_Content.GetComponent<ContentSizeFitter>().gameObject.SetActive(true);
        //     scroll_Area_Content.GetComponent<ContentSizeFitter>().enabled = true;
        // }
        // yield return Show_Toast.Instance.Set_Instance_Status_False();
        HideProgressBar();
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
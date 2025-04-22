
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using EasyUI.Progress;
using System.Threading.Tasks;

public class Update_Field_Device_Connection_Screen : MonoBehaviour
{
    [SerializeField] private GameObject field_Device_Connection_Panel_Prefab;
    [SerializeField] private Canvas canvas_Parent;
    [SerializeField] private TMP_Text MCC_Title;
    [SerializeField] private Image connection_ImagePrefab;
    [SerializeField] private GameObject scroll_Area_Content;
    [SerializeField] private ScrollRect scroll_Area;
    [SerializeField] private GameObject gameObject_Empty;
    private List<GameObject> instantiatedImages = new List<GameObject>();

    private void OnEnable()
    {
        canvas_Parent ??= GetComponentInParent<Canvas>();
        scroll_Area.verticalNormalizedPosition = 1f;

        InitializeReferences();
        UpdateTitle();

        RunApplyFunctions();
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
        MCC_Title.text = "Sơ đồ đấu dây tủ biến tần " + StaticVariable.temp_FieldDeviceInformationModel.Name;
    }

    private async void RunApplyFunctions()
    {
        ShowProgressBar("Đang tải hình ảnh...", "Vui lòng chờ...");

        var tasks = new List<Task>();

        connection_ImagePrefab.gameObject.SetActive(true);
        foreach (var image in StaticVariable.temp_FieldDeviceInformationModel.ListConnectionImages)
        {
            // Debug.Log("image.url: " + image.url);
            var new_FieldDevice_connection_image = Instantiate(connection_ImagePrefab, scroll_Area_Content.transform);
            instantiatedImages.Add(new_FieldDevice_connection_image.gameObject);
            tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(image.Name, new_FieldDevice_connection_image));
        }

        connection_ImagePrefab.gameObject.SetActive(false);
        gameObject_Empty.transform.SetAsLastSibling();

        await Task.WhenAll(tasks);

        //Resize hình ảnh
        foreach (var image in instantiatedImages)
        {
            StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(image.GetComponent<Image>()));
        }
        scroll_Area.verticalNormalizedPosition = 1f;

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

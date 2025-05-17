using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using EasyUI.Progress;
using System.Threading.Tasks;
using System.Linq;

public class Update_JB_TSD_Detail_UI : MonoBehaviour, IJBView
{
    [SerializeField] private GameObject jB_TSD_Detail_Panel;
    [SerializeField] private TMP_Text jbNameValue;
    [SerializeField] private ScrollRect scroll_Area;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject jb_Infor_Prefab;
    [SerializeField] private TMP_Text jb_location_value;
    [SerializeField] private Image jb_location_imagePrefab;
    [SerializeField] private Image jb_connection_imagePrefab;
    [SerializeField] private GameObject emptySpace;
    private List<GameObject> instantiatedImages = new List<GameObject>();
    private JBPresenter _presenter;
    private JBInformationModel _jbInformationModel;

    void Awake()
    {
        _presenter = new JBPresenter(this, ManagerLocator.Instance.JBManager._IJBService);
    }

    private void OnEnable()
    {
        Initialize();
        _presenter.LoadDetailById(GlobalVariable.JBId);

    }
    public void DisplayDetail(JBInformationModel model)
    {
        if (model != null)
        {
            _jbInformationModel = model;
            GlobalVariable.temp_JBInformationModel = _jbInformationModel;
            GlobalVariable.JBId = _jbInformationModel.Id;
        }
        UpdateJBTextInfor(_jbInformationModel);
        UpdateUIImages(_jbInformationModel);
    }

    private void OnDisable()
    {
        // if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        // {
        //     jb_Infor_Prefab.GetComponent<ContentSizeFitter>().enabled = false;
        //     content.GetComponent<ContentSizeFitter>().enabled = false;
        // }
        ClearInstantiatedImageObjects();
    }

    private void Initialize()
    {
        jbNameValue ??= jB_TSD_Detail_Panel.transform.Find("Horizontal_JB_TSD_jbNameValue/JB_TSD_jbNameValue").GetComponent<TMP_Text>();
        content ??= jB_TSD_Detail_Panel.transform.Find("Scroll_Area/Content").gameObject;
        jb_Infor_Prefab ??= content.transform.Find("JB_Infor").gameObject;
        jb_location_value ??= jb_Infor_Prefab.transform.Find("Text_JB_location_group/Location_Value").GetComponent<TMP_Text>();
        jb_location_imagePrefab ??= jb_Infor_Prefab.transform.Find("JB_location_imagePrefab").GetComponent<Image>();
        jb_connection_imagePrefab ??= content.transform.Find("JB_connection_imagePrefab").GetComponent<Image>();
    }

    private void UpdateJBTextInfor(JBInformationModel model)
    {
        if (!string.IsNullOrEmpty(model.Name))
        {
            jbNameValue.text = model.Name;
        }
        else
        {
            jbNameValue.text = "Chưa cập nhật";
            jbNameValue.color = Color.red;
            jbNameValue.fontStyle = FontStyles.Bold;
        }
        if (!string.IsNullOrEmpty(model.Location))
        {
            jb_location_value.text = model.Location;

        }
        else
        {
            jb_location_value.text = "Được ghi chú trong sơ đồ";
            jb_location_value.color = Color.red;
            jb_location_value.fontStyle = FontStyles.Bold;
        }

    }

    private async void UpdateUIImages(JBInformationModel model)
    {
        ShowProgressBar("Đang tải hình ảnh...");
        try
        {
            var tasks = new List<Task>();
            jb_location_imagePrefab.gameObject.SetActive(true);
            jb_connection_imagePrefab.gameObject.SetActive(true);
            if (model.OutdoorImage == null)
            {
                jb_location_imagePrefab.gameObject.SetActive(false);
            }
            else
            {
                tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(model.OutdoorImage.Name, jb_location_imagePrefab));
            }
            if (model.ListConnectionImages.Any())
            {
                foreach (var imageModel in model.ListConnectionImages)
                {
                    Debug.Log("image.Name: " + imageModel.Name);

                    var new_Connection_Image = Instantiate(jb_connection_imagePrefab, content.transform);

                    instantiatedImages.Add(new_Connection_Image.gameObject);

                    tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(imageModel.Name, new_Connection_Image));

                }
            }

            // if (!GlobalVariable.temp_DeviceInformationModel.TryGetValue(GlobalVariable.device_Code, out var device))
            // {
            //     // Debug.Log("device is null");
            // }
            // else
            // {
            //     // jb_connection_imagePrefab.gameObject.SetActive(true);
            //     foreach (var image in device.AdditionalConnectionImages)
            //     {
            //         var new_Additional_Image = Instantiate(jb_connection_imagePrefab, content.transform);
            //         instantiatedImages.Add(new_Additional_Image.gameObject);
            //         tasks.Add(LoadImage.Instance.LoadImageFromUrlAsync(image.Name, new_Additional_Image));
            //     }
            // }

            await Task.WhenAll(tasks);

            jb_connection_imagePrefab.gameObject.SetActive(false);


            emptySpace.transform.SetAsLastSibling();

            //Resize hình ảnh
            StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(jb_location_imagePrefab));
            foreach (var image in instantiatedImages)
            {
                StartCoroutine(Resize_GameObject_Function.Set_NativeSize_For_GameObject(image.GetComponent<Image>()));
            }

            scroll_Area.verticalNormalizedPosition = 1f;
        }

        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading images: {ex.Message}");
        }
        finally
        {
            HideProgressBar();
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

    private void ShowProgressBar(string jbNameValue)
    {
        Progress.Show(jbNameValue, ProgressColor.Blue, true);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }

    public void ShowLoading(string jbNameValue = "Loading...")
    {
    }

    public void HideLoading()
    {
    }

    public void ShowSuccess()
    {
        if (GlobalVariable.APIRequestType.Contains("GET_JB"))
        {
            Show_Toast.Instance.ShowToast("success", "Tải dữ liệu thành công");
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }
    public void ShowError(string message)
    {
        if (GlobalVariable.APIRequestType.Contains("GET_JB"))
        {
            Show_Toast.Instance.ShowToast("failure", "Tải dữ liệu thất bại");
        }
        StartCoroutine(Show_Toast.Instance.Set_Instance_Status_False(1f));
    }

    public void DisplayList(List<JBInformationModel> models)
    {
    }

    public void DisplayCreateResult(bool success)
    {

    }
    public void DisplayUpdateResult(bool success)
    {

    }

    public void DisplayDeleteResult(bool success)
    {

    }
}
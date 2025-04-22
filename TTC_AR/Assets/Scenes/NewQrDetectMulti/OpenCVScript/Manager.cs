using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OpenCVForUnity.UnityUtils.Helper;


public class Manager : MonoBehaviour
{
    // int i = 0;
    // Color[] colors = { Color.red, Color.blue, Color.green };
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text title;
    [SerializeField] private ARHelperMulti aRHelper;
    [SerializeField] private Button backBtn;
    [SerializeField] private EventPublisher eventPublisher;

    public bool isCanvasOpen = false;
    public bool isPaused = false;
    private string activescene;

    // Start is called before the first frame update
    void Start()
    {
        activescene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        backBtn.onClick.AddListener(CloseCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        DetectClickedObject();
        isCanvasOpen = canvas.gameObject.activeSelf;
    }

    public void DetectClickedObject()
    {
        Ray ray = new Ray();

        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position
            ray = Camera.allCameras[1].ScreenPointToRay(Input.mousePosition);
        }
        else if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Create a ray from the camera to the touch position
                ray = Camera.main.ScreenPointToRay(touch.position);
            }
        }
        else
        {
            return;
        }

        // Perform the raycast
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask: LayerMask.GetMask("Game Object", "UI", "Default")))
        {
            // Get the GameObject that was hit
            GameObject clickedObject = hit.collider.gameObject;
            Vector3 hitPosition = hit.point;

            // Debug.LogWarning("Clicked on: " + clickedObject.name);
            float tolerance = 0.03f;
            var qrMarker = aRHelper.markers.Values.FirstOrDefault(marker => Vector3.Distance(marker.qrPosition, hitPosition) <= tolerance);
            if (qrMarker != null)
            {
                var key = aRHelper.markers.FirstOrDefault(pair => pair.Value == qrMarker).Key;
                if (key != null)
                {
                    // Debug.LogWarning("Found QRMarker with key: " + key);
                    if (activescene == "NewQRCodeDetectorMulti")
                    {
                        title.text = "Module " + key;
                        eventPublisher.TriggerEvent_ButtonClicked();
                        OpenCanvas();
                    }
                    else
                    {
                        title.text = "Tủ " + key;
                        eventPublisher.TriggerEvent_ButtonClicked();
                        OpenCanvas();
                        // if (StaticVariable.temp_MccInformationModel != null)
                        // {
                        //     OpenCanvas();
                        // }
                        // else
                        // {
                        //     Debug.LogError("No MCC information found for the selected cabinet.");
                        //     StartCoroutine(ShowToastError.Instance.ShowToast("Vui lòng chọn lại khu vực phù hợp. Thông tin bạn cần tìm không có ở khu vực đã chọn"));
                        // }
                    }
                }
            }
            else
            {
                Debug.LogWarning("No QRMarker found at the hit position.");
            }
        }
    }

    private void OpenCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    public void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}

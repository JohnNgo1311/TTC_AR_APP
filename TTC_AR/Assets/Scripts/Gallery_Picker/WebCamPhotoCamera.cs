using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class WebCamPhotoCamera : MonoBehaviour
{
    public static WebCamPhotoCamera Instance { get; private set; }

    [SerializeField] private WebCamTexture webCamTexture;
    [SerializeField] private Texture2D capturedPhoto;

    [Header("Canvas")]
    public GameObject ConfirmImageCanvas;



    [Header("Take Photo Module")]
    public GameObject Take_Photo_Module;
    public RawImage CameraScreen;
    public Button TakeButton;
    public Button cancel_Take_Photo_Button;
    public AspectRatioFitter aspectRatioFitterForCaptureScreen;



    [Header("Preview Photo Module")]
    public GameObject Preview_Photo_Module;
    public RawImage preview_Image;
    public Button acceptButton;
    public Button reTakeButton;
    public AspectRatioFitter aspectRatioFitterForPreviewImage;

    private PickPhotoFromCamera pickPhotoFromCamera;
    // public string imagePath;
    // public string fileName = "captured_image.png";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void OnEnable()
    {
        // Use lambda expressions to reduce overhead and improve performance
        TakeButton.onClick.AddListener(() => TakePhotoOnClick());
        acceptButton.onClick.AddListener(() => AcceptPhoto());
        reTakeButton.onClick.AddListener(() => ReTakePhoto());
        cancel_Take_Photo_Button.onClick.AddListener(() => Stop_Camera_To_Take_Photo());
        // Initialize UI elements to inactive state
        SetDeActiveUIElements(false);
    }
    void OnDisable()
    {
        // Remove listeners to prevent memory leaks
        TakeButton.onClick.RemoveAllListeners();
        acceptButton.onClick.RemoveAllListeners();
        reTakeButton.onClick.RemoveAllListeners();
        cancel_Take_Photo_Button.onClick.RemoveAllListeners();
    }
    void Start()
    {

    }

    public void SetDeActiveUIElements(bool isActive)
    {
        Take_Photo_Module.SetActive(isActive);
        Preview_Photo_Module.SetActive(isActive);
    }


    //! Bật camera để chụp ảnh
    public void StartCameraToTakePhoto(PickPhotoFromCamera pickPhotoFromCamera)
    {
        Take_Photo_Module.SetActive(true);

        Preview_Photo_Module.SetActive(false);
        this.pickPhotoFromCamera = pickPhotoFromCamera;

        if (WebCamTexture.devices.Length > 0)
        {
            WebCamDevice selectedDevice = WebCamTexture.devices[0];
            if (Application.platform == RuntimePlatform.Android)
            {
                foreach (WebCamDevice device in WebCamTexture.devices)
                {
                    if (!device.isFrontFacing)
                    {
                        selectedDevice = device; //! chọn camera sau
                        break;
                    }
                }
            }
            webCamTexture = new WebCamTexture(selectedDevice.name); //! Khởi tạo WebCamTexture với camera đã chọn

            CameraScreen.texture = webCamTexture; //! Gán texture cho RawImage

            webCamTexture.Play();

            StartCoroutine(AdjustAspectRatio());
        }
        else
        {
            Debug.LogError("Không tìm thấy camera nào!");
        }
    }
    //! Dừng camera sau khi chụp ảnh
    public void Stop_Camera_To_Take_Photo()
    {
        SetDeActiveUIElements(false);
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            webCamTexture = null;
        }
        this.pickPhotoFromCamera = null;
    }


    //! Chỉnh sửa tỉ lệ khung hình của camera
    private IEnumerator AdjustAspectRatio()
    {
        yield return new WaitUntil(() => webCamTexture.width > 100);

        float videoRatio = (float)webCamTexture.width / webCamTexture.height;
        aspectRatioFitterForCaptureScreen.aspectRatio = videoRatio;
        aspectRatioFitterForPreviewImage.aspectRatio = videoRatio;
    }

    //! Chụp ảnh
    public void TakePhotoOnClick()
    {
        StartCoroutine(TakePhoto());

        Take_Photo_Module.SetActive(false);

        // CameraScreen.gameObject.SetActive(false);
        // TakeButton.gameObject.SetActive(false);
        // cancel_Take_Photo_Button.gameObject.SetActive(false);

        Preview_Photo_Module.SetActive(true);


    }

    private IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();
        //? Tạo Texture2D mới với kích thước của camera
        capturedPhoto = new Texture2D(webCamTexture.width, webCamTexture.height);
        //? Lấy ảnh từ camera và gán vào Texture2D
        capturedPhoto.SetPixels(webCamTexture.GetPixels());
        //? Lưu ảnh vào bộ nhớ
        capturedPhoto.Apply();
        yield return PreviewPhotoCoroutine(capturedPhoto, preview_Image);
    }



    public void AcceptPhoto() //! => Gán Image Sau khi đã review vào confirmImage
    {
        if (capturedPhoto != null)
        {
            if (pickPhotoFromCamera != null)
            {
                ConfirmImageCanvas.SetActive(true);
                pickPhotoFromCamera.UpdateConfirmImage(capturedPhoto);
            }
        }
        Stop_Camera_To_Take_Photo();
    }


    //! Hiển thị ảnh đã chụp => gán vào Preview_Image
    private IEnumerator PreviewPhotoCoroutine(Texture2D photo, RawImage previewImage)
    {
        yield return new WaitForEndOfFrame();
        previewImage.texture = photo;
        previewImage.gameObject.SetActive(true);
    }

    public void ReTakePhoto()
    {
        Take_Photo_Module.SetActive(true);

        // CameraScreen.gameObject.SetActive(true);
        // cancel_Take_Photo_Button.gameObject.SetActive(true);
        // TakeButton.gameObject.SetActive(true);

        Preview_Photo_Module.SetActive(false);
        // preview_Image.gameObject.SetActive(false);
        // reTakeButton.gameObject.SetActive(false);
        // acceptButton.gameObject.SetActive(false);
        webCamTexture.Play();
    }
}

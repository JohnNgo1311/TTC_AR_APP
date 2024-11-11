using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class WebCamPhotoCamera : MonoBehaviour
{
    public static WebCamPhotoCamera Instance { get; private set; }

    private WebCamTexture webCamTexture;
    public GameObject Take_Photo_Module;
    public RawImage Camera_Screen_For_Take_Photo;
    public RawImage preview_Image;
    public Button take_Photo_Button;
    public Button save_Photo_Button;
    public Button retake_Photo_Button;
    public Button cancel_Take_Photo_Button;

    private Update_Photo_JB currentUpdatePhotoJB; // Đối tượng nhận ảnh hiện tại

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

    void Start()
    {
        SetActiveUIElements(false);
        if (take_Photo_Button != null && save_Photo_Button != null && retake_Photo_Button != null && cancel_Take_Photo_Button != null)
        {
            take_Photo_Button.onClick.AddListener(Take_Photo_Button_OnClick);
            save_Photo_Button.onClick.AddListener(SavePhoto);
            retake_Photo_Button.onClick.AddListener(ReTakePhoto);
            cancel_Take_Photo_Button.onClick.AddListener(Stop_Camera_To_Take_Photo);
        }
        else
        {
            Debug.LogError("Some buttons are missing!");
        }
    }

    private void SetActiveUIElements(bool isActive)
    {
        Camera_Screen_For_Take_Photo.gameObject.SetActive(isActive);
        preview_Image.gameObject.SetActive(isActive);
        take_Photo_Button.gameObject.SetActive(isActive);
        save_Photo_Button.gameObject.SetActive(isActive);
        retake_Photo_Button.gameObject.SetActive(isActive);
        cancel_Take_Photo_Button.gameObject.SetActive(isActive);
        Take_Photo_Module.SetActive(isActive);
    }

    public void Start_Camera_To_Take_Photo(Update_Photo_JB updatePhotoJB)
    {
        Take_Photo_Module.SetActive(true);
        cancel_Take_Photo_Button.gameObject.SetActive(true);
        Camera_Screen_For_Take_Photo.gameObject.SetActive(true);
        take_Photo_Button.gameObject.SetActive(true);

        currentUpdatePhotoJB = updatePhotoJB; // Lưu đối tượng nhận ảnh hiện tại

        if (WebCamTexture.devices.Length > 0)
        {
            WebCamDevice cameraDevice = WebCamTexture.devices[0];
            webCamTexture = new WebCamTexture(cameraDevice.name);
            Camera_Screen_For_Take_Photo.texture = webCamTexture;
            webCamTexture.Play();
            //Debug"Camera detected: " + cameraDevice.name);
        }
        else
        {
            Debug.LogError("No camera detected!");
        }
    }

    public void Stop_Camera_To_Take_Photo()
    {
        SetActiveUIElements(false);
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
        currentUpdatePhotoJB = null; // Xóa đối tượng nhận ảnh hiện tại
    }

    public void ReTakePhoto()
    {
        preview_Image.gameObject.SetActive(false);
        Camera_Screen_For_Take_Photo.gameObject.SetActive(true);
        take_Photo_Button.gameObject.SetActive(true);
        retake_Photo_Button.gameObject.SetActive(false);
        save_Photo_Button.gameObject.SetActive(false);
        webCamTexture.Play();
    }

    public void Take_Photo_Button_OnClick()
    {
        StartCoroutine(TakePhoto());
        Camera_Screen_For_Take_Photo.gameObject.SetActive(false);
        take_Photo_Button.gameObject.SetActive(false);
        cancel_Take_Photo_Button.gameObject.SetActive(false);
    }

    private IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        StartCoroutine(PreviewPhotoCoroutine(photo, preview_Image));
    }

    public void SavePhoto()
    {
        //Debug"Photo saved");

        Texture2D savedPhoto = preview_Image.texture as Texture2D;

        // Kiểm tra xem đối tượng hiện tại có tồn tại không và cập nhật ảnh
        currentUpdatePhotoJB?.UpdateImage(savedPhoto);

        preview_Image.gameObject.SetActive(false);
        Camera_Screen_For_Take_Photo.gameObject.SetActive(false);
        retake_Photo_Button.gameObject.SetActive(false);
        save_Photo_Button.gameObject.SetActive(false);
        Take_Photo_Module.SetActive(false);

        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
    }

    private IEnumerator PreviewPhotoCoroutine(Texture2D photo, RawImage previewImage)
    {
        yield return new WaitForEndOfFrame();
        previewImage.texture = photo;
        preview_Image.gameObject.SetActive(true);
        save_Photo_Button.gameObject.SetActive(true);
        retake_Photo_Button.gameObject.SetActive(true);
    }
}

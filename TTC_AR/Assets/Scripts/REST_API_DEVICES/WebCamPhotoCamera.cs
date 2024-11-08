using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

public class WebCamPhotoCamera : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    public GameObject Take_Photo_Module;
    public RawImage Camera_Screen_For_Take_Photo;
    public RawImage preview_Image;
    public Button take_Photo_Button;
    public Button save_Photo_Button;
    public Button retake_Photo_Button;
    public Button cancel_Take_Photo_Button;


    void Start()
    {
        SetActiveUIElements(false);
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
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {

        }
        else
        {
        };
    }
    public void Start_Camera_To_Take_Photo()
    {
        Take_Photo_Module.SetActive(true);
        cancel_Take_Photo_Button.gameObject.SetActive(true);
        Camera_Screen_For_Take_Photo.gameObject.SetActive(true);
        take_Photo_Button.gameObject.SetActive(true);

        if (WebCamTexture.devices.Length > 0)
        {
            WebCamDevice cameraDevice = WebCamTexture.devices[0];
            webCamTexture = new WebCamTexture(cameraDevice.name);
            Camera_Screen_For_Take_Photo.texture = webCamTexture;
            //Camera_Screen_For_Take_Photo.material.mainTexture = webCamTexture;
            webCamTexture.Play();
            Debug.Log("Camera detected: " + cameraDevice.name);
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
        Debug.Log("Photo saved");
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

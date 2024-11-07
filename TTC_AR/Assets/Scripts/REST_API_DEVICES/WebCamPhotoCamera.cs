using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    //public TMP_InputField photoNameInputField;
    public Image preview_Image;
    void Start()
    {
    }

    public void StartCamera()
    {
        WebCamDevice webCamDevice = new WebCamDevice();
        /*WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
            webCamDevice = devices[i];
        }*/
        webCamTexture = new WebCamTexture(webCamDevice.name);
        GetComponent<Renderer>().material.mainTexture = webCamTexture; // Add Mesh Renderer to the GameObject to which this script is attached
        webCamTexture.Play();
    }

    public void StopCamera()
    {
        webCamTexture.Stop();
    }

    public void Take_Photo_Button_OnClick()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        StartCoroutine(PreviewPhotoCoroutine(photo, preview_Image));
    }
    public void savePhoto(Texture2D photo)
    {
        // Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        // Save the photo with a specific name
        //  string filePath = Path.Combine(Application.persistentDataPath, photoNameInputField.text + ".png");
        //   File.WriteAllBytes(filePath, bytes);
        Debug.Log("Photo saved");
    }
    IEnumerator PreviewPhotoCoroutine(Texture2D photo_after_taken, Image previewImage)
    {
        yield return new WaitForEndOfFrame();
        previewImage.sprite = Sprite.Create(photo_after_taken, new Rect(0, 0, photo_after_taken.width, photo_after_taken.height), new Vector2(0.5f, 0.5f));
    }

}

using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{

    public NativeGallery.Permission permission; // Permission to access Camera Roll
    public Image image;
    public void SavePhotoToCameraRoll(Texture2D MyTexture, string AlbumName, string filename)
    {
        NativeGallery.SaveImageToGallery(MyTexture, AlbumName, filename, (callback, path) =>
        {
            if (callback == false)
            {
                Debug.Log("Failed to save !");
            }
            else
            {
                Debug.Log("Photo is saved to Camera Roll on phone device.");

                // Function_onSaved_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
            }

        });

    }

    public void PickPhotoCameraRoll()
    {

        if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log("Permission Granted");
            NativeGallery.GetImageFromGallery((path) =>
            {
                if (path == null)
                {
                    Debug.Log("Pick Photo : Canceled");
                }
                else
                {
                    Debug.Log("Pick Photo : Success");
                    Variables.ActiveScene.Set("Picked File", path);
                    image.sprite = LoadSpriteFromPath(path);
                    // Function_onPicked_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
                }
            }, "image/*");
        }
        else
        {
            Debug.Log("Permission Not Granted");
            AskPermission();

        }

    }
    private Sprite LoadSpriteFromPath(string path)
    {
        byte[] imageData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    public void AskPermission()
    {
        NativeGallery.Permission permissionResult = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        permission = permissionResult;

        if (permission == NativeGallery.Permission.Granted)
        {
            PickPhotoCameraRoll();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;

public class SavePhoto : MonoBehaviour
{
    public NativeGallery.Permission permission; // Permission to access Camera Roll
    public string imagePath;
    public Image image;

    public async void SavePhotoToCameraRoll(Texture2D myTexture, string albumName, string filename)
    {
        NativeGallery.Permission result = await Task.Run(() => NativeGallery.SaveImageToGallery(myTexture, albumName, filename));
        if (result != NativeGallery.Permission.Granted)
        {
            Debug.LogError("Failed to save!");
        }
        else
        {
            Debug.Log("Photo is saved to Camera Roll on phone device.");
            // Function_onSaved_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
        }
    }

    public void PickPhotoCameraRoll(Image imageForUpload)
    {
        if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log("Permission Granted");
            NativeGallery.GetImageFromGallery((path) => HandlePickedPhoto(path, imageForUpload), "Select an image", "image/*");
        }
        else
        {
            Debug.LogWarning("Permission Not Granted");
            imagePath = null;
            AskPermission();
        }
    }

    private void HandlePickedPhoto(string path, Image imageForUpload)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("Pick Photo: Canceled");
            imagePath = null;
            imageForUpload.sprite = null;
        }
        else
        {
            Debug.Log("Pick Photo: Success");
            imagePath = path;
            imageForUpload.sprite = LoadSpriteFromPath(path);
            // Function_onPicked_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
        }
    }

    private Sprite LoadSpriteFromPath(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                  (int)sprite.textureRect.y,
                                                  (int)sprite.textureRect.width,
                                                  (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }

    public void AskPermission()
    {
        NativeGallery.Permission permissionResult = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        permission = permissionResult;

        if (permission == NativeGallery.Permission.Granted)
        {
            PickPhotoCameraRoll(image);
        }
    }
}

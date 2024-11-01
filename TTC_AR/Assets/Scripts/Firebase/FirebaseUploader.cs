using Firebase.Storage;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseUploader
{
    public StorageReference storageReference;
    public DatabaseReference dbReference;
    public SavePhoto savePhoto;

    void Start()
    {
    }

    public async void UploadFile(SavePhoto savePhoto, Image imageForUpload, string imagesFolderPath, string fileName)
    {
        savePhoto.image = imageForUpload;
        savePhoto.PickPhotoCameraRoll(imageForUpload);

        Debug.Log("Uploading file: " + savePhoto.imagePath);

        var metadata = new MetadataChange { ContentType = "image/jpeg" };
        storageReference = storageReference.Child(fileName);

        try
        {
            var uploadTask = await storageReference.PutFileAsync(savePhoto.imagePath, metadata);
            Debug.Log("Upload success: " + uploadTask);

            var downloadUrl = await storageReference.GetDownloadUrlAsync();
            Debug.Log("Download URL: " + downloadUrl);

            SaveFileUrlToDatabase(imagesFolderPath, fileName, downloadUrl.ToString());
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Upload failed: " + ex.Message);
        }
    }

    private async void SaveFileUrlToDatabase(string imagesFolderPath, string fileName, string url)
    {
        string keyItem = fileName.Split('.')[0];

        try
        {
            await dbReference.Child(keyItem).SetValueAsync(url);
            Debug.Log("File URL saved successfully: " + url);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving URL to Database: " + ex.Message);
        }
    }
}

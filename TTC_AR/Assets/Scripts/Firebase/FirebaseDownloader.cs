using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.Networking;
using Firebase;

public class FirebaseDownloader : MonoBehaviour
{
    public DatabaseReference dbReference { get; set; }
    private string fileName = "";
    void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        if (dbReference == null)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
        }

    }

    //! Lưu ý là get hình ảnh ko theo thứ tự mà là file nào nhỏ thì get file đó trước
    public void LoadFilesFromDatabase(string imagesFolderPath)
    {
        if (dbReference != null)
        {
            dbReference.Child(imagesFolderPath).GetValueAsync().ContinueWithOnMainThread(async task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error loading data from Firebase Database: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    List<Task> downloadTasks = new List<Task>();

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        string fileUrl = childSnapshot.Value.ToString();
                        Debug.Log("File URL: " + fileUrl);
                        downloadTasks.Add(DownloadFileFromUrl(fileUrl, imagesFolderPath));
                    }

                    await Task.WhenAll(downloadTasks);
                }
            });
        }
        else
        {
            Debug.LogError("Database reference is null.");
        }
    }

    private async Task DownloadFileFromUrl(string url, string imagesFolderPath)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            request.timeout = 30; // Set timeout to avoid waiting too long
            var asyncOperation = request.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield(); // Do not block the main thread while downloading the image
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Failed to load image: " + request.error);
            }
            else
            {
                fileName = System.IO.Path.GetFileNameWithoutExtension(url);
                fileName = fileName.Split(imagesFolderPath + "%2F")[1];
               // GlobalVariable.list_Name_And_Image_JB_Location_A.Add(fileName, Texture_To_Sprite.ConvertTextureToSprite(DownloadHandlerTexture.GetContent(request)));
                Debug.Log("Image downloaded successfully");
               // Debug.Log("Image count: " + GlobalVariable.list_Name_And_Image_JB_Location_A.Count);
                Debug.Log("Image name: " + fileName);
            }
        }
    }
}

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

    void Start()
    {
        // Initialize Firebase or other startup tasks
        //InitializeFirebase();
    }

    private void InitializeFirebase()
    {

        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        // {
        //     if (task.Result == DependencyStatus.Available)
        //     {
        //         dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        //     }
        //     else
        //     {
        //         Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
        //     }
        // });
    }

    // Download JSON from Firebase database and convert to Texture2D
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

                        // Download file from URL
                        downloadTasks.Add(DownloadFileFromUrl(fileUrl));
                    }

                    await Task.WhenAll(downloadTasks);
                }
            });
        }
    }

    private async Task DownloadFileFromUrl(string url)
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
                GlobalVariable.list_Image_JB_Location.Add(DownloadHandlerTexture.GetContent(request));
                Debug.Log("Image downloaded successfully");
            }
        }
    }
}

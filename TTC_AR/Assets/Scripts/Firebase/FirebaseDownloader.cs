using System.Collections;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.UI;

public class FirebaseDownloader
{
    public DatabaseReference dbReference;
    void Start()
    {
    }

    //? Download json from firebase database and convert to Texture2D
    public void LoadFilesFromDatabase(string imagesFolderPath)
    {
        if (dbReference != null)
        {
            dbReference.Child(imagesFolderPath).GetValueAsync().ContinueWithOnMainThread(async task =>
         {
             if (task.IsFaulted)
             {
                 Debug.LogError("Lỗi khi tải dữ liệu từ Firebase Database: " + task.Exception);
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 foreach (var childSnapshot in snapshot.Children)
                 {
                     string fileUrl = childSnapshot.Value.ToString();
                     Debug.Log("File URL: " + fileUrl);

                     // Tải file từ URL
                     await DownloadFileFromUrl(fileUrl);
                 }
             }
         });
        }

    }

    private async Task DownloadFileFromUrl(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            request.timeout = 15; // Thiết lập thời gian timeout để tránh đợi quá lâu
            var asyncOperation = request.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield(); // Không chặn luồng chính của Unity trong khi tải ảnh
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

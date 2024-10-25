using Firebase.Storage;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseUploader
{
    public StorageReference storageReference;
    public DatabaseReference dbReference;

    void Start()
    {
    }

    public void UploadFile(string imagesFolderPath, string fileName)
    {
        // Tải lên ảnh
        storageReference.PutFileAsync(imagesFolderPath).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                // Lấy URL tải về
                storageReference.GetDownloadUrlAsync().ContinueWithOnMainThread(urlTask =>
                {
                    if (urlTask.IsCompleted && !urlTask.IsFaulted)
                    {
                        string downloadUrl = urlTask.Result.ToString();
                        // Lưu URL vào Firebase Database
                        SaveFileUrlToDatabase(imagesFolderPath, fileName, downloadUrl);
                    }
                });
            }
            else
            {
                Debug.LogError("Upload failed: " + task.Exception);
            }
        });
    }
    private void SaveFileUrlToDatabase(string imagesFolderPath, string fileName, string url)
    {
        // Cập nhật hoặc thêm URL mới vào Firebase Database
        dbReference.Child(imagesFolderPath).Child(fileName).SetValueAsync(url).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("File URL đã được lưu thành công: " + url);
            }
            else
            {
                Debug.LogError("Lỗi khi lưu URL vào Database: " + task.Exception);
            }
        });
    }
}

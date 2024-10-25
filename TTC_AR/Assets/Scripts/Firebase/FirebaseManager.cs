using UnityEngine;
using UnityEngine.UI;
using Firebase;
using System;
using System.Collections;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.IO;
using TMPro;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    [SerializeField] private string firebaseUrl = "gs://ttc-project-81b04.appspot.com/";
    [SerializeField] private string imageFolderPath = "";
    [SerializeField] private string imageName = "";

    [Header("Firebase")]
    public DatabaseReference dbReference;
    public FirebaseStorage storage;
    public StorageReference storageReference;
    public FirebaseDownloader firebaseDownloader = new FirebaseDownloader();
    public FirebaseUploader firebaseUploader = new FirebaseUploader();

    public InputField filenameInputField;
    public Image image;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        firebaseDownloader = null;
        firebaseUploader = null;
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                storage = FirebaseStorage.DefaultInstance;
                storageReference = storage.GetReferenceFromUrl(firebaseUrl);

                firebaseDownloader.dbReference = dbReference;
                
                firebaseUploader.dbReference = dbReference;
                firebaseUploader.storageReference = storageReference;

                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    public void LoadFilesFromDatabase()
    {
        firebaseDownloader.LoadFilesFromDatabase(imageFolderPath);
    }

    public void UploadFile()
    {
        firebaseUploader.UploadFile(imageFolderPath, imageName);
    }

    public void GetImage()
    {
        if (storageReference != null)
        {
            LoadImageFromFirebase(imageFolderPath, imageName).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to load image from Firebase: " + task.Exception);
                }
            });
        }
    }

    private async Task LoadImageFromFirebase(string folderPath, string imageName)
    {
        StorageReference imageRef = storageReference.Child(folderPath).Child(imageName);
        try
        {
            Uri downloadUri = await imageRef.GetDownloadUrlAsync();
            await LoadImageFromUrl(downloadUri.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogError("Error fetching image from Firebase: " + ex.Message);
        }
    }

    private async Task LoadImageFromUrl(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            request.timeout = 15;
            var asyncOperation = request.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Failed to load image: " + request.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }
    }
}

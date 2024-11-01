using UnityEngine;
using UnityEngine.UI;
using Firebase;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine.Networking;
using Firebase.Database;
using Firebase.Storage;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }
    [SerializeField] private string firebaseUrl = "gs://ttc-project-81b04.appspot.com";
    [SerializeField] private string imageFolderPath = "JB_Outdoor_Location";
    [SerializeField] private string imageName = "JB1_Location.jpg";

    public DatabaseReference dbReference;
    public FirebaseStorage storage;
    public StorageReference storageReference;
    public FirebaseDownloader FirebaseDownloader = new FirebaseDownloader();
    public FirebaseUploader FirebaseUploader = new FirebaseUploader();
    //public Image Image;
    public bool LoadImage = false;
    public bool LoadDataFromFirebase = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("FirebaseManager already exists. Deleting duplicate.");
        }
        if (SceneManager.GetActiveScene().name.Contains("PLCBox"))
        {
            LoadImage = true;
            LoadDataFromFirebase = true;
        }
        else
        {
            LoadImage = false;
            LoadDataFromFirebase = true;
        }
    }
    private void Start()
    {

    }
    private void OnDestroy()
    {
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
             {
                 if (task.Result == DependencyStatus.Available)
                 {
                     Debug.Log("Firebase dependencies are available.");

                     // Khởi tạo Database và Storage
                     dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                     if (dbReference == null)
                     {
                         Debug.LogError("dbReference is null after initializing FirebaseDatabase.");
                     }
                     else
                     {
                         Debug.Log("Database reference initialized successfully.");
                         // Gán dbReference cho các class khác
                         FirebaseDownloader.dbReference = dbReference;
                         FirebaseUploader.dbReference = dbReference.Child(imageFolderPath);
                         LoadFilesFromDatabase();
                     }

                     storage = FirebaseStorage.DefaultInstance;
                     if (storage == null)
                     {
                         Debug.LogError("Firebase storage is null.");
                     }
                     else
                     {
                         Debug.Log("Storage initialized successfully.");
                         storageReference = storage.GetReferenceFromUrl(firebaseUrl).Child(imageFolderPath);
                         FirebaseUploader.storageReference = storageReference;
                     }


                     Debug.Log("Firebase initialized successfully.");
                 }
                 else
                 {
                     Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
                 }
             });
    }

    public void LoadFilesFromDatabase()
    {
        if (LoadDataFromFirebase)
        {
            FirebaseDownloader.LoadFilesFromDatabase(imageFolderPath);
        }
    }

    public void UploadFile()
    {
        //   FirebaseUploader.UploadFile(SavePhoto, Image, imageFolderPath, imageName);
    }


    //? Get Specific Image
    public void GetImage()
    {
        if (storageReference != null)
        {
            LoadImageFromFirebase(imageFolderPath, imageName).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError($"Failed to load image from Firebase: {task.Exception}");
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
            Debug.LogError($"Error fetching image from Firebase: {ex.Message}");
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
                Debug.LogError($"Failed to load image: {request.error}");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                //   if (Image != null)
                //  {
                //      Image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                //  }
                Resources.UnloadUnusedAssets(); // Free up unused resources
            }
        }
    }
}

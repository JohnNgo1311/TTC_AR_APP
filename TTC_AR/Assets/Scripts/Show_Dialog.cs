using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Show_Dialog : MonoBehaviour
{
    // Singleton Instance
    public static Show_Dialog Instance { get; private set; }

    [SerializeField] private GameObject toastPrefab;
    [SerializeField] private Transform toastParent;
    [SerializeField] private string toastMessageInitial = "";
    [SerializeField] private bool showToastInitial = false;
    [SerializeField] private string toastStatus = "loading";

    // Preload Sprites for better performance
    private Sprite loadingSprite;
    private Sprite successSprite;
    private Sprite failureSprite;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            PreloadSprites();
        }

        // Show initial toast if enabled
        if (showToastInitial)
        {
            ShowToast(toastStatus, toastMessageInitial);
        }
    }

    private void PreloadSprites()
    {
        loadingSprite = Resources.Load<Sprite>("images/UIimages/loading_notification");
        successSprite = Resources.Load<Sprite>("images/UIimages/success_notification");
        failureSprite = Resources.Load<Sprite>("images/UIimages/error_notification");
    }

    public void ShowToast(string toastStatus, string message, float duration = 2f)
    {
        // Instantiate toast from prefab
        GameObject toastInstance = Instantiate(toastPrefab, toastParent);
        TMP_Text toastText = toastInstance.GetComponentInChildren<TMP_Text>();
        Image toastBackground = toastInstance.GetComponentInChildren<Image>();

        if (toastText != null && toastBackground != null)
        {
            toastText.text = message;

            // Set color and sprite based on toast status
            switch (toastStatus.ToLower())
            {
                case "loading":
                    toastText.color = new Color32(0x00, 0x96, 0xFF, 0xFF); // Correct color code
                    toastBackground.sprite = loadingSprite;
                    break;
                case "success":
                    toastText.color = new Color32(0x27, 0xC8, 0x6F, 0xFF); // Correct color code
                    toastBackground.sprite = successSprite;
                    break;
                case "failure":
                    toastText.color = new Color32(0xFF, 0x49, 0x39, 0xFF); // Correct color code
                    toastBackground.sprite = failureSprite;
                    break;
                default:
                    Debug.LogWarning("Unknown toast status: " + toastStatus);
                    break;
            }
        }

        // Destroy the toast instance after the specified duration
        Destroy(toastInstance, duration);
    }
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
}

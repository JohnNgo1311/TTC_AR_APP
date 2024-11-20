using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Show_Dialog : MonoBehaviour
{
    // Singleton Instance
    public static Show_Dialog Instance { get; set; }

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
        toastParent = GetComponent<Canvas>().transform;
        Debug.Log(toastParent.name);
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.LogWarning("Multiple instances of Show_Dialog found. Destroying duplicate.");
        }
        else
        {
            Instance = this;
            Debug.Log("Show_Dialog");
            PreloadSprites();
        }
        if (showToastInitial)
        {
            if (GlobalVariable.recentScene == "SelectionScene" && GlobalVariable.previousScene != "LoginScene")
            {
                return;
            }
            else
            {
                Set_Instance_Status_True();
                ShowToast(toastStatus, toastMessageInitial);
                StartCoroutine(Set_Instance_Status_False());
                showToastInitial = false;
            }
        }
    }
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        // Show initial toast if enabled

    }

    private void PreloadSprites()
    {
        loadingSprite = Resources.Load<Sprite>("images/UIimages/loading_notification");
        successSprite = Resources.Load<Sprite>("images/UIimages/success_notification");
        failureSprite = Resources.Load<Sprite>("images/UIimages/error_notification");
    }

    public void ShowToast(string toastStatus, string message)
    {
        Transform existingToast = toastParent.Find("Toast_Prefab_Group(Clone)/Background");
        TMP_Text toastText;
        Image toastBackground;

        if (existingToast != null)
        {
            existingToast.gameObject.transform.parent.gameObject.SetActive(true);
            toastText = existingToast.GetComponentInChildren<TMP_Text>();
            toastBackground = existingToast.GetComponentInChildren<Image>();
        }
        else
        {
            GameObject toastInstance = Instantiate(toastPrefab, toastParent);
            toastInstance.gameObject.SetActive(true);
            toastInstance.transform.GetChild(0).gameObject.SetActive(true);
            toastText = toastInstance.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
            toastBackground = toastInstance.transform.GetChild(0).GetComponentInChildren<Image>();
        }

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
        // StartCoroutine(SetInstanceDisable(toastParent.Find("Toast_Prefab_Group(Clone)")));
    }
    public void Set_Instance_Status_True()
    {
        // yield return new WaitForSeconds(0.5f);
        if (toastParent.Find("Toast_Prefab_Group(Clone)") != null)
        {
            //Debug$"Set_Instance_Status: {status}");

            toastParent.Find("Toast_Prefab_Group(Clone)").gameObject.SetActive(true);

        }
    }
    public IEnumerator Set_Instance_Status_False()
    {
        yield return new WaitForSeconds(1f);
        if (toastParent.Find("Toast_Prefab_Group(Clone)") != null)
        {
            //Debug$"Set_Instance_Status: {status}");

            toastParent.Find("Toast_Prefab_Group(Clone)").gameObject.SetActive(false);

        }
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Show_Dialog : MonoBehaviour
{
    // Singleton Instance
    public static Show_Dialog Instance { get; set; }
    public Transform dialogParent;
    public GameObject dialogPrefab;
    public string dialogStatus = "loading";
    public Transform existingdialog;
    public Image dialogBackground;
    public TMP_Text dialogText;
    // Tìm tất cả các GameObject trong scene
    public GameObject[] allObjects;
    // Preload Sprites for better performance
    [SerializeField] private string dialogMessageInitial = "";
    [SerializeField] private bool showdialogInitial = false;
    private Sprite loadingSprite;
    private Sprite successSprite;
    private Sprite failureSprite;


    private void Awake()
    {
        allObjects = FindObjectsOfType<GameObject>();
        dialogParent ??= GetComponent<Canvas>().transform;
        Debug.Log(dialogParent.name);
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.LogWarning("Multiple instances of Show_dialog found. Destroying duplicate.");
        }
        else
        {
            Instance = this;
            Debug.Log("Show_dialog");
            PreloadSprites();
        }
        if (showdialogInitial)
        {
            if (GlobalVariable.recentScene == MyEnum.SelectionsScene.GetDescription() && GlobalVariable.previousScene != MyEnum.LoginScene.GetDescription())
            {
                return;
            }
            else
            {
                Set_Instance_Status_True();
                Showdialog(dialogStatus, dialogMessageInitial);
                StartCoroutine(Set_Instance_Status_False());
                showdialogInitial = false;
            }
        }
    }
    void Start()
    {
    }

    private void PreloadSprites()
    {
        loadingSprite = Resources.Load<Sprite>("images/UIimages/loading_notification");
        successSprite = Resources.Load<Sprite>("images/UIimages/success_notification");
        failureSprite = Resources.Load<Sprite>("images/UIimages/error_notification");
    }

    public void Showdialog(string dialogStatus, string message)
    {
        if (existingdialog == null)
        {
            existingdialog = Instantiate(dialogPrefab, dialogParent).transform;
            var layoutdialog = existingdialog.transform.GetChild(0);
            dialogText = layoutdialog.GetComponentInChildren<TMP_Text>();
            dialogBackground = layoutdialog.GetComponentInChildren<Image>();
            existingdialog.gameObject.SetActive(true);
        }
        else
        {
            existingdialog.gameObject.SetActive(true);
        }

        if (dialogText != null && dialogBackground != null)
        {
            dialogText.text = message;
            switch (dialogStatus.ToLower())
            {
                case "loading":
                    dialogText.color = new Color32(0x00, 0x96, 0xFF, 0xFF);
                    dialogBackground.sprite = loadingSprite;
                    break;
                case "success":
                    dialogText.color = new Color32(0x27, 0xC8, 0x6F, 0xFF);
                    dialogBackground.sprite = successSprite;
                    break;
                case "failure":
                    dialogText.color = new Color32(0xFF, 0x49, 0x39, 0xFF);
                    dialogBackground.sprite = failureSprite;
                    break;
                default:
                    Debug.LogWarning("Unknown dialog status: " + dialogStatus);
                    break;
            }
        }

    }
    private void SetInstanceStatus(bool status)
    {

        if (status == false)
        {
            if (existingdialog == null)
            {
                Debug.LogError("existingdialog is null. Make sure it is assigned properly.");
                return;
            }

            existingdialog.gameObject.SetActive(status);
        }

        foreach (GameObject obj in allObjects)
        {
            if (obj != null && obj.name == "LeanTouch")
            {
                obj.SetActive(!status);
                Debug.Log($"{(status ? "Deactivated" : "Activated")}: {obj.name}");
            }
        }
    }


    public void Set_Instance_Status_True()
    {
        SetInstanceStatus(true);
    }

    public IEnumerator Set_Instance_Status_False()
    {
        yield return new WaitForSeconds(1f);
        SetInstanceStatus(false);
        Debug.Log("Tắt dialog");
    }
}
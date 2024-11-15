using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
public class OpenCanvas : MonoBehaviour
{
    public List<GameObject> targetCanvas;
    public List<GameObject> imageTargets;

    [SerializeField]
    private List<string> tagName = new List<string>();

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private List<GameObject> btnOpen = new List<GameObject>(), btnClose = new List<GameObject>();

    [SerializeField]
    private List<GameObject> generalPanel = new List<GameObject>();

    private bool isShowCanvas = false;

    [SerializeField]
    private List<TMP_Text> list_Title = new List<TMP_Text>();

    private List<GameObject> activated_imageTargets = new List<GameObject>();
    private List<ObserverBehaviour> observerBehaviours = new List<ObserverBehaviour>();

    public UnityEngine.UI.Image image;

    private VuforiaBehaviour vuforiaBehaviour;
    public void PauseARCamera()
    {
        // Chạy chụp ảnh màn hình trong một Coroutine để tránh lag
        StartCoroutine(TakeScreenshotCoroutine());

        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = false;  // Tắt AR Camera
            image.gameObject.SetActive(true);
        }
    }

    public void ResumeARCamera()
    {
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = true; // Bật AR Camera
            image.gameObject.SetActive(false);
        }
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        // Tạo RenderTexture một lần và tái sử dụng nếu có thể
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = renderTexture;

        // Lưu lại culling mask ban đầu để khôi phục sau này
        int originalCullingMask = mainCamera.cullingMask;
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("BackBox") |
             1 << LayerMask.NameToLayer("BackBox_Content") |
             1 << LayerMask.NameToLayer("Canvas_Frame"));

        // Render và chờ kết quả
        mainCamera.Render();

        // Cập nhật sprite của hình ảnh khi render hoàn tất
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Khôi phục culling mask và giải phóng tài nguyên
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        // Khôi phục lại culling mask ban đầu
        mainCamera.cullingMask = originalCullingMask;
        // Gán ảnh vào image
        image.sprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo
    }
    void Awake()
    {
        vuforiaBehaviour = mainCamera.gameObject.GetComponent<VuforiaBehaviour>();

        // Tối ưu hóa kiểm tra danh sách
        foreach (var canvas in targetCanvas)
        {
            if (canvas != null)
            {
                var generalPanelObj = canvas.transform.Find("General_Panel")?.gameObject;
                if (generalPanelObj != null)
                {
                    generalPanel.Add(generalPanelObj);
                    btnClose.Add(generalPanelObj.transform.Find("Close_Canvas_Btn")?.gameObject);
                }
            }
        }
        foreach (var target in imageTargets)
        {
            if (target != null)
            {
                var btnOpenObj = target.transform.GetChild(0)?.gameObject;
                if (btnOpenObj != null)
                {
                    btnOpen.Add(btnOpenObj);
                    tagName.Add(btnOpenObj.tag);
                    list_Title.Add(target.transform.Find("imageTarget_Title")?.GetComponent<TMP_Text>());
                    observerBehaviours.Add(target.GetComponent<ObserverBehaviour>());

                    if (target.activeSelf)
                    {
                        activated_imageTargets.Add(target);
                    }
                }
            }
        }
    }

    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status, TMP_Text title, string name)
    {
        if (status.Status == Status.TRACKED)
        {
            title.text = ConvertString(name);
            title.gameObject.SetActive(true);
        }
        else
        {
            title.gameObject.SetActive(false);
        }
    }

    public static string ConvertString(string input)
    {
        if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        {
            return input.Insert(2, ".").Insert(4, "."); // Chèn dấu chấm vào vị trí 2 và 4
        }
        else
        {

            return input.Replace("_", " "); ;
        }
    }

    private void OnDestroy()
    {
        tagName.Clear();
        btnOpen.Clear();
        btnClose.Clear();
        generalPanel.Clear();

    }

    void Start()
    {

        // Tắt runtime UI nếu đang bật
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
        {
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        }

        for (int i = 0; i < observerBehaviours.Count; i++)
        {
            int index = i; // Cần dùng biến tạm để tránh lỗi closure trong lambda
            observerBehaviours[index].OnTargetStatusChanged += (behaviour, status) => OnStatusChanged(behaviour, status, list_Title[index], btnOpen[index].name);
        }

        SetActiveForList(targetCanvas, false);
        SetActiveForList(generalPanel, true);
        SetActiveForList(btnClose, true);
        SetActiveForList(btnOpen, true);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GrapperAScanScene")
        {
            activated_imageTargets = GlobalVariable.activated_iamgeTargets;
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = Input.mousePosition;
#if !UNITY_EDITOR
            inputPosition = Input.GetTouch(0).position;
#endif
            HandleInput(inputPosition);
        }
    }

    private RaycastHit[] hits = new RaycastHit[10];

    private void HandleInput(Vector3 inputPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
        int hitCount = Physics.RaycastNonAlloc(ray, hits);

        for (int i = 0; i < hitCount; i++)
        {
            int index = tagName.FindIndex(tag => hits[i].collider.CompareTag(tag));
            if (index != -1)
            {
                if (isShowCanvas)
                {
                    StartCoroutine(OnCloseCanvas(index));
                }
                else
                {
                    StartCoroutine(OnOpenCanvas(index));
                }
                isShowCanvas = !isShowCanvas;
                break;
            }
        }
    }


    private IEnumerator OnOpenCanvas(int index)
    {
        if (IsValidIndex(index, targetCanvas))
            targetCanvas[index].SetActive(true);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        SetActiveForList(activated_imageTargets, false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        if (IsValidIndex(index, btnOpen))
            btnOpen[index]?.SetActive(false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        if (IsValidIndex(index, btnClose))
            btnClose[index]?.SetActive(true);
        PauseARCamera();
    }

    private IEnumerator OnCloseCanvas(int index)
    {
        if (IsValidIndex(index, targetCanvas))
            targetCanvas[index]?.SetActive(false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        SetActiveForList(activated_imageTargets, true);

        if (IsValidIndex(index, btnClose) && btnClose[index]?.activeSelf == true)
            btnClose[index]?.SetActive(false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        if (IsValidIndex(index, btnOpen) && btnOpen[index]?.activeSelf == false)
            btnOpen[index]?.SetActive(true);
        ResumeARCamera();

    }

    private bool IsValidIndex(int index, List<GameObject> list)
    {
        return list != null && index >= 0 && index < list.Count;
    }

    private void SetActiveForList(List<GameObject> list, bool isActive)
    {
        if (list != null)
        {
            foreach (var obj in list)
            {
                if (obj != null && obj.activeSelf != isActive)
                {
                    obj.SetActive(isActive);
                }
            }
        }
    }
}
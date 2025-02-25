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
    private List<string> tagName = new List<string>() { };

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private List<GameObject> btnOpen = new List<GameObject>() { }, btnClose = new List<GameObject>() { };

    [SerializeField]
    private List<GameObject> generalPanel = new List<GameObject>();

    private bool isShowCanvas = false;

    [SerializeField]
    private List<TMP_Text> list_Title = new List<TMP_Text>();
    private List<GameObject> activated_imageTargets = new List<GameObject>();
    private List<ObserverBehaviour> observerBehaviours = new List<ObserverBehaviour>();
    [SerializeField] Init_Modules_Canvas Init_Modules_Canvas;


    void Awake()
    {
        StartCoroutine(Initialize());
    }
    private IEnumerator Initialize()
    {
        yield return null;
        if (SceneManager.GetActiveScene().name != MyEnum.FieldDevicesScene.GetDescription())
        {
            if (Init_Modules_Canvas != null)
            {
                yield return new WaitUntil(() => Init_Modules_Canvas.isInstantiating == false);
                // Tối ưu hóa kiểm tra danh sách
                targetCanvas = Init_Modules_Canvas.targetCanvas;
                Destroy(Init_Modules_Canvas.moduleCanvasPrefab.gameObject);
                Debug.Log("targetCanvas.Count: " + targetCanvas.Count);
            }

        }
        if (targetCanvas.Count > 0 && imageTargets.Count > 0)
        {
            Debug.Log("targetCanvas.Count: " + targetCanvas.Count);
            Debug.Log("imageTargets.Count: " + imageTargets.Count);
            foreach (var canvas in targetCanvas)
            {
                if (canvas != null)
                {
                    var generalPanelObj = canvas.transform.Find("Basic_Panel")?.gameObject;
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
            for (int i = 0; i < observerBehaviours.Count; i++)
            {
                int index = i; // Cần dùng biến tạm để tránh lỗi closure trong lambda
                observerBehaviours[index].OnTargetStatusChanged += (behaviour, status) => OnStatusChanged(behaviour, status, list_Title[index], btnOpen[index].name);
            }

            SetActiveForList(targetCanvas, false);
            SetActiveForList(generalPanel, true);
            // SetActiveForList(btnClose, true);
            SetActiveForList(btnOpen, true);
        }

    }
    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status, TMP_Text title, string name)
    {
        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
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
        if (SceneManager.GetActiveScene().name == MyEnum.FieldDevicesScene.GetDescription())
        {
            if (status.Status == Status.TRACKED)
            {
                title.text = name;
                title.gameObject.SetActive(true);
            }
            else
            {
                title.gameObject.SetActive(false);
            }
        }
    }

    public static string ConvertString(string input)
    {
        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
        {
            return input.Insert(2, ".").Insert(4, "."); // Chèn dấu chấm vào vị trí 2 và 4
        }
        else
        {
            return input;
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

    }

    void Update()
    {

        activated_imageTargets = GlobalVariable.activated_iamgeTargets;


#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame ||
            (UnityEngine.InputSystem.Touchscreen.current != null && UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            Vector3 inputPosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#if !UNITY_EDITOR
            inputPosition = UnityEngine.InputSystem.Touchscreen.current.primaryTouch.position.ReadValue();
#endif
            HandleInput(inputPosition);
        }
#else
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 inputPosition = Input.mousePosition;
#if !UNITY_EDITOR
            inputPosition = Input.GetTouch(0).position;
#endif
            HandleInput(inputPosition);
        }
#endif
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
            targetCanvas[index].gameObject.SetActive(true);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        SetActiveForList(activated_imageTargets, false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        if (IsValidIndex(index, btnOpen))
            btnOpen[index]?.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == MyEnum.GrapperAScanScene.GetDescription())
        {
            yield return new WaitUntil(() =>
        GlobalVariable.ActiveCloseCanvasButton = true
            );
            Debug.Log("OnOpenCanvas 1");

        }
        else if (SceneManager.GetActiveScene().name == MyEnum.FieldDevicesScene.GetDescription())
        {
            yield return new WaitUntil(() =>
            GlobalVariable.temp_ListFieldDeviceConnectionImages.Count > 0
            && GlobalVariable.temp_ListFieldDeviceConnectionImages.Count > 0);
            Debug.Log("OnOpenCanvas 2");

        }
        if (IsValidIndex(index, btnClose))
            btnClose[index]?.gameObject.SetActive(true);
    }

    private IEnumerator OnCloseCanvas(int index)
    {
        if (IsValidIndex(index, targetCanvas))
            targetCanvas[index]?.gameObject.SetActive(false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        SetActiveForList(activated_imageTargets, true);

        if (IsValidIndex(index, btnClose) && btnClose[index]?.gameObject.activeSelf == true)
            btnClose[index]?.SetActive(false);
        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo

        if (IsValidIndex(index, btnOpen) && btnOpen[index]?.gameObject.activeSelf == false)
            btnOpen[index]?.SetActive(true);

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
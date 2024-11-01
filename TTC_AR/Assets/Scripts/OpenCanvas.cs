using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    void Awake()
    {
        // Tối ưu hóa kiểm tra danh sách
        for (int i = 0; i < targetCanvas.Count; i++)
        {
            var generalPanelObj = targetCanvas[i]?.transform.Find("General_Panel")?.gameObject;
            if (generalPanelObj != null)
            {
                generalPanel.Add(generalPanelObj);
                btnClose.Add(generalPanelObj.transform.Find("Close_Canvas_Btn")?.gameObject);
            }
        }

        for (int i = 0; i < imageTargets.Count; i++)
        {
            var btnOpenObj = imageTargets[i]?.transform.GetChild(0)?.gameObject;
            if (btnOpenObj != null)
            {
                btnOpen.Add(btnOpenObj);
                tagName.Add(btnOpenObj.tag);
                list_Title.Add(imageTargets[i].transform.Find("imageTarget_Title")?.GetComponent<TMP_Text>());
                observerBehaviours.Add(imageTargets[i].GetComponent<ObserverBehaviour>());

                if (imageTargets[i].activeSelf)
                {
                    activated_imageTargets.Add(imageTargets[i]);
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
        return input.Insert(2, ".").Insert(4, ".");
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
        activated_imageTargets = GlobalVariable.activated_iamgeTargets;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
            HandleInput(inputPosition);
        }
        
    }

    private void HandleInput(Vector3 inputPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int index = tagName.FindIndex(tag => hit.collider.CompareTag(tag));
            if (index != -1)
            {
                if (isShowCanvas)
                {
                    OnCloseCanvas(index);
                }
                else
                {
                    OnOpenCanvas(index);
                }
                isShowCanvas = !isShowCanvas;
            }
        }
    }

    private void OnOpenCanvas(int index)
    {
        if (IsValidIndex(index, targetCanvas))
            targetCanvas[index].SetActive(true);

        SetActiveForList(activated_imageTargets, false);

        if (IsValidIndex(index, btnOpen))
            btnOpen[index]?.SetActive(false);

        if (IsValidIndex(index, btnClose))
            btnClose[index]?.SetActive(true);
    }

    private void OnCloseCanvas(int index)
    {
        if (IsValidIndex(index, targetCanvas))
            targetCanvas[index]?.SetActive(false);

        SetActiveForList(activated_imageTargets, true);

        if (IsValidIndex(index, btnClose) && btnClose[index]?.activeSelf == true)
            btnClose[index]?.SetActive(false);

        if (IsValidIndex(index, btnOpen) && btnOpen[index]?.activeSelf == false)
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

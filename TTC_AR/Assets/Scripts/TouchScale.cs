using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchScale : MonoBehaviour
{
    private float initialTouchDistance;
    private Vector3 initialScale;
    private bool isScaling = false;
    [SerializeField] private ScrollRect parentScrollRect;
    [SerializeField] private ScrollRect objectScrollRect;
    // [SerializeField] private bool keepScaleFunc = false;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private Canvas originalCanvas;
    private Canvas tempCanvas;

    private void Start()
    {
       
        InitializeComponents();
    }

    private void Update()
    {
        HandleScreenOrientation();
    }

    private void InitializeComponents()
    {
        initialScale = transform.localScale;

        // Cache components
        //if(parentScrollRect!=null) parentScrollRect = parentScrollRect ?? GetComponentInParent<ScrollRect>();
        raycaster = raycaster ?? GetComponentInParent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        originalCanvas = originalCanvas ?? GetComponentInParent<Canvas>();
    }

    private void HandleScreenOrientation()
    {
        EnableScalingMode();
    }

    private void EnableScalingMode()
    {
        HandleTouchScaling();  // Xử lý scaling khi có touch
        EnableObjectScrollRect();  // Cho phép scroll
    }

    private void HandleTouchScaling()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (BothTouchesOverGameObject(touch0, touch1))
            {
                ProcessScaling(touch0, touch1);
            }
        }
        else
        {
            ResetScalingState();
        }
    }

    private void ProcessScaling(Touch touch0, Touch touch1)
    {
        float currentTouchDistance = Vector2.Distance(touch0.position, touch1.position);

        if (!isScaling)
        {
            BeginScaling(currentTouchDistance);
        }
        else
        {
            ApplyScaling(currentTouchDistance);
        }
    }

    private void BeginScaling(float currentTouchDistance)
    {
        initialTouchDistance = currentTouchDistance;
        isScaling = true;

        SetParentScrollRectEnabled(false);
        AddTemporaryCanvas();
    }

    private void ApplyScaling(float currentTouchDistance)
    {
        float scaleFactor = currentTouchDistance / initialTouchDistance;
        transform.localScale = initialScale * scaleFactor;
    }

    private void ResetScalingState()
    {
        isScaling = false;
        initialScale = transform.localScale;

        SetParentScrollRectEnabled(true);
        RemoveTemporaryCanvas();
    }

    private bool BothTouchesOverGameObject(Touch touch0, Touch touch1)
    {
        return IsTouchOverGameObject(touch0) && IsTouchOverGameObject(touch1);
    }

    private bool IsTouchOverGameObject(Touch touch)
    {
        pointerEventData = new PointerEventData(eventSystem) { position = touch.position };
        var results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        return results.Exists(result => result.gameObject == gameObject);
    }

    private void SetParentScrollRectEnabled(bool enabled)
    {
        if (parentScrollRect != null)
        {
            parentScrollRect.enabled = enabled;
        }
    }

    private void AddTemporaryCanvas()
    {
        if (tempCanvas == null)
        {
            tempCanvas = gameObject.AddComponent<Canvas>();
            tempCanvas.overrideSorting = true;
            tempCanvas.sortingOrder = 1000; // Đảm bảo trên cùng
        }
    }

    private void RemoveTemporaryCanvas()
    {
        if (tempCanvas != null)
        {
            Destroy(tempCanvas);
            tempCanvas = null;
        }
    }

    private void DisableObjectScrollRect()
    {
        if (objectScrollRect != null)
        {
            objectScrollRect.enabled = false;
        }
    }

    private void EnableObjectScrollRect()
    {
        if (objectScrollRect != null)
        {
            objectScrollRect.enabled = true;
        }
    }
}
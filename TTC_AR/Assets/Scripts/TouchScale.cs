using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchScale : MonoBehaviour
{
    private float initialTouchDistance;
    private Vector3 initialScale;
    private bool isScaling = false;
    [SerializeField] private ScrollRect objectScrollRect;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private Canvas originalCanvas;
    private Canvas tempCanvas;
    private Touch touch0;
    private Touch touch1;
    private void Start()
    {
        InitializeComponents();
    }

    private void Update()
    {
        HandleTouchScaling();
        EnableObjectScrollRect();
    }

    private void InitializeComponents()
    {
        initialScale = transform.localScale;
        raycaster = GetComponentInParent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        originalCanvas = GetComponentInParent<Canvas>();
    }

#if ENABLE_LEGACY_INPUT_MANAGER
    private void HandleTouchScaling()
    {
        if (Input.touchCount == 2)
        {
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);

            if (BothTouchesOverGameObject(touch0.position, touch1.position))
            {
                ProcessScaling(touch0.position, touch1.position);
            }
        }
        else
        {
            ResetScalingState();
        }
    }
#elif ENABLE_INPUT_SYSTEM
    private void HandleTouchScaling()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count == 2)
        {
            var touch0 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
            var touch1 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[1];

            if (BothTouchesOverGameObject(touch0.screenPosition, touch1.screenPosition))
            {
                ProcessScaling(touch0.screenPosition, touch1.screenPosition);
            }
        }
        else
        {
            ResetScalingState();
        }
    }
#endif

    private void ProcessScaling(Vector2 touch0Position, Vector2 touch1Position)
    {
        float currentTouchDistance = Vector2.Distance(touch0Position, touch1Position);

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
        //AddTemporaryCanvas();
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
        //     RemoveTemporaryCanvas();
    }

    private bool BothTouchesOverGameObject(Vector2 touch0Position, Vector2 touch1Position)
    {
        return IsTouchOverGameObject(touch0Position) && IsTouchOverGameObject(touch1Position);
    }

    private bool IsTouchOverGameObject(Vector2 touchPosition)
    {
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }
        pointerEventData.position = touchPosition;
        var results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        return results.Exists(result => result.gameObject == gameObject);
    }

    // private void AddTemporaryCanvas()
    // {
    //     if (tempCanvas == null)
    //     {
    //         tempCanvas = gameObject.AddComponent<Canvas>();
    //         tempCanvas.overrideSorting = true;
    //         tempCanvas.sortingOrder = 1000;
    //     }
    // }

    // private void RemoveTemporaryCanvas()
    // {
    //     if (tempCanvas != null)
    //     {
    //         Destroy(tempCanvas);
    //         tempCanvas = null;
    //     }
    // }

    private void EnableObjectScrollRect()
    {
        if (objectScrollRect != null)
        {
            objectScrollRect.enabled = true;
        }
    }
}

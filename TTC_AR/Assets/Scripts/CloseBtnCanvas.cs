using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtnCanvas : MonoBehaviour
{
    public GameObject btnCanvas;
    public bool isMonitorCanvasOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
    void OnEnable()
    {
        btnCanvas.SetActive(false);
    }
    void OnDisable()
    {
        if (isMonitorCanvasOpen)
        {
            btnCanvas.SetActive(false);

        }
        else btnCanvas.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenOneScreen : MonoBehaviour
{
    public GameObject screen;
    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        screen.SetActive(false);
    }
    void Update()
    {
    }
    public void OpenScreen()
    {
        screen.SetActive(true);
    }

    public void NavigatePop()
    {

        screen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenOneScreen : MonoBehaviour
{
    public GameObject screen;
    void Start()
    {
        screen.SetActive(false);
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

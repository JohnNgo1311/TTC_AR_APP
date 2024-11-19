using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resize_Image_After_Orientation : MonoBehaviour
{
  private Image imageComponent;
  private ScreenOrientation lastOrientation;
  void Awake()
  {
    lastOrientation = Screen.orientation;
    imageComponent = GetComponent<Image>();
  }
  void Update()
  {
    if (Screen.orientation != lastOrientation)
    {
      StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent));
      lastOrientation = Screen.orientation;
    }

  }
  void Start()
  {

  }
}
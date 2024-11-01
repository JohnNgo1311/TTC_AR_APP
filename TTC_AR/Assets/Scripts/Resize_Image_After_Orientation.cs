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
  // private RectTransform originalRectTransform; // Biến lưu RectTransform ban đầu

  private ScreenOrientation lastOrientation;
  void Awake()
  {
    // Lưu orientation khi bắt đầu ứng dụng
    // originalRectTransform = GetComponent<RectTransform>();
    lastOrientation = Screen.orientation;
    imageComponent = gameObject.GetComponent<Image>();

  }
  void Update()
  {
    // Kiểm tra nếu orientation đã thay đổi
    if (Screen.orientation != lastOrientation)
    {
      // Orientation đã thay đổi
      Debug.Log("Orientation changed from " + lastOrientation + " to " + Screen.orientation);
      // Khôi phục vị trí và kích thước ban đầu khi màn hình xoay
      //   gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(originalRectTransform.sizeDelta.x, originalRectTransform.sizeDelta.y);

      Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent);
      // Cập nhật orientation hiện tại
      lastOrientation = Screen.orientation;
      // Gọi hàm xử lý theo yêu cầu*/
    }

  }
  void Start()
  {
    if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
      UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
  }
}
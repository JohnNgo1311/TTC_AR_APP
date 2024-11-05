using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Texture_To_Sprite : MonoBehaviour
{
  void Start()
  {
    if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
      UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
  }
  public static Sprite ConvertTextureToSprite(Texture2D texture)
  {
    // Tạo một Sprite từ Texture2D
    Rect rect = new Rect(0, 0, texture.width, texture.height);
    Vector2 pivot = new Vector2(0.5f, 0.5f); // Điểm pivot ở giữa hình ảnh
    Sprite sprite = Sprite.Create(texture, rect, pivot);

    return sprite;
  }


}
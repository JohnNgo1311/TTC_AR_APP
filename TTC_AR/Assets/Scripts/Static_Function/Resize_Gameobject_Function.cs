using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Resize_Gameobject_Function : MonoBehaviour
{
  void Start()
  {
    if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
      UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
  }
  public static void Resize_Parent_GameObject(RectTransform contentTransform)
  {
    LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
    Canvas.ForceUpdateCanvases();
    float totalHeight = 0f;
    foreach (Transform child in contentTransform.gameObject.transform)
    {
      RectTransform childRect = child.gameObject.GetComponent<RectTransform>();
      if (childRect != null && childRect.gameObject.activeSelf)
      {
        totalHeight += childRect.rect.height;
      }
    }
    contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, (float)(totalHeight * 1.00005));
  }
  public static void Set_NativeSize_For_GameObject(Image imageComponent)
  {
    LayoutRebuilder.ForceRebuildLayoutImmediate(imageComponent.rectTransform);
    Canvas.ForceUpdateCanvases();

    if (imageComponent.sprite == null)
    {
      Debug.LogWarning("Sprite is null on Image component.");
      return;
    }

    Rect spriteRect = imageComponent.sprite.rect;
    RectTransform rectTransform = imageComponent.gameObject.GetComponent<RectTransform>();

    float scale_width = rectTransform.sizeDelta.x / spriteRect.width;
    float scale_height = rectTransform.sizeDelta.y / spriteRect.height;

    Debug.Log($"scale_width: {scale_width}");
    Debug.Log($"scale_height: {scale_height}");

    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, spriteRect.height * scale_width);
  }

}
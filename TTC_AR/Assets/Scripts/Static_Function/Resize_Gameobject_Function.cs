using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Resize_Gameobject_Function : MonoBehaviour
{

  //public static bool isResize = false;
  void Start()
  {
  }

  public static void Resize_Parent_GameObject(RectTransform contentTransform)
  {
    LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
    Canvas.ForceUpdateCanvases();
    float totalHeight = 0f;
    foreach (RectTransform childRect in contentTransform)
    {
      if (childRect.gameObject.activeSelf)
      {
        totalHeight += childRect.rect.height;
      }
    }
    contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, totalHeight * 1.00005f);
  }

  public static IEnumerator Set_NativeSize_For_GameObject(Image imageComponent)
  {
    Canvas.ForceUpdateCanvases();

    if (imageComponent.sprite == null)
    {
      Debug.LogWarning("Sprite is null on Image component.");
      yield break;
    }


    // Lấy kích thước gốc của hình ảnh
    float originalWidth = imageComponent.sprite.rect.width;
    float originalHeight = imageComponent.sprite.rect.height;

    RectTransform rectTransform = imageComponent.rectTransform;
    float aspectRatio = originalWidth / originalHeight;
    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / aspectRatio);
    LayoutRebuilder.ForceRebuildLayoutImmediate(imageComponent.rectTransform);

  }
}

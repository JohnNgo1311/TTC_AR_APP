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
    LayoutRebuilder.ForceRebuildLayoutImmediate(imageComponent.rectTransform);
    Canvas.ForceUpdateCanvases();
    //isResize = false;
    if (imageComponent.sprite == null)
    {
      Debug.LogWarning("Sprite is null on Image component.");
      yield break;
    }
    // imageComponent.gameObject.SetActive(false);
    yield return new WaitForSeconds(0.3f);

    Rect spriteRect = imageComponent.sprite.rect; //Lấy kích thước của sprite 
    RectTransform rectTransform = imageComponent.rectTransform;
    float scaleWidth = rectTransform.sizeDelta.x / spriteRect.width;

    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, spriteRect.height * scaleWidth);
    //imageComponent.gameObject.SetActive(true);
    //isResize = true;
  }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Resize_Images_After_Orientation : MonoBehaviour
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
      StartCoroutine(CheckOrientationChange());
    }
  }

  IEnumerator CheckOrientationChange()
  {
    yield return Resize_GameObject_Function.Set_NativeSize_For_GameObject(imageComponent);
    Debug.Log("Resize Image");
    lastOrientation = Screen.orientation;
    Debug.Log("Orientation Changed");
  }

  private void OnDestroy()
  {
    StopAllCoroutines();
  }
}
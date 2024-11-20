using System.Collections;
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

  void Start()
  {
  }
  void Update()
  {
    StartCoroutine(CheckOrientationChange());
  }
  IEnumerator CheckOrientationChange()
  {
    yield return new WaitForSeconds(0.5f);
    if (Screen.orientation != lastOrientation)
    {
      StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(imageComponent));
      Debug.Log("Resize Image");
      yield return new WaitForSeconds(1.5f);
      lastOrientation = Screen.orientation;
      Debug.Log("Orientation Changed");
    }
  }

  private void OnDestroy()
  {
    StopAllCoroutines();
  }
}
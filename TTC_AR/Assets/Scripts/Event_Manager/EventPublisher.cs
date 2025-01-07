using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class EventPublisher : MonoBehaviour
{
  [Preserve]
  public delegate void ClickAction();
  [Preserve]
  public delegate void OrientationChangeAction(ScreenOrientation newOrientation);

  [Preserve]
  public event ClickAction OnButtonClicked;
  [Preserve]
  public event OrientationChangeAction OnOrientationChanged;

  public void TriggerEvent_ButtonClicked()
  {
    Debug.Log("Event Triggered in Publisher!"); // Thông báo khi sự kiện được kích hoạt
    OnButtonClicked?.Invoke(); // Kích hoạt sự kiện nếu có Subscriber
  }
  public void TriggerOrientationChange(ScreenOrientation newOrientation)
  {
    OnOrientationChanged?.Invoke(newOrientation);
  }

}

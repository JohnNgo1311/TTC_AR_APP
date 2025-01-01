using System; // Thư viện để sử dụng delegate và event
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public event Action OnButtonClicked; // Khai báo sự kiện dạng được kích bởi nhấn Button
    public event Action<ScreenOrientation> OnOrientationChanged;

    // Phương thức trigger sự kiện

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

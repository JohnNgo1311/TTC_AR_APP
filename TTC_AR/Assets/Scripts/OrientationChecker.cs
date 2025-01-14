using UnityEngine;

public class OrientationChecker : MonoBehaviour
{
    public EventPublisher eventPublisher;

    private ScreenOrientation _currentOrientation;

    private void Start()
    {
        // Lưu orientation ban đầu
        _currentOrientation = Screen.orientation;
    }

    private void Update()
    {
        // Kiểm tra nếu orientation thay đổi
        if (_currentOrientation != Screen.orientation)
        {
            // Cập nhật orientation
            _currentOrientation = Screen.orientation;

            // Gọi phương thức trigger sự kiện
            // eventPublisher.TriggerOrientationChange(_currentOrientation);
        }
    }
}

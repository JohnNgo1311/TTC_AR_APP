using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeOut_Function : MonoBehaviour
{
  public float timeoutDuration = 60f;  // Thời gian timeout (tính bằng giây)
  private float timeSinceLastInteraction; // Thời gian kể từ lần tương tác cuối cùng
  private Coroutine timerCoroutine;
  [SerializeField]
  private float timeShowToast = 5;
  private bool activeLogOut = false;
  void Start()
  {
    if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
      UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    // Khởi tạo bộ đếm thời gian
    ResetTimeout();
  }

  void Update()
  {
    // Kiểm tra nếu có tác động vào màn hình (như chạm hoặc di chuyển chuột)
    if (Input.anyKey || Input.touchCount > 0)
    {
      activeLogOut = false;
      ResetTimeout();  // Reset thời gian đếm nếu có tương tác
    }

  }

  // Hàm reset thời gian khi có tương tác
  public void ResetTimeout()
  {
    // Reset thời gian về 0
    timeSinceLastInteraction = 0f;

    // Nếu timer đang chạy thì dừng lại
    if (timerCoroutine != null)
    {
      StopCoroutine(timerCoroutine);
    }

    // Khởi động lại timer
    timerCoroutine = StartCoroutine(TimerRoutine());
  }

  // Hàm thoát ứng dụng
  public void ExitApplication()
  {
    Debug.Log("Ứng dụng sẽ thoát do không có tương tác...");
    Application.Quit();  // Thoát ứng dụng trên Android
  }

  // Coroutine đếm thời gian
  IEnumerator TimerRoutine()
  {
    while (true)
    {
      // Chờ 1 giây trước khi tăng thời gian
      yield return new WaitForSeconds(1);

      // Tăng thời gian đếm
      timeSinceLastInteraction += 1f;
      Debug.Log(timeSinceLastInteraction);
      // Kiểm tra nếu thời gian vượt quá giới hạn timeout
      if (timeSinceLastInteraction >= timeoutDuration)
      {
        activeLogOut = true;
        Show_Dialog.Instance.ShowToast("failure", "Phát hiện treo máy lâu! Hãy chạm vào màn hình hoặc ứng dụng sẽ tự thoát", timeShowToast);
        StartCoroutine(WaitShowToast());
        break;
      }
    }
  }

  IEnumerator WaitShowToast()
  {
    yield return new WaitForSeconds(timeShowToast);
    if (activeLogOut) ExitApplication();  // Thoát ứng dụng                       
                                          // Debug.Log("Timeout! Application will exit...");
  }
}

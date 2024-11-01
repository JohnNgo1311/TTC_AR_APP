using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneLoader : MonoBehaviour
{
    public string LastSceneKey;
    public string InitialSceneName; // Đặt tên cảnh ban đầu của bạn ở đây

    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;

        // Kiểm tra nếu có cảnh được lưu trữ từ lần chạy trước
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            string lastScene = PlayerPrefs.GetString(LastSceneKey);

            // Nếu cảnh hiện tại không phải là cảnh ban đầu, tải cảnh ban đầu
            if (lastScene != InitialSceneName)
            {
                SceneManager.LoadSceneAsync(InitialSceneName);
            }
        }
        else
        {        // Nếu không có cảnh nào được lưu, tải cảnh ban đầu
            SceneManager.LoadSceneAsync(InitialSceneName);
        }
    }
}

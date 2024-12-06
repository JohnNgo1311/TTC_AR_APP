using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo không bị hủy khi chuyển Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetScreenOrientation(bool isOrientation = false)
    {
        if (!isOrientation)
        {
            if (GlobalVariable.sceneNamesLandScape.Contains(GlobalVariable.recentScene))
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
        else
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
    public void NavigateToScene(string recentSceneName, string previousSceneName)
    {
        GlobalVariable.recentScene = recentSceneName;
        GlobalVariable.previousScene = previousSceneName;
        SceneManager.LoadScene(recentSceneName);
        PlayerPrefs.SetString(recentSceneName, SceneManager.GetActiveScene().name);
    }

    public IEnumerator WaitAndNavigate(string recentSceneName, string previousSceneName)
    {
        yield return new WaitUntil(() => GlobalVariable.ready_To_Nav_New_Scene == true);
        if (GlobalVariable.recentScene != recentSceneName)
        {
            Show_Dialog.Instance.Set_Instance_Status_True();
            Show_Dialog.Instance.ShowToast("loading", "Đang chuyển trang...");
            NavigateToScene(recentSceneName, previousSceneName);
        }
    }
}

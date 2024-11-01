using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavScene : MonoBehaviour
{
    public string previousSceneName;
    public List<string> recentSceneName;
    public List<Button> listButton;
    private int index;

    void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        for (int i = 0; i < listButton.Count; i++)
        {
            int localIndex = i; // Tạo bản sao cục bộ của i
            listButton[i].onClick.AddListener(() => NavigateNewScene(localIndex));
        }
    }

    public async void NavigateNewScene(int buttonIndex)
    {
        // Đợi cho đến khi ready_To_Nav_New_Scene == true
        while (!GlobalVariable.ready_To_Nav_New_Scene)
        {
            Debug.Log("Waiting for ready_To_Nav_New_Scene == true +  " + GlobalVariable.ready_To_Nav_New_Scene.ToString());
            await Task.Yield();  // Chờ đợi một chút trước khi kiểm tra lại
        }

        // Khi ready_To_Nav_New_Scene == true, tiếp tục thực hiện điều hướng
        if (GlobalVariable.recentScene != recentSceneName[buttonIndex])
        {
            GlobalVariable.recentScene = recentSceneName[buttonIndex];
            GlobalVariable.previousScene = previousSceneName;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(recentSceneName[buttonIndex]); // Sử dụng bất đồng bộ để tải scene mới
            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }
            PlayerPrefs.SetString(recentSceneName[buttonIndex], SceneManager.GetActiveScene().name);
        }
    }

}

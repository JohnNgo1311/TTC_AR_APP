using System.Collections;
using System.Collections.Generic;
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
            int localIndex = i; // Create a local copy of i
            listButton[i].onClick.AddListener(() => NavigateNewScene(localIndex));
        }
    }

    public void NavigateNewScene(int buttonIndex)
    {
        StartCoroutine(WaitForReadyAndNavigate(buttonIndex));
    }

    private IEnumerator WaitForReadyAndNavigate(int buttonIndex)
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("loading", "Đang chuyển trang...");
        // Wait until ready_To_Nav_New_Scene is true
        while (!GlobalVariable.ready_To_Nav_New_Scene)
        {
            yield return null;  // Wait for the next frame
        }
        // When ready_To_Nav_New_Scene is true, proceed with navigation
        if (GlobalVariable.recentScene != recentSceneName[buttonIndex])
        {
            GlobalVariable.recentScene = recentSceneName[buttonIndex];
            GlobalVariable.previousScene = previousSceneName;
            SceneManager.LoadScene(recentSceneName[buttonIndex]); // Use synchronous loading
            PlayerPrefs.SetString(recentSceneName[buttonIndex], SceneManager.GetActiveScene().name);
        }
        yield return new WaitForSeconds(0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavScene : MonoBehaviour
{
    public string previousSceneName;
    public List<string> recentSceneName;
    public List<Button> listButton;

    void Start()
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            int localIndex = i; // Tạo bản sao cục bộ của `i`
            listButton[i].onClick.AddListener(() => NavigateNewScene(localIndex));
        }
    }

    public void NavigateNewScene(int buttonIndex)
    {
        StartCoroutine(Scene_Manager.Instance.WaitAndNavigate(recentSceneName[buttonIndex], previousSceneName));
    }
}

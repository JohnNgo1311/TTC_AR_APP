using System;
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
    private void Awake()
    {

    }
    private void Start()
    {
        // Lặp qua tất cả các button và đăng ký sự kiện onClick
        for (int i = 0; i < listButton.Count; i++)
        {
            int localIndex = i; // Tạo bản sao cục bộ của `i` để tránh lỗi tham chiếu trong lambda
            if (SceneManager.GetActiveScene().name == MyEnum.MenuScene.GetDescription() &&
            (listButton[i].gameObject.name.Contains("GrapperB")
             || listButton[i].gameObject.name.Contains("GrapperC")
            || listButton[i].gameObject.name.Contains("LH_Btn"))
            )
            {

            }
            else listButton[i].onClick.AddListener(() =>
             {
                 // Khi một button được nhấn, gọi NavigateNewScene coroutine
                 StartCoroutine(NavigateNewScene(localIndex));
             });
        }
    }

    private IEnumerator NavigateNewScene(int buttonIndex)
    { // Đợi trước khi thực hiện các thao tác tiếp theo
        int index = buttonIndex;

        Debug.Log($"Navigate to {recentSceneName[index]}");
        Debug.Log($"Previous scene: {previousSceneName}");
        Debug.Log(GlobalVariable.ready_To_Nav_New_Scene);
        if (recentSceneName[index] != previousSceneName)
        {
            foreach (var button in listButton)
            {
                button.interactable = false;
            }
            if (index > 3)
            {
                GlobalVariable.ready_To_Nav_New_Scene = true;
            }
            // Gọi coroutine WaitAndNavigate để di chuyển đến cảnh mới
            yield return Scene_Manager.Instance.WaitAndNavigate(recentSceneName[index], previousSceneName);
        }  // Vô hiệu hóa tất cả button
       ;
    }
}
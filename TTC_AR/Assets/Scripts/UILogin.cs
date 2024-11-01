using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public TMPro.TMP_InputField userNameField;
    public TMPro.TMP_InputField passwordField;
    public Button loginButton;
    public string targetSceneName;

    // Dictionary lưu thông tin tài khoản
    private readonly Dictionary<string, string> staffAccounts = new Dictionary<string, string>
    {
        {"ttc", "123456"},
        {"admin", "123456"},
        {"Nhut", "123456"}
    };

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (GlobalVariable.loginSuccess == true && !string.IsNullOrWhiteSpace(GlobalVariable.accountModel.userName))
        {
            userNameField.text = GlobalVariable.accountModel.userName;
            passwordField.text = GlobalVariable.accountModel.password;
        }
    }

    private void Start()
    {
        if (UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI)
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        loginButton.onClick.AddListener(HandleLogin);
    }

    private void HandleLogin()
    {
        string userName = userNameField.text;
        string password = passwordField.text;

        // Xác thực thông tin đăng nhập
        if (staffAccounts.TryGetValue(userName, out string foundPassword) && foundPassword == password)
        {
            // Cập nhật thông tin tài khoản
            GlobalVariable.accountModel.userName = userName;
            GlobalVariable.accountModel.password = password;
            GlobalVariable.recentScene = targetSceneName;
            GlobalVariable.previousScene = "LoginScene";

            StartCoroutine(LoadSceneAsync(targetSceneName));
            GlobalVariable.loginSuccess = true;

        }
        else
        {
            Debug.Log("Invalid username or password");
            Show_Dialog.Instance.ShowToast("failure", "Sai tên đăng nhập hoặc mật khẩu", 1);
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Show_Dialog.Instance.ShowToast("loading", "Đang đăng nhập...", 1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        PlayerPrefs.SetString(targetSceneName, SceneManager.GetActiveScene().name);
    }
}

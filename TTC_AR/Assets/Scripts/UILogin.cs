using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public TMPro.TMP_InputField userNameField;
    public TMPro.TMP_InputField passwordField;
    public Button loginButton;
    public string targetSceneName;
    string userName;
    string password;

    // Dictionary lưu thông tin tài khoản
    private readonly Dictionary<string, string> staffAccounts = new Dictionary<string, string>
    {   
        {"ttc", "123456"},
        {"admin", "123456"},
        {"Nhut", "123456"}
    };

    private void Awake()
    {
        if (Screen.orientation != ScreenOrientation.Portrait) Screen.orientation = ScreenOrientation.Portrait;
        if (GlobalVariable.loginSuccess && !string.IsNullOrWhiteSpace(GlobalVariable.accountModel.userName))
        {
            userNameField.text = GlobalVariable.accountModel.userName;
            passwordField.text = GlobalVariable.accountModel.password;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            Application.Quit();
        }
    }
    private void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
    }

    private void HandleLogin()
    {
        string userName = userNameField.text;
        string password = passwordField.text;
        {
            if (staffAccounts.TryGetValue(userName, out string foundPassword) && foundPassword == password)
            {
                StartCoroutine(Show_Toast_loading());
            }
            else
            {
                StartCoroutine(Show_Toast_False());
            }
        }
    }
    IEnumerator Show_Toast_loading()
    {
        GlobalVariable.accountModel.userName = userName;
        GlobalVariable.accountModel.password = password;
        GlobalVariable.recentScene = targetSceneName;
        GlobalVariable.previousScene = "LoginScene";

        yield return LoadSceneAsync(targetSceneName);
        GlobalVariable.loginSuccess = true;
    }

    IEnumerator Show_Toast_False()
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("failure", "Sai tên đăng nhập hoặc mật khẩu!");
        yield return new WaitForSeconds(1f);
        Debug.Log("Sai tên đăng nhập hoặc mật khẩu!");
        yield return Show_Dialog.Instance.Set_Instance_Status_False();
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("loading", "Đang đăng nhập...");
        yield return Show_Dialog.Instance.Set_Instance_Status_False();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        GlobalVariable.ready_To_Nav_New_Scene = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        PlayerPrefs.SetString(targetSceneName, SceneManager.GetActiveScene().name);
    }
}

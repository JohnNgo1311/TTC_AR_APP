using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login_Btn_On_Click : MonoBehaviour
{
    [SerializeField] private TMP_InputField userNameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Button loginButton;
    [SerializeField] private string targetSceneName;

    private string userName;
    private string password;

    private readonly Dictionary<string, string> staffAccounts = new Dictionary<string, string>
    {
        {"ttc", "123456"},
        {"admin", "123456"},
        {"nhut", "123456"},
        {"my", "123456"}
    };

    private void Awake()
    {
        SetScreenOrientation();
        PopulateFieldsIfLoggedIn();
    }

    private void SetScreenOrientation()
    {
        if (Screen.orientation != ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    private void PopulateFieldsIfLoggedIn()
    {
        if (GlobalVariable.loginSuccess && !string.IsNullOrWhiteSpace(GlobalVariable.accountModel.userName))
        {
            userNameField.text = GlobalVariable.accountModel.userName;
            passwordField.text = GlobalVariable.accountModel.password;
        }
    }

    private void Update()
    {
        CheckForExitInput();
    }

    private void CheckForExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
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
        userName = userNameField.text;
        password = passwordField.text;

        if (staffAccounts.TryGetValue(userName.ToLower(), out string foundPassword) && foundPassword == password)
        {
            StartCoroutine(ShowToastLoading());
        }
        else
        {
            StartCoroutine(ShowToastFailure());
        }
    }

    private IEnumerator ShowToastLoading()
    {
        UpdateGlobalVariables();
        yield return LoadSceneAsync(targetSceneName);
    }

    private void UpdateGlobalVariables()
    {
        GlobalVariable.accountModel.userName = userName;
        GlobalVariable.accountModel.password = password;
        GlobalVariable.recentScene = targetSceneName;
        GlobalVariable.previousScene = "LoginScene";
        GlobalVariable.loginSuccess = true;
    }

    private IEnumerator ShowToastFailure()
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("failure", "Sai tên đăng nhập hoặc mật khẩu!");
        yield return new WaitForSeconds(1f);
        Debug.Log("Sai tên đăng nhập hoặc mật khẩu!");
        GlobalVariable.loginSuccess = false;
        yield return Show_Dialog.Instance.Set_Instance_Status_False();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Show_Dialog.Instance.Set_Instance_Status_True();
        Show_Dialog.Instance.ShowToast("loading", "Đang đăng nhập...");
        GlobalVariable.ready_To_Nav_New_Scene = true;
        yield return Show_Dialog.Instance.Set_Instance_Status_False();

        SceneManager.LoadScene(sceneName);
    }
}

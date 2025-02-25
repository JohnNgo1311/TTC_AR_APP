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

    private string userName = "";
    private string password = "";

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
    private void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
    }
    private void Update()
    {
        CheckForExitInput();
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


    private void CheckForExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            Application.Quit();
        }
    }



    private void HandleLogin()
    {
        userName = userNameField.text;
        password = passwordField.text;

        if (staffAccounts.TryGetValue(userName.ToLower(), out string foundPassword) && foundPassword == password)
        {
            StartCoroutine(HandleLoginSuccess());
        }
        else
        {
            StartCoroutine(HandleLoginFailure());
        }
    }

    private IEnumerator HandleLoginSuccess()
    {
        UpdateGlobalVariables();
        yield return LoadSceneAsync(targetSceneName);
    }

    private void UpdateGlobalVariables()
    {
        GlobalVariable.accountModel.userName = userName;
        GlobalVariable.accountModel.password = password;
        GlobalVariable.recentScene = targetSceneName;
        GlobalVariable.previousScene = MyEnum.LoginScene.GetDescription();
        GlobalVariable.loginSuccess = true;
    }

    private IEnumerator HandleLoginFailure()
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        Show_Toast.Instance.ShowToast("failure", "Sai tên đăng nhập hoặc mật khẩu!");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Sai tên đăng nhập hoặc mật khẩu!");
        GlobalVariable.loginSuccess = false;
        yield return Show_Toast.Instance.Set_Instance_Status_False();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Show_Toast.Instance.Set_Instance_Status_True();
        Show_Toast.Instance.ShowToast("loading", "Đang đăng nhập...");
        GlobalVariable.ready_To_Nav_New_Scene = true;
        yield return Show_Toast.Instance.Set_Instance_Status_False();

        SceneManager.LoadScene(sceneName);
    }
}

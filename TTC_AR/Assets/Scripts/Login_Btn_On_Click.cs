using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EasyUI.Progress;
using System.Threading.Tasks;

public class Login_Btn_On_Click : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField userNameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Button loginButton;
    [SerializeField] private string targetSceneName;

    private string userName = "";
    private string password = "";

    private readonly Dictionary<string, string> staffAccounts = new()
    {
        {"ttc", "123456"},
        {"admin", "123456"},
        {"nhut", "123456"},
        {"my", "123456"}
    };

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        PopulateFieldsIfLoggedIn();
    }

    private void OnEnable()
    {
        GlobalVariable.APIRequestType.Clear();
        StopAllCoroutines();
        loginButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
    }

    private void Update()
    {
        HandleExitInput();
    }

    private void PopulateFieldsIfLoggedIn()
    {
        if (GlobalVariable.loginSuccess &&
            !string.IsNullOrWhiteSpace(GlobalVariable.accountModel.userName) &&
            !string.IsNullOrWhiteSpace(GlobalVariable.accountModel.password))
        {
            userNameField.text = GlobalVariable.accountModel.userName;
            passwordField.text = GlobalVariable.accountModel.password;
        }
    }

    private void HandleExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Gamepad.current?.buttonEast?.wasPressedThisFrame == true ||
            Keyboard.current?.escapeKey.wasPressedThisFrame == true)
        {
            Application.Quit();
        }
    }

    private void HandleLogin()
    {
        userName = userNameField.text.Trim();
        password = passwordField.text.Trim();

        if (staffAccounts.TryGetValue(userName.ToLower(), out string correctPassword) &&
            password == correctPassword)
        {
            StartCoroutine(LoginProcess());
        }
        else
        {
            StartCoroutine(ShowLoginFailure("Tên đăng nhập hoặc mật khẩu không chính xác!"));
        }
    }

    private IEnumerator LoginProcess()
    {
        ShowProgressBar("Đang đăng nhập...");

        //!
        GlobalVariable.APIRequestType.Add("GET_Company");
        GlobalVariable.APIRequestType.Add("GET_Grapper_List");

        var task1 = ManagerLocator.Instance.CompanyManager._ICompanyService.GetCompanyByIdAsync(1);
        var task2 = ManagerLocator.Instance.GrapperManager._IGrapperService.GetListGrapperAsync(1);


        yield return new WaitUntil(() => Task.WhenAll(task1, task2).IsCompleted);

        GlobalVariable.APIRequestType.Remove("GET_Company");
        GlobalVariable.APIRequestType.Remove("GET_Grapper_List");

        if (task1.IsFaulted || task2.IsFaulted)
        {
            HideProgressBar();
            yield return ShowLoginFailure("Tải dữ liệu thất bại");
            yield break;
        }
        //!

        SetLoginSuccessData();
        yield return new WaitForSeconds(0.5f);
        HideProgressBar();
        yield return SceneManager.LoadSceneAsync(targetSceneName);
    }

    private IEnumerator ShowLoginFailure(string message)
    {
        Show_Toast.Instance.ShowToast("failure", message);
        GlobalVariable.loginSuccess = false;
        yield return new WaitForSeconds(0.25f);
        yield return Show_Toast.Instance.Set_Instance_Status_False();
        HideProgressBar();
    }
    private void SetLoginSuccessData()
    {
        GlobalVariable.accountModel.userName = userName;
        GlobalVariable.accountModel.password = password;
        GlobalVariable.ready_To_Nav_New_Scene = true;
        GlobalVariable.recentScene = targetSceneName;
        GlobalVariable.previousScene = MyEnum.LoginScene.GetDescription();
        GlobalVariable.loginSuccess = true;
    }

    private void ShowProgressBar(string title)
    {
        Progress.Show(title, ProgressColor.Blue, true);
        // Progress.SetDetailsText(details);
    }

    private void HideProgressBar()
    {
        Progress.Hide();
    }
}

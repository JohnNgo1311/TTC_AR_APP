using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Nav_Scene_Back : MonoBehaviour
{
    public string previousSceneName;
    public string recentSceneName;
    [SerializeField]
    private bool isOrientation = false;

    private void Awake()
    {
        Scene_Manager.Instance.SetScreenOrientation(isOrientation);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            NavigatePop();
        }
    }

    public void NavigatePop()
    {
        GlobalVariable.ready_To_Nav_New_Scene = true;
        Scene_Manager.Instance.NavigateToScene(recentSceneName, previousSceneName);
    }
}

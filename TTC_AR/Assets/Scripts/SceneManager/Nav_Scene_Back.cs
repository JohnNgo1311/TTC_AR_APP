using UnityEngine;

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

    public void NavigatePop()
    {
        Scene_Manager.Instance.NavigateToScene(recentSceneName, previousSceneName);
    }
}

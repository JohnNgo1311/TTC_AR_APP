
using UnityEngine;
using UnityEngine.UI;
public class Active_AR_Camera : MonoBehaviour
{
    [SerializeField]
    private Button active_ARCamera_Button;
    [SerializeField]
    private Button deactive_ARCamera_Button;

    private void Start()
    {
        AddListenerBtn();
    }

    private void Destroy()
    {
        RemoveListenerBtn();
    }

    private void AddListenerBtn()
    {
        active_ARCamera_Button.onClick.AddListener(ResumeCamera);
        deactive_ARCamera_Button.onClick.AddListener(PauseCamera);
    }

    private void RemoveListenerBtn()
    {
        active_ARCamera_Button.onClick.RemoveAllListeners();
        deactive_ARCamera_Button.onClick.RemoveAllListeners();
    }

    private void ResumeCamera()
    {
        GlobalVariable.isCameraPaused = false;
        deactive_ARCamera_Button.gameObject.SetActive(true);
        active_ARCamera_Button.gameObject.SetActive(false);
    }

    private void PauseCamera()
    {
        GlobalVariable.isCameraPaused = true;
        deactive_ARCamera_Button.gameObject.SetActive(false);
        active_ARCamera_Button.gameObject.SetActive(true);
    }

}
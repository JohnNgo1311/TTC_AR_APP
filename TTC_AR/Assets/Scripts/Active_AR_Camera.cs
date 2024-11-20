using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;
public class Active_AR_Camera : MonoBehaviour
{
    public UnityEngine.UI.Image image;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private List<Button> active_ARCamera_Buttons = new List<Button>() { };
    private VuforiaBehaviour vuforiaBehaviour;
    void Awake()
    {
        vuforiaBehaviour = mainCamera.gameObject.GetComponent<VuforiaBehaviour>();
    }
    private void Start()
    {
        if (!active_ARCamera_Buttons[0].gameObject.activeSelf) active_ARCamera_Buttons[0].gameObject.SetActive(true);
        if (active_ARCamera_Buttons[1].gameObject.activeSelf) active_ARCamera_Buttons[1].gameObject.SetActive(false);
        active_ARCamera_Buttons[0].onClick.AddListener(PauseARCamera);
        active_ARCamera_Buttons[1].onClick.AddListener(ResumeARCamera);
    }
    private void OnDestroy()
    {
        active_ARCamera_Buttons[0].onClick.RemoveListener(PauseARCamera);
        active_ARCamera_Buttons[1].onClick.RemoveListener(ResumeARCamera);
    }
    public void PauseARCamera()
    {
        // Chạy chụp ảnh màn hình trong một Coroutine để tránh lag
        StartCoroutine(TakeScreenshotCoroutine());
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = false;  // Tắt AR Camera
            image.gameObject.SetActive(true);
            active_ARCamera_Buttons[0].gameObject.SetActive(false);
            active_ARCamera_Buttons[1].gameObject.SetActive(true);
        }
    }

    public void ResumeARCamera()
    {
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = true; // Bật AR Camera
            image.gameObject.SetActive(false);
            active_ARCamera_Buttons[0].gameObject.SetActive(true);
            active_ARCamera_Buttons[1].gameObject.SetActive(false);
        }
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        // Tạo RenderTexture một lần và tái sử dụng nếu có thể
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = renderTexture;

        // Lưu lại culling mask ban đầu để khôi phục sau này
        int originalCullingMask = mainCamera.cullingMask;
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("BackBox") |
             1 << LayerMask.NameToLayer("BackBox_Content") |
             1 << LayerMask.NameToLayer("Canvas_Frame"));

        // Render và chờ kết quả
        mainCamera.Render();

        // Cập nhật sprite của hình ảnh khi render hoàn tất
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Khôi phục culling mask và giải phóng tài nguyên
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        // Khôi phục lại culling mask ban đầu
        mainCamera.cullingMask = originalCullingMask;
        // Gán ảnh vào image
        image.sprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        yield return null;  // Đảm bảo coroutine hoàn tất trong frame tiếp theo
    }

}
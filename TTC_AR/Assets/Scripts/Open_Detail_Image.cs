using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Open_Detail_Image : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject detail_Image;
    [SerializeField]
    private Button close_Button;
    private Vector3 originalPosition_Detail_Image_Content;
    //    private RectTransform originalRectTransform_Detail_Image;
    void Awake()
    {
        canvas = GameObject.Find("Overlay_Canvas_To_Watch_Image").GetComponent<Canvas>();
        detail_Image = canvas.gameObject.transform.Find("Content/Detail_Image_To_Watch").gameObject;
        close_Button = canvas.gameObject.transform.Find("Back_Button_From_Detail_Panel").GetComponent<Button>();
        originalPosition_Detail_Image_Content = detail_Image.transform.parent.gameObject.transform.position;
        if (canvas.gameObject.activeSelf)
        {
            if (!detail_Image.activeSelf)
            {
                detail_Image.SetActive(true);
            }
            canvas.gameObject.SetActive(false);
        }
        //  originalRectTransform_Detail_Image = detail_Image.GetComponent<RectTransform>();
    }
    void Start()
    {
        close_Button.onClick.AddListener(Close_Detail_Canvas);

    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.buttonEast != null && Gamepad.current.buttonEast.wasPressedThisFrame) || (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
        // {
        //     Close_Detail_Canvas();
        // }
    }

    public void Open_Detail_Canvas(Image image_To_Watch_Detail)
    {
        StartCoroutine(Resize_Gameobject_Function.Set_NativeSize_For_GameObject(detail_Image.GetComponent<Image>()));
        if (!canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(true);
        }
        detail_Image.GetComponent<Image>().sprite = image_To_Watch_Detail.sprite;
    }

    public void Close_Detail_Canvas()
    {
        if (canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }
        detail_Image.transform.parent.gameObject.transform.position = originalPosition_Detail_Image_Content;
    }
    private void OnDestroy()
    {
        close_Button.onClick.RemoveListener(Close_Detail_Canvas);
    }
}

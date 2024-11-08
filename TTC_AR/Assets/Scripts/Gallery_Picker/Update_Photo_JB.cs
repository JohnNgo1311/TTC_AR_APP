using UnityEngine;
using UnityEngine.UI;

public class Update_Photo_JB : MonoBehaviour
{
    public Image image_To_Update;
    public Button camera_Option_Btn;
    public Button gallery_Option_Btn;

    void Start()
    {
        image_To_Update = image_To_Update ?? gameObject.transform.GetChild(0).GetComponent<Image>();
        camera_Option_Btn = camera_Option_Btn ?? gameObject.transform.Find("Options_Group/Camera_Option").GetComponent<Button>();
        gallery_Option_Btn = gallery_Option_Btn ?? gameObject.transform.Find("Options_Group/Gallery_Option").GetComponent<Button>();
    }
    public void UpdateImage(Texture2D savedPhoto)
    {
        if (savedPhoto != null)
        {
            image_To_Update.sprite = Sprite.Create(savedPhoto, new Rect(0, 0, savedPhoto.width, savedPhoto.height), new Vector2(0.5f, 0.5f));
        }
    }

    public void Open_Camera_To_Take_Photo()
    {
        WebCamPhotoCamera.Instance.Start_Camera_To_Take_Photo(this);
    }
}

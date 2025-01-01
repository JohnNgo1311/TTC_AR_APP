using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGrapperOnClick : MonoBehaviour
{
    public string grapperName;
    public Button onClickButton;
    [SerializeField] private EventPublisher eventPublisher; // Tham chiếu đến Publisher

    private void OnEnable()
    {
        // Đăng ký chỉ SelectGrapperOnClick() cho nút
        onClickButton.onClick.AddListener(HandleButtonClick);
    }
    private void HandleButtonClick()
    {
        // Thực hiện SelectGrapperOnClick trước
        SelectGrapperOnClick();
        // Sau khi hoàn thành, gọi TriggerEvent()
        eventPublisher.TriggerEvent_ButtonClicked();
    }
    private void SelectGrapperOnClick()
    {
        foreach (var grapper in GlobalVariable.temp_List_Grapper_General_Models)
        {
            if (grapper.Name == grapperName)
            {
                GlobalVariable.temp_Grapper_General_Model = grapper;
                break; // Dừng vòng lặp sau khi tìm thấy
            }
        }
    }

    private void OnDisable()
    {
        // Gỡ bỏ tất cả listener khi đối tượng bị disable
        onClickButton.onClick.RemoveAllListeners();
    }
}

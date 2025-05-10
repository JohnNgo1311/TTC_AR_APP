using System.Collections.Generic;

public interface ISearchDeviceAndJBView
{
    void ShowLoading(string title = "Loading...");
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayJBInfor(JBInformationModel model);
    void DisplayDevice(DeviceInformationModel model);
    void SetInit();
    void DisplayCreateResult(bool success);

}
using System.Collections.Generic;

public interface IFieldDeviceView
{
    void ShowLoading(string title = "Đang tải dữ liệu");
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<FieldDeviceInformationModel> models);
    void DisplayDetail(FieldDeviceInformationModel model);
    void DisplayCreateResult(bool success);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
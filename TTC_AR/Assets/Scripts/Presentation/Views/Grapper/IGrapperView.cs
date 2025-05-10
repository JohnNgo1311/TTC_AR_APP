using System.Collections.Generic;

public interface IGrapperView
{
    void ShowLoading(string title = "Loading...");
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<GrapperInformationModel> models);
    void DisplayDetail(GrapperInformationModel model);
    void DisplayCreateResult(bool success);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
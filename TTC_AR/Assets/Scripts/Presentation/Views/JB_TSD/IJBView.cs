using System.Collections.Generic;

public interface IJBView
{
    void ShowLoading(string title = "Loading...");
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<JBInformationModel> models);
    void DisplayDetail(JBInformationModel model);
    void DisplayCreateResult(bool success);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
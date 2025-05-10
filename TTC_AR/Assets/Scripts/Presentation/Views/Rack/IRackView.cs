using System.Collections.Generic;

public interface IRackView
{
    void ShowLoading(string title = "Loading...");
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<RackInformationModel> models);
    void DisplayDetail(RackInformationModel model);
    void DisplayCreateResult(bool success);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
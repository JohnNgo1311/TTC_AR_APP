using System.Collections.Generic;

public interface IModuleSpecificationView
{
    void ShowLoading();
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<ModuleSpecificationModel> models);
    void DisplayDetail(ModuleSpecificationModel model);
    void DisplayCreateResult(bool success);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
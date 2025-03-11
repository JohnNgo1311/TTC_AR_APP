using System.Collections.Generic;

public interface IImageView
{
    void ShowLoading();
    void HideLoading();
    void ShowError(string message);
    void ShowSuccess();
    void DisplayList(List<ImageInformationModel> models);
    void DisplayDetail(ImageInformationModel model);
    void DisplayUpdateResult(bool success);
    void DisplayDeleteResult(bool success);
}
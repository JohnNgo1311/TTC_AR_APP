using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Interfaces;

public class ImagePresenter
{
    private readonly IImageView _view;
    private readonly IImageService _service;

    public ImagePresenter(
        IImageView view,
        IImageService service)
    {
        _view = view;
        _service = service;
    }

    public async void LoadListImage(string grapperId)
    {
        GlobalVariable.APIRequestType = "GET_Image_List";
        _view.ShowLoading();
        try
        {
            var ImageBasicDto = await _service.GetListImageAsync(grapperId);
            if (ImageBasicDto != null)
            {
                if (ImageBasicDto.Count > 0)
                {
                    var models = ImageBasicDto.Select(dto => ConvertFromBasicDto(dto)).ToList();

                    _view.DisplayList(models);

                }
                else
                {
                    var models = new List<ImageInformationModel>();
                    _view.DisplayList(models);
                }
                _view.ShowSuccess();

            }
            else
            {
                _view.ShowError("No Images found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
            UnityEngine.Debug.Log("Error: " + ex.Message);
        }
        finally
        {
            _view.HideLoading();
        }
    }

    public async void LoadDetailById(string ImageId)
    {
        GlobalVariable.APIRequestType = "GET_Image";
        _view.ShowLoading();
        try
        {
            var dto = await _service.GetImageByIdAsync(ImageId.ToString());
            if (dto != null)
            {
                var model = ConvertFromResponseDto(dto);
                _view.DisplayDetail(model);
                _view.ShowSuccess();
            }
            else
            {
                _view.ShowError("Image not found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
        }
    }
    public async void CreateNewImage(string grapperId, ImageInformationModel model)
    {
        GlobalVariable.APIRequestType = "POST_Image";
        _view.ShowLoading();
        try
        {
            var dto = ConvertToRequestDto(model);
            var result = await _service.CreateNewImageAsync(grapperId, dto);

            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Create New Image failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
        }
    }
    public async void DeleteImage(string ImageId)
    {
        GlobalVariable.APIRequestType = "DELETE_Image";
        _view.ShowLoading();
        try
        {
            var result = await _service.DeleteImageAsync(ImageId);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Delete Image failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();

        }
    }


    //! Dto => Model
    private ImageInformationModel ConvertFromResponseDto(ImageResponseDto dto)
    {
        return new ImageInformationModel(
            id: dto.Id,
            name: dto.Name,
           url: dto.Url
        );
    }
    private ImageInformationModel ConvertFromBasicDto(ImageBasicDto dto)
    {
        return new ImageInformationModel(
            id: dto.Id,
            name: dto.Name
        );
    }

    //! Model => Dto
    private ImageRequestDto ConvertToRequestDto(ImageInformationModel model)
    {
        return new ImageRequestDto(
            name: model.Name,
            byteString: new byte[0]


        )
       ;
    }
}
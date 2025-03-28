using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Interfaces;
using UnityEngine;
using UnityEngine.UI;

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
        GlobalVariable.APIRequestType.Add("GET_Image_List");
        _view.ShowLoading("Đang tải dữ liệu...");
        try
        {
            var ImageBasicDto = await _service.GetListImageAsync(grapperId);
            if (ImageBasicDto != null)
            {
                if (ImageBasicDto.Any())
                {
                    var models = ImageBasicDto.Select(dto => ConvertFromBasicDto(dto)).ToList();

                    GlobalVariable.temp_List_ImageInformationModel = models;

                    GlobalVariable.temp_ListImage_Name = models.Select(m => m.Name).ToList();

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
            GlobalVariable.APIRequestType.Remove("GET_Image_List");
        }
    }

    public async void LoadDetailById(string ImageId)
    {
        GlobalVariable.APIRequestType.Add("GET_Image");
        _view.ShowLoading("Đang tải dữ liệu...");

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
            GlobalVariable.APIRequestType.Remove("GET_Image");
        }
    }
    public async void CreateNewImage(string grapperId, ImageInformationModel model)
    {
        GlobalVariable.APIRequestType.Add("POST_Image");
        _view.ShowLoading("Đang thực hiện...");

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
            GlobalVariable.APIRequestType.Remove("POST_Image");
        }
    }
    public async void DeleteImage(string ImageId)
    {
        GlobalVariable.APIRequestType.Add("DELETE_Image");
        _view.ShowLoading("Đang thực hiện...");

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
            GlobalVariable.APIRequestType.Remove("DELETE_Image");
        }
    }
    public async void UploadImageFromGallery(string grapperId, Texture2D image, string fieldName, string fileName, string filePath)
    {
        GlobalVariable.APIRequestType.Add("POST_Image");
        _view.ShowLoading("Đang cập nhật...");
        try
        {
            Debug.Log("Run Presenter");
            var result = await _service.UploadNewImageFromGallery(grapperId, image, fieldName, fileName, filePath);

            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Upload Image failed");

            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");

        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("POST_Image");
        }
    }
    public async void UploadImageFromCamera(string grapperId, Texture2D image, string fieldName, string fileName)
    {
        GlobalVariable.APIRequestType.Add("POST_Image");
        _view.ShowLoading("Đang cập nhật...");
        try

        {
            Debug.Log("Run Presenter");
            Debug.Log("UploadImageFromCamera: " + grapperId + " " + fieldName + " " + fileName);

            var result = await _service.UploadNewImageFromCamera(grapperId, image, fieldName, fileName);

            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Upload Image failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");

        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("POST_Image");
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
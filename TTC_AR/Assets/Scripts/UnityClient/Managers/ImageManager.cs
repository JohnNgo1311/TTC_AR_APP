
using System;
using UnityEngine;
using System.Net.Http;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Interfaces;
using System.Linq;

public class ImageManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp ImageManager 
    //! ImageManager sẽ gọi ImageService thông qua ServiceLocator
    //! ImageService sẽ gọi ImageRepository thông qua ServiceLocator

    #region Services
    public IImageService _IImageService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IImageService = ServiceLocator.Instance.ImageService;
    }
    public async void GetImageList(string companyId)
    {
        try
        {
            var ImageList = await _IImageService.GetListImageAsync(companyId); //! Gọi _IImageService từ Application Layer
            if (ImageList != null && ImageList.Any())
            {
                foreach (var Image in ImageList)
                    Debug.Log($"Image: {Image.Name}");
            }
            else
            {
                Debug.Log("No Images found");
            }
        }
        catch (ArgumentException ex) // Lỗi validation
        {
            Debug.LogError($"Validation error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (HttpRequestException ex) // Lỗi mạng/HTTP
        {
            Debug.LogError($"Network error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (Exception ex) // Lỗi khác
        {
            Debug.LogError($"Unexpected error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
    }

    public async void GetImageById(string ImageId)
    {
        try
        {
            var Image = await _IImageService.GetImageByIdAsync(ImageId); // Gọi Service
            if (Image != null)
            {
                Debug.Log($"Image: {Image.Name}");
            }
            else
            {
                Debug.Log("Image not found");
            }
        }

        catch (ArgumentException ex) // Lỗi validation
        {
            Debug.LogError($"Validation error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (HttpRequestException ex) // Lỗi mạng/HTTP
        {
            Debug.LogError($"Network error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (Exception ex) // Lỗi khác
        {
            Debug.LogError($"Unexpected error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
    }

    public async void CreateNewImage(string companyId, ImageRequestDto ImageRequestDto)
    {
        try
        {
            bool result = await _IImageService.CreateNewImageAsync(companyId, ImageRequestDto);
            Debug.Log(result ? "Image created successfully" : "Failed to create Image");
            //? hiển thị Dialog hoặc showToast tại đây
        }

        catch (ArgumentException ex) // Lỗi validation
        {
            Debug.LogError($"Validation error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây


        }
        catch (HttpRequestException ex) // Lỗi mạng/HTTP
        {
            Debug.LogError($"Network error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (Exception ex) // Lỗi khác
        {
            Debug.LogError($"Unexpected error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
    }

    public async void DeleteImage(string ImageId)
    {
        try
        {
            bool result = await _IImageService.DeleteImageAsync(ImageId);
            Debug.Log(result ? "Image deleted successfully" : "Failed to delete Image");
        }

        catch (ArgumentException ex) // Lỗi validation
        {
            Debug.LogError($"Validation error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (HttpRequestException ex) // Lỗi mạng/HTTP
        {
            Debug.LogError($"Network error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
        catch (Exception ex) // Lỗi khác
        {
            Debug.LogError($"Unexpected error: {ex.Message}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
    }
}
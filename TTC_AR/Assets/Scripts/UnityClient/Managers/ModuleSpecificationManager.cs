
using System;
using UnityEngine;
using System.Net.Http;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Dtos.ModuleSpecification;
using Domain.Entities;


public class ModuleSpecificationManager : MonoBehaviour
{
    //! Tương tác người dùng sẽ gọi trực tiếp ModuleSpecificationManager 
    //! ModuleSpecificationManager sẽ gọi ModuleSpecificationService thông qua ServiceLocator
    //! ModuleSpecificationService sẽ gọi ModuleSpecificationRepository thông qua ServiceLocator
    #region Services
    public IModuleSpecificationService _IModuleSpecificationService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IModuleSpecificationService = ServiceLocator.Instance.ModuleSpecificationService;
    }
    public async void GetModuleSpecificationList(int companyId)
    {
        try
        {
            var ModuleSpecificationList = await _IModuleSpecificationService.GetListModuleSpecificationAsync(companyId); //! Gọi _IModuleSpecificationService từ Application Layer
            if (ModuleSpecificationList != null && ModuleSpecificationList.Count > 0)
            {
                GlobalVariable.temp_ListModuleSpecificationBasicDto = ModuleSpecificationList;

                foreach (var ModuleSpecification in ModuleSpecificationList)
                    Debug.Log($"ModuleSpecification: {ModuleSpecification.Code}");
            }
            else
            {
                Debug.Log("No ModuleSpecifications found");
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

    public async void GetModuleSpecificationById(int ModuleSpecificationId)
    {
        try
        {
            var ModuleSpecification = await _IModuleSpecificationService.GetModuleSpecificationByIdAsync(ModuleSpecificationId.ToString()); // Gọi Service
            if (ModuleSpecification != null)
            {
                Debug.Log($"ModuleSpecification: {ModuleSpecification.Code}");
            }
            else
            {
                Debug.Log("ModuleSpecification not found");
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

    public async void CreateNewModuleSpecification(int companyId, ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
    {
        try
        {
            bool result = await _IModuleSpecificationService.CreateNewModuleSpecificationAsync(companyId, ModuleSpecificationRequestDto);
            Debug.Log(result ? "ModuleSpecification created successfully" : "Failed to create ModuleSpecification");
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
    public async void UpdateModuleSpecification(ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
    {
        try
        {
            bool result = await _IModuleSpecificationService.UpdateModuleSpecificationAsync(GlobalVariable.ModuleSpecificationId, ModuleSpecificationRequestDto);
            Debug.Log(result ? "ModuleSpecification updated successfully" : "Failed to update ModuleSpecification");
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
    public async void DeleteModuleSpecification(int ModuleSpecificationId)
    {
        try
        {
            bool result = await _IModuleSpecificationService.DeleteModuleSpecificationAsync(ModuleSpecificationId);
            Debug.Log(result ? "ModuleSpecification deleted successfully" : "Failed to delete ModuleSpecification");
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
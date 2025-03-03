
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Net.Http;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Dtos;

public class ModuleSpecificationManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp ModuleSpecificationManager 
    //! ModuleSpecificationManager sẽ gọi ModuleSpecificationService thông qua ServiceLocator
    //! ModuleSpecificationService sẽ gọi ModuleSpecificationRepository thông qua ServiceLocator

    #region Services
    private IModuleSpecificationService _IModuleSpecificationService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IModuleSpecificationService = ServiceLocator.Instance.ModuleSpecificationService;
    }
    public async void GetModuleSpecificationList(int grapperId)
    {
        try
        {
            var ModuleSpecificationList = await _IModuleSpecificationService.GetListModuleSpecificationAsync(grapperId); //! Gọi _IModuleSpecificationService từ Application Layer
            if (ModuleSpecificationList != null && ModuleSpecificationList.Count > 0)
            {
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
            var ModuleSpecification = await _IModuleSpecificationService.GetModuleSpecificationByIdAsync(ModuleSpecificationId); // Gọi Service
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

    public async void CreateNewModuleSpecification(int grapperId, ModuleSpecificationRequestDto ModuleSpecificationRequestDto)
    {
        try
        {
            bool result = await _IModuleSpecificationService.CreateNewModuleSpecificationAsync(grapperId, ModuleSpecificationRequestDto);
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
            bool result = await _IModuleSpecificationService.UpdateModuleSpecificationAsync(GlobalVariable.GrapperId, ModuleSpecificationRequestDto);
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
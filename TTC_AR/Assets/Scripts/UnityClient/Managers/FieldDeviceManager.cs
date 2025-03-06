
using System;
using UnityEngine;
using System.Linq;
using System.Net.Http;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Dtos.FieldDevice;

public class FieldDeviceManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp FieldDeviceManager 
    //! FieldDeviceManager sẽ gọi FieldDeviceService thông qua ServiceLocator
    //! FieldDeviceService sẽ gọi FieldDeviceRepository thông qua ServiceLocator

    #region Services
    private IFieldDeviceService _IFieldDeviceService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IFieldDeviceService = ServiceLocator.Instance.FieldDeviceService;
    }
    public async void GetFieldDeviceList(int grapperId)
    {
        try
        {
            var FieldDeviceList = await _IFieldDeviceService.GetListFieldDeviceAsync(grapperId); //! Gọi _IFieldDeviceService từ Application Layer
            if (FieldDeviceList != null && FieldDeviceList.Any())
            {
                foreach (var FieldDevice in FieldDeviceList)
                    Debug.Log($"FieldDevice: {FieldDevice.Name}");
            }
            else
            {
                Debug.Log("No FieldDevices found");
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

    public async void GetFieldDeviceById(int FieldDeviceId)
    {
        try
        {
            var FieldDevice = await _IFieldDeviceService.GetFieldDeviceByIdAsync(FieldDeviceId); // Gọi Service
            if (FieldDevice != null)
            {

            }
            else
            {
                Debug.Log("FieldDevice not found");
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

    public async void CreateNewFieldDevice(int grapperId, FieldDeviceRequestDto FieldDeviceRequestDto)
    {
        try
        {
            bool result = await _IFieldDeviceService.CreateNewFieldDeviceAsync(grapperId, FieldDeviceRequestDto);
            Debug.Log(result ? "FieldDevice created successfully" : "Failed to create FieldDevice");
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
    public async void UpdateFieldDevice(FieldDeviceRequestDto FieldDeviceRequestDto)
    {
        try
        {
            bool result = await _IFieldDeviceService.UpdateFieldDeviceAsync(GlobalVariable.GrapperId, FieldDeviceRequestDto);
            Debug.Log(result ? "FieldDevice updated successfully" : "Failed to update FieldDevice");
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
    public async void DeleteFieldDevice(int FieldDeviceId)
    {
        try
        {
            bool result = await _IFieldDeviceService.DeleteFieldDeviceAsync(FieldDeviceId);
            Debug.Log(result ? "FieldDevice deleted successfully" : "Failed to delete FieldDevice");
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
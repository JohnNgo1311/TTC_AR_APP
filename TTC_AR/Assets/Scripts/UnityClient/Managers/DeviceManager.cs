
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
using PimDeWitte.UnityMainThreadDispatcher;
using System.Linq;
using System.Net.Http;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
public class DeviceManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp DeviceManager 
    //! DeviceManager sẽ gọi DeviceService thông qua ServiceLocator
    //! DeviceService sẽ gọi DeviceRepository thông qua ServiceLocator

    #region Services
    private IDeviceService _IDeviceService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IDeviceService = ServiceLocator.Instance.DeviceService;
    }
    public async void GetDeviceList(int grapperId)
    {
        try
        {
            var DeviceList = await _IDeviceService.GetListDeviceAsync(grapperId); //! Gọi _IDeviceService từ Application Layer
            if (DeviceList != null && DeviceList.Count > 0)
            {
                foreach (var Device in DeviceList)
                    Debug.Log($"Device: {Device.Code}");
            }
            else
            {
                Debug.Log("No Devices found");
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

    public async void GetDeviceById(int DeviceId)
    {
        try
        {
            var Device = await _IDeviceService.GetDeviceByIdAsync(DeviceId); // Gọi Service
            if (Device != null)
            {
                Debug.Log($"Device: {Device.Code}");
            }
            else
            {
                Debug.Log("Device not found");
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

    public async void CreateNewDevice(int grapperId, DeviceRequestDto DeviceRequestDto)
    {
        try
        {
            bool result = await _IDeviceService.CreateNewDeviceAsync(grapperId, DeviceRequestDto);
            Debug.Log(result ? "Device created successfully" : "Failed to create Device");
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
    public async void UpdateDevice(DeviceRequestDto DeviceRequestDto)
    {
        try
        {
            bool result = await _IDeviceService.UpdateDeviceAsync(GlobalVariable.GrapperId, DeviceRequestDto);
            Debug.Log(result ? "Device updated successfully" : "Failed to update Device");
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
    public async void DeleteDevice(int DeviceId)
    {
        try
        {
            bool result = await _IDeviceService.DeleteDeviceAsync(DeviceId);
            Debug.Log(result ? "Device deleted successfully" : "Failed to delete Device");
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

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
using ApplicationLayer.Dtos.Rack;

public class RackManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp RackManager 
    //! RackManager sẽ gọi RackService thông qua ServiceLocator
    //! RackService sẽ gọi RackRepository thông qua ServiceLocator

    #region Services
    private IRackService _IRackService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IRackService = ServiceLocator.Instance.RackService;
    }
    public async void GetRackList(int RackId)
    {
        try
        {
            var RackList = await _IRackService.GetListRackAsync(RackId); //! Gọi _IRackService từ Application Layer
            if (RackList != null && RackList.Count > 0)
            {
                foreach (var Rack in RackList)
                    Debug.Log($"Rack: {Rack.Name}");
            }
            else
            {
                Debug.Log("No Racks found");
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

    public async void GetRackById(int RackId)
    {
        try
        {
            var RackResponseDto = await _IRackService.GetRackByIdAsync(RackId); // Gọi Service
            if (RackResponseDto != null)
            {
                Debug.Log($"Rack: {RackResponseDto.Name}");
            }
            else
            {
                Debug.Log("Rack not found");

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

    public async void CreateNewRack(int RackId, RackRequestDto RackRequestDto)
    {
        try
        {
            bool result = await _IRackService.CreateNewRackAsync(RackId, RackRequestDto);
            Debug.Log(result ? "Rack created successfully" : "Failed to create Rack");
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
    public async void UpdateRack(int RackId, RackRequestDto RackRequestDto)
    {
        RackId = GlobalVariable.RackId;
        try
        {
            bool result = await _IRackService.UpdateRackAsync(RackId, RackRequestDto);
            Debug.Log(result ? "Rack updated successfully" : "Failed to update Rack");
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
    public async void DeleteRack(int RackId)
    {
        RackId = GlobalVariable.RackId;
        try
        {
            bool result = await _IRackService.DeleteRackAsync(RackId);
            Debug.Log(result ? "Rack deleted successfully" : "Failed to delete Rack");
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

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
using ApplicationLayer.Dtos.JB;

public class JBManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp JBManager 
    //! JBManager sẽ gọi JBService thông qua ServiceLocator
    //! JBService sẽ gọi JBRepository thông qua ServiceLocator

    #region Services
    private IJBService _IJBService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _IJBService = ServiceLocator.Instance.JBService;
    }
    public async void GetJBList(int grapperId)
    {
        try
        {
            var jbList = await _IJBService.GetListJBAsync(grapperId); //! Gọi _IJBService từ Application Layer
            if (jbList != null && jbList.Count > 0)
            {
                foreach (var jb in jbList)
                    Debug.Log($"JB: {jb.Name}, Location: {jb.Location}");
            }
            else
            {
                Debug.Log("No JBs found");
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

    public async void GetJBById(int jBId)
    {
        try
        {
            var jBResponseDto = await _IJBService.GetJBByIdAsync(jBId); // Gọi Service
            if (jBResponseDto != null)
            {
                Debug.Log($"jBResponseDto: {jBResponseDto.Name}, Location: {jBResponseDto.Location}");
            }
            else
            {
                Debug.Log("jBResponseDto not found");
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

    public async void CreateNewJB(int grapperId, JBRequestDto jBRequestDto)
    {
        try
        {
            bool result = await _IJBService.CreateNewJBAsync(grapperId, jBRequestDto);
            Debug.Log(result ? "JB created successfully" : "Failed to create JB");
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
    public async void UpdateJB(JBRequestDto jBRequestDto)
    {
        try
        {
            bool result = await _IJBService.UpdateJBAsync(GlobalVariable.GrapperId, jBRequestDto);
            Debug.Log(result ? "JB updated successfully" : "Failed to update JB");
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
    public async void DeleteJB(int jBId)
    {
        try
        {
            bool result = await _IJBService.DeleteJBAsync(jBId);
            Debug.Log(result ? "JB deleted successfully" : "Failed to delete JB");
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
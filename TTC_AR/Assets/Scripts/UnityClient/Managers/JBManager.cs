
using System;
using UnityEngine;
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
    public IJBService _IJBService;
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
                {
                    Debug.Log($"jBResponseDto: {jb.Name}, Location: {jb.Location}");
                    if (jb.OutdoorImageResponseDto != null)
                    {
                        Debug.Log($"OutdoorImage: {jb.OutdoorImageResponseDto.Id}, OutdoorImage: {jb.OutdoorImageResponseDto.Name}, OutdoorImage: {jb.OutdoorImageResponseDto.Url}");

                    }
                    else
                    {
                        Debug.Log("OutdoorImage is null");
                    }
                    if (jb.ConnectionImageResponseDtos != null)
                    {
                        if (jb.ConnectionImageResponseDtos.Count > 0)
                        {
                            foreach (var connectionImage in jb.ConnectionImageResponseDtos)
                            {
                                Debug.Log($"ConnectionImage: {connectionImage.Id}, ConnectionImage: {connectionImage.Name}, ConnectionImage: {connectionImage.Url}");
                            }
                        }
                        else
                        {
                            Debug.Log("List ConnectionImage is empty");
                        }
                    }
                    else
                    {
                        Debug.Log("List ConnectionImage is null");
                    }
                }
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

    public async void GetJBById(int JBId)
    {
        try
        {
            var jBResponseDto = await _IJBService.GetJBByIdAsync(JBId); // Gọi Service
            if (jBResponseDto != null)
            {
                Debug.Log($"jBResponseDto: {jBResponseDto.Name}, Location: {jBResponseDto.Location}");
                if (jBResponseDto.DeviceBasicDtos != null)
                {
                    if (jBResponseDto.DeviceBasicDtos.Count > 0)
                    {
                        foreach (var device in jBResponseDto.DeviceBasicDtos)
                        {
                            Debug.Log($"Device: {device.Id}, Device: {device.Code}");
                        }
                    }
                    else
                    {
                        Debug.Log("List Device is empty");
                    }
                }
                else
                {
                    Debug.Log("List Device is null");
                }

                if (jBResponseDto.ModuleBasicDtos != null)
                {
                    if (jBResponseDto.ModuleBasicDtos.Count > 0)
                    {
                        foreach (var module in jBResponseDto.ModuleBasicDtos)
                        {
                            Debug.Log($"Module: {module.Id}, Module: {module.Name}");
                        }
                    }
                    else
                    {
                        Debug.Log("List Module is empty");
                    }
                }
                else
                {
                    Debug.Log("List Module is null");
                }


                if (jBResponseDto.OutdoorImageResponseDto != null)
                {
                    Debug.Log($"OutdoorImage: {jBResponseDto.OutdoorImageResponseDto.Id}, OutdoorImage: {jBResponseDto.OutdoorImageResponseDto.Name}, OutdoorImage: {jBResponseDto.OutdoorImageResponseDto.Url}");
                }
                else
                {
                    Debug.Log("OutdoorImage is null");
                }


                if (jBResponseDto.ConnectionImageResponseDtos != null)
                {
                    if (jBResponseDto.ConnectionImageResponseDtos.Count > 0)
                    {
                        foreach (var connectionImage in jBResponseDto.ConnectionImageResponseDtos)
                        {
                            Debug.Log($"ConnectionImage: {connectionImage.Id}, ConnectionImage: {connectionImage.Name}, ConnectionImage: {connectionImage.Url}");
                        }
                    }
                    else
                    {
                        Debug.Log("List ConnectionImage is empty");
                    }
                }

                else
                {
                    Debug.Log("List ConnectionImage is null");
                }
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
    public async void UpdateJB(int JBId, JBRequestDto jBRequestDto)
    {
        JBId = GlobalVariable.JBId;
        try
        {
            bool result = await _IJBService.UpdateJBAsync(JBId, jBRequestDto);
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
    public async void DeleteJB(int JBId)
    {
        JBId = GlobalVariable.JBId;
        try
        {
            bool result = await _IJBService.DeleteJBAsync(JBId);
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

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
public class JBManager : MonoBehaviour
{
    #region Services
    private IJBService _IJBService;
    #endregion
    private readonly HttpClient _httpClient = new HttpClient();

    private void Start()
    {
        //! Dependency Injection
        _IJBService = ServiceLocator.Instance.JBService;
    }
    public async void GetJBList(int grapperId)
    {
        try
        {
            var jbList = await _IJBService.GetListJBAsync(grapperId); // Gọi Service
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
        catch (Exception ex)
        {
            Debug.LogError($"Error getting JB list: {ex.Message}");
            // Có thể hiển thị UI thông báo lỗi cho người chơi
        }
    }

    public async void GetJBById(int jBId)
    {
        try
        {
            var jb = await _IJBService.GetJBByIdAsync(jBId); // Gọi Service
            if (jb != null)
            {
                Debug.Log($"JB: {jb.Name}, Location: {jb.Location}");
            }
            else
            {
                Debug.Log("JB not found");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting JB: {ex.Message}");
            // Có thể hiển thị UI thông báo lỗi cho người chơi
        }
    }

    public async void CreateNewJB(int grapperId, JBRequestDto jBRequestDto)
    {
        try
        {
            bool result = await _IJBService.CreateNewJBAsync(grapperId, jBRequestDto);
            Debug.Log(result ? "JB created successfully" : "Failed to create JB");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error creating JB: {ex.Message}");
            // Có thể hiển thị UI thông báo lỗi cho người chơi
        }
    }
    public async void UpdateJB(JBRequestDto jBRequestDto)
    {
        try
        {
            bool result = await _IJBService.UpdateJBAsync(GlobalVariable.GrapperId, jBRequestDto);
            Debug.Log(result ? "JB updated successfully" : "Failed to update JB");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating JB: {ex.Message}");
            // Có thể hiển thị UI thông báo lỗi cho người chơi
        }
    }
    public async void DeleteJB(int jBId)
    {
        try
        {
            bool result = await _IJBService.DeleteJBAsync(jBId);
            Debug.Log(result ? "JB deleted successfully" : "Failed to delete JB");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error deleting JB: {ex.Message}");
            // Có thể hiển thị UI thông báo lỗi cho người chơi
        }
    }
}
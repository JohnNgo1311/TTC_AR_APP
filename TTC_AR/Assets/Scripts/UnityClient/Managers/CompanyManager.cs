
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
using ApplicationLayer.Dtos.Company;

public class CompanyManager : MonoBehaviour
{


    //! Tương tác người dùng sẽ gọi trực tiếp CompanyManager 
    //! CompanyManager sẽ gọi CompanyService thông qua ServiceLocator
    //! CompanyService sẽ gọi CompanyRepository thông qua ServiceLocator

    #region Services
    public ICompanyService _ICompanyService;
    #endregion

    private void Start()
    {
        //! Dependency Injection
        _ICompanyService = ServiceLocator.Instance.CompanyService;
    }
    public async void GetCompanyById(string CompanyId)
    {
        try
        {
            var Company = await _ICompanyService.GetCompanyByIdAsync(CompanyId); // Gọi Service
            if (Company != null)
            {
                Debug.Log($"Company: {Company.Name}");
            }
            else
            {
                Debug.Log("Company not found");
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
            Debug.LogError($"Unexpected error: {ex.Message}, {ex.StackTrace}, {ex.InnerException}, {ex.Source}, {ex.TargetSite}");
            //? hiển thị Dialog hoặc showToast tại đây

        }
    }

}
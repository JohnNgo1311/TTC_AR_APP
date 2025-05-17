using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using Unity.VisualScripting;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Grapper;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Rack;
using System.Threading.Tasks;
using UnityEngine.UI;
using ApplicationLayer.Dtos.Company;
using UnityEngine;
public class LoginPresenter
{
    private readonly ILoginView _view;
    private readonly ICompanyService _companyService;
    private readonly IGrapperService _grapperService;
    private readonly IJBService _jbService;

    public LoginPresenter(
        ILoginView view,
        ICompanyService CompanyService,
        IGrapperService GrapperService,
        IJBService JBService
        )
    {
        _view = view;
        _companyService = CompanyService;
        _grapperService = GrapperService;
        _jbService = JBService;
    }

    //! Get list Login chỉ có Id và Code
    public async void LoginProcess()
    {
        GlobalVariable.APIRequestType.Add("GET_Company");
        GlobalVariable.APIRequestType.Add("GET_Grapper_List");
        GlobalVariable.APIRequestType.Add("GET_JB_List_Information");

        _view.ShowLoading("Đang đăng nhập...");

        try
        {
            var companyTask = _companyService.GetCompanyByIdAsync(1);
            var grapperTask = _grapperService.GetListGrapperAsync(1);
            var jbTask = _jbService.GetListJBInformationAsync(1);

            await Task.WhenAll(companyTask, grapperTask, jbTask);

            var companyDto = companyTask.Result;
            var grapperDtos = grapperTask.Result;
            var jbDtos = jbTask.Result;

            if (companyDto == null || grapperDtos == null || jbDtos == null)
            {
                _view.ShowError("Tải dữ liệu thất bại!");
                return;
            }

            if (companyDto != null)
            {
                var models = ConvertCompanyModelFromDto(companyDto);
                if (models != null)
                {
                    GlobalVariable.temp_CompanyInformationModel = models;
                }
            }
            else
            {
                _view.ShowError("Tải dữ liệu thất bại!");
            }
            if (grapperDtos != null)
            {
                var models = new List<GrapperInformationModel>();
                if (grapperDtos.Any())
                {
                    models = grapperDtos.Select(dto => ConvertGrapperFromResponseDto(dto)).ToList();
                }
                if (models != null)
                {
                    GlobalVariable.temp_List_GrapperInformationModel = models;
                }
            }
            else
            {
                _view.ShowError("Tải dữ liệu thất bại!");

            }

            if (jbDtos != null)
            {
                if (jbDtos.Any())
                {
                    var models = jbDtos.Select(dto => ConvertFromResponseDto(dto)).ToList();
                    GlobalVariable.temp_List_JBInformationModel = models;
                    GlobalVariable.temp_Dictionary_JBInformationModel = models.ToDictionary(m => m.Name, m => m);
                    Debug.Log(GlobalVariable.temp_Dictionary_JBInformationModel.Count);
                }

            }
            else
            {
                _view.ShowError("Tải dữ liệu thất bại!");

            }

            _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
        }
        catch (Exception ex)
        {
            _view.ShowError("Tải dữ liệu thất bại!");
            Debug.LogError("Lỗi khi tải dữ liệu: " + ex.Message);
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_Company");
            GlobalVariable.APIRequestType.Remove("GET_Grapper_List");
            GlobalVariable.APIRequestType.Remove("GET_JB_List_Information");
        }
    }

    private CompanyInformationModel ConvertCompanyModelFromDto(CompanyResponseDto dto)
    {
        return new CompanyInformationModel(
            id: dto.Id,
            name: dto.Name,
            listGrapperInformationModel: dto.GrapperBasicDtos.Select(
                grapper => new GrapperInformationModel(
                    id: grapper.Id,
                    name: grapper.Name
                )).ToList(),
            listModuleSpecificationModel: dto.ModuleSpecificationBasicDtos.Select(
                module => new ModuleSpecificationModel(
                    id: module.Id,
                    code: module.Code
                )).ToList(),
            listAdapterSpecificationModel: dto.AdapterSpecificationBasicDtos.Select(
                adapter => new AdapterSpecificationModel(
                    id: adapter.Id,
                    code: adapter.Code
                )).ToList()
        );
    }

    private JBInformationModel ConvertFromResponseDto(JBGeneralDto dto)
    {
        return new JBInformationModel(
            id: dto.Id,
            name: dto.Name,
            location: string.IsNullOrEmpty(dto.Location) ? "Được ghi chú trên sơ đồ" : dto.Location,
            outdoorImage: dto.OutdoorImageBasicDto != null ? new ImageInformationModel(
                id: dto.OutdoorImageBasicDto.Id,
                name: dto.OutdoorImageBasicDto.Name
            ) : null,
            listConnectionImages: dto.ConnectionImageBasicDtos.Any() ? dto.ConnectionImageBasicDtos.Select(device => new ImageInformationModel(
                id: device.Id,
                name: device.Name
            )).ToList() : new List<ImageInformationModel>()
        );
    }
    //! Dto => Model
    private GrapperInformationModel ConvertGrapperFromResponseDto(GrapperBasicDto dto)
    {
        return new GrapperInformationModel(
            id: dto.Id,
            name: dto.Name);
    }
}

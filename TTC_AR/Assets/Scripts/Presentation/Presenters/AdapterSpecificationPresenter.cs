using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Interfaces;

public class AdapterSpecificationPresenter
{
    private readonly IAdapterSpecificationView _view;
    private readonly IAdapterSpecificationService _service;

    public AdapterSpecificationPresenter(
        IAdapterSpecificationView view,
        IAdapterSpecificationService service)
    {
        _view = view;
        _service = service;
    }

    public async void LoadListAdapterSpecification(string companyId)
    {
        GlobalVariable.APIRequestType.Add("GET_AdapterSpecification_List");
        _view.ShowLoading("Đang tải dữ liệu...");

        try
        {
            var adapterSpecificationBasicDto = await _service.GetListAdapterSpecificationAsync(companyId);
            if (adapterSpecificationBasicDto != null)
            {
                if (adapterSpecificationBasicDto.Count > 0)
                {
                    var models = adapterSpecificationBasicDto.Select(dto => ConvertFromBasicDto(dto)).ToList();

                    _view.DisplayList(models);

                }
                else
                {
                    var models = new List<AdapterSpecificationModel>();
                    _view.DisplayList(models);
                }
                _view.ShowSuccess();

            }
            else
            {
                _view.ShowError("No AdapterSpecifications found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_AdapterSpecification_List");
        }
    }

    public async void LoadDetailById(string adapterId)
    {
        GlobalVariable.APIRequestType.Add("GET_AdapterSpecification");
        _view.ShowLoading("Đang tải dữ liệu...");

        try
        {
            var dto = await _service.GetAdapterSpecificationByIdAsync(adapterId.ToString());
            if (dto != null)
            {
                var model = ConvertFromResponseDto(dto);
                _view.DisplayDetail(model);
                _view.ShowSuccess();
            }
            else
            {
                _view.ShowError("AdapterSpecification not found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_AdapterSpecification");
        }
    }
    public async void CreateNewAdapterSpecification(string companyId, AdapterSpecificationModel model)
    {
        GlobalVariable.APIRequestType.Add("POST_AdapterSpecification");
        _view.ShowLoading("Đang thực hiện...");

        try
        {
            var dto = ConvertToRequestDto(model);
            var result = await _service.CreateNewAdapterSpecificationAsync(companyId, dto);

            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Create New AdapterSpecification failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("POST_AdapterSpecification");
        }
    }


    public async void UpdateAdapterSpecification(string adapterSpecificationId, AdapterSpecificationModel model)
    {
        GlobalVariable.APIRequestType.Add("PUT_AdapterSpecification");
        _view.ShowLoading("Đang thực hiện...");

        try
        {
            var dto = ConvertToRequestDto(model);
            var result = await _service.UpdateAdapterSpecificationAsync(adapterSpecificationId, dto);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Update AdapterSpecification failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("PUT_AdapterSpecification");
        }
    }
    public async void DeleteAdapterSpecification(string adapterSpecificationId)
    {
        GlobalVariable.APIRequestType.Add("DELETE_AdapterSpecification");
        _view.ShowLoading("Đang thực hiện...");

        try
        {
            var result = await _service.DeleteAdapterSpecificationAsync(adapterSpecificationId);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Delete AdapterSpecification failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("DELETE_AdapterSpecification");

        }
    }


    //! Dto => Model
    private AdapterSpecificationModel ConvertFromResponseDto(AdapterSpecificationResponseDto dto)
    {
        return new AdapterSpecificationModel(
            id: dto.Id,
            code: dto.Code,
            type: dto.Type,
            communication: dto.Communication,
            numOfModulesAllowed: dto.NumOfModulesAllowed,
            commSpeed: dto.CommSpeed,
            inputSupply: dto.InputSupply,
            outputSupply: dto.OutputSupply,
            inrushCurrent: dto.InrushCurrent,
            alarm: dto.Alarm,
            note: dto.Note,
            pdfManual: dto.PdfManual
        );
    }
    private AdapterSpecificationModel ConvertFromBasicDto(AdapterSpecificationBasicDto dto)
    {
        return new AdapterSpecificationModel(
            id: dto.Id,
            code: dto.Code
        );
    }

    //! Model => Dto
    private AdapterSpecificationRequestDto ConvertToRequestDto(AdapterSpecificationModel model)
    {
        return new AdapterSpecificationRequestDto(
            code: model.Code,
            type: model.Type,
            communication: model.Communication,
            numOfModulesAllowed: model.NumOfModulesAllowed,
            commSpeed: model.CommSpeed,
            inputSupply: model.InputSupply,
            outputSupply: model.OutputSupply,
            inrushCurrent: model.InrushCurrent,
            alarm: model.Alarm,
            note: model.Note,
            pdfManual: model.PdfManual
        )
       ;
    }
}
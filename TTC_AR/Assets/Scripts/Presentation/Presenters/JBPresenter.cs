using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Dtos.FieldDevice;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Module;
using ApplicationLayer.Dtos.Device;

public class JBPresenter
{
    private readonly IJBView _view;
    private readonly IJBService _service;

    public JBPresenter(
        IJBView view,
        IJBService service)
    {
        _view = view;
        _service = service;
    }


    //! Get list JB nhưng chỉ có Id và Name
    public async void LoadListJBGeneral(string grapperId)
    {
        GlobalVariable.APIRequestType.Add("GET_JB_List_General");

        _view.ShowLoading("Đang tải dữ liệu...");

        try
        {
            var jbBasicDtos = await _service.GetListJBGeneralAsync(grapperId);
            if (jbBasicDtos != null)
            {
                if (jbBasicDtos.Count > 0)
                {
                    var models = jbBasicDtos.Select(dto => ConvertFromBasicDto(dto)).ToList();
                    _view.DisplayList(models);
                }
                else
                {
                    var models = new List<JBInformationModel>();
                    _view.DisplayList(models);
                }
                _view.ShowSuccess();
            }
            else
            {
                _view.ShowError("No JBs found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
            UnityEngine.Debug.Log("Error: " + ex.Message);
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_JB_List_General");
        }
    }

    //! Get list JB nhưng không có list Devices và List Modules
    public async void LoadListJBInformation(string grapperId)
    {
        GlobalVariable.APIRequestType.Add("GET_JB_List_Information");
        _view.ShowLoading("Đang tải dữ liệu...");
        try
        {
            var jbGeneralDtos = await _service.GetListJBInformationAsync(grapperId);
            if (jbGeneralDtos != null)
            {
                if (jbGeneralDtos.Count > 0)
                {
                    var models = jbGeneralDtos.Select(dto => ConvertFromGeneralDto(dto)).ToList();

                    _view.DisplayList(models);

                }
                else
                {
                    var models = new List<JBInformationModel>();
                    _view.DisplayList(models);
                }
                _view.ShowSuccess();

            }
            else
            {
                _view.ShowError("No JBs found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
            UnityEngine.Debug.Log("Error: " + ex.Message);
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_JB_List_Information");
        }
    }

    //! GET JB Detail với đầy đủ thông tin
    public async void LoadDetailById(string JBId)
    {
        GlobalVariable.APIRequestType.Add("GET_JB");
        _view.ShowLoading("Đang tải dữ liệu...");
        try
        {
            var jBResponseDto = await _service.GetJBByIdAsync(JBId.ToString());
            if (jBResponseDto != null)
            {
                var model = ConvertFromResponseDto(jBResponseDto);
                _view.DisplayDetail(model);
                _view.ShowSuccess();
            }
            else
            {
                _view.ShowError("JB not found");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_JB");
        }
    }
    public async void CreateNewJB(string grapperId, JBInformationModel model)
    {
        GlobalVariable.APIRequestType.Add("POST_JB");
        _view.ShowLoading("Đang thực hiện...");
        try
        {
            var dto = ConvertToRequestDto(model);
            var result = await _service.CreateNewJBAsync(grapperId, dto);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Create New JB failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("POST_JB");
        }
    }

    public async void UpdateJB(string JBId, JBInformationModel model)
    {
        GlobalVariable.APIRequestType.Add("PUT_JB");
        _view.ShowLoading("Đang thực hiện...");
        try
        {
            var dto = ConvertToRequestDto(model);
            var result = await _service.UpdateJBAsync(JBId, dto);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Update JB failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("PUT_JB");
        }
    }
    public async void DeleteJB(string JBId)
    {
        GlobalVariable.APIRequestType.Add("DELETE_JB");
        _view.ShowLoading("Đang thực hiện...");
        try
        {
            var result = await _service.DeleteJBAsync(JBId);
            if (result)
            {
                _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
            }
            else
            {
                _view.ShowError("Delete JB failed");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("DELETE_JB");
        }
    }



    //! Dto => Model
    private JBInformationModel ConvertFromResponseDto(JBResponseDto dto)
    {
        return new JBInformationModel(
            id: dto.Id,
            name: dto.Name,
            location: dto.Location,
            listDeviceInformation: dto.DeviceBasicDtos?.Select(deviceDto => new DeviceInformationModel(
                id: deviceDto.Id,
              code: deviceDto.Code
            )).ToList(),

            listModuleInformation: dto.ModuleBasicDtos?.Select(moduleDto => new ModuleInformationModel(
                id: moduleDto.Id,
                name: moduleDto.Name)).ToList(),
            outdoorImage: dto.OutdoorImageResponseDto != null ? new ImageInformationModel(
                 id: dto.OutdoorImageResponseDto.Id,
               name: dto.OutdoorImageResponseDto.Name,
                url: dto.OutdoorImageResponseDto.Url
            ) : null,

            listConnectionImages: dto.ConnectionImageResponseDtos?.Select(imageDto => new ImageInformationModel(
                id: imageDto.Id,
               name: imageDto.Name,
                url: imageDto.Url
            )).ToList()
        );
    }
    private JBInformationModel ConvertFromGeneralDto(JBGeneralDto dto)
    {
        return new JBInformationModel(
            id: dto.Id,
            name: dto.Name,
            location: dto.Location,
            outdoorImage: dto.OutdoorImageResponseDto != null ? new ImageInformationModel(
                 id: dto.OutdoorImageResponseDto.Id,
               name: dto.OutdoorImageResponseDto.Name,
                url: dto.OutdoorImageResponseDto.Url
            ) : null,

            listConnectionImages: dto.ConnectionImageResponseDtos?.Select(imageDto => new ImageInformationModel(
                id: imageDto.Id,
               name: imageDto.Name,
                url: imageDto.Url
            )).ToList()

        );
    }
    private JBInformationModel ConvertFromBasicDto(JBBasicDto dto)
    {
        return new JBInformationModel(
            id: dto.Id,
            name: dto.Name
        );
    }

    //! Model => Dto
    private JBRequestDto ConvertToRequestDto(JBInformationModel model)
    {
        return new JBRequestDto(
            name: model.Name,

            location: model.Location,

            deviceBasicDtos: model.ListDeviceInformation?.Select(deviceModel => new DeviceBasicDto(
                id: deviceModel.Id,
                code: deviceModel.Code
            )).ToList(),

            moduleBasicDtos: model.ListModuleInformation?.Select(moduleModel => new ModuleBasicDto(
                id: moduleModel.Id,
                name: moduleModel.Name
            )).ToList(),

            outdoorImageBasicDto: model.OutdoorImage != null ? new ImageBasicDto(
                id: model.OutdoorImage.Id,
                name: model.OutdoorImage.Name
            ) : null,

            connectionImageBasicDtos: model.ListConnectionImages?.Select(imageModel => new ImageBasicDto(
                id: imageModel.Id,
                name: imageModel.Name
            )).ToList()
        );
    }
}
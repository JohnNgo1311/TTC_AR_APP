using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using Unity.VisualScripting;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Rack;
using System.Threading.Tasks;
using UnityEngine.UI;
public class SearchDeviceAndJBPresenter
{
    private readonly ISearchDeviceAndJBView _view;
    private readonly IJBService _JBService;
    private readonly IDeviceService _DeviceService;

    public SearchDeviceAndJBPresenter(
        ISearchDeviceAndJBView view,
        IJBService jBService,
        IDeviceService deviceService)
    {
        _view = view;
        _JBService = jBService;
        _DeviceService = deviceService;
    }

    //! Get list SearchDeviceAndJB chỉ có Id và Code
    public async void LoadDataForSearching(string grapperId)
    {
        GlobalVariable.APIRequestType.Add("GET_JB_List_Information");
        GlobalVariable.APIRequestType.Add("GET_Device_List_Information_FromGrapper");
        _view.ShowLoading("Đang tải dữ liệu...");
        try
        {
            // UnityEngine.Debug.Log("Run Presenter");

            var jBGeneralDtosTask = _JBService.GetListJBInformationAsync(grapperId);
            // var jBGeneralDtos = await _JBService.GetListJBInformationAsync(grapperId);

            // UnityEngine.Debug.Log("Run Presenter JB");

            var deviceResponseDtosTask = _DeviceService.GetListDeviceInformationFromGrapperAsync(grapperId);
            // var deviceResponseDtos = await _DeviceService.GetListDeviceInformationFromGrapperAsync(grapperId);

            // UnityEngine.Debug.Log("Run Presenter Device");

            await Task.WhenAll(
                jBGeneralDtosTask,
            deviceResponseDtosTask);

            var jBGeneralDtos = await jBGeneralDtosTask;
            var deviceResponseDtos = await deviceResponseDtosTask;

            if (jBGeneralDtos != null)
            {
                var models = new List<JBInformationModel>();
                if (jBGeneralDtos.Any())
                {
                    models = jBGeneralDtos.Select(dto => ConvertJBFromGeneralDto(dto)).ToList();
                }
                else
                {
                    models = new List<JBInformationModel>();
                }
                GlobalVariable_Search_Devices.temp_ListJBInformationModel = models;
            }

            if (deviceResponseDtos != null)
            {
                var models = new List<DeviceInformationModel>();
                if (deviceResponseDtos.Any())
                {
                    models = deviceResponseDtos.Select(dto => ConvertDeviceFromResponseDto(dto)).ToList();
                }
                else
                {
                    models = new List<DeviceInformationModel>();
                }
                GlobalVariable_Search_Devices.temp_ListDeviceInformationModel = models;


            }
            UnityEngine.Debug.Log("Run Presenter Task Successfully");

            _view.ShowSuccess();
            _view.SetInit();

        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("Error: " + ex.Message);
            _view.ShowError($"Error: {ex.Message}");
        }
        finally
        {
            _view.HideLoading();
            GlobalVariable.APIRequestType.Remove("GET_JB_List_Information");
            GlobalVariable.APIRequestType.Remove("GET_Device_List_Information_FromGrapper");
        }
    }

    // public async void LoadImageAsync(string imageName, Image imagePrefab)
    // {
    //     string url = $"{GlobalVariable.baseUrl}files/{imageName}";
    //     await LoadImage.Instance.LoadImageFromUrlAsync(url, imagePrefab);
    // }
    public async Task LoadImageAsync(string name, Image imagePrefab)
    {
        await LoadImage.Instance.LoadImageFromUrlAsync(name, imagePrefab);
    }



    private JBInformationModel ConvertJBFromGeneralDto(JBGeneralDto dto)
    {
        return new JBInformationModel(
            id: dto.Id,
            name: dto.Name,
            location: dto.Location,
            outdoorImage: dto.OutdoorImageBasicDto != null ? new ImageInformationModel(
                 id: dto.OutdoorImageBasicDto.Id,
               name: dto.OutdoorImageBasicDto.Name
            //    ,
            //     url: dto.OutdoorImageBasicDto.Url
            ) : null,

            listConnectionImages: dto.ConnectionImageBasicDtos?.Select(imageDto => new ImageInformationModel(
                id: imageDto.Id,
               name: imageDto.Name
            //    ,
            //     url: imageDto.Url
            )).ToList()

        );
    }
    //! Dto => Model
    private DeviceInformationModel ConvertDeviceFromResponseDto(DeviceResponseDto dto)
    {
        return new DeviceInformationModel(
            id: dto.Id,
            code: dto.Code,
            function: dto.Function,
            range: dto.Range,
            unit: dto.Unit,
            ioAddress: dto.IOAddress,
            jbInformationModels: dto.JBGeneralDtos.Any() ? dto.JBGeneralDtos.Select(jb => new JBInformationModel(
                id: jb.Id,
                name: jb.Name,
            location: jb?.Location,
            outdoorImage: jb.OutdoorImageBasicDto != null ? new ImageInformationModel(
                           id: jb.OutdoorImageBasicDto.Id,
                           name: jb.OutdoorImageBasicDto.Name
                           //    ,
                           //    url: jb.OutdoorImageBasicDto.Ur
                           ) : null,
            listConnectionImages: jb.ConnectionImageBasicDtos.Any() ?
            jb.ConnectionImageBasicDtos.Select(
            connectionImage => new ImageInformationModel(
                                   id: connectionImage.Id,
                                   name: connectionImage.Name
                                   //    ,
                                   //    url: connectionImage.Url
                                   )
            ).ToList() : new List<ImageInformationModel>()
            )).ToList() : new List<JBInformationModel>(),
            moduleInformationModel: dto.ModuleBasicDto != null ? new ModuleInformationModel(
                                        dto.ModuleBasicDto.Id,
                                        dto.ModuleBasicDto.Name
            ) : null,
            additionalConnectionImages: dto.AdditionalImageBasicDtos.Any() ? dto.AdditionalImageBasicDtos?.Select(
                imageDto => new ImageInformationModel(
                                id: imageDto.Id,
                                name: imageDto.Name
                // ,
                // url: imageDto.Url
                )).ToList() : new List<ImageInformationModel>()
            );
    }
}

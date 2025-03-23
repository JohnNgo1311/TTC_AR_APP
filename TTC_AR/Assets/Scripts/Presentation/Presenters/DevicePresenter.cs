// using System;
// using System.Collections.Generic;
// using System.Linq;
// using ApplicationLayer.Dtos.Device;
// using ApplicationLayer.Interfaces;
// using ApplicationLayer.Dtos.Image;
// using ApplicationLayer.Dtos.Module;
// using Unity.VisualScripting;
// using ApplicationLayer.Dtos.JB;
// using System.Diagnostics;
// public class DevicePresenter
// {
//     private readonly IDeviceView _view;
//     private readonly IDeviceService _service;

//     public DevicePresenter(
//         IDeviceView view,
//         IDeviceService service)
//     {
//         _view = view;
//         _service = service;
//     }


//     //! Get list Device nhưng chỉ có Id và Name
//     public async void LoadListDeviceInformationFromGrapper(string grapperId)
//     {
//         GlobalVariable.APIRequestType.Add("GET_Device_List_Information_FromGrapper");
//         _view.ShowLoading("Đang tải dữ liệu...");
//         try
//         {
//             var DeviceResponseDtos = await _service.GetListDeviceInformationFromGrapperAsync(grapperId);
//             if (DeviceResponseDtos != null)
//             {
//                 if (DeviceResponseDtos.Any())
//                 {
//                     var models = DeviceResponseDtos.Select(dto => ConvertFromResponseDto(dto)).ToList();
//                     _view.DisplayList(models);
//                 }
//                 else
//                 {
//                     var models = new List<DeviceInformationModel>();
//                     _view.DisplayList(models);
//                 }
//                 _view.ShowSuccess();
//             }
//             else
//             {
//                 _view.ShowError("No Devices found");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//             UnityEngine.Debug.Log("Error: " + ex.Message);
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("GET_Device_List_Information_FromGrapper");
//         }
//     }


//     //! Get list Device nhưng chỉ có Id và Name
//     public async void LoadListDeviceInformationFromModule(string moduleId)
//     {
//         GlobalVariable.APIRequestType.Add("GET_Device_List_Information_FromModule");
//         _view.ShowLoading("Đang tải dữ liệu...");
//         try
//         {
//             var DeviceResponseDtos = await _service.GetListDeviceInformationFromModuleAsync(moduleId);
//             if (DeviceResponseDtos != null)
//             {
//                 if (DeviceResponseDtos.Any())
//                 {
//                     var models = DeviceResponseDtos.Select(dto => ConvertFromResponseDto(dto)).ToList();
//                     _view.DisplayList(models);
//                 }
//                 else
//                 {
//                     var models = new List<DeviceInformationModel>();
//                     _view.DisplayList(models);
//                 }
//                 _view.ShowSuccess();
//             }
//             else
//             {
//                 _view.ShowError("No Devices found");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//             UnityEngine.Debug.Log("Error: " + ex.Message);
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("GET_Device_List_Information_FromModule");
//         }
//     }

//     //! Get list Device chỉ có Id và Code
//     public async void LoadListDeviceGeneral(string grapperId)
//     {
//         GlobalVariable.APIRequestType.Add("GET_Device_List_General");
//         _view.ShowLoading("Đang tải dữ liệu...");
//         try
//         {
//             var DeviceBasicDtos = await _service.GetListDeviceGeneralAsync(grapperId);
//             if (DeviceBasicDtos != null)
//             {
//                 if (DeviceBasicDtos.Any())
//                 {
//                     var models = DeviceBasicDtos.Select(dto => ConvertFromBasicDto(dto)).ToList();

//                     _view.DisplayList(models);
//                 }
//                 else
//                 {
//                     var models = new List<DeviceInformationModel>();
//                     _view.DisplayList(models);
//                 }
//                 _view.ShowSuccess();
//             }
//             else
//             {
//                 _view.ShowError("No Devices found");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//             UnityEngine.Debug.Log("Error: " + ex.Message);
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("GET_Device_List_General");
//         }
//     }

//     //! GET Device Detail với đầy đủ thông tin
//     public async void LoadDetailById(string DeviceId)
//     {
//         GlobalVariable.APIRequestType.Add("GET_Device");
//         _view.ShowLoading("Đang tải dữ liệu...");
//         try
//         {
//             UnityEngine.Debug.Log("Run Presenter");
//             var DeviceResponseDto = await _service.GetDeviceByIdAsync(DeviceId);
//             if (DeviceResponseDto != null)
//             {
//                 var model = ConvertFromResponseDto(DeviceResponseDto);
//                 if (model != null)
//                 {
//                     UnityEngine.Debug.Log("Get Device Detail Successfully");
//                 }
//                 UnityEngine.Debug.Log(model.Code + model.Id + model.IOAddress);
//                 _view.DisplayDetail(model);
//                 _view.ShowSuccess();
//             }
//             else
//             {
//                 _view.ShowError("Device not found");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("GET_Device");
//         }
//     }
//     public async void CreateNewDevice(string grapperId, DeviceInformationModel model)
//     {
//         GlobalVariable.APIRequestType.Add("POST_Device");
//         _view.ShowLoading("Đang thực hiện...");
//         try
//         {
//             var dto = ConvertToRequestDto(model);

//             var result = await _service.CreateNewDeviceAsync(grapperId, dto);

//             if (result)
//             {
//                 _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
//             }
//             else
//             {
//                 _view.ShowError("Create New Device failed");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("POST_Device");
//         }
//     }

//     public async void UpdateDevice(string DeviceId, DeviceInformationModel model)
//     {
//         GlobalVariable.APIRequestType.Add("PUT_Device");
//         _view.ShowLoading("Đang thực hiện...");
//         try
//         {
//             var dto = ConvertToRequestDto(model);
//             var result = await _service.UpdateDeviceAsync(DeviceId, dto);
//             if (result)
//             {
//                 _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
//             }
//             else
//             {
//                 _view.ShowError("Update Device failed");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("PUT_Device");
//         }
//     }
//     public async void DeleteDevice(string DeviceId)
//     {
//         GlobalVariable.APIRequestType.Add("DELETE_Device");
//         _view.ShowLoading("Đang thực hiện...");
//         try
//         {
//             var result = await _service.DeleteDeviceAsync(DeviceId);
//             if (result)
//             {
//                 _view.ShowSuccess(); // Chỉ hiển thị thành công nếu result == true
//             }
//             else
//             {
//                 _view.ShowError("Delete Device failed");
//             }
//         }
//         catch (Exception ex)
//         {
//             _view.ShowError($"Error: {ex.Message}");
//         }
//         finally
//         {
//             _view.HideLoading();
//             GlobalVariable.APIRequestType.Remove("DELETE_Device");
//         }
//     }



//     //! Dto => Model
//     private DeviceInformationModel ConvertFromResponseDto(DeviceResponseDto dto)
//     {
//         return new DeviceInformationModel(
//             id: dto.Id,
//             code: dto.Code,
//             function: dto.Function,
//             range: dto.Range,
//             unit: dto.Unit,
//             ioAddress: dto.IOAddress,
//             jbInformationModel: dto.JBGeneralDto != null ? new JBInformationModel(
//                 id: dto.JBGeneralDto.Id,
//                 name: dto.JBGeneralDto.Name,
//             location: dto.JBGeneralDto?.Location,
//             outdoorImage: dto.JBGeneralDto.OutdoorImageResponseDto != null ? new ImageInformationModel(
//                            id: dto.JBGeneralDto.OutdoorImageResponseDto.Id,
//                            name: dto.JBGeneralDto.OutdoorImageResponseDto.Name,
//                            url: dto.JBGeneralDto.OutdoorImageResponseDto.Url) : null,
//             listConnectionImages: dto.JBGeneralDto.ConnectionImageResponseDtos.Any() ?
//             dto.JBGeneralDto.ConnectionImageResponseDtos.Select(
//             connectionImage => new ImageInformationModel(
//                                    id: connectionImage.Id,
//                                    name: connectionImage.Name,
//                                    url: connectionImage.Url)
//             ).ToList() : new List<ImageInformationModel>()
//             ) : null,
//             moduleInformationModel: dto.ModuleBasicDto != null ? new ModuleInformationModel(
//                                         dto.ModuleBasicDto.Id,
//                                         dto.ModuleBasicDto.Name
//             ) : null,
//             additionalConnectionImages: dto.AdditionalImageResponseDtos.Any() ? dto.AdditionalImageResponseDtos?.Select(
//                 imageDto => new ImageInformationModel(
//                                 id: imageDto.Id,
//                                 name: imageDto.Name,
//                                 url: imageDto.Url
//                 )).ToList() : new List<ImageInformationModel>()
//             );
//     }
//     private DeviceInformationModel ConvertFromBasicDto(DeviceBasicDto dto)
//     {
//         return new DeviceInformationModel(
//             id: dto.Id,
//            code: dto.Code
//         );
//     }

//     //! Model => Dto
//     private DeviceRequestDto ConvertToRequestDto(DeviceInformationModel model)
//     {
//         return new DeviceRequestDto(
//             code: model.Code,
//             function: model.Function,
//             range: model.Range,
//             unit: model.Unit,
//             ioAddress: model.IOAddress,
//             jbBasicDto: model.JBInformationModel == null ? null : new JBBasicDto(
//                 id: model.JBInformationModel.Id,
//                 name: model.JBInformationModel.Name
//             ),
//             moduleBasicDto: model.ModuleInformationModel == null ? null : new ModuleBasicDto(
//                 id: model.ModuleInformationModel.Id,
//                 name: model.ModuleInformationModel.Name
//             ),
//          additionalImageBasicDtos: model.AdditionalConnectionImages.Any() ? model.AdditionalConnectionImages.Select(
//             imageInfor => new ImageBasicDto(
//                 id: imageInfor.Id,
//                 name: imageInfor.Name
//             )
//          ).ToList() : new List<ImageBasicDto>()

//         );
//     }
// }
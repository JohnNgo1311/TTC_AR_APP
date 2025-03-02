// UseCases/Interfaces/IDeviceUseCase.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDeviceUseCase
{
    Task<List<DeviceInformationModel>> GetListDeviceModel(int grapperId);
    Task<DeviceInformationModel> GetDeviceModel(int DeviceId);
    Task<bool> UpdateDeviceModel(DeviceGeneralModel model);
    Task<bool> AddNewDeviceModel(DevicePostGeneralModel model);
    Task<bool> DeleteDeviceModel(int DeviceId);
}
// UseCases/DeviceUseCase.cs
//! Implement
public class DeviceUseCase : IDeviceUseCase
{
    private readonly IDeviceRepository _repository;

    public DeviceUseCase(IDeviceRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeviceInformationModel> GetDeviceModel(int DeviceId)
    {
        return await _repository.GetDevice(DeviceId);
    }
    public async Task<List<DeviceInformationModel>> GetListDeviceModel(int grapperId)
    {
        return await _repository.GetListDevice(grapperId);
    }
    public async Task<bool> UpdateDeviceModel(DeviceGeneralModel model)
    {
        if (string.IsNullOrEmpty(model.Code))
        {
            throw new ArgumentException("Name cannot be empty");
        }
        return await _repository.UpdateDevice(model);
    }

    public async Task<bool> AddNewDeviceModel(DevicePostGeneralModel model)
    {
        return await _repository.AddDevice(model);
    }
    public async Task<bool> DeleteDeviceModel(int DeviceId)
    {
        return await _repository.DeleteDevice(DeviceId);
    }

}
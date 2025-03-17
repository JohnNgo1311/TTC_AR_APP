// Infrastructure/Interfaces/IDeviceRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IDeviceRepository
{
    Task<List<DeviceInformationModel>> GetListDevice(int grapperId);
    Task<DeviceInformationModel> GetDevice(int DeviceId);
    Task<bool> UpdateDevice(DeviceInformationModel model);
    Task<bool> AddDevice(DevicePostGeneralModel model);
    Task<bool> DeleteDevice(int DeviceId);

}

// Infrastructure/Repositories/DeviceRepository.cs
public class DeviceRepository : IDeviceRepository
{
    private string _apiUrl = "http://52.230.123.204:81/api";

    public async Task<DeviceInformationModel> GetDevice(int DeviceId)
    {
        return await APIManager.Instance.GetDeviceData($"{_apiUrl}/Devices/{DeviceId}");
    }

    public async Task<List<DeviceInformationModel>> GetListDevice(int grapperId)
    {
        return await APIManager.Instance.GetListDeviceData($"{_apiUrl}/Grapper/{grapperId}/Devices");
    }

    public async Task<bool> UpdateDevice(DeviceInformationModel model)
    {
        return await APIManager.Instance.UpdateDeviceDataAsync(model, $"{_apiUrl}");
    }

    public async Task<bool> AddDevice(DevicePostGeneralModel model)
    {
        return await APIManager.Instance.AddNewDeviceAsync(model, $"{_apiUrl}");
    }

    public async Task<bool> DeleteDevice(int DeviceId)
    {
        return await APIManager.Instance.DeleteDeviceData($"{_apiUrl}/Devices/{DeviceId}");
    }

}
// Infrastructure/Interfaces/IModuleRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IModuleRepository
{
    Task<List<ModuleInformationModel>> GetListModule(int grapperId);
    Task<ModuleInformationModel> GetModule(int ModuleId);
    // Task<bool> UpdateModule(ModuleGeneralModel model);
    // Task<bool> AddModule(ModuleGeneralModel model);
}

// Infrastructure/Repositories/ModuleRepository.cs
public class ModuleRepository : IModuleRepository
{
    private string _apiUrl = "http://52.230.123.204:81/api";

    public async Task<ModuleInformationModel> GetModule(int ModuleId)
    {
        return await APIManager.Instance.GetModuleData($"{_apiUrl}/Modules/{ModuleId}");
    }
    public async Task<List<ModuleInformationModel>> GetListModule(int grapperId)
    {
        return await APIManager.Instance.GetListModuleData($"{_apiUrl}/Grapper/{grapperId}/modules");
    }
    // public async Task<bool> UpdateModule(ModuleGeneralModel model)
    // {
    //     return await APIManager.Instance.UpdateModuleDataAsync(model, $"{_apiUrl}");
    // }

    // public async Task<bool> AddModule(ModuleGeneralModel model)
    // {
    //     return await APIManager.Instance.AddNewModuleAsync(model, $"{_apiUrl}");
    // }
}
// Infrastructure/Interfaces/IJBRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IJBRepository
{
    Task<List<JBInformationModel>> GetListJB(int grapperId);
    Task<JBInformationModel> GetJB(int JBId);
    Task<bool> UpdateJB(JBGeneralModel model);
    Task<bool> AddJB(JBPostGeneralModel model);
    Task<bool> DeleteJB(int JBId);

}

// Infrastructure/Repositories/JBRepository.cs
public class JBRepository : IJBRepository
{
    private string _apiUrl = "http://52.230.123.204:81/api";

    public async Task<JBInformationModel> GetJB(int JBId)
    {
        return await APIManager.Instance.GetJBData($"{_apiUrl}/Jbs/{JBId}");
    }
    
    public async Task<List<JBInformationModel>> GetListJB(int grapperId)
    {
        return await APIManager.Instance.GetListJBData($"{_apiUrl}/Grapper/{grapperId}/Jbs");
    }

    public async Task<bool> UpdateJB(JBGeneralModel model)
    {
        return await APIManager.Instance.UpdateJBDataAsync(model, $"{_apiUrl}");
    }

    public async Task<bool> AddJB(JBPostGeneralModel model)
    {
        return await APIManager.Instance.AddNewJBAsync(model, $"{_apiUrl}");
    }

    public async Task<bool> DeleteJB(int JBId)
    {
        return await APIManager.Instance.DeleteJBData($"{_apiUrl}/Jbs/{JBId}");
    }

}
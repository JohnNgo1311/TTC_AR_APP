// UseCases/Interfaces/IJBUseCase.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IJBUseCase
{
    Task<List<JBInformationModel>> GetListJBModel(int grapperId);
    Task<JBInformationModel> GetJBModel(int JBId);
    Task<bool> UpdateJBModel(JBGeneralModel model);
    Task<bool> AddNewJBModel(JBPostGeneralModel model);
    Task<bool> DeleteJBModel(int JBId);
}
// UseCases/JBUseCase.cs
//! Implement
public class JBUseCase : IJBUseCase
{
    private readonly IJBRepository _repository;

    public JBUseCase(IJBRepository repository)
    {
        _repository = repository;
    }

    public async Task<JBInformationModel> GetJBModel(int JBId)
    {
        return await _repository.GetJB(JBId);
    }
    public async Task<List<JBInformationModel>> GetListJBModel(int grapperId)
    {
        return await _repository.GetListJB(grapperId);
    }
    public async Task<bool> UpdateJBModel(JBGeneralModel model)
    {
        // Business logic nếu cần
        if (string.IsNullOrEmpty(model.Name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        return await _repository.UpdateJB(model);
    }

    public async Task<bool> AddNewJBModel(JBPostGeneralModel model)
    {
        // Business logic nếu cần
        if (string.IsNullOrEmpty(model.Name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        return await _repository.AddJB(model);
    }
    public async Task<bool> DeleteJBModel(int JBId)
    {
        return await _repository.DeleteJB(JBId);
    }

}
// // Infrastructure/Interfaces/IModuleSpecificationRepository.cs
// using System.Collections.Generic;
// using System.Threading.Tasks;


// public interface IModuleSpecificationRepository
// {
//     Task<List<ModuleSpecificationModel>> GetListModuleSpecification(int grapperId);
//     Task<ModuleSpecificationModel> GetModuleSpecification(int ModuleSpecificationId);
//     Task<bool> UpdateModuleSpecification(ModuleSpecificationGeneralModel model);
//     Task<bool> AddModuleSpecification(ModuleSpecificationPostGeneralModel model);
//     Task<bool> DeleteModuleSpecification(int ModuleSpecificationId);

// }

// // Infrastructure/Repositories/ModuleSpecificationRepository.cs
// public class ModuleSpecificationRepository : IModuleSpecificationRepository
// {
//     private string _apiUrl = "http://52.230.123.204:81/api";

//     public async Task<ModuleSpecification> GetModuleSpecification(int ModuleSpecificationId)
//     {
//         return await APIManager.Instance.GetModuleSpecificationData($"{_apiUrl}/ModuleSpecifications/{ModuleSpecificationId}");
//     }

//     public async Task<List<ModuleSpecification>> GetListModuleSpecification(int grapperId)
//     {
//         return await APIManager.Instance.GetListModuleSpecificationData($"{_apiUrl}/Grapper/{grapperId}/ModuleSpecifications");
//     }

//     public async Task<bool> UpdateModuleSpecification(ModuleSpecificationGeneralModel model)
//     {
//         return await APIManager.Instance.UpdateModuleSpecificationDataAsync(model, $"{_apiUrl}");
//     }

//     public async Task<bool> AddModuleSpecification(ModuleSpecificationPostGeneralModel model)
//     {
//         return await APIManager.Instance.AddNewModuleSpecificationAsync(model, $"{_apiUrl}");
//     }

//     public async Task<bool> DeleteModuleSpecification(int ModuleSpecificationId)
//     {
//         return await APIManager.Instance.DeleteModuleSpecificationData($"{_apiUrl}/ModuleSpecifications/{ModuleSpecificationId}");
//     }

// }
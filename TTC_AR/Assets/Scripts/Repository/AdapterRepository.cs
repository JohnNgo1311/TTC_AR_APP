// // Infrastructure/Interfaces/IAdapterRepository.cs
// using System.Collections.Generic;
// using System.Threading.Tasks;


// public interface IAdapterRepository
// {
//     Task<List<AdapterSpecificationModel>> GetListAdapter(int grapperId);
//     Task<AdapterSpecificationModel> GetAdapter(int AdapterId);
//     Task<bool> UpdateAdapter(AdapterSpecificationModel model);
//     Task<bool> AddAdapter(AdapterPostGeneralModel model);
//     Task<bool> DeleteAdapter(int AdapterId);

// }

// // Infrastructure/Repositories/AdapterRepository.cs
// public class AdapterRepository : IAdapterRepository
// {
//     private string _apiUrl = "http://52.230.123.204:81/api";

//     public async Task<AdapterSpecificationModel> GetAdapter(int AdapterId)
//     {
//         return await APIManager.Instance.GetAdapterData($"{_apiUrl}/Adapters/{AdapterId}");
//     }

//     public async Task<List<AdapterSpecificationModel>> GetListAdapter(int grapperId)
//     {
//         return await APIManager.Instance.GetListAdapterData($"{_apiUrl}/Grapper/{grapperId}/Adapters");
//     }

//     public async Task<bool> UpdateAdapter(AdapterSpecificationModel model)
//     {
//         return await APIManager.Instance.UpdateAdapterDataAsync(model, $"{_apiUrl}");
//     }

//     public async Task<bool> AddAdapter(AdapterPostGeneralModel model)
//     {
//         return await APIManager.Instance.AddNewAdapterAsync(model, $"{_apiUrl}");
//     }

//     public async Task<bool> DeleteAdapter(int AdapterId)
//     {
//         return await APIManager.Instance.DeleteAdapterData($"{_apiUrl}/Adapters/{AdapterId}");
//     }

// }
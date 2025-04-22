// // UseCases/Interfaces/IAdapterUseCase.cs
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// public interface IAdapterUseCase
// {
//     Task<List<AdapterSpecificationModel>> GetListAdapterModel(int grapperId);
//     Task<AdapterSpecificationModel> GetAdapterModel(int AdapterId);
//     Task<bool> UpdateAdapterModel(AdapterSpecificationModel model);
//     Task<bool> AddNewAdapterModel(AdapterPostGeneralModel model);
//     Task<bool> DeleteAdapterModel(int AdapterId);
// }
// // UseCases/AdapterUseCase.cs
// //! Implement
// public class AdapterUseCase : IAdapterUseCase
// {
//     private readonly IAdapterRepository _repository;

//     public AdapterUseCase(IAdapterRepository repository)
//     {
//         _repository = repository;
//     }

//     public async Task<AdapterSpecificationModel> GetAdapterModel(int AdapterId)
//     {
//         return await _repository.GetAdapter(AdapterId);
//     }
//     public async Task<List<AdapterSpecificationModel>> GetListAdapterModel(int grapperId)
//     {
//         return await _repository.GetListAdapter(grapperId);
//     }
//     public async Task<bool> UpdateAdapterModel(AdapterSpecificationModel model)
//     {
//         if (string.IsNullOrEmpty(model.Code))
//         {
//             throw new ArgumentException("Name cannot be empty");
//         }
//         return await _repository.UpdateAdapter(model);
//     }

//     public async Task<bool> AddNewAdapterModel(AdapterPostGeneralModel model)
//     {
//         return await _repository.AddAdapter(model);
//     }
//     public async Task<bool> DeleteAdapterModel(int AdapterId)
//     {
//         return await _repository.DeleteAdapter(AdapterId);
//     }

// }
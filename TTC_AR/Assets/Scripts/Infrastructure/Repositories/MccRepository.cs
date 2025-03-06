// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Domain.Entities;
// using Newtonsoft.Json;

// namespace Infrastructure.Repositories
// {
//     public class MccRepository : IMccRepository
//     {
//         private readonly HttpClient _httpClient;
//         private const string BaseUrl = "https://api.example.com/mcc";

//         public MccRepository(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//         }

//         public async Task<MccEntity> GetByIdAsync(int id)
//         {
//             var response = await _httpClient.GetStringAsync($"{BaseUrl}/{id}");
//             return JsonConvert.DeserializeObject<MccEntity>(response);
//         }

//         public async Task<MccEntity> CreateAsync(MccEntity mccEntity)
//         {
//             var json = JsonConvert.SerializeObject(mccEntity, new JsonSerializerSettings
//             {
//                 NullValueHandling = NullValueHandling.Ignore
//             });
//             var content = new StringContent(json, Encoding.UTF8, "application/json");
//             var response = await _httpClient.PostAsync(BaseUrl, content);
//             response.EnsureSuccessStatusCode();
//             var responseContent = await response.Content.ReadAsStringAsync();
//             return JsonConvert.DeserializeObject<MccEntity>(responseContent);
//         }

//         public async Task<MccEntity> UpdateAsync(MccEntity mccEntity)
//         {
//             var json = JsonConvert.SerializeObject(mccEntity, new JsonSerializerSettings
//             {
//                 NullValueHandling = NullValueHandling.Ignore
//             });
//             var content = new StringContent(json, Encoding.UTF8, "application/json");
//             var response = await _httpClient.PutAsync($"{BaseUrl}/{mccEntity.Id}", content);
//             response.EnsureSuccessStatusCode();
//             var responseContent = await response.Content.ReadAsStringAsync();
//             return JsonConvert.DeserializeObject<MccEntity>(responseContent);
//         }
//     }
// }
// using System;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// namespace ApplicationLayer.Dtos
// {
//   public class StaffResponseDto
//   {
//     [JsonProperty("Id")] public string UserId { get; set; } = string.Empty;
//     [JsonProperty("Name")] public string UserName { get; set; } = string.Empty;
//     [JsonProperty("Role")] public string Role { get; set; } = string.Empty;

//     [Preserve]
//     
//     public StaffResponseDto(string userId, string userName, string role)
//     {
//       UserId = userId;
//       UserName = userName;
//       Role = role;
//     }
//   }

//   [Preserve]
//   public class StaffRequestDto
//   {
//     [JsonProperty("Name")] public string UserName { get; set; } = string.Empty;
//     [JsonProperty("Password")] public string Password { get; set; } = string.Empty;
//     [Preserve]
//     
//     public StaffRequestDto(string userName, string password)
//     {
//       UserName = userName;
//       Password = password;
//     }
//   }
// }



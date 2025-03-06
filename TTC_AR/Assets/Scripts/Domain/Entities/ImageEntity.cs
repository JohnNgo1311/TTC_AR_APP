// // Domain/Entities/ImageEntity.cs
// using System;
// using Microsoft.Unity.VisualStudio.Editor;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// namespace Domain.Entities
// {
//   public class ImageEntity
//   {
//     public int Id { get; set; }
//     public string Name { get; set; } = string.Empty;

//     public string Url { get; set; } = string.Empty;

//     [Preserve]
//     
//     public ImageEntity(int id, string name)
//     {
//       Id = id;
//       Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
//     }

//     public ImageEntity(string name)
//     {
//       Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
//     }
//     [Preserve]
//     
//     public ImageEntity()
//     {
//     }

//     public ImageEntity(int id, string name, string url)
//     {
//       Id = id;
//       Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
//       Url = url;
//     }

//   }
// }




// using System;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// namespace Domain.Entities
// {
//   // Lớp cơ bản cho dữ liệu bắt buộc (POST/PUT)
//   [Preserve]
//   public class ImageBaseEntity
//   {
//     [JsonProperty("Id")]
//     public int Id { get; }

//     [JsonProperty("Name")]
//     public string Name { get; }

//     [Preserve]
//     
//     protected ImageBaseEntity(int id, string name)
//     {
//       Id = id;
//       Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name), "Name is required for Image") : name;
//     }

//     // Constructor cho POST/PUT (không cần Id)
//     public ImageBaseEntity(string name) : this(0, name)
//     {
//     }
//   }

//   // Lớp mở rộng cho dữ liệu đầy đủ (GET)
//   [Preserve]
//   public class ImageEntity : ImageBaseEntity
//   {
//     [JsonProperty("url")]
//     public string Url { get; }

//     // Constructor cho POST/PUT (không gửi Url)
//     [Preserve]
//     public ImageEntity(string name) : base(name)
//     {
//       Url = null; // Không gửi Url khi POST/PUT
//     }

//     // Constructor cho POST/PUT với Id (nếu cần gửi Id đã có)
//     [Preserve]
//     public ImageEntity(int id, string name) : base(id, name)
//     {
//       Url = null; // Không gửi Url khi POST/PUT
//     }

//     // Constructor cho GET (Url bắt buộc)
//     [Preserve]
//     
//     public ImageEntity(int id, string name, string url) : base(id, name)
//     {
//       Url = string.IsNullOrEmpty(url) ? throw new ArgumentNullException(nameof(url), "Url is required for GET") : url;
//     }
//   }
// }


using System;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class ImageEntity
  {
    [JsonProperty("Id")]
    public int Id { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
    public string? Url { get; set; }


    [Preserve]
    public ImageEntity()
    {
    }

    [Preserve]
    public ImageEntity(int id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ImageEntity(int id, string name, string url)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Url = url == string.Empty ? "Chưa cập nhật" : url;
    }
  }
}
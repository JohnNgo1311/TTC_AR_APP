
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;
// #nullable enable

// namespace Domain.Entities
// {
//   [Preserve]
//   public class MccBasicEntity
//   {
//     public int Id { get; set; }
//     public string CabinetCode { get; set; } = string.Empty;

//     [Preserve]
//     
//     public MccBasicEntity(string cabinetCode)
//     {
//       CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
//     }
//     [Preserve]
//     
//     public MccBasicEntity(int id, string cabinetCode)
//     {
//       Id = id;
//       CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
//     }
//   }



//   [Preserve]
//   public class MccEntity
//   {
//     public int Id { get; set; }
//     public string CabinetCode { get; set; } = string.Empty;
//     public string Brand { get; set; } = string.Empty;
//     public List<FieldDeviceEntity> FieldDeviceEntities { get; set; } = new();
//     public string Note { get; set; } = string.Empty;

//     [Preserve]
//     
//     public MccEntity(string cabinetCode)
//     {
//       CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
//     }
//     [Preserve]
//     
//     public MccEntity(int id, string cabinetCode)
//     {
//       Id = id;
//       CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
//     }
//     public MccEntity(int id, string cabinetCode, string? brand, List<FieldDeviceEntity> fieldDeviceEntities, string? note)
//     {
//       Id = id;
//       CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
//       Brand = string.IsNullOrEmpty(brand) ? string.Empty : brand;
//       FieldDeviceEntities = !fieldDeviceEntities.Any() ? new List<FieldDeviceEntity>() : fieldDeviceEntities;
//       Note = string.IsNullOrEmpty(note) ? string.Empty : note;
//     }
//   }
// }

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class MccEntity
  {
    [JsonProperty("Id")]
    public int Id { get; set; }

    [JsonProperty("CabinetCode")]
    public string CabinetCode { get; set; } = string.Empty;

    [JsonProperty("Brand", NullValueHandling = NullValueHandling.Ignore)]
    public string? Brand { get; set; }

    [JsonProperty("FieldDeviceEntities", NullValueHandling = NullValueHandling.Ignore)]
    public List<FieldDeviceEntity>? FieldDeviceEntities { get; set; }

    [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    public string? Note { get; set; }

    [Preserve]
    public MccEntity()
    {
      FieldDeviceEntities = new List<FieldDeviceEntity>();
    }

    [Preserve]
    public MccEntity(string cabinetCode, List<FieldDeviceEntity> fieldDeviceEntities)
    {
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
      FieldDeviceEntities = fieldDeviceEntities;
    }

    [Preserve]
    public MccEntity(string cabinetCode)
    {
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
    }

    [Preserve]

    public MccEntity(int id, string cabinetCode, List<FieldDeviceEntity> fieldDeviceEntities, string brand = "", string note = "")
    {
      Id = id;
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
      Brand = brand ?? string.Empty;
      FieldDeviceEntities = fieldDeviceEntities;
      Note = note ?? string.Empty;
    }
  }
}
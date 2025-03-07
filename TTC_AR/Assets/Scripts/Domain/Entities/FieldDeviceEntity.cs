
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using ApplicationLayer.Dtos;
// using Domain.Entities;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// [Preserve]
// public class FieldDeviceEntity
// {

//   public int id { get; set; }
//   public string Name { get; set; } = string.Empty;
//   public MccEntity MccEntity { get; set; }

//   public string RatedPower { get; set; } = string.Empty;

//   public string RatedCurrent { get; set; } = string.Empty;

//   public string ActiveCurrent { get; set; } = string.Empty;

//   public List<ImageEntity> ListConnectionImageEntities { get; set; } = new();

//   public string Note { get; set; }

//   [Preserve]
//   
//   public FieldDeviceEntity(string name)
//   {
//     Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
//   }

//   [Preserve]
//   
//   public FieldDeviceEntity(int id, string name)
//   {
//     Id = id;
//     Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
//   }
//   public FieldDeviceEntity(int id, string name, MccEntity mccEntity, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageEntity> listConnectionImageEntities, string note)
//   {
//     Id = id;
//     Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
//     Mcc = mcc ?? throw new ArgumentNullException(nameof(mccEntity));
//     RatedPower = string.IsNullOrEmpty(ratedPower) ? string.Empty : ratedPower;
//     RatedCurrent = string.IsNullOrEmpty(ratedCurrent) ? string.Empty : ratedCurrent;
//     ActiveCurrent = string.IsNullOrEmpty(activeCurrent) ? string.Empty : activeCurrent;
//     ListConnectionImageEntities = !listConnectionImageEntities.Any() ? new List<ImageEntity>() : listConnectionImageEntities;
//     Note = string.IsNullOrEmpty(note) ? string.Empty : note;
//   }

// }


// using System;
// using System.Collections.Generic;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// namespace Domain.Entities
// {
//   [Preserve]
//   public class FieldDeviceBaseEntity
//   {
//     [JsonProperty("Name")]
//     public string Name { get; set; }

//     [JsonProperty("mcc")]
//     public MccEntity MccEntity { get; set; }

//     [Preserve]
//     
//     public FieldDeviceBaseEntity(string name, MccEntity mccEntity)
//     {
//       Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name), "Name is required") : name;
//       MccEntity = mccEntity ?? throw new ArgumentNullException(nameof(mccEntity), "MccEntity is required");
//     }
//   }

//   [Preserve]
//   public class FieldDeviceEntity : FieldDeviceBaseEntity
//   {
//     [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] // Không gửi Id khi POST
//     public int id { get; set; }

//     [JsonProperty("ratedPower")]
//     public string RatedPower { get; set; }

//     [JsonProperty("ratedCurrent")]
//     public string RatedCurrent { get; set; }

//     [JsonProperty("activeCurrent")]
//     public string ActiveCurrent { get; set; }

//     [JsonProperty("connectionImages")]
//     public List<ImageEntity> ListConnectionImageEntities { get; set; }

//     [JsonProperty("note")]
//     public string Note { get; set; }

//     // Constructor cho POST
//     [Preserve]
//     public FieldDeviceEntity(string name, MccEntity mccEntity, string ratedPower , string ratedCurrent , string activeCurrent , List<ImageEntity> listConnectionImageEntities = null, string note )
//         : base(name, mccEntity)
//     {
//       Id = 0; // Không gửi khi POST
//       RatedPower = ratedPower ?? "";
//       RatedCurrent = ratedCurrent ?? "";
//       ActiveCurrent = activeCurrent ?? "";
//       ListConnectionImageEntities = listConnectionImageEntities != null ? listConnectionImageEntities : new List<ImageEntity>();
//       Note = note ?? "";
//     }

//     // Constructor cho GET/PUT
//     [Preserve]
//     
//     public FieldDeviceEntity(int id, string name, MccEntity mccEntity, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageEntity> listConnectionImageEntities, string note)
//         : base(name, mccEntity)
//     {
//       Id = id;
//       RatedPower = ratedPower ?? "";
//       RatedCurrent = ratedCurrent ?? "";
//       ActiveCurrent = activeCurrent ?? "";
//       ListConnectionImageEntities = listConnectionImageEntities != null ? listConnectionImageEntities : new List<ImageEntity>();
//       Note = note ?? "";
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
  public class FieldDeviceEntity
  {
    [JsonProperty("Id")]
    public int Id { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    // [JsonProperty("Mcc", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Mcc")]
    public MccEntity? MccEntity { get; set; }

    // [JsonProperty("ratedPower", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("RatedPower")]
    public string? RatedPower { get; set; }

    // [JsonProperty("RatedCurrent", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("RatedCurrent")]
    public string? RatedCurrent { get; set; }

    // [JsonProperty("ActiveCurrent", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ActiveCurrent")]
    public string? ActiveCurrent { get; set; }

    // [JsonProperty("ListConnectionImages", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListConnectionImages")]
    public List<ImageEntity>? ConnectionImageEntities { get; set; }

    // [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Note")]
    public string? Note { get; set; } = string.Empty;


    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeMccEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeRatedPower()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeRatedCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeActiveCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeConnectionImageEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeNote()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }


    [Preserve]
    public FieldDeviceEntity()
    {
      // ConnectionImageEntities = new List<ImageEntity>();
    }


    [Preserve]  //! Dùng khi FieldDeviceEntity được dùng làm Field để Post cho Mcc
    public FieldDeviceEntity(int id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public FieldDeviceEntity(string name, MccEntity? mccEntity, List<ImageEntity>? connectionImageEntities)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      MccEntity = mccEntity ?? throw new ArgumentNullException(nameof(mccEntity));
      ConnectionImageEntities = (connectionImageEntities == null || (connectionImageEntities != null && connectionImageEntities.Count <= 0)) ? new List<ImageEntity>() : connectionImageEntities;
    }

    [Preserve]
    //! Dùng khi Get FieldDeviceEntity từ Grapper
    public FieldDeviceEntity(int id, string name, MccEntity? mccEntity, List<ImageEntity>? connectionImageEntities, string? ratedPower, string? ratedCurrent, string? activeCurrent, string? note)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      MccEntity = mccEntity ?? null;
      RatedPower = string.IsNullOrEmpty(ratedPower) ? "Chưa cập nhật" : ratedPower;
      RatedCurrent = string.IsNullOrEmpty(ratedCurrent) ? "Chưa cập nhật" : ratedCurrent;
      ActiveCurrent = string.IsNullOrEmpty(activeCurrent) ? "Chưa cập nhật" : activeCurrent;
      ConnectionImageEntities = (connectionImageEntities == null || (connectionImageEntities != null && connectionImageEntities.Count <= 0)) ? new List<ImageEntity>() : connectionImageEntities;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
    }


    [Preserve]
    public FieldDeviceEntity(string name, List<ImageEntity>? connectionImageEntities, string? ratedPower, string? ratedCurrent, string? activeCurrent, string? note)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      RatedPower = string.IsNullOrEmpty(ratedPower) ? "Chưa cập nhật" : ratedPower;
      RatedCurrent = string.IsNullOrEmpty(ratedCurrent) ? "Chưa cập nhật" : ratedCurrent;
      ActiveCurrent = string.IsNullOrEmpty(activeCurrent) ? "Chưa cập nhật" : activeCurrent;
      ConnectionImageEntities = (connectionImageEntities == null || (connectionImageEntities != null && connectionImageEntities.Count <= 0)) ? new List<ImageEntity>() : connectionImageEntities;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
    }
  }
}
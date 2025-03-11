
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
    public string Id { get; set; } = string.Empty;

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


    // public bool ShouldSerializeId()
    // {
    //   string apiRequestType = GlobalVariable.APIRequestType;
    //   HashSet<string> allowedRequests = new HashSet<string>
    //   {
    //     HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),        
    //   };
    //   return !allowedRequests.Contains(apiRequestType);
    // }

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
        HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),


      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeRatedCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
              HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeActiveCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
              HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeConnectionImageEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
        HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeNote()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
         HttpMethodTypeEnum.POSTFieldDevice.GetDescription(),
        HttpMethodTypeEnum.PUTFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }


    [Preserve]
    public FieldDeviceEntity()
    {
      // ConnectionImageEntities = new List<ImageEntity>();
    }


    [Preserve]  //! Dùng khi FieldDeviceEntity được dùng làm Field để Post cho Mcc
    public FieldDeviceEntity(string id, string name)
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
    public FieldDeviceEntity(string id, string name, MccEntity? mccEntity, List<ImageEntity>? connectionImageEntities, string? ratedPower, string? ratedCurrent, string? activeCurrent, string? note)
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
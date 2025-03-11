
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
    public string Id { get; set; } = string.Empty;

    [JsonProperty("CabinetCode")]
    public string CabinetCode { get; set; } = string.Empty;

    // [JsonProperty("Brand", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Brand")]
    public string? Brand { get; set; }

    // [JsonProperty("FieldDevices", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListFieldDevices")]
    public List<FieldDeviceEntity>? FieldDeviceEntities { get; set; }

    // [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Note")]
    public string? Note { get; set; }


    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTMcc.GetDescription(),
        HttpMethodTypeEnum.PUTMcc.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeBrand()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETMcc.GetDescription(),
        HttpMethodTypeEnum.PUTMcc.GetDescription(),
        HttpMethodTypeEnum.POSTMcc.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeFieldDeviceEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETMcc.GetDescription(),
        HttpMethodTypeEnum.PUTMcc.GetDescription(),
        HttpMethodTypeEnum.POSTMcc.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeNote()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETMcc.GetDescription(),
        HttpMethodTypeEnum.PUTMcc.GetDescription(),
        HttpMethodTypeEnum.POSTMcc.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }


    [Preserve]
    public MccEntity()
    {
    }

    [Preserve]
    public MccEntity(string cabinetCode, List<FieldDeviceEntity>? fieldDeviceEntities)
    {
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;

      FieldDeviceEntities = (fieldDeviceEntities == null
      || (fieldDeviceEntities != null && fieldDeviceEntities.Count <= 0))
      ? new List<FieldDeviceEntity>() : fieldDeviceEntities; ;
    }

    [Preserve]
    public MccEntity(string cabinetCode)
    {
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
    }

    [Preserve]
    public MccEntity(string id, string cabinetCode, List<FieldDeviceEntity>? fieldDeviceEntities, string? brand, string? note)
    {
      Id = id;
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
      Brand = string.IsNullOrEmpty(brand) ? "Chưa cập nhật" : brand;
      FieldDeviceEntities = (fieldDeviceEntities == null
        || (fieldDeviceEntities != null && fieldDeviceEntities.Count <= 0))
        ? new List<FieldDeviceEntity>() : fieldDeviceEntities; ;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
    }
    [Preserve]
    public MccEntity(string cabinetCode, string? brand, List<FieldDeviceEntity>? fieldDeviceEntities, string? note)
    {
      CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
      Brand = string.IsNullOrEmpty(brand) ? "Chưa cập nhật" : brand;
      FieldDeviceEntities = (fieldDeviceEntities == null
        || (fieldDeviceEntities != null && fieldDeviceEntities.Count <= 0))
        ? new List<FieldDeviceEntity>() : fieldDeviceEntities; ;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
    }
  }
}
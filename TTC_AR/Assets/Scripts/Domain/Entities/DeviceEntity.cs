using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  public class DeviceEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;
    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;

    // [JsonProperty("Function", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Function")]
    public string? Function { get; set; }
    // [JsonProperty("Range", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Range")]
    public string? Range { get; set; }
    // [JsonProperty("Unit", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Unit")]
    public string? Unit { get; set; }

    // [JsonProperty("IOAddress", NullValueHandling = NullValueHandling.Ignore)]

    [JsonProperty("IOAddress")]
    public string? IOAddress { get; set; }

    // [JsonProperty("Module", NullValueHandling = NullValueHandling.Ignore)]

    [JsonProperty("Module")]
    public ModuleEntity? ModuleEntity { get; set; }

    // [JsonProperty("JB",NullValueHandling = NullValueHandling.Ignore)]

    [JsonProperty("JB")]
    public JBEntity? JBEntity { get; set; }

    // [JsonProperty("AdditionalConnectionImages", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("AdditionalConnectionImages")]
    public List<ImageEntity>? AdditionalConnectionImageEntities { get; set; }

    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeFunction()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeRange()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeUnit()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeIOAddress()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeModuleEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeJBEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeAdditionalConnectionImageEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.POSTDevice.GetDescription(),
        HttpMethodTypeEnum.PUTDevice.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }



    [Preserve]
    public DeviceEntity()
    {
      // AdditionalConnectionImageEntities = new List<ImageEntity>();
    }

    [Preserve]
    public DeviceEntity(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]

    public DeviceEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }


    [Preserve]
    public DeviceEntity(string id, string code, string function, string range, string unit, string ioAddress, ModuleEntity? moduleEntity, JBEntity? jbEntity, List<ImageEntity>? additionalConnectionImageEntities)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Function = string.IsNullOrEmpty(function) ? "Chưa cập nhật" : function;
      Range = string.IsNullOrEmpty(range) ? "Chưa cập nhật" : range;
      Unit = string.IsNullOrEmpty(unit) ? "Chưa cập nhật" : unit;
      IOAddress = string.IsNullOrEmpty(ioAddress) ? "Chưa cập nhật" : ioAddress;
      ModuleEntity = moduleEntity ?? null;
      JBEntity = jbEntity ?? null;
      AdditionalConnectionImageEntities = (additionalConnectionImageEntities == null
         || (additionalConnectionImageEntities != null && additionalConnectionImageEntities.Count <= 0))
         ? new List<ImageEntity>() : additionalConnectionImageEntities;
    }
    [Preserve]
    public DeviceEntity(string code, string function, string range, string unit, string ioAddress, ModuleEntity? moduleEntity, JBEntity? jbEntity, List<ImageEntity>? additionalConnectionImageEntities)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;

      Function = string.IsNullOrEmpty(function) ? "Chưa cập nhật" : function;

      Range = string.IsNullOrEmpty(range) ? "Chưa cập nhật" : range;

      Unit = string.IsNullOrEmpty(unit) ? "Chưa cập nhật" : unit;

      IOAddress = string.IsNullOrEmpty(ioAddress) ? "Chưa cập nhật" : ioAddress;

      ModuleEntity = moduleEntity ?? null;

      JBEntity = jbEntity ?? null;

      AdditionalConnectionImageEntities = (additionalConnectionImageEntities == null
      || (additionalConnectionImageEntities != null && additionalConnectionImageEntities.Count <= 0))
      ? new List<ImageEntity>() : additionalConnectionImageEntities;
    }
  }
}
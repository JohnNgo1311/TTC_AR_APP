using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
//Các kiểu tham chiếu không được chú thích với ? (nullable) được coi là non-nullable (không thể là null).
namespace Domain.Entities
{
  public class ModuleEntity
  {
    [JsonProperty("Id")]
    public int Id { get; set; } // non-nullable

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty; // non-nullable

    // [JsonProperty("Rack", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Rack")]
    public RackEntity? RackEntity { get; set; }
    // [JsonProperty("Devices", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListDevices")]
    public List<DeviceEntity>? DeviceEntities { get; set; }
    // [JsonProperty("JBs", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListJBs")]
    public List<JBEntity>? JBEntities { get; set; }
    // [JsonProperty("ModuleSpecification", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ModuleSpecification")]
    public ModuleSpecificationEntity? ModuleSpecificationEntity { get; set; }
    // [JsonProperty("AdapterSpecification", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("AdapterSpecification")]
    public AdapterSpecificationEntity? AdapterSpecificationEntity { get; set; }



    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return
      !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeRackEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeDeviceEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeJBEntities()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeModuleSpecificationEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeAdapterSpecificationEntity()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }


    [Preserve]
    public ModuleEntity()
    {
      // DeviceEntities = new List<DeviceEntity>();
      // JBEntities = new List<JBEntity>();
    }


    [Preserve]
    public ModuleEntity(int id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ModuleEntity(string name, RackEntity rack)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      RackEntity = rack;
    }
    [Preserve]
    public ModuleEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ModuleEntity(int id, string name, RackEntity rack, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, ModuleSpecificationEntity moduleSpecificationEntity, AdapterSpecificationEntity adapterSpecificationEntity)
    {
      Id = id;

      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

      RackEntity = rack ?? null;

      DeviceEntities = (deviceEntities == null || (deviceEntities != null && deviceEntities.Count <= 0))

      ? new List<DeviceEntity>() : deviceEntities;

      JBEntities = (jbEntities == null || (jbEntities != null && jbEntities.Count <= 0))

      ? new List<JBEntity>() : jbEntities;

      ModuleSpecificationEntity = moduleSpecificationEntity ?? null;

      AdapterSpecificationEntity = adapterSpecificationEntity ?? null;
    }

    [Preserve]
    public ModuleEntity(string name, RackEntity rack, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, ModuleSpecificationEntity moduleSpecificationEntity, AdapterSpecificationEntity adapterSpecificationEntity)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

      RackEntity = rack ?? null;

      DeviceEntities = (deviceEntities == null || (deviceEntities != null && deviceEntities.Count <= 0))

      ? new List<DeviceEntity>() : deviceEntities;

      JBEntities = (jbEntities == null || (jbEntities != null && jbEntities.Count <= 0))

      ? new List<JBEntity>() : jbEntities;

      ModuleSpecificationEntity = moduleSpecificationEntity ?? null;

      AdapterSpecificationEntity = adapterSpecificationEntity ?? null;
    }
  }
}
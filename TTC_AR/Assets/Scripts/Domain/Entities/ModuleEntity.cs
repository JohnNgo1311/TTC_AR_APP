using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
//Các kiểu tham chiếu không được chú thích với ? (nullable) được coi là non-nullable (không thể là null).
namespace Domain.Entities
{
  public class ModuleEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty; // non-nullable

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty; // non-nullable

    // [JsonProperty("Grapper", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Grapper")]
    public GrapperEntity? GrapperEntity { get; set; }

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
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };

      return !apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeName()
    {
      return true;
    }
    public bool ShouldSerializeGrapperEntity()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeRackEntity()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }

    public bool ShouldSerializeDeviceEntities()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }

    public bool ShouldSerializeJBEntities()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }

    public bool ShouldSerializeModuleSpecificationEntity()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }

    public bool ShouldSerializeAdapterSpecificationEntity()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModule.GetDescription(),
        HttpMethodTypeEnum.PUTModule.GetDescription(),
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }


    [Preserve]
    public ModuleEntity()
    {

      // DeviceEntities = new List<DeviceEntity>();
      // JBEntities = new List<JBEntity>();
    }


    [Preserve]
    public ModuleEntity(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ModuleEntity(string name, GrapperEntity grapperEntity, RackEntity rack)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      GrapperEntity = grapperEntity == null ? throw new ArgumentNullException(nameof(grapperEntity)) : grapperEntity;
      RackEntity = rack;
    }
    [Preserve]
    public ModuleEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ModuleEntity(string id, GrapperEntity grapperEntity, string name, RackEntity rack, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, ModuleSpecificationEntity moduleSpecificationEntity, AdapterSpecificationEntity adapterSpecificationEntity)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      GrapperEntity = grapperEntity == null ? throw new ArgumentNullException(nameof(grapperEntity)) : grapperEntity;

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
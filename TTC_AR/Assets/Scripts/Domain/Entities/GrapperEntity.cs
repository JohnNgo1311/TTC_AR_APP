using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class GrapperEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    // [JsonProperty("Rack", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListRacks")]
    public List<RackEntity>? RackEntities { get; set; }
    // [JsonProperty("Rack", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListModules")]
    public List<ModuleEntity>? ModuleEntities { get; set; }
    // [JsonProperty("Devices", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListDevices")]
    public List<DeviceEntity>? DeviceEntities { get; set; }
    // [JsonProperty("JBs", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListJBs")]
    public List<JBEntity>? JBEntities { get; set; }
    // [JsonProperty("Mccs", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListMccs")]
    public List<MccEntity>? MccEntities { get; set; }
    [JsonProperty("ListFieldDevices")]
    public List<FieldDeviceEntity>? FieldDeviceEntities { get; set; }


    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTGrapper.GetDescription(),
        HttpMethodTypeEnum.PUTGrapper.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }


    public bool ShouldSerializeListRacks()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeListModules()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeListDevices()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeListJBs()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeListMccs()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeListFieldDevices()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETGrapper.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }



    [Preserve]
    public GrapperEntity()
    {
      // RackEntities = new List<RackEntity>();
      // DeviceEntities = new List<DeviceEntity>();
      // JBEntities = new List<JBEntity>();
      // MccEntities = new List<MccEntity>();
    }




    [Preserve]
    public GrapperEntity(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }



    [Preserve]
    public GrapperEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public GrapperEntity(string id, string name, List<RackEntity>? rackEntities, List<ModuleEntity>? moduleEntities, List<DeviceEntity>? deviceEntities, List<JBEntity>? jbEntities, List<MccEntity>? mccEntities, List<FieldDeviceEntity>? fieldDeviceEntities)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      RackEntities = (rackEntities == null || (rackEntities != null && rackEntities.Count <= 0))
      ? new List<RackEntity>() : rackEntities; ;
      ModuleEntities = (moduleEntities == null || (moduleEntities != null && moduleEntities.Count <= 0))
      ? new List<ModuleEntity>() : moduleEntities;
      DeviceEntities = (deviceEntities == null || (deviceEntities != null && deviceEntities.Count <= 0))
      ? new List<DeviceEntity>() : deviceEntities;
      JBEntities = (jbEntities == null || (jbEntities != null && jbEntities.Count <= 0))
      ? new List<JBEntity>() : jbEntities;
      MccEntities = (mccEntities == null || (mccEntities != null && mccEntities.Count <= 0))
      ? new List<MccEntity>() : mccEntities;
      FieldDeviceEntities = (fieldDeviceEntities == null || (fieldDeviceEntities != null && fieldDeviceEntities.Count <= 0))
      ? new List<FieldDeviceEntity>() : fieldDeviceEntities;
    }
    public GrapperEntity(string name, List<RackEntity>? rackEntities, List<DeviceEntity>? deviceEntities, List<JBEntity>? jbEntities, List<MccEntity>? mccEntities, List<FieldDeviceEntity>? fieldDeviceEntities)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      RackEntities = (rackEntities == null || (rackEntities != null && rackEntities.Count <= 0))
      ? new List<RackEntity>() : rackEntities; ;
      DeviceEntities = (deviceEntities == null || (deviceEntities != null && deviceEntities.Count <= 0))
      ? new List<DeviceEntity>() : deviceEntities;
      JBEntities = (jbEntities == null || (jbEntities != null && jbEntities.Count <= 0))
      ? new List<JBEntity>() : jbEntities;
      MccEntities = (mccEntities == null || (mccEntities != null && mccEntities.Count <= 0))
      ? new List<MccEntity>() : mccEntities;
      FieldDeviceEntities = (fieldDeviceEntities == null || (fieldDeviceEntities != null && fieldDeviceEntities.Count <= 0))
      ? new List<FieldDeviceEntity>() : fieldDeviceEntities;
    }
    // public GrapperEntity(string id, string name, List<RackEntity> rackEntities, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, List<MccEntity> mccEntities, List<ModuleSpecificationEntity> moduleSpecificationEntities, List<AdapterSpecificationEntity> adapterSpecificationEntities)
    // {
    //   if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
    //   Id = id;
    //   Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    //   RackEntities = rackEntities;
    //   DeviceEntities = deviceEntities;
    //   JBEntities = jbEntities;
    //   MccEntities = mccEntities;
    //   ModuleSpecificationEntities = moduleSpecificationEntities;
    //   AdapterSpecificationEntities = adapterSpecificationEntities;

    // }
  }

}
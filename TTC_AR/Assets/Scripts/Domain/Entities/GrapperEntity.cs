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
    public int Id { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("Rack", NullValueHandling = NullValueHandling.Ignore)]
    public List<RackEntity>? RackEntities { get; set; }
    [JsonProperty("Devices", NullValueHandling = NullValueHandling.Ignore)]
    public List<DeviceEntity>? DeviceEntities { get; set; }
    [JsonProperty("JBs", NullValueHandling = NullValueHandling.Ignore)]
    public List<JBEntity>? JBEntities { get; set; }
    [JsonProperty("Mccs", NullValueHandling = NullValueHandling.Ignore)]
    public List<MccEntity>? MccEntities { get; set; }

    [Preserve]
    public GrapperEntity()
    {
      RackEntities = new List<RackEntity>();
      DeviceEntities = new List<DeviceEntity>();
      JBEntities = new List<JBEntity>();
      MccEntities = new List<MccEntity>();
    }

    [Preserve]
    public GrapperEntity(int id, string name)
    {
      Id = id;
      Name = name;
      //  Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }



    [Preserve]
    public GrapperEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public GrapperEntity(int id, string name, List<RackEntity> rackEntities, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, List<MccEntity> mccEntities)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      RackEntities = rackEntities;
      DeviceEntities = deviceEntities;
      JBEntities = jbEntities;
      MccEntities = mccEntities;

    }

    // public GrapperEntity(int id, string name, List<RackEntity> rackEntities, List<DeviceEntity> deviceEntities, List<JBEntity> jbEntities, List<MccEntity> mccEntities, List<ModuleSpecificationEntity> moduleSpecificationEntities, List<AdapterSpecificationEntity> adapterSpecificationEntities)
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
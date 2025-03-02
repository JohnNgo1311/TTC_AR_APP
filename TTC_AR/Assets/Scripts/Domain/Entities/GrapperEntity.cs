using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  [Preserve]
  public class GrapperEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RackEntity> RackEntities { get; set; } = new();
    public List<DeviceEntity> DeviceEntities { get; set; } = new();
    public List<JBEntity> JBEntities { get; set; } = new();
    public List<MccEntity> MccEntities { get; set; } = new();
    public List<ModuleSpecificationEntity> ModuleSpecificationEntities { get; set; } = new();
    public List<AdapterSpecificationEntity> AdapterSpecificationEntities { get; set; } = new();


    [Preserve]
    [JsonConstructor]
    public GrapperEntity(string name)
    {
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
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
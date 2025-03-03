using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  public class ModuleEntity
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RackName { get; set; } = string.Empty;
    public RackEntity? Rack { get; set; }
    public List<DeviceEntity> Devices { get; set; } = new();
    public List<JBEntity> JBs { get; set; } = new();
    public ModuleSpecificationEntity? ModuleSpecification { get; set; }
    public AdapterSpecificationEntity? AdapterSpecification { get; set; }

    [Preserve]
    [JsonConstructor]
    public ModuleEntity(string name, string rackName, RackEntity rack)
    {
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
      RackName = rackName == "" ? throw new ArgumentNullException(nameof(rackName)) : rackName;
      Rack = rack;

    }
    public ModuleEntity(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
    public ModuleEntity(string name)
    {
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
    // [Preserve]
    // [JsonConstructor]
    // public ModuleEntity(int id, string name, int rackId, RackEntity rack, List<DeviceEntity> devices, List<JBEntity> jbs, ModuleSpecificationEntity? moduleSpecification, AdapterSpecificationEntity? adapterSpecification)
    // {
    //   Id = id;
    //   Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    //   RackId = rackId;
    //   Rack = rack;
    //   Devices = devices;
    //   JBs = jbs;
    //   ModuleSpecification = moduleSpecification;
    //   AdapterSpecification = adapterSpecification;
    // }
  }
}
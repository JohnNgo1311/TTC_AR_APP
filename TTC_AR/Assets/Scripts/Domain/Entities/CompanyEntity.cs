using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class CompanyEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    // [JsonProperty("ListGrappers", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListGrappers")]
    public List<GrapperEntity>? GrapperEntities { get; set; }

    // [JsonProperty("ListModuleSpecifications", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListModuleSpecifications")]
    public List<ModuleSpecificationEntity>? ModuleSpecificationEntities { get; set; }

    // [JsonProperty("ListAdapterSpecifications", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("ListAdapterSpecifications")]
    public List<AdapterSpecificationEntity>? AdapterSpecificationEntities { get; set; }

    [Preserve]
    public CompanyEntity()
    {
      // GrapperEntities = new List<GrapperEntity>();
      // ModuleSpecificationEntities = new List<ModuleSpecificationEntity>();
      // AdapterSpecificationEntities = new List<AdapterSpecificationEntity>();
    }

    [Preserve]
    public CompanyEntity(string id, string name, List<GrapperEntity>? grapperEntities, List<ModuleSpecificationEntity>? moduleSpecificationEntities, List<AdapterSpecificationEntity>? adapterSpecificationEntities)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      GrapperEntities = grapperEntities;
      ModuleSpecificationEntities = (moduleSpecificationEntities == null || (moduleSpecificationEntities != null && !moduleSpecificationEntities.Any())) ? new List<ModuleSpecificationEntity>() : moduleSpecificationEntities; ;
      AdapterSpecificationEntities = (adapterSpecificationEntities == null || (adapterSpecificationEntities != null && !adapterSpecificationEntities.Any())) ? new List<AdapterSpecificationEntity>() : adapterSpecificationEntities; ;
    }
    public bool ShouldSerializeId()
    {
      return true;
    }

    public bool ShouldSerializeName()
    {
      return true;
    }
    public bool ShouldSerializeGrapperEntities()
    {
      return true;
    }
    public bool ShouldSerializeModuleSpecificationEntities()
    {
      return true;
    }

    public bool ShouldSerializeAdapterSpecificationEntities()
    {
      return true;
    }
  }
}
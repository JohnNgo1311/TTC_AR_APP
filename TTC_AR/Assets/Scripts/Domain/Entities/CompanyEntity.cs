using System;
using System.Collections.Generic;
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

    [JsonProperty("ListGrappers", NullValueHandling = NullValueHandling.Ignore)]
    public List<GrapperEntity>? GrapperEntities { get; set; }

    [JsonProperty("ListModuleSpecifications", NullValueHandling = NullValueHandling.Ignore)]
    public List<ModuleSpecificationEntity>? ModuleSpecificationEntities { get; set; }

    [JsonProperty("ListAdapterSpecifications", NullValueHandling = NullValueHandling.Ignore)]
    public List<AdapterSpecificationEntity>? AdapterSpecificationEntities { get; set; }

    [Preserve]
    public CompanyEntity()
    {

      GrapperEntities = new List<GrapperEntity>();
      ModuleSpecificationEntities = new List<ModuleSpecificationEntity>();
      AdapterSpecificationEntities = new List<AdapterSpecificationEntity>();
    }

    [Preserve]
    public CompanyEntity(string id, string name, List<GrapperEntity> grapperEntities, List<ModuleSpecificationEntity> moduleSpecificationEntities, List<AdapterSpecificationEntity> adapterSpecificationEntities)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      // Name = name;
      GrapperEntities = grapperEntities;
      ModuleSpecificationEntities = moduleSpecificationEntities;
      AdapterSpecificationEntities = adapterSpecificationEntities;
    }
  }
}
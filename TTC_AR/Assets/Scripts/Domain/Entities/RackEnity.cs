using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class RackEntity
  {
    [JsonProperty("Id")]
    public int Id { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("Modules", NullValueHandling = NullValueHandling.Ignore)]
    public List<ModuleEntity>? ModuleEntities { get; set; }

    [Preserve]
    public RackEntity()
    {
      ModuleEntities = new List<ModuleEntity>();
    }


    [Preserve]
    public RackEntity(int id, string name, List<ModuleEntity> moduleEntities)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

      ModuleEntities = !moduleEntities.Any() ? new List<ModuleEntity>() : moduleEntities;
    }

    [Preserve]

    public RackEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]

    public RackEntity(int id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

  }

}
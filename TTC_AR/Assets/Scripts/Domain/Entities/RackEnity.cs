using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  [Preserve]
  public class RackEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ModuleEntity> ModuleEntities { get; set; } = new();

    // [Preserve]
    // [JsonConstructor]
    // public RackEntity(int id, string name, List<ModuleEntity> moduleEntities)
    // {
    //   Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    //   Id = id;
    //   ModuleEntities = moduleEntities;
    // }
  }

}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  [Preserve]
  public class CompanyEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<GrapperEntity> GrapperEntities { get; set; } = new();

    [Preserve]
    [JsonConstructor]
    public CompanyEntity(string name)
    {
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
    // public CompanyEntity(int id, string name, List<GrapperEntity> grapperEntities)
    // {
    //   Id = id;
    //   Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    //   GrapperEntities = grapperEntities;
    // }
  }
}
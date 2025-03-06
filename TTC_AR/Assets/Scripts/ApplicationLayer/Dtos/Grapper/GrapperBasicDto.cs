using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.FieldDevice;
using ApplicationLayer.Dtos.Mcc;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Grapper
{

  [Preserve]
  public class GrapperBasicDto
  {
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("Name")] public string Name { get; set; } = string.Empty;

    [Preserve]

    public GrapperBasicDto(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
  }

}
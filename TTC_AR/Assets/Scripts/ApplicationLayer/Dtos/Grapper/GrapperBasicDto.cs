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
    [JsonProperty("Id")] public string Id { get; set; }
    [JsonProperty("Name")] public string Name { get; set; } = string.Empty;

    [Preserve]

    public GrapperBasicDto(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }
  }

}
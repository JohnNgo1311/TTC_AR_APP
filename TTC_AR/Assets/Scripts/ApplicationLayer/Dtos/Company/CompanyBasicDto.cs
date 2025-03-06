using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
namespace ApplicationLayer.Dtos.Company
{
  [Preserve]

  public class CompanyBasicDto
  {
    [JsonProperty("Id")] public string Id { get; set; }
    [JsonProperty("Name")] public string Name { get; set; }

    [Preserve]

    public CompanyBasicDto(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

  }


}
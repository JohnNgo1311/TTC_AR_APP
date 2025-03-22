using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.AdapterSpecification
{
  [Preserve]

  public class AdapterSpecificationBasicDto
  {
    [JsonProperty("Id")] public string Id { get; set; }
    [JsonProperty("Code")] public string Code { get; set; }

    [Preserve]

    public AdapterSpecificationBasicDto(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentException(nameof(code)) : code;
    }
  }

}


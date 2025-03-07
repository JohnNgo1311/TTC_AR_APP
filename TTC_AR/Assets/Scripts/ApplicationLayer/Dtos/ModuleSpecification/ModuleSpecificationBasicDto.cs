
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.ModuleSpecification
{
  public class ModuleSpecificationBasicDto
  {
    [JsonProperty("Id")] public string Id { get; set; }
    [JsonProperty("Code")] public string Code { get; set; }

    [Preserve]

    public ModuleSpecificationBasicDto(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new System.ArgumentException(nameof(code)) : code;
    }
  }

}
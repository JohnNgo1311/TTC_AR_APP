using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
namespace ApplicationLayer.Dtos
{
  [Preserve]
  public class CompanyBasicDto
  {
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("Name")] public string Name { get; set; }

    [Preserve]
    [JsonConstructor]
    public CompanyBasicDto(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }

  }

  [Preserve]
  public class CompanyResponseDto : CompanyBasicDto
  {
    [JsonProperty("ListGrappers")] public List<GrapperBasicDto> GrapperBasicDtos { get; set; }
    [JsonProperty("ListModuleSpecifications")] public List<ModuleSpecificationBasicDto> ModuleSpecificationBasicDtos { get; set; }
    [JsonProperty("ListAdapterSpecifications")] public List<AdapterSpecificationBasicDto> AdapterSpecificationBasicDtos { get; set; }

    [Preserve]
    [JsonConstructor]
    public CompanyResponseDto(int id, string name, List<GrapperBasicDto> grapperBasicDtos, List<ModuleSpecificationBasicDto> moduleSpecificationBasicDtos, List<AdapterSpecificationBasicDto> adapterSpecificationBasicDtos) : base(id, name)
    {
      GrapperBasicDtos = grapperBasicDtos ?? new List<GrapperBasicDto>();
      ModuleSpecificationBasicDtos = moduleSpecificationBasicDtos ?? new List<ModuleSpecificationBasicDto>();
      AdapterSpecificationBasicDtos = adapterSpecificationBasicDtos ?? new List<AdapterSpecificationBasicDto>();
    }
  }

  [Preserve]
  public class CompanyRequestDto
  {
    [JsonProperty("Name")] public string Name { get; set; }
    [JsonProperty("ListGrappers")] public List<GrapperBasicDto> GrapperBasicDtos { get; set; }
    [JsonProperty("ListModuleSpecifications")] public List<ModuleSpecificationBasicDto> ModuleSpecificationBasicDtos { get; set; }
    [JsonProperty("ListAdapterSpecifications")] public List<AdapterSpecificationBasicDto> AdapterSpecificationBasicDtos { get; set; }

    [Preserve]
    [JsonConstructor]
    public CompanyRequestDto(string name, List<GrapperBasicDto> grapperBasicDtos, List<ModuleSpecificationBasicDto> moduleSpecificationBasicDtos, List<AdapterSpecificationBasicDto> adapterSpecificationBasicDtos)
    {
      Name = name ?? throw new ArgumentException(nameof(name));
      GrapperBasicDtos = grapperBasicDtos ?? new List<GrapperBasicDto>();
      ModuleSpecificationBasicDtos = moduleSpecificationBasicDtos ?? new List<ModuleSpecificationBasicDto>();
      AdapterSpecificationBasicDtos = adapterSpecificationBasicDtos ?? new List<AdapterSpecificationBasicDto>();
    }
  }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
[Preserve]
public class RackInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;

  [JsonProperty("Name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("ListModules")]
  public List<ModuleInformationModel>? ListModuleInformationModel { get; set; }

  [Preserve]
  public RackInformationModel(string id, string name, List<ModuleInformationModel>? listModuleInformationModel)
  {
    Id = id;
    Name = name;
    ListModuleInformationModel = listModuleInformationModel;
  }
  [Preserve]
  public RackInformationModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
  [Preserve]
  public RackInformationModel(string name, List<ModuleInformationModel>? listModuleInformationModel)
  {
    Name = name;
    ListModuleInformationModel = listModuleInformationModel;
  }
}

[Preserve]
public class RackBasicModel
{
  [JsonProperty("Id")] public string Id { get; set; }
  [JsonProperty("Name")] public string Name { get; set; }

  [Preserve]

  public RackBasicModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
}

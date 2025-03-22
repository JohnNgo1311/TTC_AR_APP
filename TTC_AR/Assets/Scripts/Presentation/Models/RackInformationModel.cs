using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class RackInformationModel
{
#nullable enable
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("ListModules")]
  public List<ModuleInformationModel>? ListModuleInformationModel { get; set; }

  [Preserve]

  public RackInformationModel(string id, string name, List<ModuleInformationModel> listModuleInformationModel)
  {
    Id = id;
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

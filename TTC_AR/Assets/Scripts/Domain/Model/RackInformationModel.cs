using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class RackInformationModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("ListModules")]
  public List<ModuleInformationModel> ListModuleInformationModel { get; set; }

  [Preserve]
  [JsonConstructor]
  public RackInformationModel(int id, string name, List<ModuleInformationModel> listModuleInformationModel)
  {
    Id = id;
    Name = name;
    ListModuleInformationModel = listModuleInformationModel;
  }
}

[Preserve]
public class RackBasicModel
{
  [JsonProperty("id")] public int Id { get; set; }
  [JsonProperty("name")] public string Name { get; set; }

  [Preserve]
  [JsonConstructor]
  public RackBasicModel(int id, string name)
  {
    Id = id;
    Name = name;
  }
}

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

  [JsonProperty("ListModule")]
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
public class Rack_General_Model
{
  [JsonProperty("id")] public int Id { get; set; }
  [JsonProperty("name")] public string Name { get; set; }
  [JsonProperty("modules")] public List<Module_General_Non_Rack_Model> List_Module_General_Non_Rack_Model { get; set; }

  [Preserve]
  [JsonConstructor]
  public Rack_General_Model(int id, string name, List<Module_General_Non_Rack_Model> list_Module_General_Non_Rack_Model)
  {
    Id = id;
    Name = name;
    List_Module_General_Non_Rack_Model = list_Module_General_Non_Rack_Model;
  }
}
[Preserve]
public class Rack_Non_List_Module_Model
{
  [JsonProperty("id")] public int Id { get; set; }
  [JsonProperty("name")] public string Name { get; set; }
  
  [Preserve]
  [JsonConstructor]
  public Rack_Non_List_Module_Model(int id, string name)
  {
    Id = id;
    Name = name;
  }
}

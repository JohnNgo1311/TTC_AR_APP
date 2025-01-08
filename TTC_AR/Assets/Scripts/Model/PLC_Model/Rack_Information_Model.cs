using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class Rack_Information_Model
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("List_Module")]
  public List<Module_Information_Model> List_Module_Model { get; set; }
  [Preserve]
  [JsonConstructor]
  public Rack_Information_Model(string id, string name, List<Module_Information_Model> listModuleModel)
  {
    Id = id;
    Name = name;
    List_Module_Model = listModuleModel;
  }
}

[Preserve]
public class Rack_General_Model
{
  public string Id { get; set; }
  public string Name { get; set; }
  public List<Module_General_Non_Rack_Model> List_Module_General_Non_Rack_Model { get; set; }
  [Preserve]
  [JsonConstructor]
  public Rack_General_Model(string id, string name, List<Module_General_Non_Rack_Model> list_Module_General_Non_Rack_Model)
  {
    Id = id;
    Name = name;
    List_Module_General_Non_Rack_Model = list_Module_General_Non_Rack_Model;
  }
}
[Preserve]
public class Rack_Non_List_Module_Model
{
  public string Id { get; set; }
  public string Name { get; set; }
  [Preserve]
  [JsonConstructor]
  public Rack_Non_List_Module_Model(string id, string name)
  {
    Id = id;
    Name = name;
  }
}

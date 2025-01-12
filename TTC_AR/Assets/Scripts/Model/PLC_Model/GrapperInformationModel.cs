using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class GrapperInformationModel
{
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("racks")]
  public List<Rack_Non_List_Module_Model> List_Rack_Non_List_Module_Model { get; set; }
  [Preserve]
  [JsonConstructor]
  public GrapperInformationModel(int id, string name, List<Rack_Non_List_Module_Model> list_Rack_Non_List_Module_Model)
  {
    Id = id;
    Name = name;
    List_Rack_Non_List_Module_Model = list_Rack_Non_List_Module_Model;
  }
}

[Preserve]
public class Grapper_General_Model
{
  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  [JsonProperty("racks")]
  public List<Rack_Non_List_Module_Model> List_Rack_Non_List_Module_Model { get; set; }

  public Grapper_General_Model()
  {
  }

  [Preserve]
  [JsonConstructor]
  public Grapper_General_Model(int id, string name, List<Rack_Non_List_Module_Model> list_Rack_Non_List_Module_Model)
  {
    Id = id;
    Name = name;
    List_Rack_Non_List_Module_Model = list_Rack_Non_List_Module_Model;
  }
}
[Preserve]
public class Grapper_General_Non_List_Rack_Model
{

  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  [Preserve]
  [JsonConstructor]
  public Grapper_General_Non_List_Rack_Model(int id, string name)
  {
    Id = id;
    Name = name;
  }
  public Grapper_General_Non_List_Rack_Model() { }
}
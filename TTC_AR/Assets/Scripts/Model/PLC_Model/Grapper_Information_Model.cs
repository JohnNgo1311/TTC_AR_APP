using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class Grapper_Information_Model
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("List_Rack")]
  public List<Rack_Information_Model> List_Rack_Information_Model { get; set; }
  [Preserve]
  [JsonConstructor]
  public Grapper_Information_Model(string id, string name, List<Rack_Information_Model> list_Rack_Information_Model)
  {
    Id = id;
    Name = name;
    List_Rack_Information_Model = list_Rack_Information_Model;
  }
  public Grapper_Information_Model() { }
}

[Preserve]
public class Grapper_General_Model
{
  public string Id { get; set; }
  public string Name { get; set; }
  public List<Rack_General_Model> List_Rack_General_Model { get; set; }

  public Grapper_General_Model()
  {
  }

  [Preserve]
  [JsonConstructor]
  public Grapper_General_Model(string id, string name, List<Rack_General_Model> list_Rack_General_Model)
  {
    Id = id;
    Name = name;
    List_Rack_General_Model = list_Rack_General_Model;
  }
}
[Preserve]
public class Grapper_General_Non_List_Model
{

  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  [Preserve]
  [JsonConstructor]
  public Grapper_General_Non_List_Model(string id, string name)
  {
    Id = id;
    Name = name;
  }
  public Grapper_General_Non_List_Model() { }
}
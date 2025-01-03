using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class Grapper_Information_Model
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("List_Rack")]
  public List<Rack_Information_Model> List_Rack_Information_Model { get; set; }

  public Grapper_Information_Model(string id, string name, List<Rack_Information_Model> list_Rack_Information_Model)
  {
    Id = id;
    Name = name;
    List_Rack_Information_Model = list_Rack_Information_Model;
  }
}
public class Grapper_General_Model
{
  public string Id { get; set; }
  public string Name { get; set; }
  public List<Rack_General_Model> List_Rack_General_Model { get; set; }

  public Grapper_General_Model(string id, string name, List<Rack_General_Model> list_Rack_General_Model)
  {
    Id = id;
    Name = name;
    List_Rack_General_Model = list_Rack_General_Model;
  }
}
public class Grapper_General_Non_List_Model
{
  public string Id { get; set; }
  public string Name { get; set; }

  public Grapper_General_Non_List_Model(string id, string name)
  {
    Id = id;
    Name = name;
  }
}
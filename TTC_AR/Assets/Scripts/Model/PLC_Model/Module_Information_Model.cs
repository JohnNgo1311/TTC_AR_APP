using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class Module_Information_Model
{
#nullable enable

  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("List_Device")]
  public List<Device_Information_Model> List_Device_Model { get; set; }

  [JsonProperty("List_JB")]
  public List<JB_Information_Model> List_JB_Model { get; set; }

  [JsonProperty("Module_Specification")]
  public Module_Specification_Model? Specification_Model { get; set; }

  [JsonProperty("Rack")]
  public Rack_Non_List_Module_Model Rack_Non_List_Module_Mode { get; set; }

  public Module_Information_Model(string id, string name, List<Device_Information_Model> list_Device_Model, List<JB_Information_Model> list_JB_Model, Module_Specification_Model? specification_Model, Rack_Non_List_Module_Model rack_Non_List_Module_Model)
  {
    Id = id;
    Name = name;
    List_Device_Model = list_Device_Model;
    List_JB_Model = list_JB_Model;
    Specification_Model = specification_Model;
    Rack_Non_List_Module_Mode = rack_Non_List_Module_Model;
  }

}
public class Module_General_Model //! Module Module có Id, Name và Rack tương ứng (Rack chỉ chứa Id và Name)
{
  public string Id { get; set; }
  public string Name { get; set; }
  public Rack_Non_List_Module_Model Rack_Non_List_Module_Model { get; set; }

  public Module_General_Model(string id, string name, Rack_Non_List_Module_Model rack_Non_List_Module_Model)
  {
    Id = id;
    Name = name;
    Rack_Non_List_Module_Model = rack_Non_List_Module_Model;
  }
}
public class Module_General_Non_Rack_Model //! Module chỉ có Id và Name, không chứa Rack
{
  public string Id { get; set; }
  public string Name { get; set; }

  public Module_General_Non_Rack_Model(string id, string name)
  {
    Id = id;
    Name = name;
  }
}



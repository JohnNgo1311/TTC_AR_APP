using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class ModuleInformationModel
{
#nullable enable

  [JsonProperty("id")] public int Id { get; set; }

  [JsonProperty("name")] public string Name { get; set; }
  [JsonProperty("rack")] public Rack_Non_List_Module_Model Rack { get; set; }
  [JsonProperty("jbs")] public List<JBInformationModel> ListJBInformationModel { get; set; }

  [JsonProperty("devices")] public List<DeviceInformationModel_FromModule> ListDeviceInformationModel_FromModule { get; set; }

  [JsonProperty("specification")] public ModuleSpecificationGeneralModel? ModuleSpecificationGeneralModel { get; set; }


  [Preserve]
  [JsonConstructor]
  public ModuleInformationModel(int id, string name, Rack_Non_List_Module_Model rack, List<JBInformationModel> listJBInformationModel, List<DeviceInformationModel_FromModule> listDeviceInformationModel_FromModule, ModuleSpecificationGeneralModel? moduleSpecificationGeneralModel)
  {
    Id = id;
    Name = name;
    Rack = rack;
    ListJBInformationModel = listJBInformationModel;
    ListDeviceInformationModel_FromModule = listDeviceInformationModel_FromModule;
    ModuleSpecificationGeneralModel = moduleSpecificationGeneralModel;
  }

}
[Preserve]
public class ModuleGeneralModel //! Module Module có Id, Name và Rack tương ứng (Rack chỉ chứa Id và Name)
{
  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  [JsonProperty("Rack")]
  public Rack_Non_List_Module_Model Rack_Non_List_Module_Model { get; set; }

  [Preserve]
  [JsonConstructor]
  public ModuleGeneralModel(int id, string name, Rack_Non_List_Module_Model rack_Non_List_Module_Model)
  {
    Id = id;
    Name = name;
    Rack_Non_List_Module_Model = rack_Non_List_Module_Model;
  }
}
[Preserve]
public class ModuleGeneralNonRackModel //! Module chỉ có Id và Name, không chứa Rack
{
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [Preserve]
  [JsonConstructor]
  public ModuleGeneralNonRackModel(int id, string name)
  {
    Id = id;
    Name = name;
  }
}




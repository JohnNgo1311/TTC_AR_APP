using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class ModuleInformationModel
{

  [JsonProperty("Id")] public string Id { get; set; } = string.Empty;

  [JsonProperty("Name")] public string Name { get; set; } = string.Empty;
  [JsonProperty("Grapper")] public GrapperInformationModel? Grapper { get; set; }
  [JsonProperty("Rack")] public RackInformationModel? Rack { get; set; }
  [JsonProperty("ListDevices")] public List<DeviceInformationModel>? ListDeviceInformationModel { get; set; }
  [JsonProperty("ListJBs")] public List<JBInformationModel>? ListJBInformationModel { get; set; }
  [JsonProperty("ModuleSpecification")] public ModuleSpecificationModel? ModuleSpecificationModel { get; set; }
  [JsonProperty("AdapterSpecification")] public AdapterSpecificationModel? AdapterSpecificationModel { get; set; }


  [Preserve]
  public ModuleInformationModel()
  {

  }


  [Preserve]
  public ModuleInformationModel(string id, string name, GrapperInformationModel? grapper, RackInformationModel? rack, List<DeviceInformationModel>? listDeviceInformationModel, List<JBInformationModel>? listJBInformationModel, ModuleSpecificationModel? moduleSpecificationModel, AdapterSpecificationModel? adapterSpecificationModel)
  {
    Id = id;
    Name = name;
    Grapper = grapper;
    Rack = rack;
    ListDeviceInformationModel = listDeviceInformationModel;
    ListJBInformationModel = listJBInformationModel;
    ModuleSpecificationModel = moduleSpecificationModel;
    AdapterSpecificationModel = adapterSpecificationModel;
  }

  [Preserve]
  public ModuleInformationModel(string id, string name)
  {
    Id = id;
    Name = name;
  }

  [Preserve]
  public ModuleInformationModel(string name, RackInformationModel? rack, List<DeviceInformationModel>? listDeviceInformationModel, List<JBInformationModel>? listJBInformationModel, ModuleSpecificationModel? moduleSpecificationModel, AdapterSpecificationModel? adapterSpecificationModel)
  {
    Name = name;
    Rack = rack;
    ListDeviceInformationModel = listDeviceInformationModel;
    ListJBInformationModel = listJBInformationModel;
    ModuleSpecificationModel = moduleSpecificationModel;
    AdapterSpecificationModel = adapterSpecificationModel;
  }
}

[Preserve]
public class ModuleBasicModel//! Module Module có Id, Name và Rack tương ứng (Rack chỉ chứa Id và Name)
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  public ModuleBasicModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
}

[Preserve]
public class ModuleGeneralModel //! Module Module có Id, Name và Rack tương ứng (Rack chỉ chứa Id và Name)
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  [JsonProperty("Rack")]
  public RackBasicModel RackBasicModel { get; set; }

  [JsonProperty("ListDevices")] public List<DeviceBasicModel> ListDeviceBasicModel { get; set; }

  [JsonProperty("ListJBs")] public List<JBBasicModel> ListJBBasicModel { get; set; }

  [JsonProperty("ModuleSpecification")] public ModuleSpecificationBasicModel? ModuleSpecificationBasicModel { get; set; }
  [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicModel? AdapterSpecificationBasicModel { get; set; }

  [Preserve]

  public ModuleGeneralModel(string id, string name, RackBasicModel rackBasicModel, List<DeviceBasicModel> listDeviceBasicModel, List<JBBasicModel> listJBBasicModel, ModuleSpecificationBasicModel? moduleSpecificationBasicModel, AdapterSpecificationBasicModel? adapterSpecificationBasicModel)
  {
    Id = id;
    Name = name;
    RackBasicModel = rackBasicModel;
    ListDeviceBasicModel = listDeviceBasicModel;
    ListJBBasicModel = listJBBasicModel;
    ModuleSpecificationBasicModel = moduleSpecificationBasicModel;
    AdapterSpecificationBasicModel = adapterSpecificationBasicModel;
  }

}




using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class ModuleInformationModel
{
#nullable enable
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("Rack")]
  public RackInformationModel? Rack { get; set; }

  [JsonProperty("Grapper")]
  public GrapperInformationModel? Grapper { get; set; }

  [JsonProperty("ListDevices")]
  public List<DeviceInformationModel>? ListDeviceInformationModel { get; set; }

  [JsonProperty("ListJBs")]
  public List<JBInformationModel>? ListJBInformationModel { get; set; }

  [JsonProperty("ModuleSpecification")]
  public ModuleSpecificationModel? ModuleSpecificationModel { get; set; }

  [JsonProperty("AdapterSpecification")]
  public AdapterSpecificationModel? AdapterSpecificationModel { get; set; }


  [Preserve]
  [JsonConstructor]
  public ModuleInformationModel(string id, string name, RackInformationModel rack, List<DeviceInformationModel> listDeviceInformationModel_FromModule, List<JBInformationModel> listJBInformationModel, ModuleSpecificationModel? moduleSpecificationModel, AdapterSpecificationModel? adapterSpecificationModel)
  {
    Id = id;
    Name = name;
    Rack = rack;
    ListDeviceInformationModel = listDeviceInformationModel_FromModule;
    ListJBInformationModel = listJBInformationModel;
    ModuleSpecificationModel = moduleSpecificationModel;
    AdapterSpecificationModel = adapterSpecificationModel;
  }
}



[Preserve]
public class ModuleBasicModel//! Module Module có Id, Name và Rack tương ứng (Rack chỉ chứa Id và Name)
{
  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }
  public ModuleBasicModel(int id, string name)
  {
    Id = id;
    Name = name;
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
  public RackBasicModel RackBasicModel { get; set; }

  [JsonProperty("ListDevices")] public List<DeviceBasicModel> ListDeviceBasicModel { get; set; }

  [JsonProperty("ListJBs")] public List<JBBasicModel> ListJBBasicModel { get; set; }

  [JsonProperty("ModuleSpecification")] public ModuleSpecificationBasicModel? ModuleSpecificationBasicModel { get; set; }
  [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicModel? AdapterSpecificationBasicModel { get; set; }

  [Preserve]
  [JsonConstructor]
  public ModuleGeneralModel(int id, string name, RackBasicModel rackBasicModel, List<DeviceBasicModel> listDeviceBasicModel, List<JBBasicModel> listJBBasicModel, ModuleSpecificationBasicModel? moduleSpecificationBasicModel, AdapterSpecificationBasicModel? adapterSpecificationBasicModel)
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




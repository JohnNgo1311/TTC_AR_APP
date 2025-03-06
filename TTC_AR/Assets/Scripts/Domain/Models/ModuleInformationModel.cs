using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class ModuleInformationModel
{
#nullable enable

  [JsonProperty("Id")] public int Id { get; set; }

  [JsonProperty("Name")] public string Name { get; set; }
  [JsonProperty("rack")] public RackBasicModel Rack { get; set; }

  [JsonProperty("listDevices")] public List<DeviceInformationModel> ListDeviceInformationModel_FromModule { get; set; }

  [JsonProperty("listJBs")] public List<JBInformationModel> ListJBInformationModel { get; set; }

  [JsonProperty("moduleSpecification")] public ModuleSpecificationModel? ModuleSpecificationModel { get; set; }
  [JsonProperty("AdapterSpecification")] public AdapterSpecificationModel? AdapterSpecificationModel { get; set; }



  [Preserve]

  public ModuleInformationModel(int id, string name, RackBasicModel rack, List<DeviceInformationModel> listDeviceInformationModel_FromModule, List<JBInformationModel> listJBInformationModel, ModuleSpecificationModel? moduleSpecificationModel, AdapterSpecificationModel? adapterSpecificationModel)
  {
    Id = id;
    Name = name;
    Rack = rack;
    ListDeviceInformationModel_FromModule = listDeviceInformationModel_FromModule;
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

  [JsonProperty("listDevices")] public List<DeviceBasicModel> ListDeviceBasicModel { get; set; }

  [JsonProperty("listJBs")] public List<JBBasicModel> ListJBBasicModel { get; set; }

  [JsonProperty("moduleSpecification")] public ModuleSpecificationBasicModel? ModuleSpecificationBasicModel { get; set; }
  [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicModel? AdapterSpecificationBasicModel { get; set; }

  [Preserve]

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




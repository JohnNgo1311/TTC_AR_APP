using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class GrapperInformationModel
{
  [JsonProperty("Id")] public string Id { get; set; }

  [JsonProperty("Name")] public string Name { get; set; }

  [JsonProperty("ListRacks")] public List<RackBasicModel> List_RackBasicModel { get; set; }

  [JsonProperty("ListDevices")] public List<DeviceInformationModel> ListDeviceInformationModel { get; set; }

  [JsonProperty("ListJBs")] public List<JBInformationModel> ListJBInformationModel { get; set; }

  [JsonProperty("ListMCCs")] public List<MccInformationModel> ListMccInformationModel { get; set; }

  [JsonProperty("ListModuleSpecifications")] public List<ModuleSpecificationModel> ListModuleSpecificationModel { get; set; }

  [JsonProperty("ListAdapterSpecifications")] public List<AdapterSpecificationModel> ListAdapterSpecificationModel { get; set; }


  [Preserve]

  public GrapperInformationModel(string id, string name, List<RackBasicModel> list_RackBasicModel, List<DeviceInformationModel> listDeviceInformationModel, List<JBInformationModel> listJBInformationModel, List<MccInformationModel> listMccInformationModel, List<ModuleSpecificationModel> listModuleSpecificationModel, List<AdapterSpecificationModel> listAdapterSpecificationModel)
  {
    Id = id;
    Name = name;
    List_RackBasicModel = list_RackBasicModel;
    ListDeviceInformationModel = listDeviceInformationModel;
    ListJBInformationModel = listJBInformationModel;
    ListMccInformationModel = listMccInformationModel;
    ListModuleSpecificationModel = listModuleSpecificationModel;
    ListAdapterSpecificationModel = listAdapterSpecificationModel;
  }

}

[Preserve]
public class GrapperBasicModel
{
  [JsonProperty("Id")] public string Id { get; set; }
  [JsonProperty("Name")] public string Name { get; set; }

  [Preserve]

  public GrapperBasicModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
[Preserve]
public class CompanyInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("ListGrappers")]
  public List<GrapperInformationModel> ListGrapperInformationModel { get; set; }
  [JsonProperty("ListModuleSpecifications")]
  public List<ModuleSpecificationModel> ListModuleSpecificationModel { get; set; }
  [JsonProperty("ListAdapterSpecifications")]
  public List<AdapterSpecificationModel> ListAdapterSpecificationModel { get; set; }
  [Preserve]

  public CompanyInformationModel(string id, string name, List<GrapperInformationModel> listGrapperInformationModel, List<ModuleSpecificationModel> listModuleSpecificationModel, List<AdapterSpecificationModel> listAdapterSpecificationModel)
  {
    Id = id;
    Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    ListGrapperInformationModel = listGrapperInformationModel.Any() ? listGrapperInformationModel : new List<GrapperInformationModel>();
    ListModuleSpecificationModel = listModuleSpecificationModel.Any() ? listModuleSpecificationModel : new List<ModuleSpecificationModel>();
    ListAdapterSpecificationModel = listAdapterSpecificationModel.Any() ? listAdapterSpecificationModel : new List<AdapterSpecificationModel>();
  }

}

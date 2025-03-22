using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class CompanyInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("ListGrappers")]
  public List<GrapperInformationModel> ListGrapperInformationModel { get; set; }

  [Preserve]

  public CompanyInformationModel(string id, string name, List<GrapperInformationModel> listGrapperInformationModel)
  {
    Id = id;
    Name = name;
    ListGrapperInformationModel = listGrapperInformationModel;
  }

}

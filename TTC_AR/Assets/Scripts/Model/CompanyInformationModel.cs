using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class CompanyInformationModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("ListGrappers")]
  public List<GrapperInformationModel> ListGrapperInformationModel { get; set; }

  [Preserve]
  [JsonConstructor]
  public CompanyInformationModel(int id, string name, List<GrapperInformationModel> listGrapperInformationModel)
  {
    Id = id;
    Name = name;
    ListGrapperInformationModel = listGrapperInformationModel;
  }

}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class CompanyModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }

  [JsonProperty("CompanyName")]
  public string Name { get; set; }

  [JsonProperty("ListGrapper")]
  public List<GrapperInformationModel> ListGrapperInformationModel { get; set; }

  [Preserve]
  [JsonConstructor]
  public CompanyModel(int id, string name, List<GrapperInformationModel> listGrapperInformationModel)
  {
    // Kiểm tra id không được null
    Id = id;
    // Kiểm tra name không được null
    Name = name ?? throw new ArgumentNullException(nameof(name), "Company name cannot be null");
    // Kiểm tra List_Grapper_Information_Model không được null
    ListGrapperInformationModel = listGrapperInformationModel
        ?? throw new ArgumentNullException(nameof(listGrapperInformationModel), "List of grapper information cannot be null");
  }

}

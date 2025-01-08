using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class Company_Model
{
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("CompanyName")]
  public string Name { get; set; }

  [JsonProperty("ListGrapper")]
  public List<Grapper_Information_Model> List_Grapper_Information_Model { get; set; }

  [Preserve]
  [JsonConstructor]
  public Company_Model(string id, string name, List<Grapper_Information_Model> listGrapperInformationModel)
  {
    // Kiểm tra id không được null
    Id = id;
    // Kiểm tra name không được null
    Name = name;
    // Kiểm tra List_Grapper_Information_Model không được null
    List_Grapper_Information_Model = listGrapperInformationModel;

  }

  [Preserve]
  [JsonConstructor]
  public Company_Model()
  {
    // Id = string.Empty;
    // Name = string.Empty;
    // List_Grapper_Information_Model = new List<Grapper_Information_Model>();
  }
}

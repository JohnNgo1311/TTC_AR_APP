
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class MccModel
{

  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("grapper")]
  public Grapper_General_Model Grapper_General_Model { get; set; }
  [JsonProperty("name")]
  public string CabinetCode { get; set; }

  [JsonProperty("type")]
  public string Type { get; set; }

  [JsonProperty("brand")]
  public string? Brand { get; set; }

  [JsonProperty("note")]
  public string? Note { get; set; }
  [JsonProperty("fieldDevices")]
  public List<FieldDeviceInformationModel> FieldDeviceInformationModel { get; set; }
  [Preserve]
  [JsonConstructor]
  public MccModel(int id, Grapper_General_Model grapper_General_Model, string cabinetCode, string type, string? brand, string? note, List<FieldDeviceInformationModel> fieldDeviceInformationModel)
  {
    Id = id;
    Grapper_General_Model = grapper_General_Model;
    CabinetCode = cabinetCode;
    Type = type;
    Brand = brand;
    Note = note;
    FieldDeviceInformationModel = fieldDeviceInformationModel;
  }

}
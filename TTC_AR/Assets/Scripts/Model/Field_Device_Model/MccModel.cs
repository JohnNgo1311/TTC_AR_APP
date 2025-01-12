
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#nullable enable

[Serializable]
public class MccModel
{

  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("grapper")]
  public Grapper_General_Model grapper_General_Model { get; set; }
  [JsonProperty("name")]
  public string CabinetCode { get; set; }

  [JsonProperty("type")]
  public string Type { get; set; }

  [JsonProperty("brand")]
  public string? Brand { get; set; }

  [JsonProperty("note")]
  public string? Note { get; set; }
  [JsonProperty("fieldDevices")]
  public FieldDeviceInformationModel fieldDeviceInformationModel { get; set; }
  public MccModel(int id, Grapper_General_Model grapper_General_Model, string cabinetCode, string type, string? brand, string? note, FieldDeviceInformationModel fieldDeviceInformationModel)
  {
    Id = id;
    this.grapper_General_Model = grapper_General_Model;
    CabinetCode = cabinetCode;
    Type = type;
    Brand = brand;
    Note = note;
    this.fieldDeviceInformationModel = fieldDeviceInformationModel;
  }

  public class FieldDevice_General_Model
  {
    [JsonProperty("Id")]
    public string Id { get; set; }
    [JsonProperty("Type")]
    public string Type { get; set; }
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Cabinet_Code")]
    public string Cabinet_Code { get; set; }
    public FieldDevice_General_Model(string id, string type, string name, string cabinet_Code)
    {
      Id = id;
      Type = type;
      Name = name;
      Cabinet_Code = cabinet_Code;
    }
  }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class MccInformationModel
{

  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;
  [JsonProperty("cabinetCode")]
  public string CabinetCode { get; set; } = string.Empty;

  [JsonProperty("brand")]
  public string? Brand { get; set; }
  [JsonProperty("ListFieldDevices")]
  public List<FieldDeviceInformationModel>? ListFieldDeviceInformation { get; set; }

  [JsonProperty("note")]
  public string? Note { get; set; }

  [Preserve]

  public MccInformationModel(string id, string cabinetCode, string? brand, List<FieldDeviceInformationModel>? listFieldDeviceInformation, string? note)
  {
    Id = id;
    CabinetCode = cabinetCode;
    Brand = brand;
    ListFieldDeviceInformation = listFieldDeviceInformation;
    Note = note;
  }
  public MccInformationModel(string cabinetCode, string? brand, List<FieldDeviceInformationModel>? listFieldDeviceInformation, string? note)
  {
    CabinetCode = cabinetCode;
    Brand = brand;
    ListFieldDeviceInformation = listFieldDeviceInformation;
    Note = note;
  }
  public MccInformationModel(string id, string cabinetCode)
  {
    Id = id;
    CabinetCode = cabinetCode;
  }
}

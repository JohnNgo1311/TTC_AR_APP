
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class MccInformationModel
{

  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("cabinetCode")]
  public string CabinetCode { get; set; }

  [JsonProperty("brand")]
  public string? Brand { get; set; }
  [JsonProperty("listFieldDevices")]
  public List<FieldDeviceInformationModel> ListFieldDeviceInformation { get; set; }

  [JsonProperty("note")]
  public string? Note { get; set; }

  [Preserve]

  public MccInformationModel(string id, string cabinetCode, string? brand, List<FieldDeviceInformationModel> listFieldDeviceInformation, string? note)
  {
    Id = id;
    CabinetCode = cabinetCode;
    Brand = brand;
    ListFieldDeviceInformation = listFieldDeviceInformation;
    Note = note;
  }
}

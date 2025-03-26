
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class FieldDeviceInformationModel
{

  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;
  [JsonProperty("Name")]
  public string Name { get; set; } = string.Empty;
  [JsonProperty("Mcc")]
  public MccInformationModel? Mcc { get; set; }

  [JsonProperty("RatedPower")]
  public string? RatedPower { get; set; }

  [JsonProperty("RatedCurrent")]
  public string? RatedCurrent { get; set; }

  [JsonProperty("ActiveCurrent")]
  public string? ActiveCurrent { get; set; }

  [JsonProperty("ListConnectionImages")]
  public List<ImageInformationModel>? ListConnectionImages { get; set; }

  [JsonProperty("Note")]
  public string? Note { get; set; }
  [Preserve]

  public FieldDeviceInformationModel(string id, string name, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageInformationModel>? listConnectionImages, string? note)
  {
    Id = id;
    Name = name;
    RatedPower = ratedPower;
    RatedCurrent = ratedCurrent;
    ActiveCurrent = activeCurrent;
    ListConnectionImages = listConnectionImages;
    Note = note;
  }

  public FieldDeviceInformationModel(string name, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageInformationModel>? listConnectionImages, string? note)
  {
    Name = name;
    RatedPower = ratedPower;
    RatedCurrent = ratedCurrent;
    ActiveCurrent = activeCurrent;
    ListConnectionImages = listConnectionImages;
    Note = note;
  }
  public FieldDeviceInformationModel(string id, string name)
  {
    Id = id;
    Name = name;
  }


  [Preserve]
  public FieldDeviceInformationModel()
  {
  }

  public class FieldDevice_Basic_Model
  {
    [JsonProperty("Id")]
    public string Id { get; set; }
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("CabinetCode")]
    public string CabinetCode { get; set; }

    public FieldDevice_Basic_Model(string id, string name, string cabinetCode)
    {
      Id = id;
      Name = name;
      CabinetCode = cabinetCode;
    }

  }
}
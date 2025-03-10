
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class FieldDeviceInformationModel
{

  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("CabinetCode")]
  public string CabinetCode { get; set; }

  [JsonProperty("ratedPower")]
  public string? RatedPower { get; set; }

  [JsonProperty("ratedCurrent")]
  public string? RatedCurrent { get; set; }

  [JsonProperty("activeCurrent")]
  public string? ActiveCurrent { get; set; }

  [JsonProperty("connectionImages")]
  public List<string> ListConnectionImages { get; set; }

  [JsonProperty("Note")]
  public List<string> Note { get; set; }

  [Preserve]

  public FieldDeviceInformationModel(int id, string name, string cabinetCode, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<string> listConnectionImages, List<string> note)
  {
    Id = id;
    Name = name;
    CabinetCode = cabinetCode;
    RatedPower = ratedPower;
    RatedCurrent = ratedCurrent;
    ActiveCurrent = activeCurrent;
    ListConnectionImages = listConnectionImages;
    Note = note;
  }


  public class FieldDevice_Basic_Model
  {
    [JsonProperty("Id")]
    public int Id { get; set; }
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("CabinetCode")]
    public string CabinetCode { get; set; }

    public FieldDevice_Basic_Model(int id, string name, string cabinetCode)
    {
      Id = id;
      Name = name;
      CabinetCode = cabinetCode;
    }

  }
}
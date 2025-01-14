
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class FieldDeviceInformationModel
{

  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("ratedPower")]
  public string? RatedPower { get; set; }

  [JsonProperty("ratedCurrent")]
  public string? RatedCurrent { get; set; }

  [JsonProperty("activeCurrent")]
  public string? ActiveCurrent { get; set; }

  [JsonProperty("connectionImages")]
  public List<string> ListConnectionImages { get; set; }
  [Preserve]
  [JsonConstructor]
  public FieldDeviceInformationModel(int id, string name, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<string> listConnectionImages)
  {
    Id = id;
    Name = name;
    RatedPower = ratedPower;
    RatedCurrent = ratedCurrent;
    ActiveCurrent = activeCurrent;
    ListConnectionImages = listConnectionImages;

  }

  public class FieldDevice_General_Model
  {
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }

    public FieldDevice_General_Model(int id, string name)
    {
      Id = id;
      Name = name;
    }
  }
}
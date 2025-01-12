
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#nullable enable

[Serializable]
public class FieldDeviceInformationModel
{

  [JsonProperty("id")]
  public string Id { get; set; }
  [JsonProperty("type")]
  public string Type { get; set; }
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

  [JsonProperty("note")]
  public string? Note { get; set; }
  public FieldDeviceInformationModel(string id, string type, string name, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<string> listConnectionImages, string? note)
  {
    Id = id;
    Type = type;
    Name = name;
    RatedPower = ratedPower;
    RatedCurrent = ratedCurrent;
    ActiveCurrent = activeCurrent;
    ListConnectionImages = listConnectionImages;
    Note = note;
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
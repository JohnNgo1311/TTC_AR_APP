
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#nullable enable

[Serializable]
public class FieldDevice_Information_Model
{

  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Type")]
  public string Type { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("Cabinet_Code")]
  public string Cabinet_Code { get; set; }

  [JsonProperty("Brand")]
  public string? Brand { get; set; }

  [JsonProperty("Rated_Power")]
  public string? Rated_Power { get; set; }

  [JsonProperty("Output_Power")]
  public string? Output_Power { get; set; }
  [JsonProperty("Rated_Current")]
  public string? Rated_Current { get; set; }

  [JsonProperty("Operating_Current")]
  public string? Operating_Current { get; set; }
  [JsonProperty("Operating_Voltage")]
  public string? Operating_Voltage { get; set; }
  [JsonProperty("Frequency")]
  public string? Frequency { get; set; }
  [JsonProperty("Rotation_Speed")]
  public string? Rotation_Speed { get; set; }
  [JsonProperty("List_connection_Images")]
  public List<string> List_connection_Images { get; set; }

  [JsonProperty("Noted")]
  public string? Noted { get; set; }

  public FieldDevice_Information_Model(string id, string type, string name, string cabinet_Code, string? brand, string? rated_Power, string? output_Power, string? rated_Current, string? operating_Current, string? operating_Voltage, string? frequency, string? rotation_Speed, List<string> list_connection_Images, string? noted)
  {
    Id = id;
    Type = type;
    Name = name;
    Cabinet_Code = cabinet_Code;
    Brand = brand;
    Rated_Power = rated_Power;
    Output_Power = output_Power;
    Rated_Current = rated_Current;
    Operating_Current = operating_Current;
    Operating_Voltage = operating_Voltage;
    Frequency = frequency;
    Rotation_Speed = rotation_Speed;
    List_connection_Images = list_connection_Images;
    Noted = noted;
  }

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
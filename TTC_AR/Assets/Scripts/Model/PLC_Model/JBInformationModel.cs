using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class JBInformationModel
{
#nullable enable
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("location")]
  public string Location { get; set; }

  [JsonProperty("outdoorImages")]
  public string OutdoorImage { get; set; }

  [JsonProperty("connectionImages")]
  public List<string> ListConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBInformationModel(int id, string name, string location, string outdoor_Image, List<string> list_Connection_Images)
  {
    Id = id;
    Name = name;
    Location = location;
    OutdoorImage = outdoor_Image;
    ListConnectionImages = list_Connection_Images;
  }

}


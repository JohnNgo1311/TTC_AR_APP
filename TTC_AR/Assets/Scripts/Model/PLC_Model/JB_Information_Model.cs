using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class JB_Information_Model
{
#nullable enable
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("Location")]
  public string Location { get; set; }

  [JsonProperty("Outdoor_Image")]
  public string Outdoor_Image { get; set; }

  [JsonProperty("List_Connection_Images")]
  public List<string> List_Connection_Images { get; set; }

  [Preserve]
  [JsonConstructor]
  public JB_Information_Model(string id, string name, string location, string outdoor_Image, List<string> list_Connection_Images)
  {
    Id = id;
    Name = name;
    Location = location;
    Outdoor_Image = outdoor_Image;
    List_Connection_Images = list_Connection_Images;
  }

}


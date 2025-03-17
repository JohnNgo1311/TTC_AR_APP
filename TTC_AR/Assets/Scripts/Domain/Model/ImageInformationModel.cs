using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class ImageInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("Url")]
  public string url { get; set; }
  [Preserve]
  [JsonConstructor]
  public ImageInformationModel(string id, string name, string url)
  {
    Id = id;
    Name = name;
    this.url = url;
  }
}


[Preserve]
public class ImageBasicModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [Preserve]
  [JsonConstructor]
  public ImageBasicModel(int id, string name)
  {
    Id = id;
    Name = name;
  }
}
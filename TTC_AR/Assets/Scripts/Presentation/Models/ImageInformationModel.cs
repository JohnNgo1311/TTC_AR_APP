using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
[Preserve]
public class ImageInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;

  [JsonProperty("Name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("Url")]
  public string? url { get; set; }
  [Preserve]
  public ImageInformationModel(string id, string name, string url)
  {
    Id = id;
    Name = name;
    this.url = url;
  }
  public ImageInformationModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
  public ImageInformationModel()
  {
  }
}


[Preserve]
public class ImageBasicModel
{
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Name")]
  public string Name { get; set; }

  [Preserve]

  public ImageBasicModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
}
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos
{
  public class ImageBasicDto
  {
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("Name")] public string Name { get; set; }

    [Preserve]
    [JsonConstructor]
    public ImageBasicDto(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }

  }


  [Preserve]
  public class ImageResponseDto : ImageBasicDto
  {
    [JsonProperty("Url")] public string url { get; set; }

    [Preserve]
    [JsonConstructor]
    public ImageResponseDto(int id, string name, string url) : base(id, name)
    {
      this.url = url;
    }
  }


  [Preserve]
  public class ImageBasicRequestDto
  {
    [JsonProperty("Name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("ByteString")] public byte[] ByteString { get; set; } = new byte[0];


    [Preserve]
    [JsonConstructor]
    public ImageBasicRequestDto(string name, byte[] byteString)
    {
      Name = name ?? throw new System.ArgumentException(nameof(name));
      ByteString = byteString ?? throw new System.ArgumentException(nameof(byteString));
    }
  }
}
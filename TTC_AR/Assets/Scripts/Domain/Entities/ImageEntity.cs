
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class ImageEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("Url")]
    public string? Url { get; set; }

    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTImage.GetDescription(),
        HttpMethodTypeEnum.GETListImage.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeUrl()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETImage.GetDescription(),
        HttpMethodTypeEnum.GETJB.GetDescription(),
        HttpMethodTypeEnum.GETListJB.GetDescription(),
        HttpMethodTypeEnum.GETDevice.GetDescription(),
        HttpMethodTypeEnum.GETListDevice.GetDescription(),
        HttpMethodTypeEnum.GETFieldDevice.GetDescription(),
      };
      return allowedRequests.Contains(apiRequestType);
    }
    [Preserve]
    public ImageEntity()
    {
    }

    [Preserve]
    public ImageEntity(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    [Preserve]
    public ImageEntity(string id, string name, string url)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Url = string.IsNullOrEmpty(url) ? "Chưa cập nhật" : url;
    }
  }
}
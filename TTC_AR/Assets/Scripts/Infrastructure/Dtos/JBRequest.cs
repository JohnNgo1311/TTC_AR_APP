using System.Collections.Generic;
using ApplicationLayer.Dtos;
using Domain.Entities;
using Newtonsoft.Json;
#nullable enable
namespace Infrastructure.Dtos
{
    public class JBRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("deviceEntities")]
        public List<DeviceRequestForJB> DeviceEntities { get; set; } = new();

        [JsonProperty("moduleEntities")]
        public List<ModuleRequestForJB> ModuleEntities { get; set; } = new();

        [JsonProperty("outdoorImageEntity")]
        public ImageRequestForJB? OutdoorImageEntity { get; set; }

        [JsonProperty("connectionImageEntities")]
        public List<ImageRequestForJB> ConnectionImageEntities { get; set; } = new();
    }
    public class JBListResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("deviceEntities")]
        public List<DeviceRequestForJB> DeviceEntities { get; set; } = new();

        [JsonProperty("moduleEntities")]
        public List<ModuleRequestForJB> ModuleEntities { get; set; } = new();

        [JsonProperty("outdoorImageEntity")]
        public ImageEntity? OutdoorImageEntity { get; set; }

        [JsonProperty("connectionImageEntities")]
        public List<ImageEntity> ConnectionImageEntities { get; set; } = new();
    }
    
    public class DeviceRequestForJB
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; } = string.Empty;
    }
    public class ModuleRequestForJB
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class ImageRequestForJB
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
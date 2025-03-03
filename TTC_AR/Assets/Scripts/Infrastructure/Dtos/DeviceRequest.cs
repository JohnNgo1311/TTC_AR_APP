using System.Collections.Generic;
using Domain.Entities;
using Newtonsoft.Json;
#nullable enable
namespace Infrastructure.Dtos
{
    public class DeviceRequest
    {
        [JsonProperty("code")] public string Code { get; set; } = string.Empty;
        [JsonProperty("function")] public string Function { get; set; } = string.Empty;
        [JsonProperty("range")] public string Range { get; set; } = string.Empty;
        [JsonProperty("unit")] public string Unit { get; set; } = string.Empty;
        [JsonProperty("ioAddress")] public string IOAddress { get; set; } = string.Empty;
        [JsonProperty("module")] public ModuleRequestForDevice ModuleEntity { get; set; } = new();
        [JsonProperty("jb")] public JBRequestForDevice JBEntity { get; set; } = new();
        [JsonProperty("additionalConnectionImages")] public List<ImageRequestForDevice> AdditionalImageEntity { get; set; } = new();
    }


    public class JBRequestForDevice
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;
    }
    public class ModuleRequestForDevice
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class ImageRequestForDevice
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
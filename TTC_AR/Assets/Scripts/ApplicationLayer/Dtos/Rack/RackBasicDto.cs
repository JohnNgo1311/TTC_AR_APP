using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackBasicDto
    {
        [JsonProperty("Id")] public string Id { get; set; }
        [JsonProperty("Name")] public string Name { get; set; }

        [Preserve]

        public RackBasicDto(string id, string name)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentException(nameof(name)) : name;
        }
    }




}
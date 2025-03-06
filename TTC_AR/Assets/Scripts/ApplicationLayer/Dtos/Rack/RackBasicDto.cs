using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackBasicDto
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Name")] public string Name { get; set; }

        [Preserve]

        public RackBasicDto(int id, string name)
        {
            Id = id;
            Name = name == "" ? throw new ArgumentException(nameof(name)) : name;
        }
    }




}
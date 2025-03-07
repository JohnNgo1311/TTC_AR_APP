
using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Module;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable    
namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackRequestDto
    {
        [JsonProperty("Name")] public string Name { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicDto>? ModuleBasicDtos { get; set; }

        [Preserve]

        public RackRequestDto(string name, List<ModuleBasicDto>? moduleBasicDtos)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentException(nameof(name)) : name;
            ModuleBasicDtos = moduleBasicDtos ?? new List<ModuleBasicDto>();
        }

    }
}
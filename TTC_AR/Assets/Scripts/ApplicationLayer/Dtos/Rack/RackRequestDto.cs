
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackRequestDto
    {
        [JsonProperty("Name")] public string Name { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicModel> ModuleBasicModels { get; set; }

        [Preserve]

        public RackRequestDto(string name, List<ModuleBasicModel> moduleBasicModels)
        {
            Name = name == "" ? throw new System.ArgumentException(nameof(name)) : name;
            ModuleBasicModels = moduleBasicModels ?? new List<ModuleBasicModel>();
        }

    }
}
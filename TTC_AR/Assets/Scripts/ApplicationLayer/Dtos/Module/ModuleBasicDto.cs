using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.Module
{
    public class ModuleBasicDto
    {
        [JsonProperty("Id")] public string Id { get; set; }
        [JsonProperty("Name")] public string Name { get; set; }

        [Preserve]
        public ModuleBasicDto(string id, string name)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
        }
    }





}



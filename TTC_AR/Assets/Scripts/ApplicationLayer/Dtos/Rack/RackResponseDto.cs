using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Module;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackResponseDto : RackBasicDto
    {
        [JsonProperty("ListModules")] public List<ModuleBasicDto> ModuleBasicDtos { get; set; }

        [Preserve]
        public RackResponseDto(string id, string name, List<ModuleBasicDto> moduleBasicDtos) : base(id, name)
        {
            ModuleBasicDtos = moduleBasicDtos;
        }
    }
}
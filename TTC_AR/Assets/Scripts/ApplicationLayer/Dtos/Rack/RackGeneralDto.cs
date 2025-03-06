using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackGeneralDto : RackBasicDto
    {
        [JsonProperty("ListModules")] public List<ModuleBasicModel> ModuleBasicModels { get; set; }

        [Preserve]

        public RackGeneralDto(int id, string name, List<ModuleBasicModel> moduleBasicModels) : base(id, name)
        {
            ModuleBasicModels = moduleBasicModels ?? new List<ModuleBasicModel>();
        }
    }
}
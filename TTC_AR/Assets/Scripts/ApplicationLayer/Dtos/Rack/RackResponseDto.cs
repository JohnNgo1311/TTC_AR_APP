using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Rack
{
    [Preserve]
    public class RackResponseDto : RackBasicDto
    {
        [JsonProperty("ListModules")] public List<ModuleGeneralModel> ModuleGeneralModels { get; set; }


        [Preserve]

        public RackResponseDto(int id, string name, List<ModuleGeneralModel> moduleGeneralModels) : base(id, name)
        {
            ModuleGeneralModels = moduleGeneralModels ?? new List<ModuleGeneralModel>();
        }

    }
}
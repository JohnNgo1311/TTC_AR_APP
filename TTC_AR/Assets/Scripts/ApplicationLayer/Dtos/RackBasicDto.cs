using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos
{
    [Preserve]
    public class RackBasicDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }

        [Preserve]
        [JsonConstructor]
        public RackBasicDto(int id, string name)
        {
            Id = id;
            Name = name == "" ? throw new ArgumentException(nameof(name)) : name;
        }
    }


    [Preserve]
    public class RackResponseDto : RackBasicDto
    {
        [JsonProperty("ListModules")] public List<ModuleGeneralModel> ModuleGeneralModels { get; set; }


        [Preserve]
        [JsonConstructor]
        public RackResponseDto(int id, string name, List<ModuleGeneralModel> moduleGeneralModels) : base(id, name)
        {
            ModuleGeneralModels = moduleGeneralModels ?? new List<ModuleGeneralModel>();
        }

    }

    [Preserve]
    public class RackGeneralDto : RackBasicDto
    {
        [JsonProperty("ListModules")] public List<ModuleBasicModel> ModuleBasicModels { get; set; }

        [Preserve]
        [JsonConstructor]
        public RackGeneralDto(int id, string name, List<ModuleBasicModel> moduleBasicModels) : base(id, name)
        {
            ModuleBasicModels = moduleBasicModels ?? new List<ModuleBasicModel>();
        }
    }

    [Preserve]
    public class RackRequestDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicModel> ModuleBasicModels { get; set; }

        [Preserve]
        [JsonConstructor]
        public RackRequestDto(string name, List<ModuleBasicModel> moduleBasicModels)
        {
            Name = name == "" ? throw new System.ArgumentException(nameof(name)) : name;
            ModuleBasicModels = moduleBasicModels ?? new List<ModuleBasicModel>();
        }

    }
}
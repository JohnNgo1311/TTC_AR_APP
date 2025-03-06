using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.ModuleSpecification;
using ApplicationLayer.Dtos.Rack;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.Module
{
    [Preserve]
    public class ModuleRequestDto
    {
        [JsonProperty("Name")] public string Name { get; set; } = string.Empty;
        [JsonProperty("Rack")] public RackBasicDto RackBasicDto { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; } = new List<DeviceBasicDto>();
        [JsonProperty("listJBs")] public List<JBBasicDto> JBBasicDtos { get; set; } = new List<JBBasicDto>();
        [JsonProperty("moduleSpecification")] public ModuleSpecificationBasicDto? ModuleSpecificationBasicDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicDto? AdapterSpecificationBasicDto { get; set; }

        [Preserve]

        public ModuleRequestDto(string name, RackBasicDto rackBasicDto, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, ModuleSpecificationBasicDto? moduleSpecificationBasicDto, AdapterSpecificationBasicDto? adapterSpecificationBasicDto)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            RackBasicDto = rackBasicDto ?? throw new ArgumentNullException(nameof(rackBasicDto));
            DeviceBasicDtos = deviceBasicDtos;
            JBBasicDtos = jBBasicDtos;
            ModuleSpecificationBasicDto = moduleSpecificationBasicDto;
            AdapterSpecificationBasicDto = adapterSpecificationBasicDto;
        }

    }
}

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
    public class ModuleResponseDto : ModuleBasicDto
    {
        [JsonProperty("Rack")] public RackBasicDto RackBasicDto { get; set; }
        [JsonProperty("listDevices")] public List<DeviceGeneralDto> DeviceGeneralDtos { get; set; } = new List<DeviceGeneralDto>();
        [JsonProperty("listJBs")] public List<JBGeneralDto> JBGeneralDtos { get; set; } = new List<JBGeneralDto>();
        [JsonProperty("moduleSpecification")] public ModuleSpecificationResponseDto? ModuleSpecificationResponseDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationResponseDto? AdapterSpecificationResponseDto { get; set; }

        [Preserve]

        public ModuleResponseDto(int id, string name, RackBasicDto rackBasicDto, List<DeviceGeneralDto> deviceGeneralDtos, List<JBGeneralDto> jbGeneralDtos, ModuleSpecificationResponseDto? moduleSpecificationResponseDto, AdapterSpecificationResponseDto? adapterSpecificationResponseDto) : base(id, name)
        {
            RackBasicDto = rackBasicDto ?? throw new ArgumentNullException(nameof(rackBasicDto));
            DeviceGeneralDtos = deviceGeneralDtos;
            JBGeneralDtos = jbGeneralDtos;
            ModuleSpecificationResponseDto = moduleSpecificationResponseDto;
            AdapterSpecificationResponseDto = adapterSpecificationResponseDto;
        }

    }
}
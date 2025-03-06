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
    public class ModuleGeneralDto : ModuleBasicDto //! Để GetModuleGeneral và GetListModuleInformation
    {
        [JsonProperty("Rack")] public RackBasicDto RackBasicDto { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; } = new List<DeviceBasicDto>();
        [JsonProperty("listJBs")] public List<JBBasicDto> JBBasicDtos { get; set; } = new List<JBBasicDto>();
        [JsonProperty("moduleSpecification")] public ModuleSpecificationBasicDto? ModuleSpecificationBasicDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicDto? AdapterSpecificationBasicDto { get; set; }

        [Preserve]

        public ModuleGeneralDto(int id, string name, RackBasicDto rackBasicDto, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, ModuleSpecificationBasicDto? moduleSpecificationBasicDto, AdapterSpecificationBasicDto? adapterSpecificationBasicDto) : base(id, name)
        {
            RackBasicDto = rackBasicDto ?? throw new ArgumentNullException(nameof(rackBasicDto));
            DeviceBasicDtos = deviceBasicDtos;
            JBBasicDtos = jBBasicDtos;
            ModuleSpecificationBasicDto = moduleSpecificationBasicDto;
            AdapterSpecificationBasicDto = adapterSpecificationBasicDto;
        }
    }
}
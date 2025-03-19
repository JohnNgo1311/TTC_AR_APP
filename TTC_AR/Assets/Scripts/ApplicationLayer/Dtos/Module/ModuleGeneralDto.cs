using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.AdapterSpecification;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Grapper;
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
        [JsonProperty("Grapper")] public GrapperBasicDto GrapperBasicDto { get; set; }
        [JsonProperty("Rack")] public RackBasicDto? RackBasicDto { get; set; }
        [JsonProperty("ListDevices")] public List<DeviceBasicDto>? DeviceBasicDtos { get; set; }
        [JsonProperty("ListJBs")] public List<JBBasicDto>? JBBasicDtos { get; set; }
        [JsonProperty("ModuleSpecification")] public ModuleSpecificationBasicDto? ModuleSpecificationBasicDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicDto? AdapterSpecificationBasicDto { get; set; }

        [Preserve]
        public ModuleGeneralDto(string id, string name, GrapperBasicDto grapperBasicDto, RackBasicDto? rackBasicDto, List<DeviceBasicDto>? deviceBasicDtos, List<JBBasicDto>? jBBasicDtos, ModuleSpecificationBasicDto? moduleSpecificationBasicDto, AdapterSpecificationBasicDto? adapterSpecificationBasicDto) : base(id, name)
        {
            GrapperBasicDto = grapperBasicDto;
            RackBasicDto = rackBasicDto;
            DeviceBasicDtos = deviceBasicDtos;
            JBBasicDtos = jBBasicDtos;
            ModuleSpecificationBasicDto = moduleSpecificationBasicDto;
            AdapterSpecificationBasicDto = adapterSpecificationBasicDto;
        }
    }
}
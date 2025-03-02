using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos
{
    public class ModuleBasicDto
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Name")] public string Name { get; set; }
        [Preserve]
        [JsonConstructor]
        public ModuleBasicDto(int id, string name)
        {
            Id = id;
            Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
        }
    }

    [Preserve]
    public class ModuleResponseDto : ModuleBasicDto
    {
        [JsonProperty("Rack")] public RackBasicDto RackBasicDto { get; set; }
        [JsonProperty("listDevices")] public List<DeviceGeneralDto> DeviceGeneralDtos { get; set; }
        [JsonProperty("listJBs")] public List<JBGeneralDto> JBGeneralDtos { get; set; }
        [JsonProperty("moduleSpecification")] public ModuleSpecificationResponseDto? ModuleSpecificationResponseDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationResponseDto? AdapterSpecificationResponseDto { get; set; }

        [Preserve]
        [JsonConstructor]
        public ModuleResponseDto(int id, string name, RackBasicDto rackBasicDto, List<DeviceGeneralDto> deviceGeneralDtos, List<JBGeneralDto> jbGeneralDtos, ModuleSpecificationResponseDto? moduleSpecificationResponseDto, AdapterSpecificationResponseDto? adapterSpecificationResponseDto) : base(id, name)
        {
            RackBasicDto = rackBasicDto ?? throw new ArgumentNullException(nameof(rackBasicDto));
            DeviceGeneralDtos = deviceGeneralDtos ?? throw new ArgumentNullException(nameof(deviceGeneralDtos));
            JBGeneralDtos = jbGeneralDtos ?? throw new ArgumentNullException(nameof(jbGeneralDtos));
            ModuleSpecificationResponseDto = moduleSpecificationResponseDto ?? throw new ArgumentNullException(nameof(moduleSpecificationResponseDto));
            AdapterSpecificationResponseDto = adapterSpecificationResponseDto ?? throw new ArgumentNullException(nameof(adapterSpecificationResponseDto));
        }

    }


    [Preserve]
    public class ModuleGeneralDto : ModuleBasicDto //! Để GetModuleGeneral và GetListModuleInformation
    {
        [JsonProperty("Rack")] public RackBasicDto RackBasicDto { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; } = new List<DeviceBasicDto>();
        [JsonProperty("listJBs")] public List<JBBasicDto> JBBasicDtos { get; set; } = new List<JBBasicDto>();
        [JsonProperty("moduleSpecification")] public ModuleSpecificationBasicDto? ModuleSpecificationBasicDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicDto? AdapterSpecificationBasicDto { get; set; }

        [Preserve]
        [JsonConstructor]
        public ModuleGeneralDto(int id, string name, RackBasicDto rackBasicDto, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, ModuleSpecificationBasicDto? moduleSpecificationBasicDto, AdapterSpecificationBasicDto? adapterSpecificationBasicDto) : base(id, name)
        {
            RackBasicDto = rackBasicDto ?? throw new ArgumentNullException(nameof(rackBasicDto));
            DeviceBasicDtos = deviceBasicDtos ?? new List<DeviceBasicDto>();
            JBBasicDtos = jBBasicDtos ?? new List<JBBasicDto>();
            ModuleSpecificationBasicDto = moduleSpecificationBasicDto;
            AdapterSpecificationBasicDto = adapterSpecificationBasicDto;
        }
    }

    [Preserve]
    public class ModuleRequestDto
    {
        [JsonProperty("Name")] public string Name { get; set; } = string.Empty;
        [JsonProperty("Rack")] public string RackName { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; } = new List<DeviceBasicDto>();
        [JsonProperty("listJBs")] public List<JBBasicDto> JBBasicDtos { get; set; } = new List<JBBasicDto>();
        [JsonProperty("moduleSpecification")] public ModuleSpecificationBasicDto? ModuleSpecificationBasicDto { get; set; }
        [JsonProperty("AdapterSpecification")] public AdapterSpecificationBasicDto? AdapterSpecificationBasicDto { get; set; }

        [Preserve]
        [JsonConstructor]
        public ModuleRequestDto(string name, string rackName, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, ModuleSpecificationBasicDto? moduleSpecificationBasicDto, AdapterSpecificationBasicDto? adapterSpecificationBasicDto)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            RackName = rackName ?? throw new ArgumentException(nameof(rackName));
            DeviceBasicDtos = deviceBasicDtos ?? new List<DeviceBasicDto>();
            JBBasicDtos = jBBasicDtos ?? new List<JBBasicDto>();
            ModuleSpecificationBasicDto = moduleSpecificationBasicDto;
            AdapterSpecificationBasicDto = adapterSpecificationBasicDto;
        }
    }
}



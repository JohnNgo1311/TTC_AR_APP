using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Module;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace ApplicationLayer.Dtos.Device
{
    [Preserve]
    public class DeviceResponseDto : DeviceBasicDto
    {
        [JsonProperty("Function")] public string? Function { get; set; }

        [JsonProperty("Range")] public string? Range { get; set; }

        [JsonProperty("Unit")] public string? Unit { get; set; }
        [JsonProperty("IOAddress")] public string? IOAddress { get; set; }
        [JsonProperty("Module")] public ModuleBasicDto? ModuleBasicDto { get; set; }
        [JsonProperty("JBs")] public List<JBGeneralDto>? JBGeneralDtos { get; set; }
        [JsonProperty("AdditionalConnectionImages")] public List<ImageBasicDto>? AdditionalImageBasicDtos { get; set; } = new List<ImageBasicDto>();

        [Preserve]

        public DeviceResponseDto(string id, string code, string? function, string? range, string? unit, string? ioAddress, ModuleBasicDto? moduleBasicDto, List<JBGeneralDto>? jbGeneralDtos, List<ImageBasicDto>? additionalImageBasicDtos) : base(id, code)
        {
            Function = function;
            Range = range;
            Unit = unit;
            IOAddress = ioAddress;
            ModuleBasicDto = moduleBasicDto;
            JBGeneralDtos = jbGeneralDtos;
            AdditionalImageBasicDtos = additionalImageBasicDtos;
        }
        // [Preserve]
        // 
        // public DeviceResponseDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBGeneralDto jbGeneralDto, List<ImageBasicDto> additionalImageBasicDto) : base(id, code)
        // {
        //     Function = function == "" ? string.Empty : function;
        //     Range = range == "" ? string.Empty : range;
        //     Unit = unit == "" ? string.Empty : unit;
        //     IOAddress = ioAddress == "" ? string.Empty : ioAddress;
        //     ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
        //     JBGeneralDto = jbGeneralDto ?? throw new ArgumentException(nameof(jbGeneralDto));
        //     AdditionalImageBasicDto = additionalImageBasicDto ?? new List<ImageBasicDto>();
        // }
    }
}




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
        [JsonProperty("Function")] public string Function { get; set; }

        [JsonProperty("Range")] public string Range { get; set; }

        [JsonProperty("Unit")] public string Unit { get; set; }
        [JsonProperty("IOAddress")] public string IOAddress { get; set; }
        [JsonProperty("Module")] public ModuleBasicDto? ModuleBasicDto { get; set; }
        [JsonProperty("JB")] public JBGeneralDto? JBGeneralDto { get; set; }
        [JsonProperty("AdditionalConnectionImages")] public List<ImageResponseDto>? AdditionalImageResponseDtos { get; set; } = new List<ImageResponseDto>();

        [Preserve]

        public DeviceResponseDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto? moduleBasicDto, JBGeneralDto? jbGeneralDto, List<ImageResponseDto>? additionalImageResponseDtos) : base(id, code)
        {
            Function = function;
            Range = range;
            Unit = unit;
            IOAddress = ioAddress;
            ModuleBasicDto = moduleBasicDto;
            JBGeneralDto = jbGeneralDto;
            AdditionalImageResponseDtos = additionalImageResponseDtos;
        }
        // [Preserve]
        // 
        // public DeviceResponseDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBGeneralDto jbGeneralDto, List<ImageResponseDto> additionalImageResponseDto) : base(id, code)
        // {
        //     Function = function == "" ? string.Empty : function;
        //     Range = range == "" ? string.Empty : range;
        //     Unit = unit == "" ? string.Empty : unit;
        //     IOAddress = ioAddress == "" ? string.Empty : ioAddress;
        //     ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
        //     JBGeneralDto = jbGeneralDto ?? throw new ArgumentException(nameof(jbGeneralDto));
        //     AdditionalImageResponseDto = additionalImageResponseDto ?? new List<ImageResponseDto>();
        // }
    }
}




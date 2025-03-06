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
    public class DeviceGeneralDto : DeviceBasicDto //! Để GetListDeviceInformation và làm Property cho GetModuleInformation
    {
        [JsonProperty("Function")] public string Function { get; set; } = string.Empty;
        [JsonProperty("Range")] public string Range { get; set; } = string.Empty;
        [JsonProperty("Unit")] public string Unit { get; set; } = string.Empty;
        [JsonProperty("IOAddress")] public string IOAddress { get; set; } = string.Empty;
        [JsonProperty("Module")] public ModuleBasicDto? ModuleBasicDto { get; set; }
        [JsonProperty("JB")] public JBBasicDto? JBBasicDto { get; set; }
        [JsonProperty("AdditionalConnectionImages")] public List<ImageResponseDto>? AdditionalImageResponseDtos { get; set; } = new List<ImageResponseDto>();

        [Preserve]

        public DeviceGeneralDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto? moduleBasicDto, JBBasicDto? jbBasicDto, List<ImageResponseDto>? additionalImageResponseDtos) : base(id, code)
        {
            Function = function;
            Range = range;
            Unit = unit;
            IOAddress = ioAddress;
            ModuleBasicDto = moduleBasicDto;
            JBBasicDto = jbBasicDto;
            AdditionalImageResponseDtos = additionalImageResponseDtos;
        }
        // [Preserve]
        // 
        // public DeviceGeneralDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBBasicDto jbBasicDto, List<ImageResponseDto> additionalImageResponseDtos) : base(id, code)
        // {
        //     Function = function == "" ? string.Empty : function;
        //     Range = range == "" ? string.Empty : range;
        //     Unit = unit == "" ? string.Empty : unit;
        //     IOAddress = ioAddress == "" ? string.Empty : ioAddress;
        //     ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
        //     JBBasicDto = jbBasicDto ?? throw new ArgumentException(nameof(jbBasicDto));
        //     AdditionalImageResponseDtos = additionalImageResponseDtos ?? new List<ImageResponseDto>();
        // }

    }
}
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
    public class DeviceRequestDto
    {
        [JsonProperty("Code")] public string Code { get; set; }
        [JsonProperty("Function")] public string Function { get; set; }
        [JsonProperty("Range")] public string Range { get; set; }
        [JsonProperty("Unit")] public string Unit { get; set; }
        [JsonProperty("IOAddress")] public string IOAddress { get; set; }
        [JsonProperty("Module")] public ModuleBasicDto? ModuleBasicDto { get; set; }
        [JsonProperty("JB")] public JBBasicDto? JBBasicDto { get; set; }
        [JsonProperty("AdditionalConnectionImages")] public List<ImageBasicDto>? AdditionalImageBasicDtos { get; set; }

        [Preserve]

        public DeviceRequestDto(string code, string function, string range, string unit, string ioAddress, ModuleBasicDto? moduleBasicDto, JBBasicDto? jbBasicDto, List<ImageBasicDto>? additionalImageBasicDtos)
        {
            Code = string.IsNullOrEmpty(code) ? throw new System.ArgumentException(nameof(code)) : code;
            Function = function;
            Range = range;
            Unit = unit;
            IOAddress = ioAddress;
            ModuleBasicDto = moduleBasicDto ?? null;
            JBBasicDto = jbBasicDto ?? null;
            AdditionalImageBasicDtos = additionalImageBasicDtos ?? null;
        }

        // [Preserve]
        // 
        // public DeviceRequestDto(string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBBasicDto jbBasicDto, List<ImageBasicDto> additionalImageBasicDtos)
        // {
        //     Code = string.IsNullOrEmpty(code) ? throw new System.ArgumentException(nameof(code)) : code;
        //     Function = function == "" ? string.Empty : function;
        //     Range = range == "" ? string.Empty : range;
        //     Unit = unit == "" ? string.Empty : unit;
        //     IOAddress = ioAddress == "" ? string.Empty : ioAddress;
        //     ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
        //     JBBasicDto = jbBasicDto ?? throw new ArgumentException(nameof(jbBasicDto));
        //     AdditionalImageBasicDtos = additionalImageBasicDtos ?? new List<ImageBasicDto>();
        // }
    }
}
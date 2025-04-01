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
        [JsonProperty("Function")] public string Function { get; set; }
        [JsonProperty("Range")] public string Range { get; set; }
        [JsonProperty("Unit")] public string Unit { get; set; }
        [JsonProperty("IOAddress")] public string IOAddress { get; set; }
        [JsonProperty("Module")] public ModuleBasicDto? ModuleBasicDto { get; set; }
        [JsonProperty("JBs")] public List<JBBasicDto>? JBBasicDtos { get; set; }
        [JsonProperty("AdditionalConnectionImages")] public List<ImageBasicDto>? AdditionalImageBasicDtos { get; set; }

        [Preserve]

        public DeviceGeneralDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto? moduleBasicDto, List<JBBasicDto>? jbBasicDtos, List<ImageBasicDto>? additionalImageBasicDtos) : base(id, code)
        {
            Function = function;
            Range = range;
            Unit = unit;
            IOAddress = ioAddress;
            ModuleBasicDto = moduleBasicDto;
            JBBasicDtos = jbBasicDtos;
            AdditionalImageBasicDtos = additionalImageBasicDtos;
        }
        // [Preserve]
        // 
        // public DeviceGeneralDto(string id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBBasicDto jbBasicDto, List<ImageBasicDto> additionalImageBasicDtos) : base(id, code)
        // {
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
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace ApplicationLayer.Dtos
{
    [Preserve]
    public class DeviceBasicDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("code")] public string Code { get; set; }

        [Preserve]
        [JsonConstructor]
        public DeviceBasicDto(int id, string code)
        {
            Id = id;
            Code = code == "" ? throw new System.ArgumentException(nameof(code)) : code;
        }
    }

    [Preserve]
    public class DeviceResponseDto : DeviceBasicDto
    {
        [JsonProperty("function")] public string Function { get; set; }

        [JsonProperty("range")] public string Range { get; set; }

        [JsonProperty("unit")] public string Unit { get; set; }
        [JsonProperty("ioAddress")] public string IOAddress { get; set; }
        [JsonProperty("module")] public ModuleBasicDto ModuleBasicDto { get; set; }
        [JsonProperty("jb")] public JBGeneralDto JBResponseDto { get; set; }
        [JsonProperty("additionalConnectionImages")] public List<ImageResponseDto> AdditionalImageResponseDto { get; set; }

        [Preserve]
        [JsonConstructor]
        public DeviceResponseDto(int id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBGeneralDto jbResponseDto, List<ImageResponseDto> additionalImageResponseDto) : base(id, code)
        {
            Function = function == "" ? string.Empty : function;
            Range = range == "" ? string.Empty : range;
            Unit = unit == "" ? string.Empty : unit;
            IOAddress = ioAddress == "" ? string.Empty : ioAddress;
            ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
            JBResponseDto = jbResponseDto ?? throw new ArgumentException(nameof(jbResponseDto));
            AdditionalImageResponseDto = additionalImageResponseDto ?? new List<ImageResponseDto>();
        }
    }


    [Preserve]
    public class DeviceGeneralDto : DeviceBasicDto //! Để GetListDeviceInformation và làm Property cho GetModuleInformation
    {
        [JsonProperty("function")] public string Function { get; set; }
        [JsonProperty("range")] public string Range { get; set; }
        [JsonProperty("unit")] public string Unit { get; set; }
        [JsonProperty("ioAddress")] public string IOAddress { get; set; }
        [JsonProperty("module")] public ModuleBasicDto ModuleBasicDto { get; set; }
        [JsonProperty("jb")] public JBBasicDto JBBasicDto { get; set; }
        [JsonProperty("additionalConnectionImages")] public List<ImageResponseDto> AdditionalImageResponseDtos { get; set; }

        [Preserve]
        [JsonConstructor]
        public DeviceGeneralDto(int id, string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBBasicDto jbBasicDto, List<ImageResponseDto> additionalImageResponseDtos) : base(id, code)
        {
            Function = function == "" ? string.Empty : function;
            Range = range == "" ? string.Empty : range;
            Unit = unit == "" ? string.Empty : unit;
            IOAddress = ioAddress == "" ? string.Empty : ioAddress;
            ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
            JBBasicDto = jbBasicDto ?? throw new ArgumentException(nameof(jbBasicDto));
            AdditionalImageResponseDtos = additionalImageResponseDtos ?? new List<ImageResponseDto>();
        }


    }

    [Preserve]
    public class DeviceRequestDto
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("function")] public string Function { get; set; }
        [JsonProperty("range")] public string Range { get; set; }
        [JsonProperty("unit")] public string Unit { get; set; }
        [JsonProperty("ioAddress")] public string IOAddress { get; set; }
        [JsonProperty("module")] public ModuleBasicDto ModuleBasicDto { get; set; }
        [JsonProperty("jb")] public JBBasicDto JBBasicDto { get; set; }
        [JsonProperty("additionalConnectionImages")] public List<ImageBasicDto> AdditionalImageBasicDto { get; set; }

        [Preserve]
        [JsonConstructor]
        public DeviceRequestDto(string code, string function, string range, string unit, string ioAddress, ModuleBasicDto moduleBasicDto, JBBasicDto jbBasicDto, List<ImageBasicDto> additionalImageBasicDto)
        {
            Code = code;
            Function = function == "" ? string.Empty : function;
            Range = range == "" ? string.Empty : range;
            Unit = unit == "" ? string.Empty : unit;
            IOAddress = ioAddress == "" ? string.Empty : ioAddress;
            ModuleBasicDto = moduleBasicDto ?? throw new ArgumentException(nameof(moduleBasicDto));
            JBBasicDto = jbBasicDto ?? throw new ArgumentException(nameof(jbBasicDto));
            AdditionalImageBasicDto = additionalImageBasicDto ?? new List<ImageBasicDto>();
        }

    }


}
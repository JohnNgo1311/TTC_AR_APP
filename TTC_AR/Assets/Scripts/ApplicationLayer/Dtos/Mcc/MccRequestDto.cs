using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.FieldDevice;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos.Mcc
{
    [Preserve]
    public class MccRequestDto
    {
        [JsonProperty("cabinetCode")]
        public string? CabinetCode { get; set; }

        [JsonProperty("brand")]
        public string? Brand { get; set; }

        [JsonProperty("listFieldDevice")]
        public List<FieldDeviceBasicDto>? FieldDeviceBasicDtos { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [Preserve]

        public MccRequestDto(string cabinetCode, string? brand, List<FieldDeviceBasicDto>? fieldDeviceBasicDtos, string? note)
        {
            CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
            Brand = brand;
            FieldDeviceBasicDtos = fieldDeviceBasicDtos;
            Note = note;
        }
    }
}
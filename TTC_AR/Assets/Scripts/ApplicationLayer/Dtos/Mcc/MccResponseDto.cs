using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.FieldDevice;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos.Mcc
{
    [Preserve]
    public class MccResponseDto : MccBasicDto
    {

        [JsonProperty("brand")]
        public string? Brand { get; set; }

        [JsonProperty("listFieldDevices")]
        public List<FieldDeviceBasicDto>? FieldDeviceBasicDtos { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [Preserve]

        public MccResponseDto(int id, string cabinetCode, string? brand, List<FieldDeviceBasicDto>? fieldDeviceBasicDtos, string? note) : base(id, cabinetCode)
        {
            Brand = brand;
            FieldDeviceBasicDtos = fieldDeviceBasicDtos;
            Note = note;
        }
    }
}
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

        [JsonProperty("Brand")]
        public string? Brand { get; set; }

        [JsonProperty("ListFieldDevices")]
        public List<FieldDeviceBasicDto>? FieldDeviceBasicDtos { get; set; }

        [JsonProperty("Note")]
        public string? Note { get; set; }

        [Preserve]

        public MccResponseDto(string id, string cabinetCode, string? brand, List<FieldDeviceBasicDto>? fieldDeviceBasicDtos, string? note) : base(id, cabinetCode)
        {
            Brand = brand;
            FieldDeviceBasicDtos = fieldDeviceBasicDtos;
            Note = note;
        }
    }
}
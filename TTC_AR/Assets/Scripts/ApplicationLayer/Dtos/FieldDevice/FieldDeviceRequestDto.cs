using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Mcc;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos.FieldDevice
{
    [Preserve]
    public class FieldDeviceRequestDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ratedPower")]
        public string? RatedPower { get; set; }

        [JsonProperty("ratedCurrent")]
        public string? RatedCurrent { get; set; }

        [JsonProperty("activeCurrent")]
        public string? ActiveCurrent { get; set; }

        [JsonProperty("listConnectionImages")]
        public List<ImageBasicDto>? ConnectionImageBasicDtos { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [Preserve]

        public FieldDeviceRequestDto(string? name, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageBasicDto>? connectionImageBasicDtos, string? note)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            RatedPower = ratedPower;
            RatedCurrent = ratedCurrent;
            ActiveCurrent = activeCurrent;
            ConnectionImageBasicDtos = connectionImageBasicDtos;
            Note = note;
        }
    }
}
using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Mcc;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.FieldDevice
{
    [Preserve]
    public class FieldDeviceRequestDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("Mcc")]
        public MccBasicDto MccBasicDto { get; set; }

        [JsonProperty("ratedPower")]
        public string RatedPower { get; set; } = string.Empty;

        [JsonProperty("ratedCurrent")]
        public string RatedCurrent { get; set; } = string.Empty;

        [JsonProperty("activeCurrent")]
        public string ActiveCurrent { get; set; } = string.Empty;

        [JsonProperty("connectionImages")]
        public List<ImageBasicDto> ConnectionImageBasicDtos { get; set; } = new List<ImageBasicDto>();

        [JsonProperty("Note")]
        public string Note { get; set; } = string.Empty;

        [Preserve]

        public FieldDeviceRequestDto(string name, MccBasicDto mccBasicDto, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageBasicDto> connectionImageBasicDtos, string note)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            MccBasicDto = mccBasicDto ?? throw new ArgumentNullException(nameof(mccBasicDto));
            RatedPower = ratedPower ?? string.Empty;
            RatedCurrent = ratedCurrent ?? string.Empty;
            ActiveCurrent = activeCurrent ?? string.Empty;
            ConnectionImageBasicDtos = connectionImageBasicDtos ?? new List<ImageBasicDto>();
            Note = note ?? string.Empty;
        }
    }
}
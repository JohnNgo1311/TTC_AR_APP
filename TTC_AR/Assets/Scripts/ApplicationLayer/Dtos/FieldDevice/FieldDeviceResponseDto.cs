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
    public class FieldDeviceResponseDto : FieldDeviceBasicDto
    {
        [JsonProperty("Mcc")]
        public MccBasicDto? Mcc { get; set; }

        [JsonProperty("RatedPower")]
        public string? RatedPower { get; set; }

        [JsonProperty("RatedCurrent")]
        public string? RatedCurrent { get; set; }

        [JsonProperty("ActiveCurrent")]
        public string? ActiveCurrent { get; set; }

        [JsonProperty("ListConnectionImages")]
        public List<ImageResponseDto>? ConnectionImages { get; set; }

        [JsonProperty("Note")]
        public string? Note { get; set; }
        [Preserve]

        public FieldDeviceResponseDto(string id, string? name, MccBasicDto? mcc, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageResponseDto>? connectionImages, string? note) : base(id, name)
        {
            Mcc = mcc ?? throw new ArgumentNullException(nameof(mcc));
            RatedPower = ratedPower;
            RatedCurrent = ratedCurrent;
            ActiveCurrent = activeCurrent;
            ConnectionImages = connectionImages;
            Note = note;
        }
    }
}
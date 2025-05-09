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
        [JsonProperty("mcc")]
        public MccBasicDto? Mcc { get; set; }

        [JsonProperty("ratedPower")]
        public string? RatedPower { get; set; }

        [JsonProperty("ratedCurrent")]
        public string? RatedCurrent { get; set; }

        [JsonProperty("activeCurrent")]
        public string? ActiveCurrent { get; set; }

        [JsonProperty("listConnectionImages")]
        public List<ImageBasicDto>? ConnectionImages { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }
        [Preserve]

        public FieldDeviceResponseDto(int id, string? name, MccBasicDto? mcc, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageBasicDto>? connectionImages, string? note) : base(id, name)
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
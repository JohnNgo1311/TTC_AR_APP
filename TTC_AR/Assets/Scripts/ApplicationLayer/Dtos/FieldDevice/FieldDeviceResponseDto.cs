using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Mcc;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.FieldDevice
{
    [Preserve]
    public class FieldDeviceResponseDto : FieldDeviceBasicDto
    {
        [JsonProperty("Mcc")]
        public MccBasicDto Mcc { get; set; }

        [JsonProperty("ratedPower")]
        public string RatedPower { get; set; } = string.Empty;

        [JsonProperty("ratedCurrent")]
        public string RatedCurrent { get; set; } = string.Empty;

        [JsonProperty("activeCurrent")]
        public string ActiveCurrent { get; set; } = string.Empty;

        [JsonProperty("connectionImages")]
        public List<ImageResponseDto> ConnectionImages { get; set; } = new List<ImageResponseDto>();

        [JsonProperty("Note")]
        public string Note { get; set; } = string.Empty;

        [Preserve]

        public FieldDeviceResponseDto(int id, string name, MccBasicDto mcc, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageResponseDto> connectionImages, string note) : base(id, name)
        {
            Mcc = mcc ?? throw new ArgumentNullException(nameof(mcc));
            RatedPower = ratedPower ?? string.Empty;
            RatedCurrent = ratedCurrent ?? string.Empty;
            ActiveCurrent = activeCurrent ?? string.Empty;
            ConnectionImages = connectionImages ?? new List<ImageResponseDto>();
            Note = note ?? string.Empty;
        }
    }
}
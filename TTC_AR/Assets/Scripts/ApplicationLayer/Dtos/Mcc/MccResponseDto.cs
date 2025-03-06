using System.Collections.Generic;
using ApplicationLayer.Dtos.FieldDevice;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Mcc
{
    [Preserve]
    public class MccResponseDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("cabinetCode")]
        public string CabinetCode { get; set; } = string.Empty;

        [JsonProperty("brand")]
        public string Brand { get; set; } = string.Empty;

        [JsonProperty("fieldDeviceEntities")]
        public List<FieldDeviceBasicDto> FieldDeviceEntities { get; set; } = new List<FieldDeviceBasicDto>();

        [JsonProperty("note")]
        public string Note { get; set; } = string.Empty;

        [Preserve]

        public MccResponseDto(int id, string cabinetCode, string brand, List<FieldDeviceBasicDto> fieldDeviceEntities, string note)
        {
            Id = id;
            CabinetCode = cabinetCode ?? string.Empty;
            Brand = brand ?? string.Empty;
            FieldDeviceEntities = fieldDeviceEntities ?? new List<FieldDeviceBasicDto>();
            Note = note ?? string.Empty;
        }
    }
}
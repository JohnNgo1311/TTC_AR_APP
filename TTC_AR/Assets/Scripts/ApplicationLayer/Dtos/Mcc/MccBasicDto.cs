using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Mcc
{
    [Preserve]
    public class MccBasicDto
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("CabinetCode")]
        public string CabinetCode { get; set; }

        [Preserve]

        public MccBasicDto(string id, string cabinetCode)
        {
            Id = string.IsNullOrEmpty(id) ? throw new ArgumentNullException(nameof(id)) : id;
            CabinetCode = string.IsNullOrEmpty(cabinetCode) ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
        }
    }
}
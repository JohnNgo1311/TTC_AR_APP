using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Image;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace ApplicationLayer.Dtos.Device
{ 
    [Preserve]
    public class DeviceBasicDto
    {
        [JsonProperty("Id")] public string Id { get; set; }
        [JsonProperty("Code")] public string Code { get; set; }

        [Preserve]

        public DeviceBasicDto(string id, string code)
        {
            Id = id;
            Code = string.IsNullOrEmpty(code) ? throw new System.ArgumentException(nameof(code)) : code;
        }
    }



}
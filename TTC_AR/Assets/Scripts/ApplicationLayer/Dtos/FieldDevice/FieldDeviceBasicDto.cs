using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.FieldDevice
{
    [Preserve]
    public class FieldDeviceBasicDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [Preserve]

        public FieldDeviceBasicDto(int id, string name)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
        }
    }
}
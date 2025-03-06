using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.Image;
using Domain.Entities;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
namespace ApplicationLayer.Dtos.JB
{
    // DTO cơ bản cho JB (dùng làm field trong DTO khác, có Id)
    [Preserve]
    public class JBBasicDto
    {
        [JsonProperty("Id")] public string Id { get; set; }
        [JsonProperty("Name")] public string Name { get; set; }

        [Preserve]

        public JBBasicDto(string id, string name)
        {
            Id = id;
            Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
        }
    }

}

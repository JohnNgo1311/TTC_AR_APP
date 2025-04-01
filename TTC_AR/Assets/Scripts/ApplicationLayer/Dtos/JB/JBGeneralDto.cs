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
    [Preserve]
    public class JBGeneralDto : JBBasicDto
    {
        [JsonProperty("Location")] public string? Location { get; set; }
        [JsonProperty("OutdoorImage")] public ImageBasicDto? OutdoorImageBasicDto { get; set; }
        [JsonProperty("ListConnectionImages")] public List<ImageBasicDto>? ConnectionImageBasicDtos { get; set; }

        [Preserve]

        public JBGeneralDto(string id, string name, string? location, ImageBasicDto? outdoorImageBasicDto, List<ImageBasicDto>? connectionImageBasicDtos) : base(id, name)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Location = location;
            OutdoorImageBasicDto = outdoorImageBasicDto;
            ConnectionImageBasicDtos = connectionImageBasicDtos;
        }

        // [Preserve]
        // 
        // public JBGeneralDto(string id, string name, string location, ImageBasicDto outdoorImageBasicDto, List<ImageBasicDto> connectionImageBasicDtos) : base(id, name)
        // {
        //     Id = id;
        //     Location = location == "" ? string.Empty : location;
        //     OutdoorImageBasicDto = outdoorImageBasicDto;
        //     ConnectionImageBasicDtos = connectionImageBasicDtos.Any() ? connectionImageBasicDtos : new List<ImageBasicDto>();
        // }
    }
}
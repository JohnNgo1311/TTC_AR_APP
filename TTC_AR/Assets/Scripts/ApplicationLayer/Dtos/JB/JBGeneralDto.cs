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
        [JsonProperty("OutdoorImage")] public ImageResponseDto? OutdoorImageResponseDto { get; set; }
        [JsonProperty("ListConnectionImages")] public List<ImageResponseDto>? ConnectionImageResponseDtos { get; set; }

        [Preserve]

        public JBGeneralDto(string id, string name, string location, ImageResponseDto? outdoorImageResponseDto, List<ImageResponseDto>? connectionImageResponseDtos) : base(id, name)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Location = location;
            OutdoorImageResponseDto = outdoorImageResponseDto;
            ConnectionImageResponseDtos = connectionImageResponseDtos;
        }

        // [Preserve]
        // 
        // public JBGeneralDto(string id, string name, string location, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto> connectionImageResponseDtos) : base(id, name)
        // {
        //     Id = id;
        //     Location = location == "" ? string.Empty : location;
        //     OutdoorImageResponseDto = outdoorImageResponseDto;
        //     ConnectionImageResponseDtos = connectionImageResponseDtos.Any() ? connectionImageResponseDtos : new List<ImageResponseDto>();
        // }
    }
}
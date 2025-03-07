using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
namespace ApplicationLayer.Dtos.JB
{
    [Preserve]
    public class JBRequestDto
    {
        [JsonProperty("Name")] public string Name { get; set; }
        [JsonProperty("Location")] public string Location { get; set; }
        [JsonProperty("ListDevices")] public List<DeviceBasicDto>? DeviceBasicDtos { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicDto>? ModuleBasicDtos { get; set; }
        [JsonProperty("OutdoorImage")] public ImageBasicDto? OutdoorImageBasicDto { get; set; }
        [JsonProperty("ListConnectionImages")] public List<ImageBasicDto>? ConnectionImageBasicDtos { get; set; }

        [Preserve]

        public JBRequestDto(string name, string location, List<DeviceBasicDto>? deviceBasicDtos, List<ModuleBasicDto>? moduleBasicDtos, ImageBasicDto? outdoorImageBasicDto, List<ImageBasicDto>? connectionImageBasicDtos)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Location = location;
            DeviceBasicDtos = deviceBasicDtos;
            ModuleBasicDtos = moduleBasicDtos;
            OutdoorImageBasicDto = outdoorImageBasicDto;
            ConnectionImageBasicDtos = connectionImageBasicDtos;
        }

        // [Preserve]
        // 
        // public JBRequestDto(string name, string location, List<DeviceBasicDto> deviceBasicDtos, List<ModuleBasicDto> moduleBasicDtos, ImageBasicDto outdoorImageBasicDto, List<ImageBasicDto> connectionImageBasicDtos)
        // {
        //     Name = name ?? throw new ArgumentException(nameof(name));
        //     Location = location == "" ? string.Empty : location;
        //     DeviceBasicDtos = deviceBasicDtos.Any() ? deviceBasicDtos : new List<DeviceBasicDto>();
        //     ModuleBasicDtos = moduleBasicDtos.Any() ? moduleBasicDtos : new List<ModuleBasicDto>();
        //     OutdoorImageBasicDto = outdoorImageBasicDto;
        //     ConnectionImageBasicDtos = connectionImageBasicDtos.Any() ? connectionImageBasicDtos : new List<ImageBasicDto>();
        // }
    }
}
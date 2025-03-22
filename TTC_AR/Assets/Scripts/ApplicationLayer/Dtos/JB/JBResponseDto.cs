
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
    public class JBResponseDto : JBBasicDto //!Get ListJB & GetJBById
    {
        [JsonProperty("Location")] public string? Location { get; set; }
        [JsonProperty("ListDevices")] public List<DeviceBasicDto>? DeviceBasicDtos { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicDto>? ModuleBasicDtos { get; set; }
        [JsonProperty("OutdoorImage")] public ImageResponseDto? OutdoorImageResponseDto { get; set; }
        [JsonProperty("ListConnectionImages")] public List<ImageResponseDto>? ConnectionImageResponseDtos { get; set; }

        [Preserve]

        public JBResponseDto(string id, string name, string location, List<DeviceBasicDto>? deviceBasicDtos, List<ModuleBasicDto>? moduleBasicDtos, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto>? connectionImageResponseDtos) : base(id, name)
        {
            Location = location;
            DeviceBasicDtos = deviceBasicDtos;
            ModuleBasicDtos = moduleBasicDtos;
            OutdoorImageResponseDto = outdoorImageResponseDto;
            ConnectionImageResponseDtos = connectionImageResponseDtos;
        }
        // [Preserve]
        // 
        // public JBResponseDto(string id, string name, string location, List<DeviceBasicDto>?? deviceBasicDtos, List<ModuleBasicDto>? moduleBasicDtos, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto>? connectionImageResponseDtos) : base(id, name)
        // {
        //     Location = location == "" ? string.Empty : location;
        //     DeviceBasicDtos = deviceBasicDtos.Any() ? deviceBasicDtos : new List<DeviceBasicDto>??();
        //     ModuleBasicDtos = moduleBasicDtos.Any() ? moduleBasicDtos : new List<ModuleBasicDto>?();
        //     OutdoorImageResponseDto = outdoorImageResponseDto;
        //     ConnectionImageResponseDtos = connectionImageResponseDtos.Any() ? connectionImageResponseDtos : new List<ImageResponseDto>?();
        // }


    }
}
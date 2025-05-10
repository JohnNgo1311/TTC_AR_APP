
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
        [JsonProperty("location")] public string? Location { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto>? DeviceBasicDtos { get; set; }
        [JsonProperty("listModules")] public List<ModuleBasicDto>? ModuleBasicDtos { get; set; }
        [JsonProperty("outdoorImage")] public ImageBasicDto? OutdoorImageBasicDto { get; set; }
        [JsonProperty("listConnectionImages")] public List<ImageBasicDto>? ConnectionImageBasicDtos { get; set; }

        [Preserve]

        public JBResponseDto(int id, string name, string location, List<DeviceBasicDto>? deviceBasicDtos, List<ModuleBasicDto>? moduleBasicDtos, ImageBasicDto outdoorImageBasicDto, List<ImageBasicDto>? connectionImageBasicDtos) : base(id, name)
        {
            Location = location;
            DeviceBasicDtos = deviceBasicDtos;
            ModuleBasicDtos = moduleBasicDtos;
            OutdoorImageBasicDto = outdoorImageBasicDto;
            ConnectionImageBasicDtos = connectionImageBasicDtos;
        }
        // [Preserve]
        // 
        // public JBResponseDto(int id, string name, string location, List<DeviceBasicDto>?? deviceBasicDtos, List<ModuleBasicDto>? moduleBasicDtos, ImageBasicDto outdoorImageBasicDto, List<ImageBasicDto>? connectionImageBasicDtos) : base(id, name)
        // {
        //     Location = location == "" ? string.Empty : location;
        //     DeviceBasicDtos = deviceBasicDtos.Any() ? deviceBasicDtos : new List<DeviceBasicDto>??();
        //     ModuleBasicDtos = moduleBasicDtos.Any() ? moduleBasicDtos : new List<ModuleBasicDto>?();
        //     OutdoorImageBasicDto = outdoorImageBasicDto;
        //     ConnectionImageBasicDtos = connectionImageBasicDtos.Any() ? connectionImageBasicDtos : new List<ImageBasicDto>?();
        // }


    }
}
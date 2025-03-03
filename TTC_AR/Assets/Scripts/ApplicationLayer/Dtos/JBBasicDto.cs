// Application/Dtos/JBDto.cs (chỉ phần liên quan)
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos
{
    // DTO cơ bản cho JB (dùng làm field trong DTO khác, có Id)
    [Preserve]
    public class JBBasicDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }

        [Preserve]
        [JsonConstructor]
        public JBBasicDto(int id, string name)
        {
            Id = id;
            Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
        }
    }

    // DTO cho request (POST/PUT), không có Id
    [Preserve]
    public class JBRequestDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("location")] public string Location { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; }
        [JsonProperty("listModules")] public List<ModuleBasicDto> ModuleBasicDtos { get; set; }
        [JsonProperty("outdoorImage")] public ImageBasicDto? OutdoorImageBasicDto { get; set; }
        [JsonProperty("listConnectionImages")] public List<ImageBasicDto> ConnectionImageBasicDtos { get; set; }

        [Preserve]
        [JsonConstructor]
        public JBRequestDto(string name, string location, List<DeviceBasicDto> deviceBasicDtos, List<ModuleBasicDto> moduleBasicDtos, ImageBasicDto outdoorImageBasicDto, List<ImageBasicDto> connectionImageBasicDtos)
        {
            Name = name;
            Location = location;
            DeviceBasicDtos = deviceBasicDtos;
            ModuleBasicDtos = moduleBasicDtos;
            OutdoorImageBasicDto = outdoorImageBasicDto;
            ConnectionImageBasicDtos = connectionImageBasicDtos;
        }

        // [Preserve]
        // [JsonConstructor]
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

    // DTO cho response (GET), có Id
    [Preserve]
    public class JBResponseDto : JBBasicDto //!Get ListJB & GetJBById
    {
        [JsonProperty("location")] public string Location { get; set; }
        [JsonProperty("listDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; }
        [JsonProperty("listModules")] public List<ModuleBasicDto> ModuleBasicDtos { get; set; }
        [JsonProperty("outdoorImage")] public ImageResponseDto? OutdoorImageResponseDto { get; set; }
        [JsonProperty("listConnectionImages")] public List<ImageResponseDto> ConnectionImageResponseDtos { get; set; }

        [Preserve]
        [JsonConstructor]
        public JBResponseDto(int id, string name, string location, List<DeviceBasicDto> deviceBasicDtos, List<ModuleBasicDto> moduleBasicDtos, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto> connectionImageResponseDtos) : base(id, name)
        {
            Location = location;
            DeviceBasicDtos = deviceBasicDtos;
            ModuleBasicDtos = moduleBasicDtos;
            OutdoorImageResponseDto = outdoorImageResponseDto;
            ConnectionImageResponseDtos = connectionImageResponseDtos;
        }
        // [Preserve]
        // [JsonConstructor]
        // public JBResponseDto(int id, string name, string location, List<DeviceBasicDto> deviceBasicDtos, List<ModuleBasicDto> moduleBasicDtos, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto> connectionImageResponseDtos) : base(id, name)
        // {
        //     Location = location == "" ? string.Empty : location;
        //     DeviceBasicDtos = deviceBasicDtos.Any() ? deviceBasicDtos : new List<DeviceBasicDto>();
        //     ModuleBasicDtos = moduleBasicDtos.Any() ? moduleBasicDtos : new List<ModuleBasicDto>();
        //     OutdoorImageResponseDto = outdoorImageResponseDto;
        //     ConnectionImageResponseDtos = connectionImageResponseDtos.Any() ? connectionImageResponseDtos : new List<ImageResponseDto>();
        // }
    }

    [Preserve]
    public class JBGeneralDto : JBBasicDto
    {
        [JsonProperty("location")] public string Location { get; set; }
        [JsonProperty("outdoorImage")] public ImageResponseDto OutdoorImageResponseDto { get; set; }
        [JsonProperty("listConnectionImages")] public List<ImageResponseDto> ConnectionImageResponseDtos { get; set; }

        [Preserve]
        [JsonConstructor]
        public JBGeneralDto(int id, string name, string location, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto> connectionImageResponseDtos) : base(id, name)
        {
            Id = id;
            Location = location;
            OutdoorImageResponseDto = outdoorImageResponseDto;
            ConnectionImageResponseDtos = connectionImageResponseDtos;
        }

        // [Preserve]
        // [JsonConstructor]
        // public JBGeneralDto(int id, string name, string location, ImageResponseDto outdoorImageResponseDto, List<ImageResponseDto> connectionImageResponseDtos) : base(id, name)
        // {
        //     Id = id;
        //     Location = location == "" ? string.Empty : location;
        //     OutdoorImageResponseDto = outdoorImageResponseDto;
        //     ConnectionImageResponseDtos = connectionImageResponseDtos.Any() ? connectionImageResponseDtos : new List<ImageResponseDto>();
        // }
    };

}

using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.FieldDevice;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Dtos.Module;
using ApplicationLayer.Dtos.Rack;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace ApplicationLayer.Dtos.Grapper
{

    [Preserve]
    public class GrapperResponseDto : GrapperBasicDto
    {
        [JsonProperty("ListRacks")] public List<RackBasicDto>? RackBasicDtos { get; set; }
        [JsonProperty("ListModules")] public List<ModuleBasicDto>? ModuleGeneralDtos { get; set; }
        [JsonProperty("ListDevices")] public List<DeviceBasicDto>? DeviceBasicDtos { get; set; }
        [JsonProperty("ListJBs")] public List<JBBasicDto>? JBBasicDtos { get; set; }
        [JsonProperty("ListMCCs")] public List<MccBasicDto>? MccBasicDtos { get; set; }
        [JsonProperty("ListFieldDevices")] public List<FieldDeviceBasicDto>? FieldDeviceBasicDtos { get; set; }


        [Preserve]
        public GrapperResponseDto(int id, string name, List<RackBasicDto>? rackBasicDtos, List<ModuleBasicDto> moduleBasicDtos, List<DeviceBasicDto>? deviceBasicDtos, List<JBBasicDto>? jBBasicDtos, List<MccBasicDto>? mccBasicDtos, List<FieldDeviceBasicDto>? fieldDeviceBasicDtos) : base(id, name)
        {   
            RackBasicDtos = rackBasicDtos;
            ModuleGeneralDtos = moduleBasicDtos;
            DeviceBasicDtos = deviceBasicDtos;
            JBBasicDtos = jBBasicDtos;
            MccBasicDtos = mccBasicDtos;
            FieldDeviceBasicDtos = fieldDeviceBasicDtos;
        }
    }

}
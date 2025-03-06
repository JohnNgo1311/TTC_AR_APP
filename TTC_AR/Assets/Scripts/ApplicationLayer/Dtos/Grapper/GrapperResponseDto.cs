using System;
using System.Collections.Generic;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.FieldDevice;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Dtos.Rack;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Grapper
{

    [Preserve]
    public class GrapperResponseDto : GrapperBasicDto
    {
        [JsonProperty("ListRacks")] public List<RackGeneralDto> RackGeneralDtos { get; set; }
        [JsonProperty("ListDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; }
        [JsonProperty("ListJBs")] public List<JBBasicDto> JBBasicDtos { get; set; }
        [JsonProperty("ListMCCs")] public List<MccBasicDto> MccBasicDtos { get; set; }
        [JsonProperty("ListFieldDevices")] public List<FieldDeviceBasicDto> FieldDeviceBasicDtos { get; set; }


        [Preserve]

        public GrapperResponseDto(int id, string name, List<RackGeneralDto> rackGeneralDtos, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, List<MccBasicDto> mccBasicDtos, List<FieldDeviceBasicDto> fieldDeviceBasicDtos) : base(id, name)
        {
            RackGeneralDtos = rackGeneralDtos ?? throw new ArgumentException(nameof(rackGeneralDtos));
            DeviceBasicDtos = deviceBasicDtos ?? throw new ArgumentException(nameof(deviceBasicDtos));
            JBBasicDtos = jBBasicDtos ?? throw new ArgumentException(nameof(jBBasicDtos));
            MccBasicDtos = mccBasicDtos ?? throw new ArgumentException(nameof(mccBasicDtos));
            FieldDeviceBasicDtos = fieldDeviceBasicDtos ?? throw new ArgumentException(nameof(fieldDeviceBasicDtos));
        }


    }

}
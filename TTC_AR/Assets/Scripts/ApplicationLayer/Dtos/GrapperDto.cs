using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
namespace ApplicationLayer.Dtos
{

  [Preserve]
  public class GrapperBasicDto
  {
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;

    [Preserve]
    [JsonConstructor]
    public GrapperBasicDto(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
  }

  [Preserve]
  public class GrapperResponseDto : GrapperBasicDto
  {
    [JsonProperty("ListRacks")] public List<RackGeneralDto> RackGeneralDtos { get; set; }
    [JsonProperty("ListDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; }
    [JsonProperty("ListJBs")] public List<JBBasicDto> JBBasicDtos { get; set; }
    [JsonProperty("ListMCCs")] public List<MccBasicDto> MccBasicDtos { get; set; }
    [JsonProperty("ListFieldDevices")] public List<FieldDeviceBasicDto> FieldDeviceBasicDtos { get; set; }


    [Preserve]
    [JsonConstructor]
    public GrapperResponseDto(int id, string name, List<RackGeneralDto> rackGeneralDtos, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, List<MccBasicDto> mccBasicDtos, List<FieldDeviceBasicDto> fieldDeviceBasicDtos) : base(id, name)
    {
      RackGeneralDtos = rackGeneralDtos ?? throw new ArgumentException(nameof(rackGeneralDtos));
      DeviceBasicDtos = deviceBasicDtos ?? throw new ArgumentException(nameof(deviceBasicDtos));
      JBBasicDtos = jBBasicDtos ?? throw new ArgumentException(nameof(jBBasicDtos));
      MccBasicDtos = mccBasicDtos ?? throw new ArgumentException(nameof(mccBasicDtos));
      FieldDeviceBasicDtos = fieldDeviceBasicDtos ?? throw new ArgumentException(nameof(fieldDeviceBasicDtos));
    }


  }

  [Preserve]
  public class GrapperRequestDto
  {
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("ListRacks")] public List<RackBasicDto> RackBasicDtos { get; set; }
    [JsonProperty("ListDevices")] public List<DeviceBasicDto> DeviceBasicDtos { get; set; }
    [JsonProperty("ListJBs")] public List<JBBasicDto> JBBasicDtos { get; set; }
    [JsonProperty("ListMCCs")] public List<MccBasicDto> MccBasicDtos { get; set; }
    [JsonProperty("ListFieldDevices")] public List<FieldDeviceBasicDto> FieldDeviceBasicDtos { get; set; }


    [Preserve]
    [JsonConstructor]
    public GrapperRequestDto(string name, List<RackBasicDto> rackBasicDtos, List<DeviceBasicDto> deviceBasicDtos, List<JBBasicDto> jBBasicDtos, List<MccBasicDto> mccBasicDtos, List<FieldDeviceBasicDto> fieldDeviceBasicDtos)
    {
      Name = name ?? throw new ArgumentException(nameof(name));
      RackBasicDtos = rackBasicDtos ?? throw new ArgumentException(nameof(rackBasicDtos));
      DeviceBasicDtos = deviceBasicDtos ?? throw new ArgumentException(nameof(deviceBasicDtos));
      JBBasicDtos = jBBasicDtos ?? throw new ArgumentException(nameof(jBBasicDtos));
      MccBasicDtos = mccBasicDtos ?? throw new ArgumentException(nameof(mccBasicDtos));
      FieldDeviceBasicDtos = fieldDeviceBasicDtos ?? throw new ArgumentException(nameof(fieldDeviceBasicDtos));
    }
  }
}
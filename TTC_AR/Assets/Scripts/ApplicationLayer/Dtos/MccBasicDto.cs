
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace ApplicationLayer.Dtos
{
  [Preserve]
  public class MccBasicDto
  {
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("cabinetCode")] public string CabinetCode { get; set; }

    [Preserve]
    [JsonConstructor]
    public MccBasicDto(int id, string cabinetCode)
    {
      Id = id;
      CabinetCode = cabinetCode == "" ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
    }


  }

  [Preserve]
  public class MccResponseDto
  {
    [JsonProperty("brand")] public string Brand { get; set; }
    [JsonProperty("listFieldDevices")] public List<FieldDeviceBasicDto> FieldDeviceBasicDtos { get; set; }
    [JsonProperty("note")] public string Note { get; set; }

    [Preserve]
    [JsonConstructor]
    public MccResponseDto(string brand, List<FieldDeviceBasicDto> fieldDeviceBasicDtos, string note)
    {
      Brand = brand == "" ? string.Empty : brand;
      FieldDeviceBasicDtos = fieldDeviceBasicDtos ?? new List<FieldDeviceBasicDto>();
      Note = note == "" ? string.Empty : note;
    }
  }

  [Preserve]
  public class MccRequestDto
  {
    [JsonProperty("brand")] public string Brand { get; set; }
    [JsonProperty("listFieldDevices")] public List<FieldDeviceBasicDto> FieldDeviceBasicDtos { get; set; }
    [JsonProperty("note")] public string Note { get; set; }
    [Preserve]
    [JsonConstructor]
    public MccRequestDto(string brand, List<FieldDeviceBasicDto> fieldDeviceBasicDtos, string note)
    {
      Brand = brand == "" ? string.Empty : brand;
      FieldDeviceBasicDtos = fieldDeviceBasicDtos ?? new List<FieldDeviceBasicDto>();
      Note = note == "" ? string.Empty : note;
    }
  }
}
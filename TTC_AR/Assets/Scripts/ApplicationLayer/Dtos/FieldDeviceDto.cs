
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable
namespace ApplicationLayer.Dtos
{
  public class FieldDeviceBasicDto
  {
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [Preserve]
    [JsonConstructor]
    public FieldDeviceBasicDto(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new System.ArgumentException(nameof(name)) : name;
    }
  }

  [Preserve]
  public class FieldDeviceResponseDto : FieldDeviceBasicDto
  {
    [JsonProperty("CabinetCode")] public string CabinetCode { get; set; }

    [JsonProperty("ratedPower")] public string RatedPower { get; set; }

    [JsonProperty("ratedCurrent")] public string RatedCurrent { get; set; }

    [JsonProperty("activeCurrent")] public string ActiveCurrent { get; set; }

    [JsonProperty("connectionImages")] public List<ImageResponseDto> ConnectionImageResponseDtos { get; set; }

    [JsonProperty("Note")] public string Note { get; set; }

    [Preserve]
    [JsonConstructor]
    public FieldDeviceResponseDto(int id, string name, string cabinetCode, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageResponseDto> connectionImageResponseDtos, string note) : base(id, name)
    {
      CabinetCode = cabinetCode == "" ? string.Empty : cabinetCode;
      RatedPower = ratedPower == "" ? string.Empty : ratedPower;
      RatedCurrent = ratedCurrent == "" ? string.Empty : ratedCurrent;
      ActiveCurrent = activeCurrent == "" ? string.Empty : activeCurrent;
      ConnectionImageResponseDtos = connectionImageResponseDtos ?? new List<ImageResponseDto>();
      Note = note == "" ? string.Empty : note;
    }


  }


  [Preserve]
  public class FieldDeviceRequestDto
  {
    [JsonProperty("CabinetCode")] public string CabinetCode { get; set; }

    [JsonProperty("ratedPower")] public string RatedPower { get; set; }

    [JsonProperty("ratedCurrent")] public string RatedCurrent { get; set; }

    [JsonProperty("activeCurrent")] public string ActiveCurrent { get; set; }

    [JsonProperty("connectionImages")] public List<ImageBasicDto> ListConnectionImageBasicDtos { get; set; } = new List<ImageBasicDto>();

    [JsonProperty("Note")] public string Note { get; set; }

    [Preserve]
    [JsonConstructor]
    public FieldDeviceRequestDto(string cabinetCode, string ratedPower, string ratedCurrent, string activeCurrent, List<ImageBasicDto> listConnectionImageBasicDtos, string note)
    {
      CabinetCode = cabinetCode ?? throw new ArgumentException(nameof(cabinetCode));
      RatedPower = ratedPower == "" ? string.Empty : ratedPower;
      RatedCurrent = ratedCurrent == "" ? string.Empty : ratedCurrent;
      ActiveCurrent = activeCurrent == "" ? string.Empty : activeCurrent;
      ListConnectionImageBasicDtos = listConnectionImageBasicDtos ?? new List<ImageBasicDto>();
      Note = note == "" ? string.Empty : note;
    }


  }
}
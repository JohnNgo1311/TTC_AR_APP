using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  public class DeviceEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;
    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;
    [JsonProperty("Function", NullValueHandling = NullValueHandling.Ignore)]
    public string? Function { get; set; }
    [JsonProperty("Range", NullValueHandling = NullValueHandling.Ignore)]
    public string? Range { get; set; }
    [JsonProperty("Unit", NullValueHandling = NullValueHandling.Ignore)]
    public string? Unit { get; set; }
    [JsonProperty("IOAddress", NullValueHandling = NullValueHandling.Ignore)]
    public string? IOAddress { get; set; }

    [JsonProperty("Module")]
    public ModuleEntity? ModuleEntity { get; set; }

    [JsonProperty("JB")]
    public JBEntity? JBEntity { get; set; }
    [JsonProperty("AdditionalConnectionImages")]
    public List<ImageEntity>? AdditionalConnectionImageEntities { get; set; }


    [Preserve]
    public DeviceEntity()
    {
      AdditionalConnectionImageEntities = new List<ImageEntity>();
    }

    [Preserve]
    public DeviceEntity(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]

    public DeviceEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }


    [Preserve]
    public DeviceEntity(string id, string code, string function, string range, string unit, string ioAddress, ModuleEntity moduleEntity, JBEntity jbEntity, List<ImageEntity> additionalConnectionImageEntities)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Function = function == string.Empty ? "Chưa cập nhật" : function;
      Range = range == string.Empty ? "Chưa cập nhật" : range;
      Unit = unit == string.Empty ? "Chưa cập nhật" : unit;
      IOAddress = ioAddress == string.Empty ? "Chưa cập nhật" : ioAddress;
      ModuleEntity = moduleEntity ?? null;
      JBEntity = jbEntity ?? null;
      AdditionalConnectionImageEntities = !additionalConnectionImageEntities.Any() ? new List<ImageEntity>() : additionalConnectionImageEntities;

    }
    [Preserve]
    public DeviceEntity(string code, string function, string range, string unit, string ioAddress, ModuleEntity moduleEntity, JBEntity jbEntity, List<ImageEntity> additionalConnectionImageEntities)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Function = function == string.Empty ? "Chưa cập nhật" : function;
      Range = range == string.Empty ? "Chưa cập nhật" : range;
      Unit = unit == string.Empty ? "Chưa cập nhật" : unit;
      IOAddress = ioAddress == string.Empty ? "Chưa cập nhật" : ioAddress;
      ModuleEntity = moduleEntity ?? null;
      JBEntity = jbEntity ?? null;
      AdditionalConnectionImageEntities = additionalConnectionImageEntities ?? null;
    }
  }
}
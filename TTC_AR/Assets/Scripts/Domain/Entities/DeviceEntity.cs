using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  public class DeviceEntity
  {
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Function { get; set; } = string.Empty;
    public string Range { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string IOAddress { get; set; } = string.Empty;
    public ModuleEntity? ModuleEntity { get; set; }
    public JBEntity? JBEntity { get; set; }
    public List<ImageEntity> AdditionalConnectionImageEntities { get; set; } = new();

    [Preserve]
    [JsonConstructor]
    public DeviceEntity(int id, string code)
    {
      Id = id;
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    [JsonConstructor]
    public DeviceEntity(string code)
    {
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    [JsonConstructor]
    public DeviceEntity()
    {
    }

    [Preserve]
    [JsonConstructor]
    public DeviceEntity(int id, string code, string function, string range, string unit, string ioAddress, ModuleEntity moduleEntity, JBEntity jbEntity, List<ImageEntity> additionalConnectionImageEntities)
    {
      Id = id;
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
      Function = function == "" ? string.Empty : function;
      Range = range == "" ? string.Empty : range;
      Unit = unit == "" ? string.Empty : unit;
      IOAddress = ioAddress == "" ? string.Empty : ioAddress;
      ModuleEntity = moduleEntity ?? throw new ArgumentException(nameof(moduleEntity));
      JBEntity = jbEntity ?? throw new ArgumentException(nameof(jbEntity));
      AdditionalConnectionImageEntities = additionalConnectionImageEntities ?? new List<ImageEntity>();
    }


    [Preserve]
    [JsonConstructor]
    public DeviceEntity(string code, string function, string range, string unit, string ioAddress, ModuleEntity moduleEntity, JBEntity jbEntity, List<ImageEntity> additionalConnectionImageEntities)
    {

      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
      Function = function == "" ? string.Empty : function;
      Range = range == "" ? string.Empty : range;
      Unit = unit == "" ? string.Empty : unit;
      IOAddress = ioAddress == "" ? string.Empty : ioAddress;
      ModuleEntity = moduleEntity ?? throw new ArgumentException(nameof(moduleEntity));
      JBEntity = jbEntity ?? throw new ArgumentException(nameof(jbEntity));
      AdditionalConnectionImageEntities = additionalConnectionImageEntities ?? new List<ImageEntity>();
    }
  }
}
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
    public ModuleEntity? Module { get; set; }
    public JBEntity? JB { get; set; }
    public List<ImageEntity> AdditionalConnectionImages { get; set; } = new();

    [Preserve]
    [JsonConstructor]
    public DeviceEntity(int id, string code)
    {
      Id = id;
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }
    [Preserve]
    [JsonConstructor]
    public DeviceEntity()
    {
    }
    // public DeviceEntity(int id, string code, string function, string range, string unit, string ioAddress, ModuleEntity module, JBEntity jb, List<ImageEntity> additionalConnectionImages)
    // {
    //   Id = id;
    //   Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    //   Function = function;
    //   Range = range;
    //   Unit = unit;
    //   IOAddress = ioAddress;
    //   Module = module;
    //   JB = jb;
    //   AdditionalConnectionImages = additionalConnectionImages;
    // }
  }
}
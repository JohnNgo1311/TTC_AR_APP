
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace Domain.Entities
{
  [Preserve]
  public class MccEntity
  {
    public int Id { get; set; }
    public string CabinetCode { get; set; }

    public string? Brand { get; set; } = string.Empty;
    public List<FieldDeviceEntity> FieldDeviceEntities { get; set; } = new();

    public string? Note { get; set; } = string.Empty;

    [Preserve]
    [JsonConstructor]
    public MccEntity(string cabinetCode)
    {
      CabinetCode = cabinetCode == "" ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
    }

    // public MccEntity(int id, string cabinetCode, string? brand, List<FieldDeviceEntity> fieldDeviceEntities, string? note)
    // {
    //   Id = id;
    //   CabinetCode = cabinetCode == "" ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
    //   Brand = brand;
    //   FieldDeviceEntities = fieldDeviceEntities;
    //   Note = note;
    // }
  }
}
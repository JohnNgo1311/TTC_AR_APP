
using System;
using System.Collections.Generic;
using Domain.Entities;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class FieldDeviceEntity
{

  public int Id { get; set; }
  public string Name { get; set; }
  public string CabinetCode { get; set; }

  public string RatedPower { get; set; } = string.Empty;

  public string RatedCurrent { get; set; } = string.Empty;

  public string ActiveCurrent { get; set; } = string.Empty;

  public List<ImageEntity> ListConnectionImages { get; set; } = new();

  public List<string> Note { get; set; } = new();

  [Preserve]
  [JsonConstructor]
  public FieldDeviceEntity(string name, string cabinetCode)
  {
    Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    CabinetCode = cabinetCode == "" ? throw new ArgumentNullException(nameof(cabinetCode)) : cabinetCode;
  }

  // public FieldDeviceEntity(int id, string name, string cabinetCode, string? ratedPower, string? ratedCurrent, string? activeCurrent, List<ImageEntity> listConnectionImages, List<string> note)
  // {
  //   Id = id;
  //   Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
  //   CabinetCode = cabinetCode;
  //   RatedPower = ratedPower;
  //   RatedCurrent = ratedCurrent;
  //   ActiveCurrent = activeCurrent;
  //   ListConnectionImages = listConnectionImages;
  //   Note = note;
  // }

}
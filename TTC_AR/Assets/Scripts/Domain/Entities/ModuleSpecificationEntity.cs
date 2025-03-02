
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace Domain.Entities
{
  [Preserve]
  public class ModuleSpecificationEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Type { get; set; } = string.Empty;
    public string NumOfIO { get; set; } = string.Empty;
    public string SignalType { get; set; } = string.Empty;
    public string CompatibleTBUs { get; set; } = string.Empty;
    public string OperatingVoltage { get; set; } = string.Empty;
    public string OperatingCurrent { get; set; } = string.Empty;
    public string FlexbusCurrent { get; set; } = string.Empty;
    public string Alarm { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string PdfManual { get; set; } = string.Empty;

    [Preserve]
    [JsonConstructor]
    public ModuleSpecificationEntity(string code)
    {
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }
    // public ModuleSpecificationEntity(int id,
    //  string code, string type, string numOfIO, string signalType, string compatibleTBUs, string operatingVoltage, string operatingCurrent, string flexbusCurrent, string alarm, string note, string pdfManual)
    // {
    //   Id = id;
    //   Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    //   Type = type;
    //   NumOfIO = numOfIO;
    //   SignalType = signalType;
    //   CompatibleTBUs = compatibleTBUs;
    //   OperatingVoltage = operatingVoltage;
    //   OperatingCurrent = operatingCurrent;
    //   FlexbusCurrent = flexbusCurrent;
    //   Alarm = alarm;
    //   Note = note;
    //   PdfManual = pdfManual;
    // }
  }
}
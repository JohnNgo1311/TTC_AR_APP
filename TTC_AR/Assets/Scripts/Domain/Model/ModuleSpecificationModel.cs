
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine.Scripting;


[Preserve]
public class ModuleSpecificationModel
{
#nullable enable
  [JsonProperty("id")]
  public int Id { get; set; }
  // [JsonProperty("adapterSpecification")]
  // public AdapterSpecificationModel? Adapter { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [JsonProperty("type")]
  public string Type { get; set; }
  [JsonProperty("numOfIO")]
  public string? NumOfIO { get; set; }

  [JsonProperty("signalType")]
  public string? SignalType { get; set; }

  [JsonProperty("compatibleTBUs")]
  public string? CompatibleTBUs { get; set; }

  [JsonProperty("operatingVoltage")]
  public string? OperatingVoltage { get; set; }
  [JsonProperty("operatingCurrent")]
  public string? OperatingCurrent { get; set; }

  [JsonProperty("flexbusCurrent")]
  public string? FlexbusCurrent { get; set; }
  [JsonProperty("alarm")]
  public string? Alarm { get; set; }
  [JsonProperty("note")]
  public string? Note { get; set; }
  [JsonProperty("PdfManual")]
  public string? PdfManual { get; set; }

  [Preserve]
  [JsonConstructor]
  public ModuleSpecificationModel(int id,
   // AdapterSpecificationModel? adapter,
   string code, string type, string? numOfIO, string? signalType, string? compatibleTBUs, string? operatingVoltage, string? operatingCurrent, string? flexbusCurrent, string? alarm, string? note, string? pdfManual)
  {
    Id = id;
    // Adapter = adapter;
    Code = code;
    Type = type;
    NumOfIO = numOfIO;
    SignalType = signalType;
    CompatibleTBUs = compatibleTBUs;
    OperatingVoltage = operatingVoltage;
    OperatingCurrent = operatingCurrent;
    FlexbusCurrent = flexbusCurrent;
    Alarm = alarm;
    Note = note;
    PdfManual = pdfManual;
  }
}
[Preserve]
public class ModuleSpecificationBasicModel
{
  [JsonProperty("id")]
  public int Id { get; set; }

  [JsonProperty("code")]
  public string Code { get; set; }
  [Preserve]
  [JsonConstructor]
  public ModuleSpecificationBasicModel(int id, string code)
  {
    Id = id;
    Code = code;
  }

}
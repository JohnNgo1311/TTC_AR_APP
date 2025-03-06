
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace Domain.Entities
{

  [Preserve]
  public class ModuleSpecificationEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;

    [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    [JsonProperty("NumOfIO", NullValueHandling = NullValueHandling.Ignore)]
    public string? NumOfIO { get; set; }

    [JsonProperty("SignalType", NullValueHandling = NullValueHandling.Ignore)]
    public string? SignalType { get; set; }

    [JsonProperty("CompatibleTBUs", NullValueHandling = NullValueHandling.Ignore)]
    public string? CompatibleTBUs { get; set; }

    [JsonProperty("OperatingVoltage", NullValueHandling = NullValueHandling.Ignore)]
    public string? OperatingVoltage { get; set; }


    [JsonProperty("OperatingCurrent", NullValueHandling = NullValueHandling.Ignore)]
    public string? OperatingCurrent { get; set; }

    [JsonProperty("FlexbusCurrent", NullValueHandling = NullValueHandling.Ignore)]
    public string? FlexbusCurrent { get; set; }

    [JsonProperty("Alarm", NullValueHandling = NullValueHandling.Ignore)]
    public string? Alarm { get; set; }

    [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    public string? Note { get; set; }

    [JsonProperty("PdfManual", NullValueHandling = NullValueHandling.Ignore)]
    public string? PdfManual { get; set; }

    [Preserve]
    public ModuleSpecificationEntity()
    {

    }

    [Preserve]
    public ModuleSpecificationEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    //! Dùng để Get hoặc làm field lúc đẩy lên ở các Entity khác
    public ModuleSpecificationEntity(string id, string code)
    {
      Id = id;
      Code = code;
      // Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public ModuleSpecificationEntity(
    string id,
    string code,
    string type,
    string numOfIO,
    string signalType,
    string compatibleTBUs,
    string operatingVoltage,
    string operatingCurrent,
    string flexbusCurrent,
    string alarm,
    string note,
    string pdfManual)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;

      Type = type == "" ? "Chưa cập nhật" : type;
      NumOfIO = numOfIO == "" ? "Chưa cập nhật" : numOfIO;
      SignalType = signalType == "" ? "Chưa cập nhật" : signalType;
      CompatibleTBUs = compatibleTBUs == "" ? "Chưa cập nhật" : compatibleTBUs;
      OperatingVoltage = operatingVoltage == "" ? "Chưa cập nhật" : operatingVoltage;
      OperatingCurrent = operatingCurrent == "" ? "Chưa cập nhật" : operatingCurrent;
      FlexbusCurrent = flexbusCurrent == "" ? "Chưa cập nhật" : flexbusCurrent;
      Alarm = alarm == "" ? "Chưa cập nhật" : alarm;
      Note = note == "" ? "Chưa cập nhật" : note;
      PdfManual = pdfManual == "" ? "Chưa cập nhật" : pdfManual;
    }
    [Preserve]
    public ModuleSpecificationEntity(
   string code,
   string type,
   string numOfIO,
   string signalType,
   string compatibleTBUs,
   string operatingVoltage,
   string operatingCurrent,
   string flexbusCurrent,
   string alarm,
   string note,
   string pdfManual)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = type == "" ? "Chưa cập nhật" : type;
      NumOfIO = numOfIO == "" ? "Chưa cập nhật" : numOfIO;
      SignalType = signalType == "" ? "Chưa cập nhật" : signalType;
      CompatibleTBUs = compatibleTBUs == "" ? "Chưa cập nhật" : compatibleTBUs;
      OperatingVoltage = operatingVoltage == "" ? "Chưa cập nhật" : operatingVoltage;
      OperatingCurrent = operatingCurrent == "" ? "Chưa cập nhật" : operatingCurrent;
      FlexbusCurrent = flexbusCurrent == "" ? "Chưa cập nhật" : flexbusCurrent;
      Alarm = alarm == "" ? "Chưa cập nhật" : alarm;
      Note = note == "" ? "Chưa cập nhật" : note;
      PdfManual = pdfManual == "" ? "Chưa cập nhật" : pdfManual;
    }

  }
}
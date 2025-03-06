using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class AdapterSpecificationEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;
    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;

    [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }
    [JsonProperty("Communication", NullValueHandling = NullValueHandling.Ignore)]
    public string? Communication { get; set; }
    [JsonProperty("NumOfModulesAllowed", NullValueHandling = NullValueHandling.Ignore)]
    public string? NumOfModulesAllowed { get; set; }
    [JsonProperty("CommSpeed", NullValueHandling = NullValueHandling.Ignore)]
    public string? CommSpeed { get; set; }
    [JsonProperty("InputSupply", NullValueHandling = NullValueHandling.Ignore)]
    public string? InputSupply { get; set; }
    [JsonProperty("OutputSupply", NullValueHandling = NullValueHandling.Ignore)]
    public string? OutputSupply { get; set; }
    [JsonProperty("InrushCurrent", NullValueHandling = NullValueHandling.Ignore)]
    public string? InrushCurrent { get; set; }
    [JsonProperty("Alarm", NullValueHandling = NullValueHandling.Ignore)]
    public string? Alarm { get; set; }
    [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    public string? Note { get; set; }

    [JsonProperty("PdfManual", NullValueHandling = NullValueHandling.Ignore)]
    public string? PdfManual { get; set; }

    [Preserve]
    public AdapterSpecificationEntity()
    {

    }

    [Preserve]
    public AdapterSpecificationEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public AdapterSpecificationEntity(string id, string code)
    {
      Id = id;
      Code = code;
      // Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public AdapterSpecificationEntity(string id, string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = string.IsNullOrEmpty(type) ? string.Empty : type;
      Communication = string.IsNullOrEmpty(communication) ? string.Empty : communication;
      NumOfModulesAllowed = string.IsNullOrEmpty(numOfModulesAllowed) ? string.Empty : numOfModulesAllowed;
      CommSpeed = string.IsNullOrEmpty(commSpeed) ? string.Empty : commSpeed;
      InputSupply = string.IsNullOrEmpty(inputSupply) ? string.Empty : inputSupply;
      OutputSupply = string.IsNullOrEmpty(outputSupply) ? string.Empty : outputSupply;
      InrushCurrent = string.IsNullOrEmpty(inrushCurrent) ? string.Empty : inrushCurrent;
      Alarm = string.IsNullOrEmpty(alarm) ? string.Empty : alarm;
      Note = string.IsNullOrEmpty(note) ? string.Empty : note;
      PdfManual = string.IsNullOrEmpty(pdfManual) ? string.Empty : pdfManual;
    }

    [Preserve]
    public AdapterSpecificationEntity(string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = type == string.Empty ? "Chưa cập nhật" : type;
      Communication = communication == string.Empty ? "Chưa cập nhật" : communication;
      NumOfModulesAllowed = numOfModulesAllowed == string.Empty ? "Chưa cập nhật" : numOfModulesAllowed;
      CommSpeed = commSpeed == string.Empty ? "Chưa cập nhật" : commSpeed;
      InputSupply = inputSupply == string.Empty ? "Chưa cập nhật" : inputSupply;
      OutputSupply = outputSupply == string.Empty ? "Chưa cập nhật" : outputSupply;
      InrushCurrent = inrushCurrent == string.Empty ? "Chưa cập nhật" : inrushCurrent;
      Alarm = alarm == string.Empty ? "Chưa cập nhật" : alarm;
      Note = note == string.Empty ? "Chưa cập nhật" : note;
      PdfManual = pdfManual == string.Empty ? "Chưa cập nhật" : pdfManual;
    }
  }
}


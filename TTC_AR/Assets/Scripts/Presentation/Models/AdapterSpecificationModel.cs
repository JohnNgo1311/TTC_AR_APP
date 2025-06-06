using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]
public class AdapterSpecificationModel
{

  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;

  [JsonProperty("Code")]
  public string Code { get; set; } = string.Empty;

  [JsonProperty("Type")]
  public string? Type { get; set; }

  [JsonProperty("Communication")]
  public string? Communication { get; set; }

  [JsonProperty("NumOfModulesAllowed")]
  public string? NumOfModulesAllowed { get; set; }

  [JsonProperty("CommSpeed")]
  public string? CommSpeed { get; set; }

  [JsonProperty("InputSupply")]
  public string? InputSupply { get; set; }
  [JsonProperty("OutputSupply")]
  public string? OutputSupply { get; set; }

  [JsonProperty("InrushCurrent")]
  public string? InrushCurrent { get; set; }
  [JsonProperty("Alarm")]
  public string? Alarm { get; set; }
  [JsonProperty("Note")]
  public string? Note { get; set; }
  [JsonProperty("PdfManual")]
  public string? PdfManual { get; set; }
  [Preserve]

  public AdapterSpecificationModel(string id, string code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
  {
    Id = id;
    Code = code;
    Type = type;
    Communication = communication;
    NumOfModulesAllowed = numOfModulesAllowed;
    CommSpeed = commSpeed;
    InputSupply = inputSupply;
    OutputSupply = outputSupply;
    InrushCurrent = inrushCurrent;
    Alarm = alarm;
    Note = note;
    PdfManual = pdfManual;
  }
  public AdapterSpecificationModel(string id, string code)
  {
    Id = id;
    Code = code;
  }

  public AdapterSpecificationModel()
  {
  }
  public AdapterSpecificationModel(string code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
  {
    Code = code;
    Type = type;
    Communication = communication;
    NumOfModulesAllowed = numOfModulesAllowed;
    CommSpeed = commSpeed;
    InputSupply = inputSupply;
    OutputSupply = outputSupply;
    InrushCurrent = inrushCurrent;
    Alarm = alarm;
    Note = note;
    PdfManual = pdfManual;
  }
}
public class AdapterSpecificationBasicModel
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Code")]
  public string Code { get; set; }
  [Preserve]

  public AdapterSpecificationBasicModel(string id, string code)
  {
    Id = id;
    Code = code;
  }
}


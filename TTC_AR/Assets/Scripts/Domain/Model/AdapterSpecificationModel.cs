using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;


[Preserve]
public class AdapterSpecificationModel
{
#nullable enable
  [JsonProperty("Id")]
  public string Id { get; set; }

  [JsonProperty("Code")]
  public string? Code { get; set; }

  [JsonProperty("Type")]
  public string? Type { get; set; }

  [JsonProperty("Communication")]
  public string? Communication { get; set; }

  [JsonProperty("NumOfModuleAllowed")]
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
  public string? Noted { get; set; }

  [JsonProperty("PDFManual")]
  public string? PdfManual { get; set; }

  [Preserve]
  [JsonConstructor]
  public AdapterSpecificationModel(string id, string? code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
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
    Noted = note;
    PdfManual = pdfManual;
  }

}
public class AdapterSpecificationBasicModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Code")]
  public string Code { get; set; }
  [Preserve]
  [JsonConstructor]
  public AdapterSpecificationBasicModel(int id, string code)
  {
    Id = id;
    Code = code;
  }
}

public class AdapterPostGeneralModel
{

  [JsonProperty("Code")]
  public string? Code { get; set; }

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
  public string? Noted { get; set; }
  [JsonProperty("PDFManual")]
  public string? PdfManual { get; set; }
  [Preserve]
  [JsonConstructor]
  public AdapterPostGeneralModel(string? code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
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
    Noted = note;
    PdfManual = pdfManual;
  }
}


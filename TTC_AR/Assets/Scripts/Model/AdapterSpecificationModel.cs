using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;


[Preserve]
public class AdapterSpecificationModel
{
#nullable enable
  [JsonProperty("id")]
  public int Id { get; set; }

  [JsonProperty("code")]
  public string? Code { get; set; }

  [JsonProperty("type")]
  public string? Type { get; set; }

  [JsonProperty("communication")]
  public string? Communication { get; set; }

  [JsonProperty("numOfModulesAllowed")]
  public string? NumOfModulesAllowed { get; set; }

  [JsonProperty("commSpeed")]
  public string? CommSpeed { get; set; }

  [JsonProperty("inputSupply")]
  public string? InputSupply { get; set; }
  [JsonProperty("outputSupply")]
  public string? OutputSupply { get; set; }

  [JsonProperty("inrushCurrent")]
  public string? InrushCurrent { get; set; }
  [JsonProperty("alarm")]
  public string? Alarm { get; set; }
  [JsonProperty("note")]
  public string? Noted { get; set; }
  [JsonProperty("pdfManual")]
  public string? PdfManual { get; set; }
  [Preserve]
  [JsonConstructor]
  public AdapterSpecificationModel(int id, string? code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
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
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [Preserve]
  [JsonConstructor]
  public AdapterSpecificationBasicModel(int id, string code)
  {
    Id = id;
    Code = code;
  }
}


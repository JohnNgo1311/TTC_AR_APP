using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos
{
  public class AdapterSpecificationBasicDto
  {
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("code")] public string Code { get; set; }

    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationBasicDto(int id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentException(nameof(code)) : code;
    }
  }
  [Preserve]
  public class AdapterSpecificationResponseDto : AdapterSpecificationBasicDto
  {
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("communication")] public string Communication { get; set; }

    [JsonProperty("numOfModulesAllowed")] public string NumOfModulesAllowed { get; set; }

    [JsonProperty("commSpeed")] public string CommSpeed { get; set; }

    [JsonProperty("inputSupply")] public string InputSupply { get; set; }
    [JsonProperty("outputSupply")] public string OutputSupply { get; set; }

    [JsonProperty("inrushCurrent")] public string InrushCurrent { get; set; }
    [JsonProperty("alarm")] public string Alarm { get; set; }
    [JsonProperty("note")] public string Noted { get; set; }
    [JsonProperty("pdfManual")] public string PdfManual { get; set; }

    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationResponseDto(int id, string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual) : base(id, code)
    {
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

    // [Preserve]
    // [JsonConstructor]
    // public AdapterSpecificationResponseDto(int id, string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual) : base(id, code)
    // {
    //   Type = type == " " ? string.Empty : type;
    //   Communication = communication == " " ? string.Empty : communication;
    //   NumOfModulesAllowed = numOfModulesAllowed == " " ? string.Empty : numOfModulesAllowed;
    //   CommSpeed = commSpeed == " " ? string.Empty : commSpeed;
    //   InputSupply = inputSupply == " " ? string.Empty : inputSupply;
    //   OutputSupply = outputSupply == " " ? string.Empty : outputSupply;
    //   InrushCurrent = inrushCurrent == " " ? string.Empty : inrushCurrent;
    //   Alarm = alarm == " " ? string.Empty : alarm;
    //   Noted = note == " " ? string.Empty : note;
    //   PdfManual = pdfManual == " " ? string.Empty : pdfManual;
    // }

  }

  [Preserve]
  public class AdapterSpecificationRequestDto
  {
    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("communication")] public string Communication { get; set; }

    [JsonProperty("numOfModulesAllowed")] public string NumOfModulesAllowed { get; set; }

    [JsonProperty("commSpeed")] public string CommSpeed { get; set; }

    [JsonProperty("inputSupply")] public string InputSupply { get; set; }
    [JsonProperty("outputSupply")] public string OutputSupply { get; set; }

    [JsonProperty("inrushCurrent")] public string InrushCurrent { get; set; }
    [JsonProperty("alarm")] public string Alarm { get; set; }
    [JsonProperty("note")] public string Noted { get; set; }
    [JsonProperty("pdfManual")] public string PdfManual { get; set; }

    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationRequestDto(string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentException(nameof(code)) : code;
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

    // [Preserve]
    // [JsonConstructor]
    // public AdapterSpecificationRequestDto(string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual)
    // {
    //   Code = code ?? throw new ArgumentException(nameof(code));
    //   Type = type == " " ? string.Empty : type;
    //   Communication = communication == " " ? string.Empty : communication;
    //   NumOfModulesAllowed = numOfModulesAllowed == " " ? string.Empty : numOfModulesAllowed;
    //   CommSpeed = commSpeed == " " ? string.Empty : commSpeed;
    //   InputSupply = inputSupply == " " ? string.Empty : inputSupply;
    //   OutputSupply = outputSupply == " " ? string.Empty : outputSupply;
    //   InrushCurrent = inrushCurrent == " " ? string.Empty : inrushCurrent;
    //   Alarm = alarm == " " ? string.Empty : alarm;
    //   Noted = note == " " ? string.Empty : note;
    //   PdfManual = pdfManual == " " ? string.Empty : pdfManual;
    // }
  }
}


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  [Preserve]
  public class AdapterSpecificationEntity
  {
    public int Id { get; set; }

    public string Code { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Communication { get; set; } = string.Empty;

    public string NumOfModulesAllowed { get; set; } = string.Empty;

    public string CommSpeed { get; set; } = string.Empty;

    public string InputSupply { get; set; } = string.Empty;
    public string OutputSupply { get; set; } = string.Empty;

    public string InrushCurrent { get; set; } = string.Empty;
    public string Alarm { get; set; } = string.Empty;
    public string Noted { get; set; } = string.Empty;
    public string PdfManual { get; set; } = string.Empty;


    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationEntity(string code)
    {
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationEntity(int id, string code)
    {
      Id = id;
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    [JsonConstructor]
    public AdapterSpecificationEntity(int id, string code, string type, string communication, string numOfModulesAllowed, string commSpeed, string inputSupply, string outputSupply, string inrushCurrent, string alarm, string note, string pdfManual)
    {
      Id = id;
      Code = code == "" ? throw new ArgumentNullException(nameof(code)) : code;
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
}


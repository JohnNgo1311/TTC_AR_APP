using System;
using System.Collections.Generic;
using Newtonsoft.Json;


[Serializable]
public class Adapter_Specification_Model
{
#nullable enable
  [JsonProperty("Code")]
  public string? Code { get; set; }
  [JsonProperty("Type")]
  public string? Type { get; set; }

  [JsonProperty("Communication")]
  public string? Communication { get; set; }

  [JsonProperty("Num_Of_Module_Allowed")]
  public string? Num_Of_Module_Allowed { get; set; }

  [JsonProperty("Comm_Speed")]
  public string? Comm_Speed { get; set; }

  [JsonProperty("Input_Supply")]
  public string? Input_SupplyInput_Supply { get; set; }
  [JsonProperty("Output_Supply")]
  public string? Output_Supply { get; set; }

  [JsonProperty("Inrush_Current")]
  public string? Inrush_Current { get; set; }
  [JsonProperty("Alarm")]
  public string? Alarm { get; set; }
  [JsonProperty("Noted")]
  public string? Noted { get; set; }
  [JsonProperty("PDF_Turtorial")]
  public string? PDF_Turtorial { get; set; }

  public Adapter_Specification_Model(string? code, string? type, string? communication, string? num_Of_Module_Allowed, string? comm_Speed, string? input_SupplyInput_Supply, string? output_Supply, string? inrush_Current, string? alarm, string? noted, string? pdf_Turtorial)
  {
    Code = code;
    Type = type;
    Communication = communication;
    Num_Of_Module_Allowed = num_Of_Module_Allowed;
    Comm_Speed = comm_Speed;
    Input_SupplyInput_Supply = input_SupplyInput_Supply;
    Output_Supply = output_Supply;
    Inrush_Current = inrush_Current;
    Alarm = alarm;
    Noted = noted;
    PDF_Turtorial = pdf_Turtorial;
  }
}



using System;
using System.Collections.Generic;
using Newtonsoft.Json;


[Serializable]
public class Module_Specification_Model
{
#nullable enable
  [JsonProperty("Code")]
  public string? Code { get; set; }
  [JsonProperty("Type")]
  public string? Type { get; set; }

  [JsonProperty("Num_Of_IO")]
  public string? Num_Of_IO { get; set; }

  [JsonProperty("Signal_Type")]
  public string? Signal_Type { get; set; }

  [JsonProperty("Compatible_TBUs")]
  public string? Compatible_TBUs { get; set; }

  [JsonProperty("Operating_Voltage")]
  public string? Operating_Voltage { get; set; }
  [JsonProperty("Operating_Current")]
  public string? Operating_Current { get; set; }

  [JsonProperty("Flexbus_Current")]
  public string? Flexbus_Current { get; set; }
  [JsonProperty("Alarm")]
  public string? Alarm { get; set; }
  [JsonProperty("Noted")]
  public string? Noted { get; set; }
  [JsonProperty("PDF_Turtorial")]
  public string? PDF_Turtorial { get; set; }
  [JsonProperty("Adapter")]
  public Adapter_Specification_Model? Adapter { get; set; }

  public Module_Specification_Model(string? code, string? type, string? num_Of_I_O, string? signal_Type, string? compatible_TBUs, string? operating_Voltage, string? operating_Current, string? flexbus_Current, string? alarm, string? noted, string? pdf_Turtorial, Adapter_Specification_Model? adapter)
  {
    Code = code;
    Type = type;
    Num_Of_IO = num_Of_I_O;
    Signal_Type = signal_Type;
    Compatible_TBUs = compatible_TBUs;
    Operating_Voltage = operating_Voltage;
    Operating_Current = operating_Current;
    Flexbus_Current = flexbus_Current;
    Alarm = alarm;
    Noted = noted;
    PDF_Turtorial = pdf_Turtorial;
    Adapter = adapter;
  }
}


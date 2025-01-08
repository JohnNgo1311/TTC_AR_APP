using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class Device_Information_Model
{
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Code")]
  public string Code { get; set; }
  [JsonProperty("Function")]
  public string Function { get; set; }
  [JsonProperty("Range")]
  public string Range { get; set; }
  [JsonProperty("Unit")]
  public string Unit { get; set; }
  [JsonProperty("IOAddress")]
  public string IOAddress { get; set; }
  [JsonProperty("Module")]
  public Module_General_Model Module_General_Model { get; set; }

  [JsonProperty("JB")]
  public JB_Information_Model JB_Information_Model { get; set; }

  [JsonProperty("AdditionalConnectionImages")]
  public List<string> Additional_Connection_Images { get; set; }
  [Preserve]
  [JsonConstructor]
  public Device_Information_Model(string id, string code, string function, string range, string unit, string ioAddress, Module_General_Model moduleGeneralModel, JB_Information_Model jbInformationModel, List<string> additionalConnectionImages)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id), "Id cannot be null");
    Code = code ?? throw new ArgumentNullException(nameof(code), " code cannot be null");
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    Module_General_Model = moduleGeneralModel ?? throw new ArgumentNullException(nameof(moduleGeneralModel), "moduleGeneralModel cannot be null"); ;
    JB_Information_Model = jbInformationModel;
    Additional_Connection_Images = additionalConnectionImages;
  }
  public Device_Information_Model()
  {
    Id = string.Empty;
    Code = string.Empty;
    Function = string.Empty;
    Range = string.Empty;
    Unit = string.Empty;
    IOAddress = string.Empty;
    Module_General_Model = new Module_General_Model(
      string.Empty,
      string.Empty,
      new Rack_Non_List_Module_Model(
      string.Empty,
      string.Empty
      ));
    JB_Information_Model = new JB_Information_Model(
      string.Empty,
      string.Empty,
      string.Empty,
      string.Empty,
      new List<string>());
  }
}


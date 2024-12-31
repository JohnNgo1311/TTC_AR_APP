using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
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

  [JsonProperty("Additional_Connection_Images")]
  public List<string> Additional_Connection_Images { get; set; }
  public Device_Information_Model(string id, string code, string function, string range, string unit, string ioAddress, Module_General_Model moduleGeneralModel, JB_Information_Model jbInformationModel, List<string> additionalConnectionImages)
  {
    Id = id;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    Module_General_Model = moduleGeneralModel;
    JB_Information_Model = jbInformationModel;
    Additional_Connection_Images = additionalConnectionImages;
  }
}


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class DeviceInformationModel
{
  [JsonProperty("id")]
  public int Id { get; set; }

  [JsonProperty("module")]
  public ModuleGeneralNonRackModel ModuleGeneralNonRackModel { get; set; }

  [JsonProperty("jb")]
  public JBInformationModel JBInformationModel { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [JsonProperty("function")]
  public string Function { get; set; }
  [JsonProperty("range")]
  public string Range { get; set; }
  [JsonProperty("unit")]
  public string Unit { get; set; }
  [JsonProperty("ioAddress")]
  public string IOAddress { get; set; }

  [JsonProperty("additionalConnectionImages")]
  public List<string> AdditionalConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public DeviceInformationModel(int id, ModuleGeneralNonRackModel module_General_Non_Rack_Model, JBInformationModel jbInformationModel, string code, string function, string range, string unit, string io_Address, List<string> additional_Connection_Images)
  {
    Id = id;
    ModuleGeneralNonRackModel = module_General_Non_Rack_Model;
    JBInformationModel = jbInformationModel;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = io_Address;
    AdditionalConnectionImages = additional_Connection_Images;
  }

  public DeviceInformationModel()
  {
    // Id = string.Empty;
    // Code = string.Empty;
    // Function = string.Empty;
    // Range = string.Empty;
    // Unit = string.Empty;
    // IOAddress = string.Empty;
    // ModuleGeneralNonRackModel = new ModuleGeneralNonRackModel(
    //   string.Empty,
    //   string.Empty,
    //   new Rack_Non_List_Module_Model(
    //   string.Empty,
    //   string.Empty
    //   ));
    // JBInformationModel = new JBInformationModel(
    //   string.Empty,
    //   string.Empty,
    //   string.Empty,
    //   string.Empty,
    //   new List<string>());
  }

}
[Preserve]
public class DeviceInformationModel_FromModule
{
  [JsonProperty("id")]
  public int Id { get; set; }

  [JsonProperty("jb")]
  public JBGeneralModel JBGeneralModel { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [JsonProperty("function")]
  public string Function { get; set; }
  [JsonProperty("range")]
  public string Range { get; set; }
  [JsonProperty("unit")]
  public string Unit { get; set; }
  [JsonProperty("ioAddress")]
  public string IOAddress { get; set; }

  [JsonProperty("additionalConnectionImages")]
  public List<string> AdditionalConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public DeviceInformationModel_FromModule(int id, JBGeneralModel jbGeneralModel, string code, string function, string range, string unit, string io_Address, List<string> additional_Connection_Images)
  {
    Id = id;
    JBGeneralModel = jbGeneralModel;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = io_Address;
    AdditionalConnectionImages = additional_Connection_Images;
  }


}
[Preserve]
public class DeviceGeneralModel
{
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [Preserve]
  [JsonConstructor]
  public DeviceGeneralModel(int id, string code)
  {
    Id = id;
    Code = code;
  }

}
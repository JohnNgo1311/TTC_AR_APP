using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class DeviceInformationModel
{
  [JsonProperty("id")]
  public int Id { get; set; }

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

  [JsonProperty("module")]
  public ModuleBasicModel ModuleBasicModel { get; set; }

  [JsonProperty("jb")]
  public JBInformationModel JBInformationModel { get; set; }

  [JsonProperty("additionalConnectionImages")]
  public List<ImageInformationModel> AdditionalConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public DeviceInformationModel(int id, string code, string function, string range, string unit, string ioAddress, ModuleBasicModel moduleBasicModel, JBInformationModel jbInformationModel, List<ImageInformationModel> additionalConnectionImages)
  {
    Id = id;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    ModuleBasicModel = moduleBasicModel;
    JBInformationModel = jbInformationModel;
    AdditionalConnectionImages = additionalConnectionImages;
  }
}


[Preserve]
public class DeviceGeneralModel
{
  [JsonProperty("id")]
  public int Id { get; set; }

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

  [JsonProperty("module")]
  public ModuleBasicModel ModuleBasicModel { get; set; }

  [JsonProperty("jb")]
  public JBBasicModel JBBasicModel { get; set; }

  [JsonProperty("additionalConnectionImages")]
  public List<ImageBasicModel> AdditionalImageModels { get; set; }

  [Preserve]
  [JsonConstructor]
  public DeviceGeneralModel(int id, string code, string function, string range, string unit, string ioAddress, ModuleBasicModel moduleBasicModel, JBBasicModel jbBasicModel, List<ImageBasicModel> additionalImageModels)
  {
    Id = id;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    ModuleBasicModel = moduleBasicModel;
    JBBasicModel = jbBasicModel;
    AdditionalImageModels = additionalImageModels;
  }
}


[Preserve]
public class DevicePostGeneralModel
{
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

  [JsonProperty("module")]
  public ModuleBasicModel ModuleBasicModel { get; set; }

  [JsonProperty("jb")]
  public JBBasicModel JBBasicModel { get; set; }

  [JsonProperty("additionalConnectionImages")]
  public List<ImageBasicModel> AdditionalConnectionBasicModel { get; set; }

  [Preserve]
  [JsonConstructor]
  public DevicePostGeneralModel(string code, string function, string range, string unit, string ioAddress, ModuleBasicModel moduleBasicModel, JBBasicModel jbBasicModel, List<ImageBasicModel> additionalConnectionBasicModel)
  {
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    ModuleBasicModel = moduleBasicModel;
    JBBasicModel = jbBasicModel;
    AdditionalConnectionBasicModel = additionalConnectionBasicModel;
  }


}


[Preserve]
public class DeviceBasicModel
{
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("code")]
  public string Code { get; set; }
  [Preserve]
  [JsonConstructor]
  public DeviceBasicModel(int id, string code)
  {
    Id = id;
    Code = code;
  }

}
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using static ModuleInformationModel;
#nullable enable
[Preserve]
public class DeviceInformationModel
{
  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;

  [JsonProperty("Code")]
  public string Code { get; set; } = string.Empty;

  [JsonProperty("Function")]
  public string? Function { get; set; }

  [JsonProperty("Range")]
  public string? Range { get; set; }

  [JsonProperty("Unit")]
  public string? Unit { get; set; }

  [JsonProperty("IoAddress")]
  public string? IOAddress { get; set; }

  [JsonProperty("Module")]
  public ModuleInformationModel? ModuleInformationModel { get; set; }

  [JsonProperty("ListJBs")]
  public List<JBInformationModel>? JBInformationModels { get; set; }

  [JsonProperty("AdditionalConnectionImages")]
  public List<ImageInformationModel>? AdditionalConnectionImages { get; set; }

  [Preserve]

  public DeviceInformationModel(string id, string code, string? function, string? range, string? unit, string? ioAddress, ModuleInformationModel? moduleInformationModel, List<JBInformationModel>? jbInformationModels, List<ImageInformationModel>? additionalConnectionImages)
  {
    Id = id;
    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    ModuleInformationModel = moduleInformationModel;
    JBInformationModels = jbInformationModels;
    JBInformationModels = jbInformationModels;
    AdditionalConnectionImages = additionalConnectionImages;
  }


  public DeviceInformationModel(string code, string? function, string? range, string? unit, string? ioAddress, ModuleInformationModel? moduleInformationModel, List<JBInformationModel>? jbInformationModels, List<ImageInformationModel>? additionalConnectionImages)
  {

    Code = code;
    Function = function;
    Range = range;
    Unit = unit;
    IOAddress = ioAddress;
    ModuleInformationModel = moduleInformationModel;
    JBInformationModels = jbInformationModels;
    JBInformationModels = jbInformationModels;
    AdditionalConnectionImages = additionalConnectionImages;
  }

  public DeviceInformationModel(string id, string code)
  {
    Id = id;
    Code = code;
  }

  public DeviceInformationModel()
  {
  }
}


[Preserve]
public class DeviceGeneralModel
{
  [JsonProperty("Id")]
  public string? Id { get; set; }

  [JsonProperty("Code")]
  public string? Code { get; set; }

  [JsonProperty("Function")]
  public string? Function { get; set; }

  [JsonProperty("Range")]
  public string? Range { get; set; }

  [JsonProperty("Unit")]
  public string? Unit { get; set; }

  [JsonProperty("IoAddress")]
  public string? IOAddress { get; set; }

  [JsonProperty("Module")]
  public ModuleBasicModel? ModuleBasicModel { get; set; }

  [JsonProperty("JB")]
  public JBBasicModel JBBasicModel { get; set; }

  [JsonProperty("AdditionalConnectionImages")]
  public List<ImageBasicModel> AdditionalImageModels { get; set; }

  [Preserve]

  public DeviceGeneralModel(string? id, string? code, string? function, string? range, string? unit, string? ioAddress, ModuleBasicModel? moduleBasicModel, JBBasicModel jbBasicModel, List<ImageBasicModel> additionalImageModels)
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
  [JsonProperty("Code")]
  public string? Code { get; set; }
  [JsonProperty("Function")]
  public string? Function { get; set; }

  [JsonProperty("Range")]
  public string? Range { get; set; }

  [JsonProperty("Unit")]
  public string? Unit { get; set; }

  [JsonProperty("IoAddress")]
  public string? IOAddress { get; set; }

  [JsonProperty("Module")]
  public ModuleBasicModel? ModuleBasicModel { get; set; }

  [JsonProperty("JB")]
  public JBBasicModel JBBasicModel { get; set; }

  [JsonProperty("AdditionalConnectionImages")]
  public List<ImageBasicModel> AdditionalConnectionBasicModel { get; set; }

  [Preserve]

  public DevicePostGeneralModel(string? code, string? function, string? range, string? unit, string? ioAddress, ModuleBasicModel? moduleBasicModel, JBBasicModel jbBasicModel, List<ImageBasicModel> additionalConnectionBasicModel)
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
  [JsonProperty("Id")]
  public string? Id { get; set; }
  [JsonProperty("Code")]
  public string? Code { get; set; }
  [Preserve]

  public DeviceBasicModel(string? id, string? code)
  {
    Id = id;
    Code = code;
  }

}
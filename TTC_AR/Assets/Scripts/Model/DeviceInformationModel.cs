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
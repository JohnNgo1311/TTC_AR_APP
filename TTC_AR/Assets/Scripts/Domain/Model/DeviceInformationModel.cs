// using System;
// using System.Collections.Generic;
// using Newtonsoft.Json;
// using UnityEngine.Scripting;

// [Preserve]
// public class DeviceInformationModel
// {
// #nullable enable
//   [JsonProperty("Id")]
//   public string Id { get; set; }

//   [JsonProperty("Code")]
//   public string Code { get; set; }

//   [JsonProperty("Function")]
//   public string? Function { get; set; }

//   [JsonProperty("Range")]
//   public string? Range { get; set; }

//   [JsonProperty("Unit")]
//   public string? Unit { get; set; }

//   [JsonProperty("IOAddress")]
//   public string? IOAddress { get; set; }

//   [JsonProperty("Module")]
//   public ModuleInformationModel? ModuleInformationModel { get; set; }

//   [JsonProperty("JB")]
//   public JBInformationModel? JBInformationModel { get; set; }

//   [JsonProperty("AdditionalConnectionImages")]
//   public List<ImageInformationModel>? AdditionalConnectionImages { get; set; }

//   [Preserve]
//   [JsonConstructor]
//   public DeviceInformationModel(string id, string code, string function, string range, string unit, string ioAddress, ModuleInformationModel moduleInformationModel, JBInformationModel jbInformationModel, List<ImageInformationModel> additionalConnectionImages)
//   {
//     Id = id;
//     Code = code;
//     Function = function;
//     Range = range;
//     Unit = unit;
//     IOAddress = ioAddress;
//     ModuleInformationModel = moduleInformationModel;
//     JBInformationModel = jbInformationModel;
//     AdditionalConnectionImages = additionalConnectionImages;
//   }

//   public static implicit operator List<object>(DeviceInformationModel v)
//   {
//     throw new NotImplementedException();
//   }
// }

// [Preserve]
// public class DevicePostGeneralModel
// {

//   [JsonProperty("function")]
//   public string Function { get; set; }

//   [JsonProperty("range")]
//   public string Range { get; set; }

//   [JsonProperty("unit")]
//   public string Unit { get; set; }

//   [JsonProperty("ioAddress")]
//   public string IOAddress { get; set; }

//   [JsonProperty("module")]
//   public ModuleBasicModel ModuleBasicModel { get; set; }

//   [JsonProperty("jb")]
//   public JBBasicModel JBBasicModel { get; set; }

//   [JsonProperty("additionalConnectionImages")]
//   public List<ImageBasicModel> AdditionalConnectionBasicModel { get; set; }

//   [Preserve]
//   [JsonConstructor]
//   public DevicePostGeneralModel(string function, string range, string unit, string ioAddress, ModuleBasicModel moduleBasicModel, JBBasicModel jbBasicModel, List<ImageBasicModel> additionalConnectionBasicModel)
//   {
//     Function = function;
//     Range = range;
//     Unit = unit;
//     IOAddress = ioAddress;
//     ModuleBasicModel = moduleBasicModel;
//     JBBasicModel = jbBasicModel;
//     AdditionalConnectionBasicModel = additionalConnectionBasicModel;
//   }

// }


// [Preserve]
// public class DeviceBasicModel
// {
//   [JsonProperty("Id")]
//   public int Id { get; set; }
//   [JsonProperty("Code")]
//   public string Code { get; set; }
//   [Preserve]
//   [JsonConstructor]
//   public DeviceBasicModel(int id, string code)
//   {
//     Id = id;
//     Code = code;
//   }

// }
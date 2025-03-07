
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace Domain.Entities
{

  [Preserve]
  public class ModuleSpecificationEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;

    // [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Type")]
    public string? Type { get; set; }

    // [JsonProperty("NumOfIO", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("NumOfIO")]
    public string? NumOfIO { get; set; }

    // [JsonProperty("SignalType", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("SignalType")]
    public string? SignalType { get; set; }

    // [JsonProperty("CompatibleTBUs", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("CompatibleTBUs")]
    public string? CompatibleTBUs { get; set; }

    // [JsonProperty("OperatingVoltage", NullValueHandling = NullValueHandling.Ignore)]\
    [JsonProperty("OperatingVoltage")]
    public string? OperatingVoltage { get; set; }

    // [JsonProperty("OperatingCurrent", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("OperatingCurrent")]
    public string? OperatingCurrent { get; set; }

    // [JsonProperty("FlexbusCurrent", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("FlexbusCurrent")]
    public string? FlexbusCurrent { get; set; }

    // [JsonProperty("Alarm", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Alarm")]
    public string? Alarm { get; set; }

    // [JsonProperty("Note", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Note")]
    public string? Note { get; set; }

    // [JsonProperty("PdfManual", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("PdfManual")]
    public string? PdfManual { get; set; }


    public bool ShouldSerializeId()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription(),
      };
      return !allowedRequests.Contains(apiRequestType);
    }

    public bool ShouldSerializeType()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeNumOfIO()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeSignalType()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeCompatibleTBUs()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeOperatingVoltage()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeOperatingCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeFlexbusCurrent()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeAlarm()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializeNote()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }
    public bool ShouldSerializePdfManual()
    {
      string apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTModuleSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTModuleSpecification.GetDescription()
      };
      return allowedRequests.Contains(apiRequestType);
    }

    [Preserve]
    public ModuleSpecificationEntity()
    {

    }

    [Preserve]
    public ModuleSpecificationEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    //! Dùng để Get hoặc làm field lúc đẩy lên ở các Entity khác
    public ModuleSpecificationEntity(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public ModuleSpecificationEntity(
    string id,
    string? code,
    string? type,
    string? numOfIO,
    string? signalType,
    string? compatibleTBUs,
    string? operatingVoltage,
    string? operatingCurrent,
    string? flexbusCurrent,
    string? alarm,
    string? note,
    string? pdfManual)
    {
      Id = id;

      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = string.IsNullOrEmpty(type) ? "Chưa cập nhật" : type;
      NumOfIO = string.IsNullOrEmpty(numOfIO) ? "Chưa cập nhật" : numOfIO;
      SignalType = string.IsNullOrEmpty(signalType) ? "Chưa cập nhật" : signalType;
      CompatibleTBUs = string.IsNullOrEmpty(compatibleTBUs) ? "Chưa cập nhật" : compatibleTBUs;
      OperatingVoltage = string.IsNullOrEmpty(operatingVoltage) ? "Chưa cập nhật" : operatingVoltage;
      OperatingCurrent = string.IsNullOrEmpty(operatingCurrent) ? "Chưa cập nhật" : operatingCurrent;
      FlexbusCurrent = string.IsNullOrEmpty(flexbusCurrent) ? "Chưa cập nhật" : flexbusCurrent;
      Alarm = string.IsNullOrEmpty(alarm) ? "Chưa cập nhật" : alarm;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
      PdfManual = string.IsNullOrEmpty(pdfManual) ? "Chưa cập nhật" : pdfManual;
    }
    [Preserve]
    public ModuleSpecificationEntity(
   string? code,
   string? type,
   string? numOfIO,
   string? signalType,
   string? compatibleTBUs,
   string? operatingVoltage,
   string? operatingCurrent,
   string? flexbusCurrent,
   string? alarm,
   string? note,
   string? pdfManual)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;

      Type = string.IsNullOrEmpty(type) ? "Chưa cập nhật" : type;
      NumOfIO = string.IsNullOrEmpty(numOfIO) ? "Chưa cập nhật" : numOfIO;
      SignalType = string.IsNullOrEmpty(signalType) ? "Chưa cập nhật" : signalType;
      CompatibleTBUs = string.IsNullOrEmpty(compatibleTBUs) ? "Chưa cập nhật" : compatibleTBUs;
      OperatingVoltage = string.IsNullOrEmpty(operatingVoltage) ? "Chưa cập nhật" : operatingVoltage;
      OperatingCurrent = string.IsNullOrEmpty(operatingCurrent) ? "Chưa cập nhật" : operatingCurrent;
      FlexbusCurrent = string.IsNullOrEmpty(flexbusCurrent) ? "Chưa cập nhật" : flexbusCurrent;
      Alarm = string.IsNullOrEmpty(alarm) ? "Chưa cập nhật" : alarm;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
      PdfManual = string.IsNullOrEmpty(pdfManual) ? "Chưa cập nhật" : pdfManual;
    }

  }
}
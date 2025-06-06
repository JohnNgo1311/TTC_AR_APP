using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
namespace Domain.Entities
{
  [Preserve]
  public class AdapterSpecificationEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;
    [JsonProperty("Code")]
    public string Code { get; set; } = string.Empty;

    // [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Type")]
    public string? Type { get; set; }
    // [JsonProperty("Communication", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("Communication")]
    public string? Communication { get; set; }
    // [JsonProperty("NumOfModulesAllowed", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("NumOfModulesAllowed")]
    public string? NumOfModulesAllowed { get; set; }
    // [JsonProperty("CommSpeed", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("CommSpeed")]
    public string? CommSpeed { get; set; }
    // [JsonProperty("InputSupply", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("InputSupply")]
    public string? InputSupply { get; set; }
    // [JsonProperty("OutputSupply", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("OutputSupply")]
    public string? OutputSupply { get; set; }
    // [JsonProperty("InrushCurrent", NullValueHandling = NullValueHandling.Ignore)]
    [JsonProperty("InrushCurrent")]
    public string? InrushCurrent { get; set; }
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
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription(),
      };
      return !apiRequestType.Any(request => allowedRequests.Contains(request));
    }

    public bool ShouldSerializeCode()
    {
      return true;
    }
    public bool ShouldSerializeType()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeCommunication()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeNumOfModulesAllowed()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeCommSpeed()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeInputSupply()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeOutputSupply()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeInrushCurrent()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeAlarm()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializeNote()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }
    public bool ShouldSerializePdfManual()
    {
      List<string> apiRequestType = GlobalVariable.APIRequestType;
      HashSet<string> allowedRequests = new HashSet<string>
      {
        HttpMethodTypeEnum.GETAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.GETModule.GetDescription(),
        HttpMethodTypeEnum.POSTAdapterSpecification.GetDescription(),
        HttpMethodTypeEnum.PUTAdapterSpecification.GetDescription()
      };
      return apiRequestType.Any(request => allowedRequests.Contains(request));
    }


    [Preserve]
    public AdapterSpecificationEntity()
    {

    }

    [Preserve]
    public AdapterSpecificationEntity(string code)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public AdapterSpecificationEntity(string id, string code)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      // Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
    }

    [Preserve]
    public AdapterSpecificationEntity(string id, string code, string? type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
    {
      Id = id;
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = string.IsNullOrEmpty(type) ? "Chưa cập nhật" : type;
      Communication = string.IsNullOrEmpty(communication) ? "Chưa cập nhật" : communication;
      NumOfModulesAllowed = string.IsNullOrEmpty(numOfModulesAllowed) ? "Chưa cập nhật" : numOfModulesAllowed;
      CommSpeed = string.IsNullOrEmpty(commSpeed) ? "Chưa cập nhật" : commSpeed;
      InputSupply = string.IsNullOrEmpty(inputSupply) ? "Chưa cập nhật" : inputSupply;
      OutputSupply = string.IsNullOrEmpty(outputSupply) ? "Chưa cập nhật" : outputSupply;
      InrushCurrent = string.IsNullOrEmpty(inrushCurrent) ? "Chưa cập nhật" : inrushCurrent;
      Alarm = string.IsNullOrEmpty(alarm) ? "Chưa cập nhật" : alarm;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
      PdfManual = string.IsNullOrEmpty(pdfManual) ? "Chưa cập nhật" : pdfManual;
    }

    [Preserve]
    public AdapterSpecificationEntity(string code, string type, string? communication, string? numOfModulesAllowed, string? commSpeed, string? inputSupply, string? outputSupply, string? inrushCurrent, string? alarm, string? note, string? pdfManual)
    {
      Code = string.IsNullOrEmpty(code) ? throw new ArgumentNullException(nameof(code)) : code;
      Type = string.IsNullOrEmpty(type) ? "Chưa cập nhật" : type;
      Communication = string.IsNullOrEmpty(communication) ? "Chưa cập nhật" : communication;
      NumOfModulesAllowed = string.IsNullOrEmpty(numOfModulesAllowed) ? "Chưa cập nhật" : numOfModulesAllowed;
      CommSpeed = string.IsNullOrEmpty(commSpeed) ? "Chưa cập nhật" : commSpeed;
      InputSupply = string.IsNullOrEmpty(inputSupply) ? "Chưa cập nhật" : inputSupply;
      OutputSupply = string.IsNullOrEmpty(outputSupply) ? "Chưa cập nhật" : outputSupply;
      InrushCurrent = string.IsNullOrEmpty(inrushCurrent) ? "Chưa cập nhật" : inrushCurrent;
      Alarm = string.IsNullOrEmpty(alarm) ? "Chưa cập nhật" : alarm;
      Note = string.IsNullOrEmpty(note) ? "Chưa cập nhật" : note;
      PdfManual = string.IsNullOrEmpty(pdfManual) ? "Chưa cập nhật" : pdfManual;
    }
  }
}


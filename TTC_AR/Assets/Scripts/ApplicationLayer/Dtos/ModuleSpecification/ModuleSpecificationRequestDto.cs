using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.ModuleSpecification
{
    [Preserve]
    public class ModuleSpecificationRequestDto
    {
        [JsonProperty("Code")] public string Code { get; set; }
        [JsonProperty("Type")] public string Type { get; set; }
        [JsonProperty("NumOfIO")] public string NumOfIO { get; set; }

        [JsonProperty("SignalType")] public string SignalType { get; set; }

        [JsonProperty("CompatibleTBUs")] public string CompatibleTBUs { get; set; }

        [JsonProperty("OperatingVoltage")] public string OperatingVoltage { get; set; }
        [JsonProperty("OperatingCurrent")] public string OperatingCurrent { get; set; }
        [JsonProperty("FlexbusCurrent")] public string FlexbusCurrent { get; set; }
        [JsonProperty("Alarm")] public string Alarm { get; set; }
        [JsonProperty("Note")] public string Note { get; set; }
        [JsonProperty("PdfManual")] public string PdfManual { get; set; }

        [Preserve]

        public ModuleSpecificationRequestDto(string code, string type, string numOfIO, string signalType, string compatibleTBUs, string operatingVoltage, string operatingCurrent, string flexbusCurrent, string alarm, string note, string pdfManual)
        {
            Code = string.IsNullOrEmpty(code) ? throw new System.ArgumentException(nameof(code)) : code;
            Type = type;
            NumOfIO = numOfIO;
            SignalType = signalType;
            CompatibleTBUs = compatibleTBUs;
            OperatingVoltage = operatingVoltage;
            OperatingCurrent = operatingCurrent;
            FlexbusCurrent = flexbusCurrent;
            Alarm = alarm;
            Note = note;
            PdfManual = pdfManual;
        }
    }
}
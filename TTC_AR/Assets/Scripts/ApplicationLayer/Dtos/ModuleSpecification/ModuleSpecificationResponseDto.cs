using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

namespace ApplicationLayer.Dtos.ModuleSpecification
{
    [Preserve]
    public class ModuleSpecificationResponseDto : ModuleSpecificationBasicDto
    {
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

        public ModuleSpecificationResponseDto(string id, string code, string type, string numOfIO, string signalType, string compatibleTBUs, string operatingVoltage, string operatingCurrent, string flexbusCurrent, string alarm, string note, string pdfManual) : base(id, code)
        {
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

        // [Preserve]
        // 
        // public ModuleSpecificationResponseDto(string id, string code, string type, string numOfIO, string signalType, string compatibleTBUs, string operatingVoltage, string operatingCurrent, string flexbusCurrent, string alarm, string note, string pdfManual) : base(id, name)
        // {
        //   Id = id
        //       Code = code == "" ? throw new System.ArgumentException(nameof(code)) : code;
        //   Type = type == "" ? string.Empty : type;
        //   NumOfIO = numOfIO == "" ? string.Empty : numOfIO;
        //   SignalType = signalType == "" ? string.Empty : signalType;
        //   CompatibleTBUs = compatibleTBUs == "" ? string.Empty : compatibleTBUs;
        //   OperatingVoltage = operatingVoltage == "" ? string.Empty : operatingVoltage;
        //   OperatingCurrent = operatingCurrent == "" ? string.Empty : operatingCurrent;
        //   FlexbusCurrent = flexbusCurrent == "" ? string.Empty : flexbusCurrent;
        //   Alarm = alarm == "" ? string.Empty : alarm;
        //   Note = note == "" ? string.Empty : note;
        //   PdfManual = pdfManual == "" ? string.Empty : pdfManual;
        // }

    }
}
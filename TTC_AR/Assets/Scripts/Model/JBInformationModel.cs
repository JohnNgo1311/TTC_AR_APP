using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using UnityEngine.Scripting;

[Preserve]
public class JBInformationModel
{
#nullable enable
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("location")]
  public string Location { get; set; }
  [JsonProperty("listDevices")]

  public List<DeviceGeneralModel> ListDeviceInformation { get; set; }
  [JsonProperty("listModules")]

  public List<ModuleGeneralNonRackModel> ListModuleInformation { get; set; }

  [JsonProperty("outdoorImages")]
  public string OutdoorImage { get; set; }

  [JsonProperty("connectionImages")]
  public List<string> ListConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBInformationModel(int id, string name, string location, List<DeviceGeneralModel> listDeviceInformation, List<ModuleGeneralNonRackModel> listModuleInformation, string outdoorImage, List<string> listConnectionImages)
  {
    Id = id;
    Name = name;
    Location = location;
    ListDeviceInformation = listDeviceInformation;
    ListModuleInformation = listModuleInformation;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }


}
public class JBGeneralModel
{
  [JsonProperty("id")]
  public int Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBGeneralModel(int id, string name)
  {
    Id = id;
    Name = name;

  }
}

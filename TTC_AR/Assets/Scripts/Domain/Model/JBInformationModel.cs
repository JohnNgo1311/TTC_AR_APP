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
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [JsonProperty("Location")]
  public string? Location { get; set; }

  [JsonProperty("ListDevices")]
  public List<DeviceInformationModel>? ListDeviceInformation { get; set; }

  [JsonProperty("ListModules")]
  public List<ModuleInformationModel>? ListModuleInformation { get; set; }

  [JsonProperty("OutdoorImage")]
  public ImageInformationModel? OutdoorImage { get; set; }

  [JsonProperty("ListConnectionImages")]
  public List<ImageInformationModel>? ListConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBInformationModel(string id, string name, string location, List<DeviceInformationModel> listDeviceInformation, List<ModuleInformationModel> listModuleInformation, ImageInformationModel outdoorImage, List<ImageInformationModel> listConnectionImages)
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



[Preserve]
public class JBGeneralModel
{
#nullable enable
  [JsonProperty("id")]
  public string Id { get; set; }
  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("location")]
  public string? Location { get; set; }

  [JsonProperty("listDevices")]
  public List<DeviceInformationModel> ListDevices { get; set; }

  [JsonProperty("listModules")]
  public List<ModuleInformationModel> ListModules { get; set; }

  [JsonProperty("outdoorImage")]
  public ImageInformationModel OutdoorImage { get; set; }

  [JsonProperty("listConnectionImages")]
  public List<ImageInformationModel> ListConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBGeneralModel(string id, string name, string? location, List<DeviceInformationModel> listDevices, List<ModuleInformationModel> listModules, ImageInformationModel outdoorImage, List<ImageInformationModel> listConnectionImages)
  {
    Id = id;
    Name = name;
    Location = location;
    ListDevices = listDevices;
    ListModules = listModules;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }
}


[Preserve]
public class JBPostGeneralModel
{
#nullable enable

  [JsonProperty("location")]
  public string? Location { get; set; }

  [JsonProperty("listDevices")]
  public List<DeviceBasicModel> ListDevices { get; set; }

  [JsonProperty("listModules")]
  public List<ModuleBasicModel> ListModules { get; set; }

  [JsonProperty("outdoorImage")]
  public ImageBasicModel OutdoorImage { get; set; }

  [JsonProperty("listConnectionImages")]
  public List<ImageBasicModel> ListConnectionImages { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBPostGeneralModel(string? location, List<DeviceBasicModel> listDevices, List<ModuleBasicModel> listModules, ImageBasicModel outdoorImage, List<ImageBasicModel> listConnectionImages)
  {
    Location = location;
    ListDevices = listDevices;
    ListModules = listModules;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }

}

public class JBBasicModel
{
  [JsonProperty("Id")]
  public int Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [Preserve]
  [JsonConstructor]
  public JBBasicModel(int id, string name)
  {
    Id = id;
    Name = name;
  }
}

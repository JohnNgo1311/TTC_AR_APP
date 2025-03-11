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

  [JsonProperty("location")]
  public string Location { get; set; }

  [JsonProperty("listDevices")]
  public List<DeviceBasicModel> ListDeviceInformation { get; set; }

  [JsonProperty("listModules")]
  public List<ModuleBasicModel> ListModuleInformation { get; set; }

  [JsonProperty("outdoorImage")]
  public ImageInformationModel OutdoorImage { get; set; }

  [JsonProperty("listConnectionImages")]
  public List<ImageInformationModel> ListConnectionImages { get; set; }

  [Preserve]

  public JBInformationModel(string id, string name, string location, List<DeviceBasicModel> listDeviceInformation, List<ModuleBasicModel> listModuleInformation, ImageInformationModel outdoorImage, List<ImageInformationModel> listConnectionImages)
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
  [JsonProperty("Id")]
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

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

  public JBGeneralModel(string id, string name, string? location, List<DeviceBasicModel> listDevices, List<ModuleBasicModel> listModules, ImageBasicModel outdoorImage, List<ImageBasicModel> listConnectionImages)
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
  [JsonProperty("Name")]
  public string Name { get; set; }
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

  public JBPostGeneralModel(string name, string? location, List<DeviceBasicModel> listDevices, List<ModuleBasicModel> listModules, ImageBasicModel outdoorImage, List<ImageBasicModel> listConnectionImages)
  {
    Name = name;
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
  public string Id { get; set; }
  [JsonProperty("Name")]
  public string Name { get; set; }

  [Preserve]

  public JBBasicModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
}

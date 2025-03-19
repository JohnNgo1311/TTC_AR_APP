
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable
[Preserve]
public class JBInformationModel
{

  [JsonProperty("Id")]
  public string Id { get; set; } = string.Empty;
  [JsonProperty("Name")]
  public string Name { get; set; } = string.Empty;

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

  public JBInformationModel(string id, string name, string? location, List<DeviceInformationModel>? listDeviceInformation, List<ModuleInformationModel>? listModuleInformation, ImageInformationModel? outdoorImage, List<ImageInformationModel>? listConnectionImages)
  {
    Id = id;
    Name = name;
    Location = location;
    ListDeviceInformation = listDeviceInformation;
    ListModuleInformation = listModuleInformation;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }
  public JBInformationModel(string name, string? location, List<DeviceInformationModel>? listDeviceInformation, List<ModuleInformationModel>? listModuleInformation, ImageInformationModel? outdoorImage, List<ImageInformationModel>? listConnectionImages)
  {
    Name = name;
    Location = location;
    ListDeviceInformation = listDeviceInformation;
    ListModuleInformation = listModuleInformation;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }
  public JBInformationModel(string id, string name)
  {
    Id = id;
    Name = name;
  }
  public JBInformationModel(string id, string name, string? location, ImageInformationModel? outdoorImage, List<ImageInformationModel>? listConnectionImages)
  {
    Id = id;
    Name = name;
    Location = location;
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

  [JsonProperty("Location")]
  public string? Location { get; set; }

  [JsonProperty("ListDevices")]
  public List<DeviceBasicModel>? ListDevices { get; set; }

  [JsonProperty("ListModules")]
  public List<ModuleBasicModel>? ListModules { get; set; }

  [JsonProperty("OutdoorImage")]
  public ImageBasicModel OutdoorImage { get; set; }

  [JsonProperty("ListConnectionImages")]
  public List<ImageBasicModel>? ListConnectionImages { get; set; }

  [Preserve]

  public JBGeneralModel(string id, string name, string? location, List<DeviceBasicModel>? listDevices, List<ModuleBasicModel>? listModules, ImageBasicModel outdoorImage, List<ImageBasicModel>? listConnectionImages)
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


[Preserve]
public class JBPostGeneralModel
{
#nullable enable
  [JsonProperty("Name")]
  public string Name { get; set; }
  [JsonProperty("Location")]
  public string? Location { get; set; }

  [JsonProperty("ListDevices")]
  public List<DeviceBasicModel>? ListDevices { get; set; }

  [JsonProperty("ListModules")]
  public List<ModuleBasicModel>? ListModules { get; set; }

  [JsonProperty("OutdoorImage")]
  public ImageBasicModel OutdoorImage { get; set; }

  [JsonProperty("ListConnectionImages")]
  public List<ImageBasicModel>? ListConnectionImages { get; set; }

  [Preserve]

  public JBPostGeneralModel(string name, string? location, List<DeviceBasicModel>? listDevices, List<ModuleBasicModel>? listModules, ImageBasicModel outdoorImage, List<ImageBasicModel>? listConnectionImages)
  {
    Name = name;
    Location = location;
    ListDevices = listDevices;
    ListModules = listModules;
    OutdoorImage = outdoorImage;
    ListConnectionImages = listConnectionImages;
  }


}

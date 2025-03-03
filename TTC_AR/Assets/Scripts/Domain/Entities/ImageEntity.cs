// Domain/Entities/ImageEntity.cs
using System;
using Microsoft.Unity.VisualStudio.Editor;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  public class ImageEntity
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    [Preserve]
    [JsonConstructor]
    public ImageEntity(int id, string name)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }

    public ImageEntity(string name)
    {
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
    }
    [Preserve]
    [JsonConstructor]
    public ImageEntity()
    {
    }

    public ImageEntity(int id, string name, string url)
    {
      Id = id;
      Name = name == "" ? throw new ArgumentNullException(nameof(name)) : name;
      Url = url;
    }

  }
}
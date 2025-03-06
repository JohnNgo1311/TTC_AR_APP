using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Dtos;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace Domain.Entities
{
  public class JBEntity
  {
    [JsonProperty("Id")]
    public string Id { get; set; } = string.Empty;// Id sẽ được sinh tự động khi lưu hoặc lấy từ repository

    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("Location")]
    public string? Location { get; set; }
    [JsonProperty("Devices")]
    public List<DeviceEntity>? DeviceEntities { get; set; }
    [JsonProperty("Modules")]
    public List<ModuleEntity>? ModuleEntities { get; set; }
    [JsonProperty("OutdoorImage")]
    public ImageEntity? OutdoorImageEntity { get; set; } // Có thể null nếu không có ảnh ngoài trời
    [JsonProperty("ConnectionImages")]
    public List<ImageEntity>? ConnectionImageEntities { get; set; }

    // Constructor mặc định để hỗ trợ khởi tạo linh hoạt
    [Preserve]

    public JBEntity()
    {
      // DeviceEntities = new List<DeviceEntity>();
      // ModuleEntities = new List<ModuleEntity>();
      // ConnectionImageEntities = new List<ImageEntity>();
    }

    //! Constructor tối thiểu để đảm bảo Name không rỗng (yêu cầu nghiệp vụ cơ bản)
    [Preserve]
    public JBEntity(string id, string name)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }
    public JBEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }
    //! Constructor đầy đủ (tùy chọn, để hỗ trợ ánh xạ từ DTO nếu cần)
    [Preserve]
    public JBEntity(string name, string location, List<DeviceEntity>? devices, List<ModuleEntity>? modules, ImageEntity? outdoorImage, List<ImageEntity>? connectionImages)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Location = location == string.Empty ? "chưa cập nhật" : location;
      DeviceEntities = devices ?? new List<DeviceEntity>();
      ModuleEntities = modules ?? new List<ModuleEntity>();
      OutdoorImageEntity = outdoorImage ?? null;
      ConnectionImageEntities = connectionImages ?? new List<ImageEntity>();
    }

    //! Constructor đầy đủ (tùy chọn, để hỗ trợ ánh xạ từ DTO nếu cần)
    [Preserve]
    public JBEntity(string id, string name, string location, List<DeviceEntity> devices, List<ModuleEntity> modules, ImageEntity? outdoorImage, List<ImageEntity> connectionImages)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Location = location == string.Empty ? "chưa cập nhật" : location;
      DeviceEntities = devices.Any() ? devices : new List<DeviceEntity>();
      ModuleEntities = modules.Any() ? modules : new List<ModuleEntity>();
      OutdoorImageEntity = outdoorImage ?? null;
      ConnectionImageEntities = connectionImages.Any() ? connectionImages : new List<ImageEntity>();
    }

    [Preserve]
    public JBEntity(string id, string name, string location, ImageEntity? outdoorImage, List<ImageEntity>? connectionImages)
    {
      Id = id;
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Location = location == string.Empty ? "chưa cập nhật" : location;
      OutdoorImageEntity = outdoorImage ?? null;
      ConnectionImageEntities = connectionImages ?? new List<ImageEntity>();
    }
  }
}
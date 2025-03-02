using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

#nullable enable

namespace Domain.Entities
{
  public class JBEntity
  {
    public int Id { get; set; } // Id sẽ được sinh tự động khi lưu hoặc lấy từ repository
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<DeviceEntity> DeviceEntities { get; set; } = new();
    public List<ModuleEntity> ModuleEntities { get; set; } = new();
    public ImageEntity? OutdoorImageEntity { get; set; } // Có thể null nếu không có ảnh ngoài trời
    public List<ImageEntity> ConnectionImageEntities { get; set; } = new();

    // Constructor mặc định để hỗ trợ khởi tạo linh hoạt
    [Preserve]
    [JsonConstructor]
    public JBEntity()
    {
    }

    //! Constructor tối thiểu để đảm bảo Name không rỗng (yêu cầu nghiệp vụ cơ bản)
    [Preserve]
    public JBEntity(string name)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }

    //! Constructor đầy đủ (tùy chọn, để hỗ trợ ánh xạ từ DTO nếu cần)
    [Preserve]
    public JBEntity(string name, string location, List<DeviceEntity> devices, List<ModuleEntity> modules, ImageEntity? outdoorImage, List<ImageEntity> connectionImages)
    {
      Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Location = string.IsNullOrEmpty(location) ? string.Empty : location;
      DeviceEntities = devices;
      ModuleEntities = modules;
      OutdoorImageEntity = outdoorImage;
      ConnectionImageEntities = connectionImages;
    }
  }
}
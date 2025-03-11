using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Image
{
    [Preserve]
    public class ImageResponseDto : ImageBasicDto
    {
        [JsonProperty("Url")]
        public string Url { get; set; }

        [Preserve]

        public ImageResponseDto(string id, string name, string url) : base(id, name)
        {
            Url = string.IsNullOrEmpty(url) ? "Chưa cập nhật" : url;
        }
    }
}
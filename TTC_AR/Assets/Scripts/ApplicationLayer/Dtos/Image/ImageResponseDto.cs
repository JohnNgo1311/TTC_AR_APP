using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Image
{
    [Preserve]
    public class ImageResponseDto : ImageBasicDto
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [Preserve]

        public ImageResponseDto(int id, string name, string url) : base(id, name)
        {
            Url = url ?? string.Empty;
        }
    }
}
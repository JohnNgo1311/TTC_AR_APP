using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ApplicationLayer.Dtos.Image
{
    [Preserve]
    public class ImageRequestDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("ByteString")]
        public byte[] ByteString { get; set; } = new byte[0];

        [Preserve]

        public ImageRequestDto(string name, byte[] byteString)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ByteString = byteString ?? throw new ArgumentNullException(nameof(byteString));
        }
    }
}
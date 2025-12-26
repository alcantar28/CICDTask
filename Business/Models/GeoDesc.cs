using System.Text.Json.Serialization;

namespace CICD.Models.Users
{
    public class GeoDesc
    {
        [JsonPropertyName("lat")]
        public string? Lat { get; set; }

        [JsonPropertyName("lng")]
        public string? Lng { get; set; }
    }
}

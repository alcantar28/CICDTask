using System.Text.Json.Serialization;

namespace CICD.Models.Users
{
    public class AddressDesc
    {
        [JsonPropertyName("street")]
        public string? Street { get; set; }

        [JsonPropertyName("suite")]
        public string? Suite { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("zipcode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("geo")]
        public GeoDesc? Geo { get; set; }
    }
}

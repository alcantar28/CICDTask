using System.Text.Json.Serialization;

namespace CICD.Models.Users
{
    public class CompanyDesc
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("catchPhrase")]
        public string? CatchPhrase { get; set; }

        [JsonPropertyName("bs")]
        public string? Bs { get; set; }

    }
}

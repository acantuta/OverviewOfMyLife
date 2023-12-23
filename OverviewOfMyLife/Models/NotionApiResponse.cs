using Newtonsoft.Json;

namespace OverviewOfMyLife.Models
{
    public class NotionApiResponse<T>
    {
        [JsonProperty("results")]
        public List<T> Results { get; set; }
        // Add other response properties as needed
    }
}

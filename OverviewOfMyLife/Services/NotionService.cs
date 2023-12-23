using Newtonsoft.Json;
using OverviewOfMyLife.Models;
using System.Globalization;

namespace OverviewOfMyLife.Services
{
    public class NotionService
    {
        private readonly HttpClient _httpClient;
        private readonly NotionSettings _notionSettings;

        public NotionService(HttpClient httpClient, NotionSettings notionSettings)
        {
            _httpClient = httpClient;
            _notionSettings = notionSettings;
        }

        public async Task<List<PlanchaItem>> GetPlanchasDataAsync()
        {
            string databaseId = _notionSettings.Databases["Planchas"];
            var response = await _httpClient.PostAsJsonAsync($"v1/databases/{databaseId}/query", new { });
            var body = response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var notionResponse = JsonConvert.DeserializeObject<NotionApiResponse>(jsonString);
            var planchaItems = new List<PlanchaItem>();

            foreach (var result in notionResponse.Results)
            {
                string dateProperty = result.Properties["Date"].date.start;
                var cantidadProperty = result.Properties["Cantidad1"].number;
                var descripcionProperty = result.Properties["Descripcion"].title.Count > 0
                    ? result.Properties["Descripcion"].title[0].Text.Content
                    : string.Empty; // Assuming the first title text contains the description

                planchaItems.Add(new PlanchaItem
                {
                    Date = DateTime.Parse(dateProperty, CultureInfo.InvariantCulture),
                    Cantidad1 = cantidadProperty,
                    Descripcion = descripcionProperty
                });
            }

            return planchaItems;
        }

    }

    
    public class NotionResponse
    {
        public string Object { get; set; }
        public List<NotionPage> Results { get; set; }
    }

    public class NotionPage
    {
        public string Object { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastEditedTime { get; set; }
        public NotionProperties Properties { get; set; }
        public string Url { get; set; }
    }

    public class NotionProperties
    {
        public NotionDate Date { get; set; }
        public NotionNumber Cantidad1 { get; set; }
        public NotionTitle Descripcion { get; set; }
    }

    public class NotionDate
    {
        public NotionDateInfo Date { get; set; }
    }

    public class NotionDateInfo
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }

    public class NotionNumber
    {
        public int Number { get; set; }
    }

    public class NotionTitle
    {
        public List<NotionText> Title { get; set; }
    }

    public class NotionText
    {
        // Assuming there is a 'text' field inside 'title' based on standard Notion API structure
        public NotionTextContent Text { get; set; }
    }

    public class NotionTextContent
    {
        public string Content { get; set; }
    }

    // Helper classes to deserialize the JSON response
    public class NotionApiResponse
    {
        public List<Result> Results { get; set; }
        // other necessary properties...
    }

    public class Result
    {
        public Dictionary<string, dynamic> Properties { get; set; }
        // other necessary properties...
    }

    public class Property
    {
        public string PlainText { get; set; }
        public DateTime Date { get; set; }
        public decimal Number { get; set; }
        // other necessary properties...
    }
}

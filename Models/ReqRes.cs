using System.Text.Json.Serialization;

namespace DemoBlazorWASM.Models
{
    public class ReqResRequest
    {
        public string? Name { get; set; }
        public string? Job { get; set; }
    }


    public class ReqResResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public Person[]? data { get; set; }
        public Support? support { get; set; }
    }

    public class Support
    {
        public string? url { get; set; }
        public string? text { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}
